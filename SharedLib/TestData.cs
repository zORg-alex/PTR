using PTR.PTRLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
    public class TestData {

		static protected List<PVEmploee> _Emploees;
		static public List<PVEmploee> Emploees {
			get {
				if (_Emploees == null || _Emploees.Count == 0) {
					_Emploees = new List<PVEmploee>();

					var a = new PVEmploee() {
						Id = 0,
						Name = "Alpha",
						Surname = "Omega",
						Phone = "2678 9123",
						LocalPhone = "777 888 22",
						Email = "AOmega@something.com",
						PartS = "Some Department's part",
						Status = true,
						PVHistEmploees = PVHistEmploees,
						IsExpanded = true,
						IsSelected = true,
						From = new DateTime(2015, 12, 20, 12, 30, 55),
						PVProfession = new PVProfession() { Name = "Some obnoxeously long Profession name" },
					};
					a.PVHistEmploees.FirstOrDefault().PVEmploee = a;
					_Emploees.Add(a);
					_Emploees.Add(new PVEmploee() { Id = 10, Name = "Bravo", Surname = "Psi" });
					_Emploees.Add(new PVEmploee() { Id = 22, Name = "Delta", Surname = "Gamma" });
					_Emploees.Add(new PVEmploee() { Id = 235, Name = "Epsilon", Surname = "Tetta" });
					_Emploees.Add(new PVEmploee() { Id = 348, Name = "Focus", Surname = "Whatever" });
					_Emploees.Add(new PVEmploee() { Id = 1451, Name = "Gerry the Spongebob's Snail" });
				}
				return _Emploees;
			}
		}

		private PVEmploee _Emploee;
		public PVEmploee Emploee {
			get { return Emploees.FirstOrDefault(); }
		}


		static protected List<PVStructural> _PVStructurals = new List<PVStructural>();
		static public List<PVStructural> PVStructurals {
			get {
				if (_PVStructurals == null || _PVStructurals.Count == 0) {
					PVStructural a = new PVStructural() {
						Id = "0",
						Name = "A Department with a ridiculously long name",
						Code = "0000",
						Level = "0",
						PVEmploeesInPart = new List<PVEmploee>(),
                        IsExpanded = true
					};
					PVStructural b = new PVStructural() {
						Id = "1",
						Name = "Some Department's part",
						Code = "0001",
						Level = "1",
						HeadDepartment = a,
						PVEmploeesInPart = new List<PVEmploee>()
					};
					a.PVEmploeesInPart.Add(Emploees.FirstOrDefault());
					b.PVEmploeesInPart = Emploees;
					a.Parts.Add(b);
					_PVStructurals.Add(a);
				}
				return _PVStructurals;
			}
		}


		static private List<UUser> _UUsers;
        static public List<UUser> UUsers {
            get {
                if (_UUsers == null || _UUsers.Count == 0) {
                    _UUsers = new List<UUser>();

                    UUser a = new UUser() {
                        Id = 0,
                        Name = "Alpha",
                        Surname = "Omega",
                        Email = "AOmega@something.com",
                        PVEmploees = { Emploees.FirstOrDefault(e=>e.Name == "Alpha" && e.Surname == "Omega") },
                        ISDAVSUsers = { ISDAVSUsers.First() },
                        ADUsers = { ADUsers.FirstOrDefault() },
                    };
					a.PVEmploees.FirstOrDefault().UUser = a;
					a.PVEmploees.Add(Emploees.FirstOrDefault());
                    a.Evaluate();
                    _UUsers.Add(a);
					a = new UUser() {
						Id = 10,
						Name = "Bravo",
						Surname = "Psi",
						PVEmploees = { Emploees.FirstOrDefault(e => e.Name == "Bravo" && e.Surname == "Psi") }
					};
					a.PVEmploees.FirstOrDefault().UUser = a;
					_UUsers.Add(a);
					a = new UUser() {
						Id = 22,
						Name = "Delta",
						Surname = "Gamma",
						PVEmploees = { Emploees.FirstOrDefault(e => e.Name == "Delta" && e.Surname == "Gamma") }
					};
					a.PVEmploees.FirstOrDefault().UUser = a;
					_UUsers.Add(a);
					a = new UUser() {
						Id = 235,
						Name = "Epsilon",
						Surname = "Tetta",
						PVEmploees = { Emploees.FirstOrDefault(e => e.Name == "Epsilon" && e.Surname == "Tetta") }
					};
					a.PVEmploees.FirstOrDefault().UUser = a;
					_UUsers.Add(a);
					a = new UUser() {
						Id = 348,
						Name = "Focus",
						Surname = "Whatever",
						PVEmploees = { Emploees.FirstOrDefault(e => e.Name == "Focus" && e.Surname == "Whatever") }
					};
					a.PVEmploees.FirstOrDefault().UUser = a;
					_UUsers.Add(a);
					a = new UUser() {
						Id = 1451,
						Name = "Gerry the Spongebob's Snail",
						PVEmploees = { Emploees.FirstOrDefault(e => e.Name == "Gerry the Spongebob's Snail") }
					};
					a.PVEmploees.FirstOrDefault().UUser = a;
					_UUsers.Add(a);
				}
                return _UUsers;
            }
        }

        static private UUser _UUser;
        static public UUser UUser {
            get {
                if (_UUser == null) {
                    _UUser = UUsers.FirstOrDefault();
                }
                return _UUser;
            }
            set { _UUser = value; }
        }


        static private List<PVHistEmploee> _PVHistEmploees;
        static public List<PVHistEmploee> PVHistEmploees {
            get {
                if (_PVHistEmploees == null) {
                    _PVHistEmploees = new List<PVHistEmploee>();

                    _PVHistEmploees.Add(new PVHistEmploee() {
                        Id = 0,
                        EmploeeID = 0,
                        ChangeDate = DateTime.Now,
                        Name = "Alpha111",
                        Surname = "Omegaaaa",
                        Phone = "2678 aaaa",
                        LocalPhone = "777 888 22",
                        Email = "A0me&a@$0m%th!ng.c0m",
                        DepartmentS = "Some Department",
                        PartS = "Some Department's part",
                        Status = true,
                        From = new DateTime(2015, 12, 20, 12, 30, 55),
                        PVProfession = new PVProfession() { Name = "A!ph@Bo$s" },
                    });
                }
                return _PVHistEmploees;
            }
        }

        static private List<IFunctionAccess> _IFunctionAccesses;
        static public List<IFunctionAccess> IFunctionAcccesses {
            get {
                if (_IFunctionAccesses == null) {
                    _IFunctionAccesses = new List<IFunctionAccess>();
                    IFunctionAccess fa = new IFunctionAccess() {
                        Id = 0,
                        IFunction = IFunction,
                        IQuestAccesses = IQuestAccess,
                        ISDAVSUser = (ISDAVSUsers == null) ? null : ISDAVSUsers.FirstOrDefault(),
						IsExpanded = true
                    };
                    _IFunctionAccesses.Add(fa);
                    fa = new IFunctionAccess() {
                        Id = 1,
                        IFunction = IFunction,
                        ISDAVSUser = (ISDAVSUsers == null) ? null : ISDAVSUsers.LastOrDefault()
                    };
                    _IFunctionAccesses.Add(fa);
                }
                return _IFunctionAccesses;
            }
        } 
		

        static private IFunctionAccess _IFunctionAccess;
        static public IFunctionAccess IFunctionAccess {
            get {
                if (_IFunctionAccesses == null) {
                    _IFunctionAccess = IFunctionAcccesses.FirstOrDefault();
                }
                return _IFunctionAccess;
            }
            set { _IFunctionAccess = value; }
        }

		static private List<IFunction> _IFunctions;
		static public List<IFunction> IFunctions {
			get {
				if (_IFunctions == null) {
					_IFunctions = new List<IFunction>();
					_IFunctions.Add(new IFunction() { Id = 0, Name = "TestFunction", Description = "Plain Test Function", IFunctionAccesses = IFunctionAcccesses });
					_IFunctions.Add(new IFunction() { Id = 1, Name = "AnotherTestFunction", Description = "Just Another Test Function" });
				}
				return _IFunctions; }
		}

        static private IFunction _IFunction;
        static public IFunction IFunction {
            get {
                if (_IFunction == null) {
                    _IFunction = IFunctions.FirstOrDefault();
                }
                return _IFunction;
            }
            set { _IFunction = value; }
        }

        static private List<IQuest> _IQuest;
		static public List<IQuest> IQuest {
			get {
				if (_IQuest == null) {
					_IQuest = new List<IQuest>();
					_IQuest.Add(new IQuest() {
						Id = 0,
						Name = "Quest1",
						Index = "TestQuestIndex1"
					});
					_IQuest.Add(new IQuest() {
						Id = 1,
						Name = "Quest2",
						Index = "TestQuestIndex2"
					});
					_IQuest.Add(new IQuest() {
						Id = 2,
						Name = "Quest3",
						Index = "TestQuestIndex3"
					});
				}
				return _IQuest;
			}
		}

		static private List<IQuestAccess> _IQuestAccess;
		static public List<IQuestAccess> IQuestAccess {
			get {
				if (_IQuestAccess == null) {
					_IQuestAccess = new List<IQuestAccess>();
					_IQuestAccess.Add(new IQuestAccess() {
						Id = 0,
						QuestId = 0,
						IQuest = IQuest.Find((f) => f.Id == 0),
						IFunctionAccess = IFunctionAcccesses.Find((f) => f.Id == 0)
					});
					_IQuestAccess.Add(new IQuestAccess() {
						Id = 1,
						QuestId = 1,
						IQuest = IQuest.Find((f) => f.Id == 1),
						IFunctionAccess = IFunctionAcccesses.Find((f) => f.Id == 0)
					});
					_IQuestAccess.Add(new IQuestAccess() {
						Id = 2,
						QuestId = 2,
						IQuest = IQuest.Find((f) => f.Id == 2),
						IFunctionAccess = IFunctionAcccesses.Find((f) => f.Id == 0)
					});
				}
				return _IQuestAccess;
			}
		}

		static private List<ISDAVSUser> _ISDAVSUsers;
		static public List<ISDAVSUser> ISDAVSUsers {
			get {
				if (_ISDAVSUsers == null) {
					_ISDAVSUsers = new List<ISDAVSUser>();
					ISDAVSUser u = new ISDAVSUser() {
						Id = 0,
						Login = "aOmega           ",
						Email = "aOmega@zz.zz                 ",
						Name = "Alpha           ",
						Surname = "Omega                ",
						Phone = "88855544          ",
						IsExpanded = true
                    };
                    ISDAVSUser u2 = new ISDAVSUser() {
                        Id = 1,
                        Name = "Betta           ",
                        Surname = "Gamma                ",
                        Login = "bGamma"
                    };
                    _ISDAVSUsers.Add(u);
                    _ISDAVSUsers.Add(u2);
                    var fa0 = IFunctionAcccesses.Find((f) => f.Id == 0);
					var fa1 = IFunctionAcccesses.Find((f) => f.Id == 1);
					if (fa0 != null) fa0.ISDAVSUser = u;
					if (fa1 != null) fa1.ISDAVSUser = u2;
					u.IFunctionAccesses.Add(fa0);
					u.IFunctionAccesses.Add(fa1);
				}
				return _ISDAVSUsers; }
		}

		static private ISDAVSUser _ISDAVSUser;
		static public ISDAVSUser ISDAVSUser { get { return ISDAVSUsers.FirstOrDefault(); } set { _ISDAVSUser = value; } }

		static protected ObservableCollection<ImportReport> _Reports;
		static public ObservableCollection<ImportReport> Reports {
			get {
				if (_Reports == null) {
					_Reports = new ObservableCollection<ImportReport>();

					_Reports.Add(new ImportReport() { Id = 1, NewRecord = 1, Table = "PVEmploees", RecordId = 10 });
					_Reports.Add(new ImportReport() { Id = 1, NewRecord = 1, Table = "ISDAVSUser", RecordId = 0 });
					_Reports.Add(new ImportReport() { Id = 1, NewRecord = 0, Table = "PVEmploees", RecordId = 22 });
					_Reports.Add(new ImportReport() { Id = 1, NewRecord = -1, Table = "PVEmploees", RecordId = 0 });
				}
				return _Reports;
			}
		}

		static private List<ADUser> _ADUsers;
		static public List<ADUser> ADUsers {
			get {
				if (_ADUsers == null) {
					_ADUsers = new List<ADUser>();
					_ADUsers.Add(new ADUser() {
						Id = 0,
						Login = "AOmega",
						Status = true,
						DN = "Alpha Omega",
						ADFolderAccesses = ADFolderAccesses,
						PermissionsByDrive = new List<DrivePermissions>() { new DrivePermissions() { Drive = ADDrive, IsExpanded = true, RootAccesses = ADFolderAccesses} },
						IsExpanded = true
					});
					_ADUsers.Add(new ADUser() {
						Id = 0,
						Login = "bGamma",
						Status = true,
						DN = "Betta Gamma",
						ADFolderAccesses = ADFolderAccesses
					});
				}
				return _ADUsers;
			}
		}

		static private List<ADFolderAccess> _ADFolderAccesses;
		static public List<ADFolderAccess> ADFolderAccesses {
			get {
				if (_ADFolderAccesses == null) {
					_ADFolderAccesses = new List<ADFolderAccess>();
					_ADFolderAccesses.Add(new ADFolderAccess() {
						Id = 0,
						ADFolder = ADFolders.First(),
						From = DateTime.Now,
						To = DateTime.Now,
						FileSystemRights_ = 1
					});
					_ADFolderAccesses.Add(new ADFolderAccess() {
						Id = 1,
						ADFolder = ADFolders.Last(),
						From = DateTime.Now,
						FileSystemRights_ = 2
					});
				}
				return _ADFolderAccesses;
			}
		}

		static private List<ADFolder> _ADFolders;
		static public List<ADFolder> ADFolders {
			get {
				if (_ADFolders == null) {
					_ADFolders = new List<ADFolder>();
					var a = new ADFolder() {
						Id = 0,
						FullPath = "X:\\TestFolder",
						Name = "TestFolder",
						ADDrive = ADDrive,
						IsRootFolder = true,
						IsExpanded = true,
						SubFolders = { new ADFolder() { FullPath = "X:\\TestFolder\\AnotherFolder", Name = "AnotherFolder", ADDrive = ADDrive } },
						Status = true
					};
					a.ADFolderAccesses = new List<ADFolderAccess>();
					a.ADFolderAccesses.Add(
						new ADFolderAccess() { ADUser = new ADUser() { Login = "aOmega"}, ADFolder = a, FileSystemRights = System.Security.AccessControl.FileSystemRights.FullControl }
					);
					a.ADFolderAccesses.Add(
						new ADFolderAccess() { ADUser = new ADUser() { Login = "Admin" }, ADFolder = a, FileSystemRights = System.Security.AccessControl.FileSystemRights.FullControl }
					);
					_ADFolders.Add(a);
					_ADFolders.Add(new ADFolder() {
						Id = 1,
						FullPath = "X:\\TestFolder2",
						Name = "TestFolder2",
						ADDrive = ADDrive,
						IsRootFolder = true,
						Status = false
					});
				}
				return _ADFolders;
			}
		}

		static private ADFolder _ADFolder;
		static public ADFolder ADFolder {
			get {
				if (_ADFolder == null) {
					_ADFolder = ADFolders.FirstOrDefault();
				}
				return _ADFolder;
			}
		}

		static private ADDrive _ADDrive;
		static public ADDrive ADDrive {
			get {
				if (_ADDrive == null) {
					_ADDrive = new ADDrive() {
						Path = "X:\\",
						ADFolders = ADFolders,
						Name = "Test Drive X"
					};
				}
				return _ADDrive;
			}
		}

		static private List<ADDrive> _ADDrives;
		static public List<ADDrive> ADDrives {
			get {
				if (_ADDrives == null) {
					_ADDrives = new List<ADDrive>();
					_ADDrives.Add(ADDrive);
					_ADDrives.Add(new ADDrive() {
						Path = "C:\\",
						Name = "Test Drive C"
					});
				}
				return _ADDrives;
			}
		}

        static private List<ImportReport> _importReports;
        static public List<ImportReport> ImportReports {
            get {
                if (_importReports == null) {
                    var list = new List<ImportReport>();
                    list.Add(new ImportReport() {
                        NewRecord = -1,
                        Table = "RandomTable"
                    });
                    list.Add(new ImportReport() {
                        NewRecord = 0,
                        Table = "AnotherTable"
                    });
					list.Add(new ImportReport() {
						NewRecord = 1,
						Table = "OneMore"
					});
					list.Add(new ImportReport() {
						NewRecord = 1,
						Table = "Hidden",
						Hide = 1
					});
					_importReports = list;
                }
                return _importReports; }
            set { _importReports = value; }
        }

    }
}