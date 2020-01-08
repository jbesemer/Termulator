using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Text;
using System.Runtime.CompilerServices;

using SharedLibrary;
using Library;

namespace UnitTesting
{
	[TestClass]
	public class PrefaceLinesWithTimestampTesting
	{
		#region // Helpers ////////////////////////////////////////////////////

		public class PrefaceLinesWithDummyTimestamp : PrefaceLinesWithTimestamp
		{
			// replace timestamps with fixed string

			public override string Timestamp()
			{
				return "<timestamp> ";
			}

			public string Final
			{
				get
				{
					return IsContinuing ? "[Continue]" : "[Complete]";
				}
			}
		}

		public string Test( string text, [CallerMemberName] string name = null )
		{
			var plwt = new PrefaceLinesWithDummyTimestamp();
			Trace( $"\nTesting {name}: {text?.Visible()}" );
			string result = plwt.Convert( text );
			Trace( $"Result -> {plwt.Final} {result?.Visible()} " );
			return result;
		}

		public string Test( PrefaceLinesWithDummyTimestamp plwt, string text, [CallerMemberName] string name = null )
		{
			Trace( $"\nTesting {name}: {text?.Visible()}" );
			string result = plwt.Convert( text );
			Trace( $"Result -> {plwt.Final} {result?.Visible()}" );
			return result;
		}

		public void Verify( string text, string expected, bool continuing, [CallerMemberName] string name = null )
		{
			var plwt = new PrefaceLinesWithDummyTimestamp();
			Trace( $"\nVerify {name}: {text?.Visible()}" );
			string result = plwt.Convert( text );
			Trace( $"Result -> {plwt.Final} {result?.Visible()}" );
			Assert.AreEqual( expected, result );
			Assert.AreEqual( continuing, plwt.IsContinuing );
		}

		public void Verify( PrefaceLinesWithDummyTimestamp plwt, string text, string expected, bool continuing, [CallerMemberName] string name = null )
		{
			Trace( $"\nVerify {name}: {text?.Visible()}" );
			string result = plwt.Convert( text );
			Trace( $"Result -> {plwt.Final} {result?.Visible()}" );
			Assert.AreEqual( expected, result );
			Assert.AreEqual( continuing, plwt.IsContinuing );
		}

		#endregion

		#region // Tests //////////////////////////////////////////////////////

		[TestMethod]
		//[ExpectedException(typeof(NullReferenceException))]
		public void TestNull()
		{
			Verify( null, "", false );
		}

		[TestMethod]
		public void TestEmpty()
		{
			Verify( "", "", false );
		}

		[TestMethod]
		public void TestWhitespaceOnly()
		{
			Verify( "            ", "<timestamp>             ", true );
			Verify( "\t\t\t\t\t\r\r", "<timestamp> \t\t\t\t\t\r\r", true );
			Verify( ".\t.\t.\t.\t.\t", "<timestamp> .\t.\t.\t.\t.\t", true );
		}

		[TestMethod]
		public void TestNoNewline()
		{
			Verify( "S", "<timestamp> S", true );
			Verify( "S\r", "<timestamp> S\r", true );
		}

		[TestMethod]
		public void TestOneLine()
		{
			Verify( "Starting...\r\n", "<timestamp> Starting...\r\n", false );
			Verify( "Starting...\n", "<timestamp> Starting...\r\n", false );
		}

		[TestMethod]
		public void TestEmbeddedNewline()
		{
			Verify( "Starting\r\n...", "<timestamp> Starting\r\n<timestamp> ...", true );
			Verify( "Starting\n...", "<timestamp> Starting\r\n<timestamp> ...", true );
		}

		[TestMethod]
		public void TestMultipleLinesComplete()
		{
			string expected = "<timestamp> One\r\n<timestamp> Two\r\n<timestamp> Three\r\n<timestamp> Four\r\n<timestamp> Five\r\n";
			//////////////////////////////////////////////////////////////////         ^^^^
			Verify( "One\r\nTwo\r\nThree\r\nFour\r\nFive\r\n", expected, false );
			string expected2 = "<timestamp> One\r\n<timestamp> Two\r\n<timestamp> Three\rFour\r\n<timestamp> Five\r\n";
			//////////////////////////////////////////////////////////////////         ^^
			Verify( "One\nTwo\nThree\rFour\nFive\n", expected2, false );
		}

		[TestMethod]
		public void TestMultipleLinesIncomplete()
		{
			string expected = "<timestamp> One\r\n<timestamp> Two\r\n<timestamp> Three\r\n<timestamp> Four\r\n<timestamp> Five\r";
			Verify( "One\r\nTwo\r\nThree\r\nFour\r\nFive\r", expected, true );
			Verify( "One\nTwo\nThree\nFour\nFive\r", expected, true );
		}

		[TestMethod]
		public void TestMultipleLines3()
		{
			var plwt = new PrefaceLinesWithDummyTimestamp();
			var sb = new StringBuilder();
			var lines = plwt.Convert( "One\nTwo\nThree" );
			sb.Append( lines );
			lines = plwt.Convert( "\nFour\nFive\n" );
			sb.Append( lines );
			lines = plwt.Convert( "Six\nSeven" );
			sb.Append( lines );
			var result = sb.ToString();
			Trace( $"TestMultipleLines3 -> {plwt.Final} {result.Visible()}" );
			var expected = "<timestamp> One\r\n<timestamp> Two\r\n<timestamp> Three\r\n<timestamp> Four\r\n<timestamp> Five\r\n<timestamp> Six\r\n<timestamp> Seven";
			Assert.AreEqual( expected, result );
			Assert.AreEqual( true, plwt.IsContinuing );
		}

		[TestMethod]
		public void TestMultipleLines4()
		{
			var plwt = new PrefaceLinesWithDummyTimestamp();
			var sb = new StringBuilder();
			var lines = plwt.Convert( "One\nTwo\nThree\n" );
			sb.Append( lines );
			lines = plwt.Convert( "Four\nFive" );
			sb.Append( lines );
			lines = plwt.Convert( "\nSix\nSeven\n" );
			sb.Append( lines );
			var result = sb.ToString();
			Trace( $"TestMultipleLines4 -> {plwt.Final} {result.Visible()}" );
			var expected = "<timestamp> One\r\n<timestamp> Two\r\n<timestamp> Three\r\n<timestamp> Four\r\n<timestamp> Five\r\n<timestamp> Six\r\n<timestamp> Seven\r\n";
			Assert.AreEqual( expected, result );
			Assert.AreEqual( false, plwt.IsContinuing );
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
