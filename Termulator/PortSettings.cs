using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Termulator
{
	public enum EnterSends { CR, LF, CRLF }

	public static class PortSettings
	{
		public const string ObisDefaultPortSettings = "115200,8,-1,0,1,0,1,0,0";

		public const string ParameterSeparator = ",";
		public static readonly char[] NameSeparator = { ':' };
		public const int MinParameterCount = 6;

		public const int InfiniteTimeout = -1;

		public static EnterSends EnterSends { get; private set; }

		public static bool Edit_NL_To_CRLF { get; private set; }
		public static bool Edit_CR_To_CRLF { get; private set; }
		public static bool ShowNonprinting { get; private set; }
		public static bool ShowWhitespace { get; private set; }
		public static bool StripHighBit { get; private set; }

		#region // Port settings History //////////////////////////////////////

		public static Dictionary<string,string>Previous;

		static PortSettings()
		{
			LoadAllPrevious();
		}

		public static void LoadAllPrevious()
		{
			Previous = new Dictionary<string, string>();
			var settings = Properties.Settings.Default.ComPortSettings;

			if( settings != null )
			{
				foreach( string setting in settings )
				{
					string[] fields = setting.Split( NameSeparator, 2 );
					if( fields.Length == 2 )
					{
						Previous[ fields[ 0 ] ] = fields[ 1 ];
					}
				}
			}
		}

		public static void SaveAllPrevious()
		{
			var coll = new StringCollection();

			foreach( var kvp in Previous )
			{
				coll.Add( 
					string.Format(
						"{0}{1}{2}",
						kvp.Key,
						NameSeparator[ 0 ],
						kvp.Value ) );
			}

			Properties.Settings.Default.ComPortSettings = coll;
			Properties.Settings.Default.Save();
		}

		public static string GetPreviousSettings( string portName )
		{
			string value = ObisDefaultPortSettings;
			Previous.TryGetValue( portName, out value );	// overwrite default with previous, if any
			return value;
		}

		public static void SavePreviousSettings( string portName, string setting )
		{
			Previous[ portName ] = setting;
			SaveAllPrevious();
		}

		#endregion

		#region // Encode /////////////////////////////////////////////////////

		public static string Encode( SerialPort port )
		{
			int[] fields 
				= new int[]{
					(int)port.BaudRate,
					(int)port.DataBits,
					(int)port.ReadTimeout,
					(int)port.Parity,
					(int)port.StopBits,
					(int)port.Handshake,
					Encode( Edit_NL_To_CRLF ),
					Encode( Edit_CR_To_CRLF ),
					Encode( ShowNonprinting ),
					Encode( ShowWhitespace ),
					(int)EnterSends,
					Encode( StripHighBit ),
				};

			return Encode( fields );
		}

		public static int Encode( bool value ) { return ( value ? 1 : 0 ); }
		
		public static string Encode( int[] fields )
		{
			string[] strings = new string[ fields.Length ];

			for( int i=0; i < fields.Length; i++ )
			{
				strings[ i ] = fields[ i ].ToString();
			}

			return string.Join( ParameterSeparator, strings );
		}

		#endregion

		#region // Decode /////////////////////////////////////////////////////

		public static int[] Decode( string settings )
		{
			string[] fields = settings.Split( ParameterSeparator[0]);
			int[] ints = new int[ fields.Length ];

			for( int i = 0; i < fields.Length; i++ )
			{
				ints[ i ] = GetInt( fields[ i ] );
			}

			return ints;
		}

		public static void Decode( SerialPort port, string settings )
		{
			int[] fields = Decode( settings );

			if( fields.Length >= MinParameterCount )
			{
				port.BaudRate = fields[ 0 ];
				port.DataBits = fields[ 1 ];
				port.ReadTimeout = fields[ 2 ];
				port.Parity = (Parity)fields[ 3 ];
				port.StopBits = (StopBits)fields[ 4 ];
				port.Handshake = (Handshake)fields[ 5 ];

				if( fields.Length >= 7 )
					Edit_NL_To_CRLF = ( fields[ 6 ] != 0 );

				if( fields.Length >= 8 )
					Edit_CR_To_CRLF = ( fields[ 7 ] != 0 );

				if( fields.Length >= 9 )
					ShowNonprinting = ( fields[ 8 ] != 0 );

				if( fields.Length >= 10 )
					ShowWhitespace = ( fields[ 9 ] != 0 );

				if( fields.Length >= 11 )
					EnterSends = (EnterSends)fields[ 10 ];

				if( fields.Length >= 12 )
					StripHighBit = ( fields[ 10 ] != 0 );

				SavePreviousSettings( port.PortName, settings );
			}
			else
			{
				// throw
			}
		}

		public static void SetObisDefaults( SerialPort port )
		{
			Decode( port, ObisDefaultPortSettings );
		}

		#endregion

		#region // Encode/decode helpers //////////////////////////////////////

		public static string EncodeTimeout( int timeout )
		{
			return
				( timeout < 0 )
					? "None"
					: timeout.ToString();
		}

		public static int DecodeTimeout( string s )
		{
			int timeout;

			if( !int.TryParse( s, out timeout ) )
			{
				timeout = InfiniteTimeout;
			}
			return timeout;
		}

		public static int GetInt( string s )
		{
			int n;

			if( int.TryParse( s, out n ) )
			{
				return n;
			}

			return -1;
		}

		#endregion
	}
}
