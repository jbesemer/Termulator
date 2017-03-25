using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;             // required for Message
using System.Runtime.InteropServices;   // required for Marshal
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Diagnostics;

namespace Library
{
	// Delegate for event handler to handle the device events 
	public delegate void DeviceDetectorEventHandler( Object sender, USB_DeviceDetectorEventArgs e );

	public class USB_DeviceDetectorEventArgs : EventArgs
	{
		public string DevicePath;

		public USB_DeviceDetectorEventArgs()
		{
		}

		public USB_DeviceDetectorEventArgs( string devType )
		{
			DevicePath = devType;
		}
	}


	/// <summary>
	/// Detects insertion or removal of removable drives.
	/// Use it in 1 or 2 steps:
	/// 1) Create instance of this class in your project and add handlers for the
	/// DeviceArrived, DeviceRemoved and QueryRemove events.
	/// AND (if you do not want drive detector to creaate a hidden form))
	/// 2) Override WndProc in your form and call DriveDetector's WndProc from there. 
	/// If you do not want to do step 2, just use the DriveDetector constructor without arguments and
	/// it will create its own invisible form to receive messages from Windows.
	/// </summary>
	public class USB_DeviceDetector
	{
		#region  // Win32 Crap ////////////////////////////////////////////////

		// Win32 

		private const int WM_CREATE = 0x0001;
		private const int WM_DEVICECHANGE = 0x0219;
		private const int DBT_DEVICEARRIVAL = 0x8000; // system detected a new device
		private const int DBT_DEVICEQUERYREMOVE = 0x8001;   // Preparing to remove (any program can disable the removal)
		private const int DBT_DEVICEREMOVECOMPLETE = 0x8004; // removed 
		private const int DBT_DEVTYP_VOLUME = 0x00000002; // drive type is logical volume
		private const int DBT_DEVNODES_CHANGED = 7;		// device was added or removed
		private const int DBT_DEVTYP_DEVICEINTERFACE = 5;
		private const int DBT_DEVTYP_HANDLE = 6;
		private const int BROADCAST_QUERY_DENY = 0x424D5144;

		const int WM_POWERBROADCAST = 0x0218;
		const int PBT_APMPOWERSTATUSCHANGE = 0x000A;
		const int PBT_APMSUSPEND = 0x0004;
		const int PBT_APMRESUMESUSPEND = 0x0007;
		const int PBT_POWERSETTINGCHANGE = 0x8013;
		const int PBT_APMRESUMEAUTOMATIC = 0x0012;

		[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode )]
		public struct DEV_BROADCAST_DEVICEINTERFACE
		{
			public Int32 dbcc_size;
			public Int32 dbcc_devicetype;
			public Int32 dbcc_reserved;
			[MarshalAs( UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16 )]
			public Guid dbcc_classguid;	// was byte[] 
			[MarshalAs( UnmanagedType.ByValArray, SizeConst = 128 )]
			public char[] dbcc_name;
		}

		#endregion

		#region  // Register / Unregister /////////////////////////////////////

		public IntPtr NotificationHandle { get; protected set; }
		public IntPtr WindowHandle { get; protected set; }

		public void Register()
		{
			Register( Dbt.USBDeviceGuid );
		}

		public void Register( Guid guid )
		{
			DEV_BROADCAST_DEVICEINTERFACE devIF
				= new DEV_BROADCAST_DEVICEINTERFACE();
			devIF.dbcc_size = Marshal.SizeOf( devIF );
			devIF.dbcc_devicetype = DBT_DEVTYP_DEVICEINTERFACE;
			devIF.dbcc_reserved = 0;
			devIF.dbcc_classguid = guid;

			// Allocate a buffer for DLL call
			IntPtr devIFBuffer = Marshal.AllocHGlobal( devIF.dbcc_size );

			// Copy devIF to buffer
			Marshal.StructureToPtr( devIF, devIFBuffer, true );

			// Register for HID device notifications
			NotificationHandle
				= Dbt.RegisterDeviceNotification(
					WindowHandle,
					devIFBuffer,
					Dbt.DEVICE_NOTIFY_WINDOW_HANDLE );

			// Copy buffer to devIF
			Marshal.PtrToStructure( devIFBuffer, devIF );

			// Free buffer
			Marshal.FreeHGlobal( devIFBuffer );
		}

		public void Unregister()
		{
			uint ret = Dbt.UnregisterDeviceNotification( NotificationHandle );
		}

		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		public static extern IntPtr RegisterDeviceNotification( IntPtr hRecipient, IntPtr NotificationFilter, uint Flags );

		[DllImport( "user32.dll" )]
		public static extern uint UnregisterDeviceNotification( IntPtr Handle );

		#endregion

