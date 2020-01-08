using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedLibrary;
using Library;

namespace UnitTesting
{
	[TestClass]
	public class PrefaceLinesWithTimestampTesting
	{
		#region // Helpers ////////////////////////////////////////////////////

		public string Test( string text, [CallerMemberName] string name=null )
		{
			Trace( $"Testing {name}: {text?.Visible()}" );
			string result = Extensions.PrefaceLinesWithTimestamp( text );
			Trace( $"Result  -> {result?.Visible()}" );
			return result;
		}

		#endregion

		#region // Tests //////////////////////////////////////////////////////

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void TestNull()
		{
			string r = Test( null );
		}

		[TestMethod]
		public void TestEmpty()
		{
			string r = Test( "" );
		}

		[TestMethod]
		public void TestWhitespaceOnly()
		{
			string r1 = Test( "            " );
			string r2 = Test( "\t\t\t\t\t\r\r" );
			string r3 = Test( ".\t.\t.\t.\t.\t" );
		}

		[TestMethod]
		public void TestNoNewline()
		{
			string r1 = Test( "S" );
			r1 = Test( "S\r" );
		}

		[TestMethod]
		public void TestOneLine()
		{
			string r1 = Test( "Starting...\r\n" );
		}

		[TestMethod]
		public void TestEmbeddedNewline()
		{
			string r1 = Test( "Starting\r\n..." );
		}

		[TestMethod]
		public void TestMultipleLinesComplete()
		{
			string r1 = Test( "One\r\nTwo\r\nThree\r\nFour\r\nFive\r\n" );
		}

		[TestMethod]
		public void TestMultipleLinesPartial()
		{
			string r1 = Test( "One\r\nTwo\r\nThree\r\nFour\r\nFive\r" );
		}

		[TestMethod]
		public void TestMultipleLines3()
		{
		}

		[TestMethod]
		public void TestMultipleLines4()
		{
		}

		#endregion

		#region // Trace Output ///////////////////////////////////////////////

		public void Trace( string message )
		{
			Debug.WriteLine( message );
		}

		#endregion
	}
}
