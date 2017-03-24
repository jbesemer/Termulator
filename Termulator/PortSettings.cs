using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Termulator
{
	public enum EnterSends { CR, LF, CRLF }

	public class PortSettings
	{
		public const String ObisDefaultPortSettings = "115200,8,-1,0,1,0,1,0";
		public const String MeterlessPowerDefaultPortSettings = "9600,8,-1,0,1,0,1,0";

		public const String ParameterSeparator = ",";
		public readonly char[] NameSeparator = { ':' };
		public const int MinParameterCount = 6;

		public const int InfiniteTimeout = -1;

		public EnterSends EnterSends { get; private set; }

		public bool Edit_NL_To_CRLF { get; private set; }
		public bool Edit_CR_To_CRLF { get; private set; }
		public bool ShowNonprinting { get; private set; }
		public bool ShowWhitespace { get; private set; }

		#region // Port settings History //////////////////////////////////////

		public Dictionary<string,string>Previous;

		public PortSettings()
		{
		}

		public void LoadAllPrevious(StringCollection settings)
		{
			Previous = new Dictionary<string, string>();

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

		public StringCollection SaveAllPrevious()
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

			return coll;
		}

		public string GetPreviousSettings( string portName )
		{
			string value = ObisDefaultPortSettings;
			Previous.TryGetValue( portName, out value );	// overwrite default with previous, if any
			return value;
		}

		public void SavePreviousSettings( string portName, string setting )
		{
			Previous[ portName ] = setting;
			SaveAllPrevious();
		}

		public void RestorePreviousSettings( SerialPort port )
		{
			Decode( port, GetPreviousSettings( port.PortName ) );
		}

		#endregion

		#region // Encode /////////////////////////////////////////////////////

		public string Encode( SerialPort port )
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
				};

			return Encode( fields );
		}

		public int Encode( bool value ) { return ( value ? 1 : 0 ); }
		
		public string Encode( int[] fields )
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

		public int[] Decode( string settings )
		{
			string[] fields = settings.Split( ParameterSeparator[0]);
			int[] ints = new int[ fields.Length ];

			for( int i = 0; i < fields.Length; i++ )
			{
				ints[ i ] = GetInt( fields[ i ] );
			}

			return ints;
		}

		public void Decode( SerialPort port, string settings )
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

				SavePreviousSettings( port.PortName, settings );
			}
			else
			{
				// throw
			}
		}

		public void SetObisDefaults( SerialPort port )
		{
			Decode( port, ObisDefaultPortSettings );
		}

		#endregion

		#region // Encode/decode helpers //////////////////////////////////////

		public string EncodeTimeout( int timeout )
		{
			return
				( timeout < 0 )
					? "None"
					: timeout.ToString();
		}

		public int DecodeTimeout( string s )
		{
			int timeout;

			if( !int.TryParse( s, out timeout ) )
			{
				timeout = InfiniteTimeout;
			}
			return timeout;
		}

		public int GetInt( string s )
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
