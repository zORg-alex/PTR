using System;
using System.Reflection;

namespace PTR.PTRLib.Common {
	public class PackUriHelper {

		/// <summary>

		/// Helper method for generating a “pack://” URI for a given relative file based on the

		/// assembly that this class is in.

		/// </summary>

		public static Uri MakePackUri(string relativeFile, Assembly assembly) {

			string uriString = "pack://application:,,,/" + assembly.ToString().Split(',')[0] + ";component/" + relativeFile;

			return new Uri(uriString);

		}


		private static string AssemblyShortName {

			get {

				if (_assemblyShortName == null) {

					Assembly a = typeof(PackUriHelper).Assembly;

					// Pull out the short name.

					_assemblyShortName = a.ToString().Split(',')[0];

				}

				return _assemblyShortName;

			}

		}

		private static string _assemblyShortName;

	}
}