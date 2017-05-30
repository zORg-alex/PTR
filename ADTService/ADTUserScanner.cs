using PTR.PTRLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ADTService {
	class ADTUserScanner {
		static private PTRContext db = new PTRContext();
        internal static bool exit = false;

        static protected int Delay { get { return ADT.Delay; } set { ADT.Delay = value; } }
        static protected bool UsersReady { get { return ADT.UsersReady; } set { ADT.UsersReady = value; } }
        

        public ADTUserScanner() {
		}

		public void Main() {
			DirectoryEntry dirEntry = new DirectoryEntry("LDAP://DC=cspdom,DC=lv");
			var res = new DirectorySearcher(dirEntry) { Filter = "(&(&(objectClass=user)(objectClass=person)(!(objectClass=computer))))" }
				.FindAll();

			foreach (SearchResult u in res) {
				string name = "";
				string cn = "";
				string displayName = "";
				string mail = "";
				string accname = "";
				var de = u.GetDirectoryEntry();
				var en = u.Properties.GetEnumerator();
				for (int i = 0; i < u.Properties.Count; i++) {
					en.MoveNext();
					IEnumerator e;
					switch (en.Key.ToString()) {
						case "name":
							e = ((en.Value) as ResultPropertyValueCollection).GetEnumerator();
							e.MoveNext();
							name = e.Current.ToString();
							break;
						case "cn":
							e = ((en.Value) as ResultPropertyValueCollection).GetEnumerator();
							e.MoveNext();
							cn = e.Current.ToString();
							break;
						case "displayName":
							e = ((en.Value) as ResultPropertyValueCollection).GetEnumerator();
							e.MoveNext();
							displayName = e.Current.ToString();
							break;
						case "mail":
							e = ((en.Value) as ResultPropertyValueCollection).GetEnumerator();
							e.MoveNext();
							mail = e.Current.ToString();
							break;
						case "samaccountname":
							e = ((en.Value) as ResultPropertyValueCollection).GetEnumerator();
							e.MoveNext();
							accname = e.Current.ToString();
							break;
						default:
							break;
					}
					if (name != "" && accname != "" && mail != "") {
						ADUser adu = GetOrCreateADUser(accname, true, name);
						UUser uu = GetOrCreateUUser(name, mail);
						adu.UUser = uu;
						
						ADTConsole.WriteLine("ADTUserScanner", "ADUser {0} UUser {1}", adu.Login, adu.UUser.ToString());
						break;
					}
				}
                Thread.Sleep(ADT.Delay);
                if (exit) break;
            }
			try {
				db.SaveChanges();
			} catch (Exception e) {
				Console.WriteLine(e.Message);
			}
			UsersReady = true;
		}

		ADUser GetOrCreateADUser(string Login, bool Status, string Name) {
			ADUser r = db.ADUsers.FirstOrDefault(u => u.Login == Login);
			if (r == null) {
				r = new ADUser() {
					Login = Login,
					DN = Name,
					Status = Status
				};
				db.ADUsers.Add(r);
				db.SaveChanges();//May be wise to comment out
				
				ADTConsole.Write("ADTUserScanner", "ADUser Created {0}; ", r.DN);
			} else {
				ADTConsole.Write("ADTUserScanner", "ADUser Found {0}; ", r.DN);
			}
			return r;
		}

		UUser GetOrCreateUUser(string FullName, string Mail) {
			UUser r = db.UUsers.FirstOrDefault((u) => (u.NormalizedFullName.ToLower() == FullName.ToLower() || u.Email == Mail));
			r = (from a in db.UUsers where a.NormalizedFullName.ToLower() == FullName.ToLower() || a.Email == Mail select a).FirstOrDefault();
			if (r == null) {
				r = new UUser() {
					Name = FullName,
					Surname = "",
					FullName = FullName,
					Email = Mail
				};
				db.UUsers.Add(r);
				db.SaveChanges();//May be wise to comment out

				ADTConsole.Write("ADTUserScanner", "UUser Created {0};\n", r.NormalizedFullName);
			} else {
				ADTConsole.Write("ADTUserScanner", "UUser Found {0};\n", r.NormalizedFullName);
			}
			return r;
		}
	}
}
