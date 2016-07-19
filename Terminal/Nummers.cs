using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminal
{
	public class Nummer
	{
		public int Value;

		public Nummer( int value )
		{
			Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public static object[] FromInts( int[] nums )
		{
			object[] objs = new object[ nums.Length ];

			for( int i = 0; i < nums.Length; i++ )
				objs[ i ] = new Nummer( nums[ i ] );

			return objs;
		}
	}

	public class Nummers : Dictionary<int, Nummer>
	{
		public object[] Original;

		public Nummers( int[] nums )
		{
			Original = new object[ nums.Length ];

			for( int i = 0; i < nums.Length; i++ )
			{
				var nummer = new Nummer( nums[ i ]);
				this[ nummer.Value ] = nummer;
				Original[ i ] = (object)nummer;
			}
		}
	}
}
