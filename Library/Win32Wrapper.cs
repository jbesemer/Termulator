﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Library
{
	public class Win32Wrapper
	{
		#region // Win32 constants ////////////////////////////////////////////

		//private const int BROADCAST_QUERY_DENY = 0x424D5144;
		public const int WM_DEVICECHANGE = 0x0219;

		/// <summary>
		/// Access rights for registry key objects.
		/// </summary>
		public enum REGKEYSECURITY : uint
		{
			/// <summary>
			/// Combines the STANDARD_RIGHTS_REQUIRED, KEY_QUERY_VALUE, 
			/// KEY_SET_VALUE, KEY_CREATE_SUB_KEY, KEY_ENUMERATE_SUB_KEYS, 
			/// KEY_NOTIFY, and KEY_CREATE_LINK access rights.
			/// </summary>
			KEY_ALL_ACCESS = 0xF003F,

			/// <summary>
			/// Reserved for system use.
			/// </summary>
			KEY_CREATE_LINK = 0x0020,

			/// <summary>
			/// Required to create a subkey of a registry key.
			/// </summary>
			KEY_CREATE_SUB_KEY = 0x0004,

			/// <summary>
			/// Required to enumerate the subkeys of a registry key.
			/// </summary>
			KEY_ENUMERATE_SUB_KEYS = 0x0008,

			/// <summary>
			/// Equivalent to KEY_READ.
			/// </summary>
			KEY_EXECUTE = 0x20019,

			/// <summary>
			/// Required to request change notifications for a registry key 
			/// or for subkeys of a registry key.
			/// </summary>
			KEY_NOTIFY = 0x0010,

			/// <summary>
			/// Required to query the values of a registry key.
			/// </summary>
			KEY_QUERY_VALUE = 0x0001,

			/// <summary>
			/// Combines the STANDARD_RIGHTS_READ, KEY_QUERY_VALUE, KEY_ENUMERATE_SUB_KEYS, 
			/// and KEY_NOTIFY values.
			/// </summary>
			KEY_READ = 0x20019,

			/// <summary>
			/// Required to create, delete, or set a registry value.
			/// </summary>
			KEY_SET_VALUE = 0x0002,

			/// <summary>
			/// Indicates that an application on 64-bit Windows should operate on the 
			/// 32-bit registry view. For more information, see Accessing an 
			/// Alternate Registry View. This flag must be combined using the OR 
			/// operator with the other flags in this table that either query or 
			/// access registry values. Windows 2000:  This flag is not supported.
			/// </summary>
			KEY_WOW64_32KEY = 0x0200,

			/// <summary>
			/// Indicates that an application on 64-bit Windows should operate on the 
			/// 64-bit registry view. For more information, see Accessing an 
			/// Alternate Registry View. This flag must be combined using the OR 
			/// operator with the other flags in this table that either query or 
			/// access registry values. Windows 2000:  This flag is not supported.
			/// </summary>
			KEY_WOW64_64KEY = 0x0100,

			/// <summary>
			/// Combines the STANDARD_RIGHTS_WRITE, KEY_SET_VALUE, 
			/// and KEY_CREATE_SUB_KEY access rights.
			/// </summary>
			KEY_WRITE = 0x20006
		}


		/// <summary>
		/// Flags controlling what is included in the device information 
		/// set built by SetupDiGetClassDevs
		/// </summary>
		[Flags]
		public enum DIGCF : int
		{
			DIGCF_DEFAULT = 0x00000001,// only valid with DIGCF_DEVICEINTERFACE
			DIGCF_PRESENT = 0x00000002,
			DIGCF_ALLCLASSES = 0x00000004,
			DIGCF_PROFILE = 0x00000008,
			DIGCF_DEVICEINTERFACE = 0x00000010,
		}

		/// <summary>
		/// Values specifying the scope of a device property change.
		/// </summary>
		public enum DICS_FLAG : uint
		{
			/// <summary>
			/// Make change in all hardware profiles
			/// </summary>
			DICS_FLAG_GLOBAL = 0x00000001,

			/// <summary>
			/// Make change in specified profile only
			/// </summary>
			DICS_FLAG_CONFIGSPECIFIC = 0x00000002,

			/// <summary>
			/// 1 or more hardware profile-specific
			/// </summary>
			DICS_FLAG_CONFIGGENERAL = 0x00000004,
		}

		/// <summary>
		/// KeyType values for SetupDiCreateDevRegKey, SetupDiOpenDevRegKey, 
		/// and SetupDiDeleteDevRegKey.
		/// </summary>
		public enum DIREG : uint
		{
			/// <summary>
			/// Open/Create/Delete device key
			/// </summary>
			DIREG_DEV = 0x00000001,

			/// <summary>
			/// Open/Create/Delete driver key
			/// </summary>
			DIREG_DRV = 0x00000002,

			/// <summary>
			/// Delete both driver and Device key
			/// </summary>
			DIREG_BOTH = 0x00000004,
		}

		public enum WinErrors : long
		{
			ERROR_SUCCESS = 0,
			ERROR_INVALID_FUNCTION = 1,
			ERROR_FILE_NOT_FOUND = 2,
			ERROR_PATH_NOT_FOUND = 3,
			ERROR_TOO_MANY_OPEN_FILES = 4,
			ERROR_ACCESS_DENIED = 5,
			ERROR_INVALID_HANDLE = 6,
			ERROR_ARENA_TRASHED = 7,
			ERROR_NOT_ENOUGH_MEMORY = 8,
			ERROR_INVALID_BLOCK = 9,
			ERROR_BAD_ENVIRONMENT = 10,
			ERROR_BAD_FORMAT = 11,
			ERROR_INVALID_ACCESS = 12,
			ERROR_INVALID_DATA = 13,
			ERROR_OUTOFMEMORY = 14,
			ERROR_INSUFFICIENT_BUFFER = 122,
			ERROR_MORE_DATA = 234,
			ERROR_NO_MORE_ITEMS = 259,
			ERROR_SERVICE_SPECIFIC_ERROR = 1066,
			ERROR_INVALID_USER_BUFFER = 1784
		}

		/// <summary>
		/// Device registry property codes
		/// </summary>
		public enum SPDRP : int
		{
			/// <summary>
			/// DeviceDesc (R/W)
			/// </summary>
			SPDRP_DEVICEDESC = 0x00000000,

			/// <summary>
			/// HardwareID (R/W)
			/// </summary>
			SPDRP_HARDWAREID = 0x00000001,

			/// <summary>
			/// CompatibleIDs (R/W)
			/// </summary>
			SPDRP_COMPATIBLEIDS = 0x00000002,

			/// <summary>
			/// unused
			/// </summary>
			SPDRP_UNUSED0 = 0x00000003,

			/// <summary>
			/// Service (R/W)
			/// </summary>
			SPDRP_SERVICE = 0x00000004,

			/// <summary>
			/// unused
			/// </summary>
			SPDRP_UNUSED1 = 0x00000005,

			/// <summary>
			/// unused
			/// </summary>
			SPDRP_UNUSED2 = 0x00000006,

			/// <summary>
			/// Class (R--tied to ClassGUID)
			/// </summary>
			SPDRP_CLASS = 0x00000007,

			/// <summary>
			/// ClassGUID (R/W)
			/// </summary>
			SPDRP_CLASSGUID = 0x00000008,

			/// <summary>
			/// Driver (R/W)
			/// </summary>
			SPDRP_DRIVER = 0x00000009,

			/// <summary>
			/// ConfigFlags (R/W)
			/// </summary>
			SPDRP_CONFIGFLAGS = 0x0000000A,

			/// <summary>
			/// Mfg (R/W)
			/// </summary>
			SPDRP_MFG = 0x0000000B,

			/// <summary>
			/// FriendlyName (R/W)
			/// </summary>
			SPDRP_FRIENDLYNAME = 0x0000000C,

			/// <summary>
			/// LocationInformation (R/W)
			/// </summary>
			SPDRP_LOCATION_INFORMATION = 0x0000000D,

			/// <summary>
			/// PhysicalDeviceObjectName (R)
			/// </summary>
			SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E,

			/// <summary>
			/// Capabilities (R)
			/// </summary>
			SPDRP_CAPABILITIES = 0x0000000F,

			/// <summary>
			/// UiNumber (R)
			/// </summary>
			SPDRP_UI_NUMBER = 0x00000010,

			/// <summary>
			/// UpperFilters (R/W)
			/// </summary>
			SPDRP_UPPERFILTERS = 0x00000011,

			/// <summary>
			/// LowerFilters (R/W)
			/// </summary>
			SPDRP_LOWERFILTERS = 0x00000012,

			/// <summary>
			/// BusTypeGUID (R)
			/// </summary>
			SPDRP_BUSTYPEGUID = 0x00000013,

			/// <summary>
			/// LegacyBusType (R)
			/// </summary>
			SPDRP_LEGACYBUSTYPE = 0x00000014,

			/// <summary>
			/// BusNumber (R)
			/// </summary>
			SPDRP_BUSNUMBER = 0x00000015,

			/// <summary>
			/// Enumerator Name (R)
			/// </summary>
			SPDRP_ENUMERATOR_NAME = 0x00000016,

			/// <summary>
			/// Security (R/W, binary form)
			/// </summary>
			SPDRP_SECURITY = 0x00000017,

			/// <summary>
			/// Security (W, SDS form)
			/// </summary>
			SPDRP_SECURITY_SDS = 0x00000018,

			/// <summary>
			/// Device Type (R/W)
			/// </summary>
			SPDRP_DEVTYPE = 0x00000019,

			/// <summary>
			/// Device is exclusive-access (R/W)
			/// </summary>
			SPDRP_EXCLUSIVE = 0x0000001A,

			/// <summary>
			/// Device Characteristics (R/W)
			/// </summary>
			SPDRP_CHARACTERISTICS = 0x0000001B,

			/// <summary>
			/// Device Address (R)
			/// </summary>
			SPDRP_ADDRESS = 0x0000001C,

			/// <summary>
			/// UiNumberDescFormat (R/W)
			/// </summary>
			SPDRP_UI_NUMBER_DESC_FORMAT = 0X0000001D,

			/// <summary>
			/// Device Power Data (R)
			/// </summary>
			SPDRP_DEVICE_POWER_DATA = 0x0000001E,

			/// <summary>
			/// Removal Policy (R)
			/// </summary>
			SPDRP_REMOVAL_POLICY = 0x0000001F,

			/// <summary>
			/// Hardware Removal Policy (R)
			/// </summary>
			SPDRP_REMOVAL_POLICY_HW_DEFAULT = 0x00000020,

			/// <summary>
			/// Removal Policy Override (RW)
			/// </summary>
			SPDRP_REMOVAL_POLICY_OVERRIDE = 0x00000021,

			/// <summary>
			/// Device Install State (R)
			/// </summary>
			SPDRP_INSTALL_STATE = 0x00000022,

			/// <summary>
			/// Device Location Paths (R)
			/// </summary>
			SPDRP_LOCATION_PATHS = 0x00000023,
		}

		#endregion

		#region // Structures /////////////////////////////////////////////////

		//pack=8 for 64 bit.
		[StructLayout( LayoutKind.Sequential, Pack = 1 )]
		public struct SP_DEVINFO_DATA
		{
			public UInt32 cbSize;
			public Guid ClassGuid;
			public UInt32 DevInst;
			public IntPtr Reserved;
		}

		[StructLayout( LayoutKind.Sequential, Pack = 1 )]
		public struct SP_DEVICE_INTERFACE_DATA
		{
			public UInt32 cbSize;
			public Guid interfaceClassGuid;
			public UInt32 flags;
			private IntPtr reserved;
		}

		[StructLayout( LayoutKind.Sequential, Pack = 1 )]
		public struct SP_DEVICE_INTERFACE_DETAIL_DATA
		{
			public UInt32 cbSize;
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 256 )]
			public string devicePath;
		}

		[StructLayout( LayoutKind.Explicit )]
		private struct DevBroadcastDeviceInterfaceBuffer
		{
			public DevBroadcastDeviceInterfaceBuffer( Int32 deviceType )
			{
				dbch_size = Marshal.SizeOf( typeof( DevBroadcastDeviceInterfaceBuffer ) );
				dbch_devicetype = deviceType;
				dbch_reserved = 0;
			}

			[FieldOffset( 0 )]
			public Int32 dbch_size;
			[FieldOffset( 4 )]
			public Int32 dbch_devicetype;
			[FieldOffset( 8 )]
			public Int32 dbch_reserved;
		}

		//Structure with information for RegisterDeviceNotification.
		//DEV_BROADCAST_HDR Structure
		/*typedef struct _DEV_BROADCAST_HDR {
		  DWORD dbch_size;
		  DWORD dbch_devicetype;
		  DWORD dbch_reserved;
		}DEV_BROADCAST_HDR, *PDEV_BROADCAST_HDR;*/
		[StructLayout( LayoutKind.Sequential )]
		public struct DEV_BROADCAST_HDR
		{
			public int dbcc_size;
			public int dbcc_devicetype;
			public int dbcc_reserved;
		}

		//DEV_BROADCAST_HANDLE Structure
		/*typedef struct _DEV_BROADCAST_HANDLE {
		  DWORD      dbch_size;
		  DWORD      dbch_devicetype;
		  DWORD      dbch_reserved;
		  HANDLE     dbch_handle;
		  HDEVNOTIFY dbch_hdevnotify;
		  GUID       dbch_eventguid;
		  LONG       dbch_nameoffset;
		  BYTE       dbch_data[1];
		}DEV_BROADCAST_HANDLE *PDEV_BROADCAST_HANDLE;*/
		[StructLayout( LayoutKind.Sequential )]
		public struct DEV_BROADCAST_HANDLE
		{
			public Int32 dbch_size;
			public Int32 dbch_devicetype;
			public Int32 dbch_reserved;
			public IntPtr dbch_handle;
			public IntPtr dbch_hdevnotify;
			public Guid dbch_eventguid;
			public long dbch_nameoffset;
			public byte dbch_data;
			public byte dbch_data1;
		}

		//DEV_BROADCAST_DEVICEINTERFACE Structure
		/*typedef struct _DEV_BROADCAST_DEVICEINTERFACE {
		  DWORD dbcc_size;
		  DWORD dbcc_devicetype;
		  DWORD dbcc_reserved;
		  GUID  dbcc_classguid;
		  TCHAR dbcc_name[1];
		}DEV_BROADCAST_DEVICEINTERFACE *PDEV_BROADCAST_DEVICEINTERFACE;*/
		[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode )]
		public struct DEV_BROADCAST_DEVICEINTERFACE
		{
			public Int32 dbcc_size;
			public Int32 dbcc_devicetype;
			public Int32 dbcc_reserved;
			[MarshalAs( UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16 )]
			public byte[] dbcc_classguid;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst = 128 )]
			public char[] dbcc_name;
		}

		//DEV_BROADCAST_VOLUME Structure
		/*typedef struct _DEV_BROADCAST_VOLUME {
		  DWORD dbcv_size;
		  DWORD dbcv_devicetype;
		  DWORD dbcv_reserved;
		  DWORD dbcv_unitmask;
		  WORD  dbcv_flags;
		}DEV_BROADCAST_VOLUME, *PDEV_BROADCAST_VOLUME;*/
		[StructLayout( LayoutKind.Sequential )]
		public struct DEV_BROADCAST_VOLUME
		{
			public Int32 dbcv_size;
			public Int32 dbcv_devicetype;
			public Int32 dbcv_reserved;
			public Int32 dbcv_unitmask;
			public Int16 dbcv_flags;
		}

		//DEV_BROADCAST_PORT Structure
		/*typedef struct _DEV_BROADCAST_PORT {
		  DWORD dbcp_size;
		  DWORD dbcp_devicetype;
		  DWORD dbcp_reserved;
		  TCHAR dbcp_name[1];
		}DEV_BROADCAST_PORT *PDEV_BROADCAST_PORT;*/
		[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode )]
		public struct DEV_BROADCAST_PORT
		{
			public Int32 dbcp_size;
			public Int32 dbcp_devicetype;
			public Int32 dbcp_reserved;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst = 128 )]
			public char[] dbcp_name;
		}

		//DEV_BROADCAST_OEM Structure
		/*typedef struct _DEV_BROADCAST_OEM {
		  DWORD dbco_size;
		  DWORD dbco_devicetype;
		  DWORD dbco_reserved;
		  DWORD dbco_identifier;
		  DWORD dbco_suppfunc;
		}DEV_BROADCAST_OEM, *PDEV_BROADCAST_OEM;*/
		[StructLayout( LayoutKind.Sequential )]
		public struct DEV_BROADCAST_OEM
		{
			public Int32 dbco_size;
			public Int32 dbco_devicetype;
			public Int32 dbco_reserved;
			public Int32 dbco_identifier;
			public Int32 dbco_suppfunc;
		}

		#endregion

		#region // USER32 Functions ///////////////////////////////////////////

		[DllImport( "user32.dll", SetLastError = true )]
		public static extern IntPtr RegisterDeviceNotification(
			IntPtr hRecipient,
			IntPtr NotificationFilter,
			Int32 Flags );

		[DllImport( "user32.dll", SetLastError = true )]
		public static extern bool UnregisterDeviceNotification( IntPtr hHandle );

		#endregion

		#region // Registry Functions /////////////////////////////////////////

		/// <summary>
		/// Retrieves the type and data for the specified value name associated 
		/// with an open registry key.
		/// </summary>
		/// <param name="hKey">A handle to an open registry key. 
		/// The key must have been opened with the KEY_QUERY_VALUE access right.</param>
		/// <param name="lpValueName">The name of the registry value.
		/// If lpValueName is NULL or an empty string, "", the function retrieves 
		/// the type and data for the key's unnamed or default value, if any.
		/// If lpValueName specifies a key that is not in the registry, the function 
		/// returns ERROR_FILE_NOT_FOUND.</param>
		/// <param name="lpReserved">This parameter is reserved 
		/// and must be NULL.</param>
		/// <param name="lpType">A pointer to a variable that receives 
		/// a code indicating the type of data stored in the specified 
		/// value. The lpType parameter can be NULL if the type code is 
		/// not required.</param>
		/// <param name="lpData">A pointer to a buffer that receives 
		/// the value's data. This parameter can be NULL if the data 
		/// is not required.</param>
		/// <param name="lpcbData">A pointer to a variable that specifies 
		/// the size of the buffer pointed to by the lpData parameter, 
		/// in bytes. When the function returns, this variable contains 
		/// the size of the data copied to lpData.
		/// The lpcbData parameter can be NULL only if lpData is NULL.
		/// If the data has the REG_SZ, REG_MULTI_SZ or REG_EXPAND_SZ 
		/// type, this size includes any terminating null character or 
		/// characters unless the data was stored without them. 
		/// For more information, see Remarks.
		/// If the buffer specified by lpData parameter is not large 
		/// enough to hold the data, the function returns ERROR_MORE_
		/// DATA and stores the required buffer size in the variable 
		/// pointed to by lpcbData. In this case, the contents of the 
		/// lpData buffer are undefined.
		/// If lpData is NULL, and lpcbData is non-NULL, the function
		/// returns ERROR_SUCCESS and stores the size of the data, in 
		/// bytes, in the variable pointed to by lpcbData. This enables 
		/// an application to determine the best way to allocate a 
		/// buffer for the value's data.If hKey specifies 
		/// HKEY_PERFORMANCE_DATA and the lpData buffer is not large 
		/// enough to contain all of the returned data, RegQueryValueEx 
		/// returns ERROR_MORE_DATA and the value returned through 
		/// the lpcbData parameter is undefined. This is because the 
		/// size of the performance data can change from one call to 
		/// the next. In this case, you must increase the buffer size 
		/// and call RegQueryValueEx again passing the updated buffer 
		/// size in the lpcbData parameter. Repeat this until the 
		/// function succeeds. You need to maintain a separate 
		/// variable to keep track of the buffer size, because the 
		/// value returned by lpcbData is unpredictable.
		/// If the lpValueName registry value does not exist, 
		/// RegQueryValueEx returns ERROR_FILE_NOT_FOUND and the 
		/// value returned through the lpcbData parameter is 
		/// undefined.</param>
		/// <returns>If the function succeeds, the return value is 
		/// ERROR_SUCCESS.
		/// If the function fails, the return value is a system error code.
		/// If the lpData buffer is too small to receive the data, the 
		/// function returns ERROR_MORE_DATA.
		/// If the lpValueName registry value does not exist, the function 
		/// returns ERROR_FILE_NOT_FOUND.</returns>
		[DllImport( "advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW", SetLastError = true )]
		public static extern int RegQueryValueEx(
			IntPtr hKey,
			string lpValueName,
			UInt32 lpReserved,
			out UInt32 lpType,
			System.Text.StringBuilder lpData,
			ref UInt32 lpcbData );

		/// <summary>
		/// Closes a handle to the specified registry key.
		/// </summary>
		/// <param name="hKey">A handle to the open key to be closed.</param>
		/// <returns>If the function succeeds, the return value is ERROR_SUCCESS.
		/// If the function fails, the return value is a nonzero error code 
		/// defined in Winerror.h.</returns>
		[DllImport( "advapi32.dll", SetLastError = true )]
		public static extern int RegCloseKey( IntPtr hKey );

		#endregion

		#region // SetupDi Functions //////////////////////////////////////////

		/// <summary>
		/// The SetupDiEnumDeviceInfo function retrieves a context structure for 
		/// a device information element of the specified
		/// device information set. Each call returns information about one device. 
		/// The function can be called repeatedly
		/// to get information about several devices.
		/// </summary>
		/// <param name="DeviceInfoSet">A handle to the device information set for 
		/// which to return an SP_DEVINFO_DATA structure that represents a device 
		/// information element.</param>
		/// <param name="MemberIndex">A zero-based index of the device information 
		/// element to retrieve.</param>
		/// <param name="DeviceInfoData">A pointer to an SP_DEVINFO_DATA structure 
		/// to receive information about an enumerated device information element. 
		/// The caller must set DeviceInfoData.cbSize to sizeof(SP_DEVINFO_DATA).</param>
		/// <returns></returns>
		[DllImport( "setupapi.dll", SetLastError = true )]
		public static extern bool SetupDiEnumDeviceInfo(
			IntPtr DeviceInfoSet,
			UInt32 MemberIndex,
			ref SP_DEVINFO_DATA DeviceInfoData );

		/// <summary>
		/// A call to SetupDiEnumDeviceInterfaces retrieves a pointer to a structure 
		/// that identifies a specific device interface
		/// in the previously retrieved DeviceInfoSet array. The call specifies 
		/// a device interface by passing an array index.
		/// To retrieve information about all of the device interfaces, an 
		/// application can loop through the array,
		/// incrementing the array index until the function returns zero, 
		/// indicating that there are no more interfaces.
		/// The GetLastError API function then returns No more data is available.
		/// </summary>
		/// <param name="hDevInfo">Input: Give it the HDEVINFO we got from 
		/// SetupDiGetClassDevs()</param>
		/// <param name="devInfo">Input (optional)</param>
		/// <param name="interfaceClassGuid">Input</param>
		/// <param name="memberIndex">Input: "Index" of the device you are 
		/// interested in getting the path for.</param>
		/// <param name="deviceInterfaceData">Output: This function fills 
		/// in an "SP_DEVICE_INTERFACE_DATA" structure.</param>
		/// <returns></returns>
		[DllImport( @"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern Boolean SetupDiEnumDeviceInterfaces(
		   IntPtr hDevInfo,
		   IntPtr devInfo,
		   ref Guid interfaceClassGuid, //ref
		   UInt32 memberIndex,
		   ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData );

		/// <summary>
		/// Gives us a device path, which is needed before CreateFile() 
		/// can be used.
		/// </summary>
		/// <param name="hDevInfo">Input: Wants HDEVINFO which can be 
		/// obtained from SetupDiGetClassDevs()</param>
		/// <param name="deviceInterfaceData">Input: Pointer to a structure 
		/// which defines the device interface.</param>
		/// <param name="deviceInterfaceDetailData">Output: Pointer to a 
		/// structure, which will contain the device path.</param>
		/// <param name="deviceInterfaceDetailDataSize">
		/// Input: Number of bytes to retrieve.</param>
		/// <param name="requiredSize">Output (optional): 
		/// The number of bytes needed to hold the entire struct</param>
		/// <param name="deviceInfoData">Output</param>
		/// <returns></returns>
		[DllImport( @"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern Boolean SetupDiGetDeviceInterfaceDetail(
		   IntPtr hDevInfo,
		   ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, //ref
		   ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
		   UInt32 deviceInterfaceDetailDataSize,
		   out UInt32 requiredSize,
		   ref SP_DEVINFO_DATA deviceInfoData );

		/// <summary>
		/// Frees up memory by destroying a DeviceInfoList
		/// </summary>
		/// <param name="hDevInfo">Input: 
		/// Give it a handle to a device info list to deallocate from RAM.
		/// </param>
		/// <returns></returns>
		[DllImport( @"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern bool SetupDiDestroyDeviceInfoList( IntPtr hDevInfo );

		/// <summary>
		/// Returns a HDEVINFO type for a device information set.
		/// We will need the HDEVINFO as in input parameter for 
		/// calling many of the other SetupDixxx() functions.
		/// </summary>
		/// <param name="ClassGuid"></param>
		/// <param name="Enumerator"></param>
		/// <param name="hwndParent"></param>
		/// <param name="Flags"></param>
		/// <returns></returns>

		// 1st form using a ClassGUID
		[DllImport( "setupapi.dll", CharSet = CharSet.Auto )]
		public static extern IntPtr SetupDiGetClassDevs(
		   ref Guid ClassGuid, //ref
		   IntPtr Enumerator,
		   IntPtr hwndParent,
		   UInt32 Flags );

		// 2nd form uses an Enumerator
		[DllImport( "setupapi.dll", CharSet = CharSet.Auto )]
		public static extern IntPtr SetupDiGetClassDevs(
		   IntPtr ClassGuid,
		   string Enumerator,
		   IntPtr hwndParent,
		   int Flags );

		/// <summary>
		/// The SetupDiGetDeviceRegistryProperty function retrieves 
		/// the specified device property.
		/// This handle is typically returned by the SetupDiGetClassDevs 
		/// or SetupDiGetClassDevsEx function.
		/// </summary>
		/// <param Name="DeviceInfoSet">Handle to the device information set 
		/// that contains the interface and its underlying device.</param>
		/// <param Name="DeviceInfoData">Pointer to an SP_DEVINFO_DATA structure 
		/// that defines the device instance.</param>
		/// <param Name="Property">Device property to be retrieved. SEE MSDN</param>
		/// <param Name="PropertyRegDataType">Pointer to a variable that receives 
		/// the registry data Type. This parameter can be NULL.</param>
		/// <param Name="PropertyBuffer">Pointer to a buffer that receives the 
		/// requested device property.</param>
		/// <param Name="PropertyBufferSize">Size of the buffer, in bytes.</param>
		/// <param Name="RequiredSize">Pointer to a variable that receives the 
		/// required buffer size, in bytes. This parameter can be NULL.</param>
		/// <returns>If the function succeeds, the return value is nonzero.</returns>
		[DllImport( "setupapi.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern bool SetupDiGetDeviceRegistryProperty(
			IntPtr DeviceInfoSet,
			ref SP_DEVINFO_DATA DeviceInfoData, //ref
			UInt32 Property,
			ref UInt32 PropertyRegDataType,
			IntPtr PropertyBuffer,
			UInt32 PropertyBufferSize,
			ref UInt32 RequiredSize );

		/// <summary>
		/// The CM_Get_Parent function obtains a device instance handle to the 
		/// parent node of a specified device node, in the local machine's device tree.
		/// </summary>
		/// <param name="pdnDevInst">Caller-supplied pointer to the device 
		/// instance handle to the parent node that this function retrieves. 
		/// The retrieved handle is bound to the local machine.</param>
		/// <param name="dnDevInst">Caller-supplied device instance handle 
		/// that is bound to the local machine.</param>
		/// <param name="ulFlags">Not used, must be zero.</param>
		/// <returns>If the operation succeeds, the function returns CR_SUCCESS. 
		/// Otherwise, it returns one of the CR_-prefixed error codes defined 
		/// in cfgmgr32.h.</returns>
		[DllImport( "setupapi.dll" )]
		public static extern int CM_Get_Parent(
		   out UInt32 pdnDevInst,
		   UInt32 dnDevInst,
		   int ulFlags );

		/// <summary>
		/// The CM_Get_Device_ID function retrieves the device instance ID 
		/// for a specified device instance, on the local machine.
		/// </summary>
		/// <param name="dnDevInst">Caller-supplied device instance handle 
		/// that is bound to the local machine.</param>
		/// <param name="Buffer">Address of a buffer to receive a device 
		/// instance ID string. The required buffer size can be obtained 
		/// by calling CM_Get_Device_ID_Size, then incrementing the received 
		/// value to allow room for the string's terminating NULL.</param>
		/// <param name="BufferLen">Caller-supplied length, in characters, 
		/// of the buffer specified by Buffer.</param>
		/// <param name="ulFlags">Not used, must be zero.</param>
		/// <returns>If the operation succeeds, the function returns 
		/// CR_SUCCESS. Otherwise, it returns one of the CR_-prefixed 
		/// error codes defined in cfgmgr32.h.</returns>
		[DllImport( "setupapi.dll", CharSet = CharSet.Auto )]
		public static extern int CM_Get_Device_ID(
		   UInt32 dnDevInst,
		   IntPtr Buffer,
		   int BufferLen,
		   int ulFlags );

		/// <summary>
		/// The SetupDiOpenDevRegKey function opens a registry key for 
		/// device-specific configuration information.
		/// </summary>
		/// <param name="hDeviceInfoSet">A handle to the device information 
		/// set that contains a device information element that represents 
		/// the device for which to open a registry key.</param>
		/// <param name="DeviceInfoData">A pointer to an SP_DEVINFO_DATA 
		/// structure that specifies the device information element in
		/// DeviceInfoSet.</param>
		/// <param name="Scope">The scope of the registry key to open. 
		/// The scope determines where the information is stored. 
		/// The scope can be global or specific to a hardware profile. 
		/// The scope is specified by one of the following values:
		/// DICS_FLAG_GLOBAL Open a key to store global configuration information. 
		/// This information is not specific to a particular hardware profile. 
		/// For NT-based operating systems this opens a key that is rooted at 
		/// HKEY_LOCAL_MACHINE. The exact key opened depends on the value of the 
		/// KeyType parameter.
		/// DICS_FLAG_CONFIGSPECIFIC Open a key to store hardware profile-specific 
		/// configuration information. This key is rooted at one of the
		/// hardware-profile specific branches, instead of HKEY_LOCAL_MACHINE. 
		/// The exact key opened depends on the value of the KeyType parameter.</param>
		/// <param name="HwProfile">A hardware profile value, which is set as follows:
		/// If Scope is set to DICS_FLAG_CONFIGSPECIFIC, HwProfile specifies the 
		/// hardware profile of the key that is to be opened.
		/// If HwProfile is 0, the key for the current hardware profile is opened.
		/// If Scope is DICS_FLAG_GLOBAL, HwProfile is ignored.</param>
		/// <param name="KeyType">The type of registry storage key to open, 
		/// which can be one of the following values:
		/// DIREG_DEV Open a hardware key for the device.
		/// DIREG_DRV Open a software key for the device.
		/// For more information about a device's hardware and software keys, 
		/// see Driver Information in the Registry.</param>
		/// <param name="samDesired">The registry security access that is 
		/// required for the requested key. For information about registry 
		/// security access values of type REGSAM, see the Microsoft Windows 
		/// SDK documentation.</param>
		/// <returns>If the function is successful, it returns a handle to 
		/// an opened registry key where private configuration data pertaining 
		/// to this device instance can be stored/retrieved.
		/// If the function fails, it returns INVALID_HANDLE_VALUE. 
		/// To get extended error information, call GetLastError.</returns>
		[DllImport( "Setupapi", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern IntPtr SetupDiOpenDevRegKey(
			IntPtr hDeviceInfoSet,
			ref SP_DEVINFO_DATA DeviceInfoData,
			UInt32 Scope,
			UInt32 HwProfile,
			UInt32 KeyType,
			UInt32 samDesired );

		[DllImport( "setupapi.dll", SetLastError = true )]
		static extern bool SetupDiClassGuidsFromName(
			string ClassName,
			ref Guid ClassGuidArray1stItem,
			UInt32 ClassGuidArraySize,
			out UInt32 RequiredSize );

		public static Guid[] GetClassGUIDs( string className )
		{
			UInt32 requiredSize = 0;
			Guid[] guidArray = new Guid[ 1 ];

			bool status
				= SetupDiClassGuidsFromName(
					className,
					ref guidArray[ 0 ],
					1,
					out requiredSize );

			if( true == status )
			{
				if( 1 < requiredSize )
				{
					guidArray = new Guid[ requiredSize ];
					SetupDiClassGuidsFromName(
						className,
						ref guidArray[ 0 ],
						requiredSize,
						out requiredSize );
				}
			}
			else
				throw new System.ComponentModel.Win32Exception();

			return guidArray;
		}

		#endregion

		#region // GetComPortInfo /////////////////////////////////////////////

		const Int64 INVALID_HANDLE_VALUE = -1;
		const int BUFFER_SIZE = 1024;

		// newer, all "COM" ports

		static public List<ComPortInfo> GetComPortInfo()
		{
			const string ClassName = "Ports";
			return ComPortsFromClassName( ClassName );
		}

		static public List<ComPortInfo> ComPortsFromClassName( string className )
		{
			List<ComPortInfo> Ports = new List<ComPortInfo>();
			Guid[] guids = Win32Wrapper.GetClassGUIDs( className );

			foreach( Guid guid in guids )
			{
				var ports = ComPortsFromGuid( guid );
				Ports.AddRange( ports );
			}

			return Ports;
		}

		static public List<ComPortInfo> ComPortsFromGuid( Guid guid )
		{
			List<ComPortInfo> Ports = new List<ComPortInfo>();

			IntPtr Handle
				= Win32Wrapper.SetupDiGetClassDevs(
					ref guid,
					IntPtr.Zero,
					IntPtr.Zero,
					(int)( Win32Wrapper.DIGCF.DIGCF_PRESENT | Win32Wrapper.DIGCF.DIGCF_PROFILE ) );

			if( Handle.ToInt32() != INVALID_HANDLE_VALUE )
				return GetDeviceProperties( Handle );

			return null;
		}

		public static List<ComPortInfo> GetDeviceProperties( IntPtr Handle )
		{
			List<ComPortInfo> results = new List<ComPortInfo>();
			IntPtr IntPtrBuffer = Marshal.AllocHGlobal( BUFFER_SIZE );
			Win32Wrapper.WinErrors LastError;
			bool Success = true;

			try
			{
				for( uint i = 0; Success; i++ )
				{
					UInt32 RequiredSize = 0;
					UInt32 RegType = 0;
					IntPtr Ptr = IntPtr.Zero;

					//Create a Device Info Data structure
					Win32Wrapper.SP_DEVINFO_DATA DevInfoData
						= new Win32Wrapper.SP_DEVINFO_DATA();
					DevInfoData.cbSize
						= (uint)Marshal.SizeOf( DevInfoData );
					Success = Win32Wrapper.SetupDiEnumDeviceInfo(
						Handle,
						i,
						ref DevInfoData );

					if( !Success )
					{
						LastError = (Win32Wrapper.WinErrors)Marshal.GetLastWin32Error();
						break;
					}

					// Get the required buffer size
					// First query for the size of the hardware ID, 
					// so we can know how big a buffer to allocate for the data.
					// then fetch the actual data

					Win32Wrapper.SetupDiGetDeviceRegistryProperty(
						Handle,
						ref DevInfoData,
						(UInt32)Win32Wrapper.SPDRP.SPDRP_HARDWAREID,
						ref RegType,
						IntPtr.Zero,
						0,
						ref RequiredSize );

					LastError = (Win32Wrapper.WinErrors)Marshal.GetLastWin32Error();

					if( LastError != Win32Wrapper.WinErrors.ERROR_INSUFFICIENT_BUFFER
					|| RequiredSize > BUFFER_SIZE
					|| !Win32Wrapper.SetupDiGetDeviceRegistryProperty(
							Handle,
							ref DevInfoData,
							(UInt32)Win32Wrapper.SPDRP.SPDRP_HARDWAREID,
							ref RegType,
							IntPtrBuffer,
							BUFFER_SIZE,
							ref RequiredSize ) )
					{
						break;
					}

					string HardwareID
						= Marshal.PtrToStringAuto( IntPtrBuffer )
							.ToUpperInvariant();

#if false
					// filter out entries that don't match expectations
					if( !String.IsNullOrEmpty( ExpectedDeviceID )
					&&	!HardwareID.Contains( ExpectedDeviceID ) )
							continue;
#endif

					ComPortInfo DP = new ComPortInfo();
					DP.HardwareID = HardwareID;

					if( Win32Wrapper.SetupDiGetDeviceRegistryProperty(
						Handle,
						ref DevInfoData,
						(UInt32)Win32Wrapper.SPDRP.SPDRP_FRIENDLYNAME,
						ref RegType,
						IntPtrBuffer,
						BUFFER_SIZE,
						ref RequiredSize ) )
					{
						DP.FriendlyName = Marshal.PtrToStringAuto( IntPtrBuffer );
					}

					if( Win32Wrapper.SetupDiGetDeviceRegistryProperty(
						Handle,
						ref DevInfoData,
						(UInt32)Win32Wrapper.SPDRP.SPDRP_LOCATION_PATHS,
						ref RegType,
						IntPtrBuffer,
						BUFFER_SIZE,
						ref RequiredSize ) )
					{
						DP.DevicePath
							= Marshal.PtrToStringAuto( IntPtrBuffer )
								.ToUpperInvariant();
					}

					// Fetch COMM Port, if any

					DP.PortName = "";

					//Open the Device Parameters Key of the device
					IntPtr hDeviceRegistryKey;

					hDeviceRegistryKey
						= Win32Wrapper.SetupDiOpenDevRegKey(
							Handle,
							ref DevInfoData,
							(UInt32)Win32Wrapper.DICS_FLAG.DICS_FLAG_GLOBAL,
							0,
							(UInt32)Win32Wrapper.DIREG.DIREG_DEV,
							(UInt32)Win32Wrapper.REGKEYSECURITY.KEY_READ );

					if( hDeviceRegistryKey.ToInt32() != INVALID_HANDLE_VALUE )
					{
						UInt32 Type = 0;
						System.Text.StringBuilder Data
							= new System.Text.StringBuilder( BUFFER_SIZE );
						UInt32 Size = (UInt32)Data.Capacity;
						int Result;

						Result = Win32Wrapper.RegQueryValueEx(
							hDeviceRegistryKey,
							"PortName",
							0,
							out Type,
							Data,
							ref Size );

						if( Result == (int)Win32Wrapper.WinErrors.ERROR_SUCCESS )
						{
							DP.PortName = Data.ToString().ToUpperInvariant();
						}

						//Close Registry
						Win32Wrapper.RegCloseKey( hDeviceRegistryKey );
					}

					// Final qualifications

					if( string.IsNullOrEmpty( DP.PortName ) // no port name
					|| !DP.PortName.StartsWith( "COM" ) )	// exclude LPT et al.
						continue;	// skip this one

					results.Add( DP );

				} // End of for(i=0; Success; i++)
			}
			finally
			{
				//Clean up the old structure we no longer need.
				Win32Wrapper.SetupDiDestroyDeviceInfoList( Handle );
				Marshal.FreeHGlobal( IntPtrBuffer );
			}

			return results;
		}

		#endregion

		#region // GetUsbComPortProperties ////////////////////////////////////

		public static ComPortInfo GetUsbComPortProperties( string portName )
		{
			return
				GetUsbComPortProperties()
				.FirstOrDefault<ComPortInfo>( p => p.PortName == portName );
		}

		// original

		public static List<ComPortInfo> GetUsbComPortProperties()
		{
			string DevEnum = "USB";
			IntPtr h = Win32Wrapper.SetupDiGetClassDevs(
				IntPtr.Zero,
				DevEnum,
				IntPtr.Zero,
				(int)( Win32Wrapper.DIGCF.DIGCF_PRESENT
					| Win32Wrapper.DIGCF.DIGCF_ALLCLASSES ) );

			if( h.ToInt32() == INVALID_HANDLE_VALUE )
				return null;

			return GetDeviceProperties( h );
		}

		#endregion
	}
}
