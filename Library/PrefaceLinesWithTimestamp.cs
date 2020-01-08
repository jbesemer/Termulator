using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
	public class PrefaceLinesWithTimestamp
	{
		#region // singleton instance /////////////////////////////////////////

		protected static PrefaceLinesWithTimestamp _Current;
		public static PrefaceLinesWithTimestamp Current
		{
			get
			{
				return _Current
					??( _Current = new PrefaceLinesWithTimestamp() );
			}
		}

		#endregion

		public string Text = "";
		public bool IsContinuing = false;

		public PrefaceLinesWithTimestamp()
		{
		}

		public string Convert( string text )
		{
			if( string.IsNullOrEmpty( text ) )
				return "";

			Text = text;
			StringBuilder result = new StringBuilder();

			while( !string.IsNullOrEmpty( Text ) )
			{
				var line = ExtractLine( ref Text );
				if( !IsContinuing )
				{
					line = Timestamp() + line;
				}
				IsContinuing = !line.EndsWith( Const.LF );

				result.Append( line );
			}

			return result.ToString();
		}

		public string ExtractLine( ref string Text )
		{
			int pos = Text.IndexOf( Const.LF );
			string r;
			if( pos >= 0 )
			{
				r = Text.Substring( 0, pos + 1 );
				Text = Text.Substring( pos + 1 );
				return r.TrimEnd() + Const.CRLF;
			}
			else
			{
				r = Text;
				Text = "";
			}

			return r;
		}

		public static string Timestamp()
		{
			DateTime dt = DateTime.Now;
			return
				string.Format(
					"{0:d2}:{1:d2}:{2:d2}.{3:d3} ",
					dt.Hour,
					dt.Minute,
					dt.Second,
					dt.Millisecond );
		}
	}
}
