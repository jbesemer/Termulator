using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminal
{
	[Flags]
	public enum SelectedVisibleCharacters { NL_to_CRLF, CR_to_CRLF, ShowWhitespace, ShowNewlines, ShowNonprinting }

	public class UnicodeControlCharacters
	{
		private Dictionary<char,string> Encoding = new Dictionary<char, string>();

		public Dictionary<char, string>.KeyCollection Keys { get { return Encoding.Keys; } }

		public Dictionary<char, string>.ValueCollection Values { get { return Encoding.Values; } }

		private void AddEntry( int code )
		{
			// encode control characters to Unicode "Control Pictures", 0x2400 thru 0x2424
			// http://en.wikipedia.org/wiki/Control_Pictures
			// Unfortunately, these aren't displayed properly in forms textbox, though
			// debug output does show properly in Studio's Output window. Probably need wpf.
			// Works here: ␋ and in Word but not brief.

			const int ControlPictureBase = 0x2400;
			char c = System.Convert.ToChar( code );
			string s = new string( System.Convert.ToChar( code + ControlPictureBase ), 1 );
			Encoding[ c ] = s;
			// Debug.WriteLine( "{0:x2}: {1}", code, s );
		}

		public UnicodeControlCharacters( SelectedVisibleCharacters selected )
		{
			for( int i = 0; i <= 0x20; i++ )
				AddEntry( i );
			AddEntry( 0x7f ); 
		}

		public bool IsControlCharacter( char c )
		{
			return Encoding.Keys.Contains<char>( c );
		}

		public string Convert( string s )
		{
			string r = "";

			foreach( char c in s )
				r += Convert( c );

			return r;
		}

		public string Convert( char c )
		{
			if( IsControlCharacter( c ) )
				return Encoding[ c ];
			else
				return new string( c, 1 );
		}

		public string UnitTest()
		{
			string text = "";

			foreach( char item in Encoding.Keys )
			{
				int code = (int)item;
				text +=
					string.Format(
						"{0:x2}: {1}\r\n",
						code,
						Convert( item ) );
			}

			return text;
		}
	}
}
