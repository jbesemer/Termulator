using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SharedLibrary;

namespace Library
{
	public static class Extensions
	{
		#region // Save/Load Form's LocationAndSize ///////////////////////////

		// for resizable windows we want to save the location and size

		public static string SaveLocationAndSize( this Form form )
		{
			if( form.Left < 0 || form.Top < 0 )
				return null;
			else
				return
					String.Format(
						"{0},{1},{2},{3}",
						form.Left,
						form.Top,
						form.Width,
						form.Height );
		}

		public static void LoadLocationAndSize( this Form form, string setting )
		{
			string[] settings = setting.Split( ',' );

			switch( settings.Length ){
			case 4:
				form.Size 
					= new System.Drawing.Size(
						Int32.Parse( settings[ 2 ] ),
						Int32.Parse( settings[ 3 ] ));
				goto case 2;
			case 2:
				form.Location 
					= new System.Drawing.Point(
						Int32.Parse( settings[ 0 ] ),
						Int32.Parse( settings[ 1 ] ));
				break;
			}
		}

		#endregion

		#region // CenterOnParent /////////////////////////////////////////////

		public static void CenterOnParent( this Form form, string parentWindowLocationSize )
		{
			int[] settings;
			int left, top, width, height;

			try
			{
				settings
					= parentWindowLocationSize
						.Split( ',' )
							.Select<string, int>( item => Int32.Parse( item ) )
								.ToArray<int>();
			}
			catch( FormatException )
			{
				return;
			}

			switch( settings.Length )
			{
			case 4:
				left = settings[ 0 ];
				top = settings[ 1 ];
				width = settings[ 2 ];
				height = settings[ 3 ];

				form.Location
					= new System.Drawing.Point(
						left + ( width - form.Width ) / 2,
						top + ( height - form.Height ) / 2 );
				break;
			}
		}

		#endregion

		#region // string[] To/From StringCollection  /////////////////////////

		public static void FromStrings( this StringCollection collection, string[] strings )
		{
			collection.Clear();

			foreach( string s in strings )
				collection.Add( s );
		}

		public static string[] ToStrings( this StringCollection collection )
		{
			string[] strings = new string[ collection.Count ];

			for( int i = 0; i < collection.Count; i++ )
				strings[ i ] = collection[ i ];

			return strings;
		}

		#endregion

		#region // PrefaceLinesWithTimestamp and Timestamp ////////////////////

		public static string PrefaceLinesWithTimestamp( string text, bool continuing=false )
		{
			if( string.IsNullOrEmpty( text ) )
				return "";

			StringBuilder r = new StringBuilder();
			string[] lines
				= text.Replace( Const.CR, "" )
					.Split( Const.LF_char );

			bool complete = text.EndsWith( Const.LF );

			for( int i = 0; i < lines.Length; i++ )
			{
				string line = lines[ i ];
				// timestamp prefix for all except first line if continuing
				if( i > 0 || !continuing )
					r.Append( Timestamp() );

				r.Append( line );

				// newline suffix for all 
				// but excluding last line if it is incomplete
				if( i < lines.Length || complete )
					r.Append( Const.CRLF );
			}

			return r.ToString();
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

	#endregion
	}
}
