using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Terminal
{
	public static class CommandHistory
	{
		public static int MaxHistory = 100;

		private static List<string>History = new List<string>();

		public static int Count { get { return History.Count; } }

		// index of current command when moving back and forth

		private static int Index = 0;	

		public static string Item( int index ) { return History[ index ]; }
		// public static string this[ int index ] { get { return History[ index ]; }}	// not legal: FML

		// clear history 

		public static void Clear()
		{
			History = new List<string>();
			Index = 0;
		}

		// add new history item

		public static void Add( string command )
		{
			// if command was previously recorded then remove it from it's 
			// previous position before appending it to the end

			int index = History.IndexOf( command );

			if( index >= 0 )
				History.RemoveAt( index );

			// if limit has been reached (or exceeded) then discard oldest entry(ies)

			while( Count >= MaxHistory )
				History.RemoveAt( 0 );

			History.Add( command );
			Index = Count;	// index is one past last entry -- have to go back to recall it
		}

		// remove existing item

		public static void Remove( string command )
		{
			int index = History.IndexOf( command );

			if( index >= 0 )
			{
				History.RemoveAt( index );
			}
		}

		// scroll back and forwards in list

		public static void Back()
		{
			if( Index > 0 )
				Index--;
		}

		public static void Forward()
		{
			if( Index < Count )
				Index++;
		}

		// fetch the current item, if any

		public static string Fetch()
		{
			if( Index < Count )
				return History[ Index ];
			else
				return "";
		}

		public static string[] FetchAll()
		{
			return History.ToArray();
		}

		public static string FetchMostRecent()
		{
			if( Count > 0 )
				return History[ Count - 1 ];
			else
				return "";
		}

		public static StringCollection SaveSettings()
		{
			var coll = new StringCollection();

			foreach( string item in History )
				coll.Add( item );

			return coll;
		}

		public static void RestoreSettings( StringCollection all )
		{
			if( all != null && all.Count > 0 )
			{
				History = all
					.ToStrings()
					.ToList<string>();
				Index = Count;	// index is one past last entry -- have to go back to recall it
			}
			else
			{
				History = new List<string>();
			}
		}
	}
}
