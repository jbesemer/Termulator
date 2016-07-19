using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Library
{
	public class ComPortInfo
	{
		#region // Constants //////////////////////////////////////////////////

		public const string COHERENT_VENDOR_ID = "vid_0d4d";

		public const string COHERENT_SSIM_PRODUCT_ID = "pid_003F";
		public const string COHERENT_PMPRO_PRODUCT_ID = "pid_0040";

		public const string COHERENT_OBIS_PRODUCT_ID = "pid_003B";
		public const string COHERENT_HEISENBERG_PRODUCT_ID = "pid_003E";

		public static string QualifiedVendorID = COHERENT_VENDOR_ID;

		public static string[] QualifiedProductIDs
			= new string[]
				{
					COHERENT_VENDOR_ID + "," + COHERENT_SSIM_PRODUCT_ID,
					COHERENT_VENDOR_ID + "," + COHERENT_PMPRO_PRODUCT_ID,
				};

		public const string pid_pattern = @"pid_([0-9a-f]{4})";
		public const string vid_pattern = @"vid_([0-9a-f]{4})";
		public static readonly Regex pid_regex = new Regex( pid_pattern, RegexOptions.IgnoreCase );
		public static readonly Regex vid_regex = new Regex( vid_pattern, RegexOptions.IgnoreCase );

		// Location of USB device info:
		// HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_0D4D&PID_003F
		// HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_0D4D&PID_0040

		#endregion

		public ComPortInfo()
		{
		}

		public ComPortInfo( string portName )
		{
			PortName = portName;
			FriendlyName = "Dummy (" + portName + ")";
		}

		public ComPortInfo( string portName, string hardwareId )
			: this( portName )
		{
			HardwareID = hardwareId;
		}

		#region // Properties /////////////////////////////////////////////////

		public string PortName;
		public string FriendlyName;
		public string HardwareID;
		public string DevicePath;

		// this is what we want to show in a combo box

		public override string ToString()
		{
			return FriendlyName;
		}

		public bool IsQualified
		{
			get
			{
				if( HardwareID != null )
					foreach( var qualified in QualifiedProductIDs )
						if( SameHardwareId( qualified ) )
							return true;

				return false;
			}
		}

		#endregion

		#region // Hardware ID Extraction/Comparision /////////////////////////

		// compare two devices' hardware IDs

		public bool SameHardwareId( string other )
		{
			return PidVidMatch( this.HardwareID, other );
		}

		public static bool PidVidMatch( string a, string b )
		{
			return GetPid( a ) == GetPid( b )
				&& GetVid( a ) == GetVid( b );
		}

		// HardwareID is like "usb\\vid_0d4d&pid_003f&rev_0200"
		// DevicePath is like "PCIROOT(0)#PCI(1D07)#USBROOT(0)#USB(3)#USB(1)"

		public bool HardwareID_Contains( string id )
		{
			return HardwareID.IndexOf( id, StringComparison.InvariantCultureIgnoreCase ) >= 0;
		}

		public bool HardwareID_Contains( string[] ids )
		{
			foreach( string id in ids )
				if( HardwareID_Contains( id ) )
					return true;	// contains one

			return false;			// contains none
		}

		public static string GetPid( string path )
		{
			Match match = pid_regex.Match( path );
			if( match.Success && match.Groups.Count == 2 )
				return match.Groups[ 1 ].Value.ToUpper();
			else
				return null; //  throw new NotSupportedException();
		}

		public static string GetVid( string path )
		{
			Match match = vid_regex.Match( path );
			if( match.Success && match.Groups.Count == 2 )
				return match.Groups[ 1 ].Value.ToUpper();
			else
				return null; // throw new NotSupportedException();
		}

		#endregion
	}
}
