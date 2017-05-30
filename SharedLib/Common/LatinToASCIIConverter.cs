using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTR.PTRLib.Common {
	class LatinToASCIIConverter {

		public static string Convert(string InString) {
			if (InString == null) return "";
			string newString = string.Empty, charString;
			char ch;
			int charsCopied;

			for (int i = 0; i < InString.Length; i++) {
				charString = InString.Substring(i, 1);
				charString = charString.Normalize(NormalizationForm.FormKD);
				// If the character doesn't decompose, leave it as-is
				if (charString.Length == 1)
					newString += charString;
				else {
					charsCopied = 0;
					for (int j = 0; j < charString.Length; j++) {
						ch = charString[j];
						// If the char is 7-bit ASCII, add
						if (ch < 128) {
							newString += ch;
							charsCopied++;
						}
					}
					/* If we've decomposed non-ASCII, give it back
                     * in its entirety, since we only mean to decompose
                     * Latin chars.
                    */
					if (charsCopied == 0)
						newString += InString.Substring(i, 1);
				}
			}
			return newString;
		}
	}
}
