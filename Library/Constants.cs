using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
	public class Const
	{
		public const string LF = "\n";
		public const string CR = "\r";
		public const string CRLF = "\r\n";
		public static readonly char[] LF_char = new char[] { '\n' };

		// FWIW [supposedly according to the HTML5 spec] 
		// textareas [which are not exactly same as textbox]
		// should return CRLF for each newline within.
		//
		// Patterns for newline on input streams:
		// 1. every two-character string consisting of a "CRLF"character pair.
		// 2. every occurrence of a "CR" not followed by a "LF". 
		// 3. every occurrence of a "LF" character not preceded by a "CR"
		//
		// "CR" = (U+000D) 
		// "LF" = (U+000A) 
		// per https://stackoverflow.com/questions/14217101/what-character-represents-a-new-line-in-a-text-area
	}
}
