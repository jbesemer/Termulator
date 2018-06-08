using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

using SharedLibrary;

namespace Library
{
	public class PortManager
		: IEnumerable
		, IEnumerable<ComPortInfo>
	{
		#region // Properties and Accessors ///////////////////////////////////

		// Properties 

		public static string SelectedPortName { get; set; }

		protected List<ComPortInfo> AllPorts;
		protected Dictionary<string, ComPortInfo> IndexByPortName;
		protected Dictionary<string, ComPortInfo> IndexByFriendlyName;

		// Accessors

		public int Count { get { return AllPorts.Count; } }

		public ComPortInfo this[ int index ]
		{
			get
			{
				try { return AllPorts[ index ]; }
				catch( ArgumentOutOfRangeException ) { return null; }
			}
		}

		public ComPortInfo this[ string portName ]
		{
			get
			{
				try { return IndexByPortName[ portName ]; }
				catch( KeyNotFoundException ) { return null; }
			}
		}

		public bool HasPortName( string portName )
		{
			return !string.IsNullOrEmpty( portName ) // .Contains throws on null
				&& IndexByPortName.Keys.Contains<string>( portName );
		}


		#endregion

		#region // IEnumerator<ComPortInfo> ///////////////////////////////////

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)GetEnumerator();
		}

		public IEnumerator<ComPortInfo> GetEnumerator()
		{
			return (IEnumerator<ComPortInfo>)AllPorts.GetEnumerator(); // IndexByFriendlyName.Keys.GetEnumerator();
		}

		#endregion

		// Constructor ////////////////////////////////////////////////

		public PortManager()
			: base()
		{
			Refresh();
		}
		
		#region // Actions ////////////////////////////////////////////////////

		public void Clear()
		{
			AllPorts = new List<ComPortInfo>();
			IndexByPortName = new Dictionary<string, ComPortInfo>( StringComparer.InvariantCultureIgnoreCase );
			IndexByFriendlyName = new Dictionary<string, ComPortInfo>( StringComparer.InvariantCultureIgnoreCase );
		}

		public void Refresh()
		{
			// update list and rebuild indicies
			Clear();
			AllPorts = Win32Wrapper.GetComPortInfo();

			foreach( var port in AllPorts )
				AddIndex( port );
		}

		// for unit testing...

		public void Add( ComPortInfo info )
		{
			AllPorts.Add( info );
		}

		public void Add( string portName )
		{
			ComPortInfo info = new ComPortInfo( portName );
			AllPorts.Add( info );
			AddIndex( info );
		}

		protected void AddIndex( ComPortInfo info )
		{
			IndexByFriendlyName[ info.FriendlyName ] = info;
			IndexByPortName[ info.PortName ] = info;
		}

		#endregion

		#region // FindQualifiedCandidate /////////////////////////////////////

		// finds the first qualified candidate, intentionally ignoring an optional 
		// port to be excluded.

		public ComPortInfo FindQualifiedCandidate( string excludePort = null )
		{
			foreach( var info in this )
				if( info.IsQualified && ( excludePort == null || info.PortName != excludePort ) )
					return info;

			return null;
		}

		public string FindQualifiedCandidateName( string excludePort = null )
		{
			var candidate = FindQualifiedCandidate( excludePort );

			if( candidate != null )
				return candidate.PortName;

			return null;
		}

		#endregion

		#region // Trace Output ///////////////////////////////////////////////

		public static void Trace( string format, params object[] args )
		{
			Debug.WriteLine( "PortManager: " + string.Format( format, args ) );
		}

		[Conditional( "TRACE_PORT_NAMES" )]
		public static void Trace_PortNames( string format, params object[] args )
		{
			Debug.WriteLine( "PortNames: " + string.Format( format, args ) );
		}

		#endregion
	}
}
