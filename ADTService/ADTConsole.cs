//using ADTService.WcfCommunication;
using System.Collections.Generic;
using System.IO;
using System.Threading;
//using System.ServiceModel;
using System;
using System.Diagnostics;

namespace ADTService {
	public class ADTConsole {

		static string sSource;
		static string sLog;
		static string sEvent;

		static ADTConsole() {
			sSource = "ADTService";
			sLog = "Application";
			sEvent = "Sample Event";
			if (!EventLog.SourceExists(sSource))
				EventLog.CreateEventSource(sSource, sLog);
		}

		//static IServiceCallback callback;
		//static bool ok = false;

        static protected string LogPath { get { return ADT.LogPath; } set { ADT.LogPath = value; } }

        static private List<ConsoleLog> consoles = new List<ConsoleLog>();
		static public List<ConsoleLog> Consoles {
			get { return consoles; }
			set { consoles = value; }
		}

		public static void Write(string ConsoleName, string String, params object[] arg) {
			string text = string.Format(String, arg);
			var c = Consoles.Find(f => f.Name == ConsoleName);
            //if (c == null) {
            //	c = new ConsoleLog() { Name = ConsoleName };
            //	Consoles.Add(c);
            //}
            //c.Lines.Add(text);
            //if (ok) {
            //	try {
            //		//callback.WriteToConsole(ConsoleName, text);
            //	} catch (System.Exception e) {
            //		ok = false;
            //		//callback = null;
            //	}
            //}
            var t = new Thread((p) => {
                WriteToLog(LogPath + ConsoleName, text);
            }) { Name = "ADTConsole " + ConsoleName + " Log Writer" };
            t.Start();
        }
		public static void WriteLine(string ConsoleName, string String, params object[] arg) {
			Write(ConsoleName, String + System.Environment.NewLine, arg);

		}

        static void WriteToLog(string LogName, string Message) {
            if (!Directory.Exists(LogPath)) {
                EventLog.WriteEntry(sSource, "Created " + LogPath + " folder for logs");
                Directory.CreateDirectory(LogPath);
            }
            System.IO.StreamWriter file = null;
            bool done = false;
            while (!done) {
                try {
                    file = new System.IO.StreamWriter(LogName + "_log.txt", true);
                } catch (IOException e) {
                    //EventLog.WriteEntry(sSource, e.Message, EventLogEntryType.Error);
                } finally {
                    if (file != null) {
                        file.Write(Message);
                        file.Close();
                        done = true;
                    }
                }
                Thread.Sleep(5);
            }
        }

        //public static void SetCallback() {
        //    try {
        //        callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
        //        ok = true;
        //    } catch (System.Exception e) {
        //        ok = false;
        //    }
        //}

        //public static void RemoveCallback() {
        //    ok = false;
        //}

        public class ConsoleLog {
			public List<string> Lines = new List<string>();
			public string Name;
		}

		public static void Cleanup() {
			foreach (var c in Consoles) {
				if (c.Lines.Count > 50) {
					c.Lines.RemoveRange(0, c.Lines.Count - 50);
				}
			}
		}

		public static List<ConsoleLog> GetCache() {
			Cleanup();
			return Consoles;
		}
	}
}
