using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Library
{

	public static class CommandFavorites
	{
		private static List<string>Favorites = new List<string>();

		public static int Count { get { return Favorites.Count; } }

		public static string Item( int index ) { return Favorites[ index ]; }
		// public static string this[ int index ] { get { return History[ index ]; }}	// not legal: FML

		// add new item

		public static void Add( string command )
		{
			// if command was previously recorded then no need to add it again

			int index = Favorites.IndexOf( command );

			if( index < 0 )
			{
				Favorites.Add( command );
			}
		}

		// remove existing item

		public static void Remove( string command )
		{
			int index = Favorites.IndexOf( command );

			if( index >= 0 )
			{
				Favorites.RemoveAt( index );
			}
		}

		public static string[] FetchAll()
		{
			return Favorites.ToArray();
		}

		public static StringCollection SaveSettings()
		{
			var coll = new StringCollection();

			foreach( string item in Favorites )
				coll.Add( item );

			return coll;
		}

		public static void RestoreSettings( StringCollection all )
		{
			if( all != null && all.Count > 0 )
			{
				Favorites = all
					.ToStrings()
					.ToList<string>();
			}
			else
			{
				Favorites = new List<string>();
			}
		}
	}
}