		/// <summary>
		/// Events signalized to the client app.
		/// Add handlers for these events in your form to be notified of removable device events 
		/// </summary>
		public event DeviceDetectorEventHandler Arrival;
		public event DeviceDetectorEventHandler Removal;

		/// <summary>
		/// The easiest way to use DriveDetector. 
		/// It will create hidden form for processing Windows messages about USB drives
		/// You do not need to override WndProc in your form.
		/// </summary>
		public USB_DeviceDetector()
		{
			DetectorForm frm = new DetectorForm( this );
			frm.Show(); // needed to trigger form.load(); form will be hidden immediately in Form_Activate()
		}

		#region // WindowProc /////////////////////////////////////////////////

		/// <summary>
		/// Message handler which must be called from client form.
		/// Processes Windows messages and calls event handlers. 
		/// </summary>
		/// <param name="m"></param>

		public void WndProc( ref Message m )
		{
			if( m.Msg == WM_DEVICECHANGE )
			{
				// Get the message event type
				int nEventType = m.WParam.ToInt32();

				Trace( "WM_DEVICECHANGE {0}", nEventType );

				// Check for devices being connected or disconnected
				if( nEventType == Dbt.DBT_DEVICEARRIVAL ||
					nEventType == Dbt.DBT_DEVICEREMOVECOMPLETE )
				{
					Dbt.DEV_BROADCAST_HDR hdr = new Dbt.DEV_BROADCAST_HDR();

					// Convert lparam to DEV_BROADCAST_HDR structure
					Marshal.PtrToStructure( m.LParam, hdr );

					if( hdr.dbch_devicetype == Dbt.DBT_DEVTYP_DEVICEINTERFACE )
					{
						Dbt.DEV_BROADCAST_DEVICEINTERFACE_1 devIF = new Dbt.DEV_BROADCAST_DEVICEINTERFACE_1();

						// Convert lparam to DEV_BROADCAST_DEVICEINTERFACE structure
						Marshal.PtrToStructure( m.LParam, devIF );

						// Get the device path from the broadcast message
						string devicePath = devIF.GetDevicePath();

						// An HID device was connected or removed
						if( nEventType == Dbt.DBT_DEVICEREMOVECOMPLETE )
						{
							if( Removal != null )
								Removal( this, new USB_DeviceDetectorEventArgs( devicePath ) );
							Trace( "Device \"{0}\" was removed", devicePath );
						}
						else if( nEventType == Dbt.DBT_DEVICEARRIVAL )
						{
							if( Arrival != null )
								Arrival( this, new USB_DeviceDetectorEventArgs( devicePath ) );
							Trace( "Device \"{0}\" arrived", devicePath );
						}
					}
				}
			}
		}

		#endregion

		#region // Trace Output ///////////////////////////////////////////////

		public static void Trace( string format, params object[] args )
		{
			Debug.WriteLine( "USB_DeviceDetector: " + string.Format( format, args ) );
		}

		#endregion

	}

	#region // Detector Form //////////////////////////////////////////////////

	/// <summary>
	/// Hidden Form which we use to receive Windows messages about flash drives
	/// </summary>
	internal class DetectorForm : Form
	{
		private USB_DeviceDetector Detector = null;

		/// <summary>
		/// Set up the hidden form. 
		/// </summary>
		/// <param name="detector">DriveDetector object which will receive notification about USB drives, see WndProc</param>
		public DetectorForm( USB_DeviceDetector detector )
		{
			Detector = detector;
			this.MinimizeBox = false;
			this.MaximizeBox = false;
			this.ShowInTaskbar = false;
			this.ShowIcon = false;
			this.FormBorderStyle = FormBorderStyle.None;

			this.Load += new System.EventHandler( this.DetectorForm_Load );
			this.Activated += new EventHandler( this.DetectorForm_Activated );
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.DetectorForm_FormClosing );
		}

		private void DetectorForm_Load( object sender, EventArgs e )
		{
			// We don't really need this, just to display the label in designer ...
			InitializeComponent();

			// Create really small form, invisible anyway.
			this.Size = new System.Drawing.Size( 5, 5 );
		}

		private void DetectorForm_Activated( object sender, EventArgs e )
		{
			this.Visible = false;
		}

		public void DetectorForm_FormClosing( object sender, FormClosingEventArgs e )
		{
		}

		/// <summary>
		/// This function receives all the windows messages for this window (form).
		/// We call the DriveDetector from here so that is can pick up the messages about
		/// drives arrived and removed.
		/// </summary>
		protected override void WndProc( ref Message m )
		{
			base.WndProc( ref m );

			if( Detector != null )
			{
				Detector.WndProc( ref m );
			}
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();

			// 
			// DetectorForm
			// 
			this.ClientSize = new System.Drawing.Size( 360, 80 );
			this.Name = "DetectorForm";

			this.ResumeLayout( false );
			this.PerformLayout();

		}
	}

	#endregion
}

