using ADTService.WcfCommunication;
using System.Collections.Generic;
using System.ServiceModel;

namespace ADTService {
	public class ADTConsole {

		static IServiceCallback callback;
		static bool ok = false;

		static private List<ConsoleLog> consoles = new List<ConsoleLog>();
		static public List<ConsoleLog> Consoles {
			get { return consoles; }
			set { consoles = value; }
		}

		public static void Write(string ConsoleName, string String, params object[] arg) {
			string text = string.Format(String, arg);
			var c = Consoles.Find(f => f.Name == ConsoleName);
			if (c == null) {
				c = new ConsoleLog() { Name = ConsoleName };
				Consoles.Add(c);
			}
			c.Lines.Add(text);
			if (ok) {
				try {
					callback.WriteToConsole(ConsoleName, text);
				} catch (System.Exception e) {
					ok = false;
					callback = null;
				}
			}
		}
		public static void WriteLine(string ConsoleName, string String, params object[] arg) {
			Write(ConsoleName, String + System.Environment.NewLine, arg);
		}

        public static void SetCallback() {
            try {
                callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
                ok = true;
            } catch (System.Exception e) {
                ok = false;
            }
        }

        public static void RemoveCallback() {
            ok = false;
        }

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
