using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Interop;

namespace Library
{
	public class Dbt
	{
		#region Dbt Class - GUIDs

		// works
		public static readonly Guid USBDeviceGuid = new Guid( "A5DCBF10-6530-11D2-901F-00C04FB951ED" ); // From: http://stackoverflow.com/questions/16245706/check-for-device-change-add-remove-events

		// did NOT work
		public static readonly Guid hidGuid = new Guid( "4d1e55b2-f16f-11cf-88cb-001111000030" );

		// not tried
		public static readonly Guid usbXpressGuid = new Guid( "3c5e1462-5695-4e18-876b-f3f3d08aaf18" );
		public static readonly Guid cp210xGuid = new Guid( "993f7832-6e2d-4a0f-b272-e2c78e74f93e" );
		public static readonly Guid newCP210xGuid = new Guid( "a2a39220-39f4-4b88-aecb-3d86a35dc748" );

		#endregion

		#region Dbt Class - Constants

		public enum EventType { Connected, Removed }

		public const ushort WM_DEVICECHANGE = 0x0219;
		public const ushort DBT_DEVICEARRIVAL = 0x8000;
		public const ushort DBT_DEVICEREMOVECOMPLETE = 0x8004;
		public const ushort DBT_DEVTYP_DEVICEINTERFACE = 0x0005;
		public const int DEVICE_NOTIFY_WINDOW_HANDLE = 0x0000;

		#endregion

		#region Dbt Class - Device Change Structures

		[StructLayout( LayoutKind.Sequential )]
		public class DEV_BROADCAST_DEVICEINTERFACE
		{
			public int dbcc_size;
			public int dbcc_devicetype;
			public int dbcc_reserved;
			public Guid dbcc_classguid;
			public char dbcc_name;
		}

		[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode )]
		public class DEV_BROADCAST_DEVICEINTERFACE_1
		{
			public int dbcc_size;
			public int dbcc_devicetype;
			public int dbcc_reserved;
			public Guid dbcc_classguid;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst = 255 )]
			public char[] dbcc_name;

			// DevicePath looks like: "\\?\USB#VID_16C0&PID_0483#605390#{a5dcbf10-6530-11d2-901f-00c04fb951ed}"

			public string GetDevicePath()
			{
				// Get the device path from the broadcast message
				string devicePath = new string( dbcc_name );

				// Remove null-terminated data from the string
				int pos = devicePath.IndexOf( (char)0 );
				if( pos != -1 )
					devicePath = devicePath.Substring( 0, pos );

				return devicePath;
			}
		}

		[StructLayout( LayoutKind.Sequential )]
		public class DEV_BROADCAST_HDR
		{
			public int dbch_size;
			public int dbch_devicetype;
			public int dbch_reserved;
		}

		#endregion

		#region DLL Imports

		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		public static extern IntPtr RegisterDeviceNotification( IntPtr hRecipient, IntPtr NotificationFilter, uint Flags );

		[DllImport( "user32.dll" )]
		public static extern uint UnregisterDeviceNotification( IntPtr Handle );

		#endregion

		#region Notifier Helper Class

		public class Notifier : IMessageFilter
		{
			public IntPtr NotificationHandle { get; protected set; }
			public IntPtr WindowHandle { get; protected set; }

			public Notifier( IntPtr Source )
			{
				WindowHandle = Source;

				Register();
				//Application.AddMessageFilter( this );
			}

			public bool PreFilterMessage( ref Message m )
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
								OnRemoval( devicePath );
								Trace( "Device \"{0}\" was removed", devicePath );
							}
							else if( nEventType == Dbt.DBT_DEVICEARRIVAL )
							{
								OnArrival( devicePath );
								Trace( "Device \"{0}\" arrived", devicePath );
							}
						}
					}
				}

				return false;
			}

#if false // unused
			public HwndSource Source { get; protected set; }

			public static IntPtr HandleFromVisual( Visual visual )
			{
				var Source
					= PresentationSource.FromVisual( visual )
						as HwndSource;	// http://stackoverflow.com/questions/1556182/finding-the-handle-to-a-wpf-window

				return Source.Handle;
			}

			public IntPtr HandleFromWindow( Window window )
			{
				return new WindowInteropHelper( window ).Handle;	// http://www.codeproject.com/Articles/133632/Win-Handle-HWND-WPF-Objects-A-Note
			}
#endif

			#region // Register & Unregister Methods

			public void Register()
			{
				Register( Dbt.USBDeviceGuid );
			}

			public void Register( Guid guid )
			{
				Dbt.DEV_BROADCAST_DEVICEINTERFACE devIF
					= new Dbt.DEV_BROADCAST_DEVICEINTERFACE();
				devIF.dbcc_size = Marshal.SizeOf( devIF );
				devIF.dbcc_devicetype = Dbt.DBT_DEVTYP_DEVICEINTERFACE;
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
			
			#endregion

			#region // Event Hooks

			public delegate void DbtEvent( object sender, string devicePath );

			public event DbtEvent Arrival;
			public event DbtEvent Removal;

			protected void OnArrival( string devicePath )
			{
				if( Arrival != null )
					Arrival( this, devicePath );
			}

			protected void OnRemoval( string devicePath )
			{
				if( Removal != null )
					Removal( this, devicePath );
			}

			// DevicePath looks like: "\\?\USB#VID_16C0&PID_0483#605390#{a5dcbf10-6530-11d2-901f-00c04fb951ed}"

			#endregion

			#region // WndProc template

			public IntPtr WndProc( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled )
			{
				// Intercept the WM_DEVICECHANGE message
				if( msg == Dbt.WM_DEVICECHANGE )
				{
					// Get the message event type
					int nEventType = wParam.ToInt32();

					// Trace( "WM_DEVICECHANGE {0}", nEventType );

					// Check for devices being connected or disconnected
					if( nEventType == Dbt.DBT_DEVICEARRIVAL ||
						nEventType == Dbt.DBT_DEVICEREMOVECOMPLETE )
					{
						Dbt.DEV_BROADCAST_HDR hdr = new Dbt.DEV_BROADCAST_HDR();

						// Convert lparam to DEV_BROADCAST_HDR structure
						Marshal.PtrToStructure( lParam, hdr );

						if( hdr.dbch_devicetype == Dbt.DBT_DEVTYP_DEVICEINTERFACE )
						{
							Dbt.DEV_BROADCAST_DEVICEINTERFACE_1 devIF = new Dbt.DEV_BROADCAST_DEVICEINTERFACE_1();

							// Convert lparam to DEV_BROADCAST_DEVICEINTERFACE structure
							Marshal.PtrToStructure( lParam, devIF );

							// Get the device path from the broadcast message
							string devicePath = devIF.GetDevicePath();

							// An HID device was connected or removed
							if( nEventType == Dbt.DBT_DEVICEREMOVECOMPLETE )
							{
								OnRemoval( devicePath );
								// Trace( "Device \"{0}\" was removed", devicePath );
							}
							else if( nEventType == Dbt.DBT_DEVICEARRIVAL )
							{
								OnArrival( devicePath );
								// Trace( "Device \"{0}\" arrived", devicePath );
							}
						}
					}
				}

				return IntPtr.Zero;
			}

			#endregion
		}

		#endregion

		#region // Trace Output ///////////////////////////////////////////////

		public static void Trace( string format, params object[] args )
		{
			Debug.WriteLine( "USB_DeviceDetector: " + string.Format( format, args ) );
		}

		#endregion
	}
}