using PTR.PTRLib;
using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ADTService {
	class ADTScanner {
		public Thread Thread;
		internal bool exit = false;
		private PTRContext db = new PTRContext();
		static protected int Delay { get { return ADT.Delay; } set { ADT.Delay = value; } }
		static protected bool skipFolderParsing { get { return ADT.skipFolderParsing; } set { ADT.skipFolderParsing = value; } }
		//real root path
		internal string rootPath;
		//visible root path, that is stored in db
		internal string rootPathDB;
		//since each ADScanner will be working on just one directory, it will use only one drive
		internal ADDrive currentADDrive;
		//since there is no Moved event in FileSystemWatcher we need to look for recently deleted folders. May be they will reappear sometime soon
		internal List<ADFolder> recentlyDeletedADFolders = new List<ADFolder>();//List of last deleted folders
		internal bool recentlyDeletedADFoldersLock = false;//Lock it because watcher is on a Worker Thread
		internal DateTime recentlyDeletedTime = DateTime.Now;//A global timer to clear List
		internal bool FinishedScanning = false;
		//Because DBContext is a weenie I found none working solution to a problem
		//When I try to create another folder in a Worker Thread, it trows an Exception, it can't use already used entity
		//So it is much easier to just do everything in one thread, so while it scanning whole drive, we also check for Responses and execute appropriate action
		List<WatcherResponse> WatcherResponses = new List<WatcherResponse>();
        bool wrl = false;
		bool WatcherResponsesLock { get { return wrl; } set { wrl = value; } }

		//static internal List<ADTScanner> Scanners = new List<ADTScanner>();
		//static internal bool Paused;
		//internal bool Resume;

		internal string Name = "";

		public ADTScanner(string path, string pathDB) {
			this.rootPath = path;
			this.rootPathDB = pathDB;
			Name = pathDB.Replace('\\', '_').Replace(":", "");
			//Scanners.Add(this);
		}
		public void Main() {
			if (rootPath == "" || rootPathDB == "") throw new Exception("Paths in 'ADTrackerConfig.xml' are not defined!");
			currentADDrive = GetOrCreateADDrive(rootPathDB);

			//Cancel if directory does not exist
			if (!Directory.Exists(rootPath)) {
				ADTConsole.WriteLine("Exceptions", "\nDirectory " + rootPath + " either does not exist or it is a network folder, that is invisible for Administrator. See http://stackoverflow.com/questions/17472168/check-if-directory-exists-on-network-drive");
				return;
			}

			//Setup watchers
			FileSystemWatcher watcher = new FileSystemWatcher() {
				Path = rootPath,
                NotifyFilter = NotifyFilters.Security | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.Attributes,
                //NotifyFilter = NotifyFilters.DirectoryName,
                Filter = "",
				IncludeSubdirectories = true
			};
			watcher.EnableRaisingEvents = true;
			//Create new records on Created
			watcher.Created += FolderChanged;
			//Update records on Changed
			watcher.Changed += FolderChanged;
			//Update records on Renamed
			watcher.Renamed += FolderRenamed;
			//Look for created on Deleted for moved folders before marking as deleted
			watcher.Deleted += FolderDeleted;

            Thread fp = new Thread(() => {
                //Walk folders recursively to add or update db
                foreach (string p in Directory.GetDirectories(rootPath)) {
                    ProcessFolder(p, null);
                    Thread.Sleep(Delay);
                }
                var CurrentFolders = db.ADFolders.Where(f => f.FullPath.Substring(0, rootPathDB.Length) == rootPathDB).ToList();
                foreach (ADFolder f in CurrentFolders) {
                    if (!f.walked) {
                        //Find unwalked folders and mark them as deleted
                        f.Status = false;
                        foreach (ADFolderAccess adfa in f.ADFolderAccesses) {
                            adfa.To = DateTime.Now;
                        }
                    }
                }
                foreach (ADFolderAccess a in db.ADFolderAccesses.Local) {
                    if (!a.walked) {
                        a.To = DateTime.Now;
                    }
                }
            });
            if (!skipFolderParsing) {
                fp.Start();
			}
			FinishedScanning = true;

			db.SaveChanges();
			while (!exit) {
				if (recentlyDeletedTime < DateTime.Now && !recentlyDeletedADFoldersLock) {
					recentlyDeletedADFoldersLock = true;
					recentlyDeletedADFolders.Clear();
					recentlyDeletedADFoldersLock = false;
				}
				CheckWatcherResponses();
				 Thread.Sleep(Delay);
			}
            fp.Abort();
            watcher.Dispose();
		}
		//Contains all things that we need that Watcher can pass with parameters, to execute right thing on it later
		class WatcherResponse {
			public enum ResponseType { Changed, Renamed, Deleted }
			public ResponseType Type;
			public string FullPath;
			public string Name;
			public string OldFullPath;
			public string OldName;
			public WatcherResponse(ResponseType Type, string Name, string FullPath) {
				this.Type = Type; this.Name = Name; this.FullPath = FullPath;
			}
		}

		void FolderChanged(object source, FileSystemEventArgs e) {
            if (!CheckIfFolder(e.FullPath)) return;
            while (WatcherResponsesLock) {
				Thread.Sleep(5);
			}
			WatcherResponses.Add(new WatcherResponse(WatcherResponse.ResponseType.Changed, e.Name, e.FullPath));
		}

        private void FolderRenamed(object sender, RenamedEventArgs e) {
            if (!CheckIfFolder(e.FullPath)) return;
            while (WatcherResponsesLock) {
				Thread.Sleep(5);
			}
			WatcherResponses.Add(new WatcherResponse(WatcherResponse.ResponseType.Renamed, e.Name, e.FullPath) { OldName = e.OldName, OldFullPath = e.OldFullPath });
		}

		private void FolderDeleted(object sender, FileSystemEventArgs e) {
            if (!CheckIfFolder(e.FullPath)) return;
            while (WatcherResponsesLock) {
				Thread.Sleep(5);
			}
			WatcherResponses.Add(new WatcherResponse(WatcherResponse.ResponseType.Deleted, e.Name, e.FullPath));
		}

        //private bool CaseBlacklist(string name) {
        //    if (name.Length < 4) return false;
        //    string ext;// = name.Substring(name.Length - 4).ToLower();
        //    ext = name.Substring(name.LastIndexOf('.'));
        //    return (ext == ".mdb" || ext == ".zip" || ext == ".tmp" || ext == ".xlsx" || ext == ".accdb" || ext == ".docx") ? true : false; //TODO Expand list
        //}

        private bool CheckIfFolder(string path) {
            FileAttributes a = File.GetAttributes(path);
            if ((a & FileAttributes.Directory) == FileAttributes.Directory) return true;
            return false;
        }

		//Here are things we should do in Worker Thread, but it will be executed from main thread instead
		private void CheckWatcherResponses() {
			if (WatcherResponses.Count > 0 && !WatcherResponsesLock) {
				WatcherResponsesLock = true;
				foreach (var r in WatcherResponses) {
					if (r.Type == WatcherResponse.ResponseType.Changed) {
						try {
							string pathDB = rootPathDB + r.FullPath.Substring(rootPath.Count());
							//string parentPathDB = pathDB.Substring(0, pathDB.LastIndexOf('\\'));

							//var parent = GetOrCreateADFolder(parentPathDB, null, "");
							ADTConsole.WriteLine(Name, "Folder \"{0}\" Changed ", pathDB);
							ProcessFolder(r.FullPath, null, true);
							//db.SaveChanges();
						} catch (Exception ex) {
							ADTConsole.WriteLine(Name, "Watcher FolderChanged: {0} ", ex.Message);
						}
					} else if (r.Type == WatcherResponse.ResponseType.Deleted) {
						try {
							//Should add a folder to mark as deleted later, because if it will reappear later, then it was moved, not deleted
							string pathDB = rootPathDB + r.FullPath.Replace("\\", @"\").Substring(rootPath.Count());
							var adf = GetOrCreateADFolder(pathDB, null, "");
							adf.Status = false;
							foreach (ADFolderAccess adfa in adf.ADFolderAccesses) {
								adfa.To = DateTime.Now;
							}
							recentlyDeletedADFolders.Add(adf);
							recentlyDeletedTime = DateTime.Now + TimeSpan.FromMinutes(1);
							ADTConsole.WriteLine(Name, "Folder \"{0}\" Deleted ", pathDB);
							//db.SaveChanges();
						} catch (Exception ex) {
							ADTConsole.WriteLine(Name, "Watcher FolderDeleted: {0} ", ex.Message);
						}
					} else if (r.Type == WatcherResponse.ResponseType.Renamed) {
						try {
							string _pathDB = rootPathDB + r.FullPath.Substring(rootPath.Count());
							string _oldPathDB = rootPathDB + r.OldFullPath.Substring(rootPath.Count());
							var adf = db.ADFolders.FirstOrDefault(f => f.FullPath == _oldPathDB);
							//adf.FullPath = _pathDB;
							//adf.Name = r.Name;
							ADTConsole.WriteLine(Name, "Folder \"{0}\" Renamed to \"{1}\" ", _oldPathDB, _pathDB);
							UpdateADFolderPath(adf, _pathDB, r.Name);
							//db.SaveChanges();
						} catch (Exception ex) {
							ADTConsole.WriteLine(Name, "Watcher FolderRenamed: {0} ", ex.Message);
						}
					}
				}
				WatcherResponses.Clear();
				WatcherResponsesLock = false;
				db.SaveChanges();
			}
		}

		ADFolder ProcessFolder(string Path, ADFolder Parent) { return ProcessFolder(Path, Parent, false); }

		ADFolder ProcessFolder(string Path, ADFolder Parent, bool OutwardRecursion) {
			CheckWatcherResponses();
			Console.WriteLine(" ");
			//DB path is the one you see on shared drives, but real path can be different on server side
			string PathDB = rootPathDB + Path.Substring(rootPath.Count());
			string[] subDirs = new string[0];
			try {
				subDirs = Directory.GetDirectories(Path);
			} catch (Exception ex) {
				ADTConsole.WriteLine(Name, "\nProcessing folder, getting subdirectories: {0} ", ex.Message);
			}
			var subDirNames = "";//May be unnecessary, could create a simple subdir name concatenating function
			foreach (var s in subDirs) {
				subDirNames += s.Substring(s.LastIndexOf('\\'));
			}

			//If it is a bacwards scanning, then process parent
			if (OutwardRecursion && Parent == null) {
				string parentPath = Path.Substring(0, Path.LastIndexOf('\\'));
				//Parent = GetOrCreateADFolder(parentPathDB, null, "");
				if (Parent == null && parentPath + '\\' != rootPath) {
					Thread.Sleep(Delay);
					Parent = ProcessFolder(parentPath, null, true);
				}
			}

			ADFolder adf = GetOrCreateADFolder(PathDB, Parent, subDirNames.GetHashCode().ToString());

			//Process Accesses
			DirectoryInfo dirInfo = new DirectoryInfo(Path);
			if (dirInfo != null) {
				try {
					DirectorySecurity dirSec = dirInfo.GetAccessControl();
					var accessRules = dirSec.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
					foreach (FileSystemAccessRule ar in accessRules) {
						string username = ar.IdentityReference.Value.Substring(ar.IdentityReference.Value.LastIndexOf("\\") + 1);
						ADUser adu = GetOrCreateADUser(username, true, username);
						if (adu.UUser == null) adu.UUser = GetOrCreateUUser(username, "");
						ADFolderAccess pfa = (Parent != null) ? db.ADFolderAccesses.FirstOrDefault(a => (a.ADFolder.FullPath == Parent.FullPath && a.ADUser.Login == adu.Login)) : null;
						ADFolderAccess adfa = GetOrCreateADFolderAccess(adf, adu, (int)ar.FileSystemRights, dirInfo.CreationTime, pfa);
						adfa.walked = true;
					}
				} catch (Exception ex) {
					ADTConsole.WriteLine(Name, "Trying to get directory AccessRules in {0}: {1} ", Path, ex.Message);
				}
			}

			//Check whether this folder is equal to one of recently deleted ones, may be it was moved recently
			recentlyDeletedADFoldersLock = true;
			var recentlyDeleted = recentlyDeletedADFolders.FirstOrDefault(f => f.Name == adf.Name);
			if (recentlyDeleted != null) {
				//Thorough check
				if (recentlyDeleted.SubHash == adf.SubHash && recentlyDeleted.AccessesToString() == adf.AccessesToString())
					recentlyDeletedADFolders.Remove(recentlyDeleted);
				else
					recentlyDeleted = null;
			}
			recentlyDeletedADFoldersLock = false;//Unlock first then process
			if (recentlyDeleted != null) {
				recentlyDeleted.ParentFolder = adf.ParentFolder;
				db.ADFolderAccesses.RemoveRange(adf.ADFolderAccesses);
				db.ADFolders.Remove(adf);
				adf = recentlyDeleted;
				UpdateADFolderPath(adf, PathDB, PathDB.Substring(PathDB.LastIndexOf("\\")));
				ADTConsole.Write(Name, "Moved deleted folder ");
			}

			//Process children if it is not a backtracing
			if (!OutwardRecursion) {
				try {
					foreach (string f in subDirs) {
						Thread.Sleep(Delay);
						ProcessFolder(f, adf);
					}
				} catch (Exception ex) {
					ADTConsole.WriteLine(Name, "{0} ", ex.Message);
				}
			}

			db.SaveChanges();
			adf.walked = true;
			return adf;
		}

		private void UpdateADFolderPath(ADFolder ADFolder, string FullPath, string Name) {
			ADFolder.Status = true;
			foreach (var a in ADFolder.ADFolderAccesses) {
				a.To = null;
			}
			ADFolder.FullPath = FullPath;
			ADFolder.Name = Name;
			foreach (ADFolder adf in ADFolder.SubFolders) {
				UpdateADFolderPath(adf, ADFolder.FullPath + @"\" + adf.Name, adf.Name);
			}
		}

		ADDrive GetOrCreateADDrive(string Path) {
			//var db = new PTRContext();
			ADDrive drive = db.ADDrives.FirstOrDefault((d) => d.Path.Substring(0, 1) == Path.Substring(0, 1));
			if (drive == null) {
				drive = new ADDrive() {
					Path = Path,
					Name = Path.Substring(0, 1)
				};
				db.ADDrives.Add(drive);
				//await db.SaveChangesAsync();
				//ADDriveList.Add(drive);
			}
			return drive;
		}

		ADFolder GetOrCreateADFolder(string PathDB, ADFolder Parent, string SubHash) {
            //var db = new PTRContext();
            try {
                var r = db.ADFolders.FirstOrDefault((f) => f.FullPath == PathDB);
                if (Parent != null) r = Parent.SubFolders.FirstOrDefault(f => f.FullPath == PathDB);
                string name = PathDB.Split('\\').Last();
                bool isRoot = (Parent == null);
                if (r == null) {
                    r = new ADFolder() {
                        FullPath = PathDB,
                        Name = name,
                        ParentFolder = Parent,
                        IsRootFolder = isRoot,
                        ADDrive = currentADDrive,
                        Status = true,
                        SubHash = SubHash
                    };
                    db.ADFolders.Add(r);
                    //await db.SaveChangesAsync();
                    //ADFolderList.Add(r);
                    ADTConsole.WriteLine(Name, "+Created ");
                } else {
                    //In case it was a dummy, regenerate ADFolder
                    if (r.Status == false) {
                        r.Status = true;
                        ADTConsole.WriteLine(Name, "Restored ");
                    } else
                        ADTConsole.WriteLine(Name, "Found ");
                    if (r.SubHash == "") {
                        r.SubHash = SubHash;
                        r.ParentFolder = Parent;
                        r.IsRootFolder = isRoot;
                        r.Name = name;
                        r.ADDrive = currentADDrive;
                        r.Status = true;
                    }
                }
                ADTConsole.Write(Name, "ADFolder {0}: ", PathDB);
                return r;
            } catch (Exception e) {
                PrintException(e, 0);
            }
            return null;
		}

        void PrintException(Exception e, int level) {
            ADTConsole.WriteLine("Exception", e.Message.PadLeft(level + e.Message.Length, '\t'));
            if (e.InnerException != null) PrintException(e.InnerException, level + 1);
        }

		ADFolder GetADFolder(string PathDB) {
			var r = db.ADFolders.FirstOrDefault((f) => f.FullPath == PathDB);
			if (r == null && !FinishedScanning) {
				//while (!FinishedScanning) {
				//	Thread.Sleep(1000);
				//}
				r = db.ADFolders.FirstOrDefault((f) => f.FullPath == PathDB);
			}
			return r;
		}

		ADUser GetOrCreateADUser(string Login, bool Status, string Name) {
			//var db = new PTRContext();
			ADUser r = db.ADUsers.FirstOrDefault(u => u.Login == Login);
			//r = (from a in db.ADUsers where a.Login == Login select a).FirstOrDefault();
			if (r == null && ADT.UsersReady == false) {
				//TODO Wait for it to process
				while (r == null) {
					r = db.ADUsers.FirstOrDefault(u => u.Login == Login);
					if (ADT.UsersReady) {
						break;//If UserScanner finished, then return whatever found or not
					}
				}
				r = db.ADUsers.FirstOrDefault(u => u.Login == Login);
			}
			if (r == null) {
				//Should never be here
				r = new ADUser() {
					Login = Login,
					DN = Name,
					Status = Status
				};
				db.ADUsers.Add(r);
				ADTConsole.Write(Name, "+ADUser {0}: ", r.Login);
			}
			return r;
		}

		UUser GetOrCreateUUser(string FullName, string Mail) {
			//var db = new PTRContext();
			UUser r = db.UUsers.FirstOrDefault((u) => (u.NormalizedFullName.ToLower() == FullName.ToLower() || u.Email == Mail));
			r = (from a in db.UUsers where 
				 a.NormalizedFullName.ToLower() == FullName.ToLower() ||
				 a.Email == Mail
				 select a).FirstOrDefault();
			if (r == null) {
				//Should never be here
				r = new UUser() {
					FullName = FullName,
					Email = Mail,
					Name = FullName,
					Surname = ""
				};
				db.UUsers.Add(r);
				ADTConsole.Write(Name, "+UUser {0}: ", r.FullName);
			} else {
				if (r.Email != Mail) {
					r.Email = Mail;
				}
			}
			return r;
		}

		private ADFolderAccess GetOrCreateADFolderAccess(ADFolder ADFolder, ADUser ADUser, int FileSystemRights_, DateTime From, ADFolderAccess ParentFolderAccess) {
			var q = (from a in db.ADFolderAccesses where a.ADUser.Login == ADUser.Login && a.ADFolder.FullPath == ADFolder.FullPath && a.FileSystemRights_ == FileSystemRights_ select a).ToArray();
			ADFolderAccess r = q.FirstOrDefault(a => { return a.From.Value.TrimMilliseconds() == From.TrimMilliseconds(); });
			//The db has timedate format, that mingles milliseconds, so can't compare them in linq successfully, so doing it locally is a solution

			if (r == null) {
				r = new ADFolderAccess() {
					ADUser = ADUser,
					ADFolder = ADFolder,
					ParentFolderAccess = ParentFolderAccess,
					FileSystemRights_ = FileSystemRights_,
					From = From
				};
				db.ADFolderAccesses.Add(r);
				ADTConsole.Write(Name, "+");
			}
			ADTConsole.Write(Name, "{0} - {1}, ", r.ADUser.Login, r.FileSystemRights);
			return r;
		}
	}
}
