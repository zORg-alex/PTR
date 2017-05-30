using PTR.PTRLib.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib {
	partial class PTRContext {
		public PTRContext(string ConnectionString):base(ConnectionString) {
		}

		public async void IFUnctionsAsync(Action<List<IFunction>> Assign) { Assign( await IFunctions.ToListAsync()); }

		public static string LatinToASCII(string String) {
			return LatinToASCIIConverter.Convert(String);
		}
	}
}
