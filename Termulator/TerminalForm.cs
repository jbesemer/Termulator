#define RESTORE_PREVIOUS_SELECTED_PORT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;

using Library;
using SharedLibrary;

namespace Termulator
{
	// TODO: 
	//      synchronize sending commands from file
	//      remember load folder, filename
	//      remember save folder
	//      convert to WPF
	//      separate ReaderThread class
	//      Friendly names
	//      Surprise Disconnect ?
	//      redirect all keyboard to command box

	public partial class TerminalForm : Form
	{
		#region // Constants, Properties //////////////////////////////////////

		public AssemblyProperties AssemblyProperties = new AssemblyProperties();

		public string Version { get { return AssemblyProperties.AssemblyVersion.ToString(); } }

		public const string HELP_FILENAME = @"Termulator.pdf";
		public const string InitialCommand = "*IDN?";

		public const string LF = "\n";
		public const string CR = "\r";
		public const string CRLF = "\r\n";
		public static readonly char[] LF_char = new char[] { '\n' };

		public static string EnterSequence = CR;

		public PortSettings PortSettings { get; protected set; }

		SerialPort Port { get; set; }

		public string PortName { get { return Port?.PortName; } }

		public string SelectedPortName { get; protected set; }

		public bool IsOpen { get { return Port != null && Port.IsOpen; } }

		public Stopwatch Stopwatch = new Stopwatch();

		UnicodeControlCharacters CharMap = new UnicodeControlCharacters( 0 );

		public bool AutoReconnect { get; protected set; }

		public uint DownloadDelay;
		public string DownloadFilename;
		public bool DownloadTraceDataSent;

		#endregion

		#region // Constructor ////////////////////////////////////////////////

		public TerminalForm()
		{
			InitializeComponent();

			Text = "Termulator v" + Version;
			historyToolStripMenuItem.Tag = 0;
			PortSettings = new PortSettings();

			SelectFontToolStripMenuItem.Visible = false;	// unimplemented
		}
		
		#endregion

		#region // Load & Close Form //////////////////////////////////////////

		private void TerminalForm_Load( object sender, EventArgs e )
		{
			this.LoadLocationAndSize( Properties.Settings.Default.MainFormLocAndSize );
			AutoReconnect = Properties.Settings.Default.AutoReconnect;
			PortSettings.LoadAllPrevious( Properties.Settings.Default.ComPortSettings );

			if( Properties.Settings.Default.IsNewInstall )
			{
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.IsNewInstall = false;
			}

#if SURPRISE_DISCONNECT
			DbtNotification = new Dbt.Notifier( this.Handle );
			DbtNotification.Arrival += DbtNotification_Arrival;
			DbtNotification.Removal += DbtNotification_Removal;
#endif

			string[] ports = SerialPort.GetPortNames();

			foreach( string name in ports )
				Debug.WriteLine( "Port: " + name );

			PortNameCombo.Items.AddRange( ports );

#if RESTORE_PREVIOUS_SELECTED_PORT
			SelectedPortName = Properties.Settings.Default.SelectedPort;
			if( !string.IsNullOrEmpty( SelectedPortName ) )
			{
				int index = PortNameCombo.FindStringExact( SelectedPortName );
				if( index >= 0 )
				{
					PortNameCombo.SelectedIndex = index;
				}
			}
#endif

			autoScrollToEndToolStripMenuItem.Checked 
				= Properties.Settings.Default.ScrollToEnd;

			showTimingToolStripMenuItem.Checked 
				= Properties.Settings.Default.ShowTimestamps;

			CommandHistory.RestoreSettings( Properties.Settings.Default.CommandHistory );
			CommandFavorites.RestoreSettings( Properties.Settings.Default.CommandFavorites );

			DownloadDelay = Properties.Settings.Default.DownloadDelay;
			DownloadFilename = Properties.Settings.Default.DownloadFilename;
			DownloadTraceDataSent = Properties.Settings.Default.DownloadTraceDataSent;

			CommandEntryTextBox.Select();
			CommandEntryTextBox.Focus();
		}

		private void TerminalForm_FormClosed( object sender, FormClosedEventArgs e )
		{
			ClosePort();

			var location = this.SaveLocationAndSize();
			if( location != null )
				Properties.Settings.Default.MainFormLocAndSize = location;

			Properties.Settings.Default.ComPortSettings 
				= PortSettings.SaveAllPrevious();

			if( PortNameCombo.SelectedIndex >= 0 )
			{
				Properties.Settings.Default.SelectedPort = (string)PortNameCombo.SelectedItem;
			}

			Properties.Settings.Default.ScrollToEnd
				= autoScrollToEndToolStripMenuItem.Checked;

			Properties.Settings.Default.ShowTimestamps
				= showTimingToolStripMenuItem.Checked;

			Properties.Settings.Default.CommandHistory = CommandHistory.SaveSettings();
			Properties.Settings.Default.CommandFavorites = CommandFavorites.SaveSettings();

			Properties.Settings.Default.DownloadDelay = DownloadDelay;
			Properties.Settings.Default.DownloadFilename = DownloadFilename;
			Properties.Settings.Default.DownloadTraceDataSent = DownloadTraceDataSent;

			Properties.Settings.Default.AutoReconnect = AutoReconnect;

			Properties.Settings.Default.Save();
		}

		#endregion

		#region // Open & Close Port //////////////////////////////////////////

		private void OpenSelectedPort()
		{
			OpenPort( SelectedPortName );
		}
		
		private void OpenPort( string portName )
		{
			ClosePort();

			try
			{
				Port = new SerialPort( portName );
				PortSettings.RestorePreviousSettings( Port );
				Port.Open();
			}
			catch( Exception e )
			{
				Debug.WriteLine( "OpenPort[ " + portName + " ] exception: " + e.Message );
				AppendTextLine( "Open Exception: " + e.Message );
			}

			//CurrentPortProperties = Win32Wrapper.GetUsbComPortProperties( portName );

			UpdatePortOpenStatus();
		}

		private void ClosePort()
		{
			if( Port != null )
			{
#if SURPRISE_DISCONNECT
				CurrentPortProperties = null;
#endif

				try { Port.Close(); }
				catch( Exception e )
				{
					Debug.WriteLine( "ClosePort[ " + PortName + " ] exception: " + e.Message );
					AppendTextLine( "Close Exception: " + e.Message );
				}
			}

			UpdatePortOpenStatus();

			Port = null;
		}

		private void TryOpenPort()
		{
			try
			{
				Port = new SerialPort( SelectedPortName );
				PortSettings.SetObisDefaults( Port );
				Port.Open();
			}
			catch( Exception e )
			{
				Debug.WriteLine( "TryOpenPort[ " + SelectedPortName + " ] exception: " + e.Message );
			}
		}

		private void UpdatePortOpenStatus()
		{
			UpdatePortOpenStatus( IsOpen );
		}

		private void UpdatePortOpenStatus( bool isOpen )
		{
			string name = PortName;

			if( isOpen )
			{
				OpenCloseButton.Text = "Close";
				PortNameCombo.BackColor = SystemColors.Window;

				if( name != null )
					AppendAnnotation( "/// " + name + " Opened ///" );

				CommandEntryTextBox.Select();
				CommandEntryTextBox.Focus();

				StartReaderThread();
			}
			else
			{
				StopReaderThread();

				PortNameCombo.BackColor = Color.LightPink;
				OpenCloseButton.Text = "Open";

				if( name != null )
					AppendAnnotation( "/// " + name + " Closed ///" );
			}
		}

		// unused, part of Surprise Disconnect WIP
		private void ReOpenPort()
		{
#if SURPRISE_DISCONNECT
			if( CurrentPortProperties != null && CurrentPortDisconnected )
			{
				CurrentPortDisconnected = false;
				OpenPort( CurrentPortProperties.PortName );
			}
#endif
		}

		#endregion

#if SURPRISE_DISCONNECT
		#region // Surprise Disconnect Handler ////////////////////////////////

		protected ComPortInfo CurrentPortProperties;
		protected USB_DeviceDetector USB_DeviceDetector;
		protected bool CurrentPortDisconnected;
		public Dbt.Notifier DbtNotification { get; protected set; }

		void DbtNotification_Removal( object sender, string devicePath )
		{
			if( CurrentPortProperties != null
			&& CurrentPortProperties.SameHardwareId( devicePath ) )
			{
				CurrentPortDisconnected = true;
				Debug.WriteLine( "Disconnecting Port" );
				AppendTextLine( "Surprise Disconnect..." );
				ClosePort();
			}
		}

		void DbtNotification_Arrival( object sender, string devicePath )
		{
			if( CurrentPortProperties != null
			&& CurrentPortProperties.SameHardwareId( devicePath ) )
			{
				CurrentPortDisconnected = false;
				Debug.WriteLine( "Reconnecting Port" );
				AppendTextLine( "...Reconnecting..." );
				ReOpenPort();
			}
		}

		#endregion
#endif

		#region // UI Events & Helpers ////////////////////////////////////////

		private void PortNameCombo_SelectedIndexChanged( object sender, EventArgs e )
		{
			SelectedPortName = (string)PortNameCombo.SelectedItem;
			OpenSelectedPort();
			CommandEntryTextBox.Focus();
		}

		private void OpenCloseButton_Click( object sender, EventArgs e )
		{
			if( IsOpen )
				ClosePort();
			else
				OpenSelectedPort();
			CommandEntryTextBox.Focus();
		}

		private void SettingsButton_Click( object sender, EventArgs e )
		{
			// ClosePort();

			var dlg = new PortSettingsForm( Port, PortSettings );
			var result = dlg.ShowDialog( this );

			if( result == System.Windows.Forms.DialogResult.OK )
			{
				switch( PortSettings.EnterSends )
				{
				case EnterSends.CR:
					EnterSequence = CR;
					break;
				case EnterSends.LF:
					EnterSequence = LF;
					break;
				case EnterSends.CRLF:
					EnterSequence = CRLF;
					break;
				}
			}

			CommandEntryTextBox.Focus();
		}

		private void InvokeSetRunning( bool ifChecked )
		{
			Invoke( new Action<bool>( SetRunning ), new object[] { ifChecked } );
		}

		private void SetRunning( bool ifRunning )
		{
			CommandEntryTextBox.Enabled = ifRunning;
			CommandEntryTextBox.Focus();
		}

		#endregion

		#region // Reader Thread //////////////////////////////////////////////

		Thread ReaderThread;
		bool ReaderRunning;

		void StartReaderThread()
		{
			ReaderThread = new Thread( () => ReaderThreadBody() );
			ReaderThread.IsBackground = true;
			ReaderRunning = true;
			ReaderThread.Start();
		}

		void StopReaderThread()
		{
			ReaderRunning = false;

			if( ReaderThread != null )
			{
				ReaderThread.Abort();
				Debug.WriteLine( "ReaderThread Aborted" );
			}

			InvokeSetRunning( false );
		}

		void ReaderThreadBody()
		{
			InvokeSetRunning( true );
			string portName = Port.PortName;

			Invoke( new Action( SendInitialCommand ) );

			const int BufferSize = 1200;
			byte[] bytes = new byte[ BufferSize ];

			while( ReaderRunning && Port != null )
			{
				try
				{
					// TODO: .ReadExisting() & convert to bytes?
					int count = Port.Read( bytes, 0, BufferSize );
					if( count > 0 )
					{
						AppendBytes( bytes, count );
					}
				}
				catch( TimeoutException )
				{
					continue;    // ignore timeout
				}
				catch( ThreadAbortException )
				{
					break;
				}
				catch( Exception ex )
				{
					if( ReaderRunning && Port != null )
					{
						string msg = "ReaderThread exception on "
							+ portName
							+ ": "
							+ ex.Message;
						Debug.WriteLine( msg );
						InvokeAppendText( msg );

						if( !AutoReconnect )
							break;  // exit thread

						// else try to reconnect
						InvokeAppendText( "\nReconnecting...");

						while( ReaderRunning && Port != null )
						{
							Thread.Sleep( 500 );
							TryOpenPort();
							if( Port.IsOpen )
							{
								InvokeAppendText( "... Reconnected\n" );
								break;  // out of inner loop and continue in outer one
							}
						}
					}
				}
			}

			Debug.WriteLine( "ReaderThread Exits" );
			Invoke( new Action( ReaderThreadStopped ) );
		}

		public void AppendBytes( byte[] bytes, int count )
		{
			if( PortSettings.Strip8thBit )
			{
				for( int i = 0; i < count; i++ )
				{
					bytes[ i ] &= 0x7f;
				}
			}

			string s = Encoding.ASCII.GetString( bytes, 0, count );
			s = TranslateText( s );
			InvokeAppendText( s );
		}

		void InvokeAppendText( string text )
		{
			Invoke( new Action<string>( AppendText ), new object[] { text } );
		}

		// HERe'S THE NEW RULE [supposedly according to the HTML5 spec] 
		// textareas [which are not exactly same as textbox] should return 
		// CRLF for each newline.
		//
		// Patterns for newline on input streams:
		// 1. every two-character string consisting of a "CRLF"character pair.
		// 2. every occurrence of a "CR" not followed by a "LF". 
		// 3. every occurrence of a "LF" (U+000A) character not preceded by a "CR"
		//
		// "CR" = (U+000D) 
		// "LF" = (U+000A) 
		// per https://stackoverflow.com/questions/14217101/what-character-represents-a-new-line-in-a-text-area

		public string TranslateText( string text )
		{
			if( PortSettings.Edit_NL_To_CRLF )
			{
				text = text.Replace( LF, CRLF );
			}
			if( PortSettings.Edit_CR_To_CRLF )
			{
				text = text.Replace( CR, CRLF );
			}
			if( PortSettings.ShowNonprinting )
			{
				text = CharMap.Convert( text );
			}

			return text;
		}

		void ReaderThreadStopped()
		{
			Debug.WriteLine( "ReaderThread Stopped" );
			// AppendText( "Exiting Reader Thread" );
			SetRunning( false );
		}

		#endregion

		#region // Append text ////////////////////////////////////////////////

		// append received text, subject to translations

		void AppendText( string text )
		{
			AppendTranscript( TranslateText( text ));
		}

		// append text without translations and followed by newline

		void AppendTextLine( string text = "" )
		{
			AppendTranscript( text + CRLF );
		}

		// append text line preceeded by blank line (unless 1st line in transcript)

		void AppendAnnotation( string text = "" )
		{
			if( TranscriptTextBox.Text.Length > 0 )
				AppendTextLine();

			AppendTextLine( text );
		}

		// append text, optionally prepending timestamp

		void AppendTranscript( string text )
		{
			TranscriptTextBox.Text += AppendTranscriptWithTimestamp( text );
			ScrollToEnd();
		}

		string AppendTranscriptWithTimestamp( string text )
		{

			if( showTimingToolStripMenuItem.Checked )
			{
				StringBuilder r = new StringBuilder();
				string[] lines 
					= text.Split(
						LF_char,
						StringSplitOptions.RemoveEmptyEntries);

				foreach( string line in lines )
				{
					r.Append( Timestamp() );
					r.Append( line );
					r.Append( "\n" );
				}

				return r.ToString();
			}
			else
				return text;
		}

		public string Timestamp()
		{
			DateTime dt = DateTime.Now;
			return
				string.Format(
					"{0:d2}:{1:d2}:{2:d2}.{3:d3} ",
					dt.Hour,
					dt.Minute,
					dt.Second,
					dt.Millisecond );
		}

		void ScrollToEnd()
		{
			if( autoScrollToEndToolStripMenuItem.Checked )
			{
				TranscriptTextBox.SelectionStart = TranscriptTextBox.TextLength;
				TranscriptTextBox.SelectionLength = 0;
				TranscriptTextBox.ScrollToCaret();
				TranscriptTextBox.Invalidate();
			}
		}

		#endregion

		#region // Command History & Favorites ////////////////////////////////

		private void CommandContextMenu_Opening( object sender, CancelEventArgs e )
		{
			historyToolStripMenuItem.Enabled = ( CommandHistory.Count > 0 );
			// favoritesToolStripMenuItem.Enabled = CommandFavorites.Count > 0;
		}

		// history is displayed this many entries per menu level.
		// menus may cascade indefinitely until the entire history is presented.

		const int MaxHistoryItemsPerLevel = 24;

		private void historyToolStripMenuItem_DropDownOpening( object sender, EventArgs e )
		{
			ToolStripMenuItem item;

			var menuItem = sender as ToolStripMenuItem;
			if( sender == null )
				return;

			menuItem.DropDownItems.Clear();

			int previous = (int)menuItem.Tag;

			if( previous <= 0 )
			{
				item = new ToolStripMenuItem( "Clear History" );
				item.DropDownOpening += clearHistoryToolStripMenuItem_Click;
				menuItem.DropDownItems.Add( item );
			}

			int limit = Math.Max( 0, CommandHistory.Count - previous );
			int first = Math.Max( 0, CommandHistory.Count - MaxHistoryItemsPerLevel - previous );
			int last = Math.Min( limit, first + MaxHistoryItemsPerLevel );

			Debug.WriteLine( "Drop prev={0} first={1}, last={2} count={3}", previous, first, last, CommandHistory.Count );

			if( first > 0 )
			{
				// if there are more older entries then we first add a special 
				// item that leads to another sub-menu with the next batch of history.

				int tag = CommandHistory.Count - first;
				Debug.WriteLine( "more.tag = {0}", tag );

				item = new ToolStripMenuItem( "Older..." )
				{
					Tag = tag,
				};
				item.DropDownOpening += historyToolStripMenuItem_DropDownOpening;
				menuItem.DropDownItems.Add( item );
			}

			for( int i = first; i < last; i++ )
			{
				string cmd = CommandHistory.Item( i );
				item = new ToolStripMenuItem( cmd );
				item.Click += HistoryMenuItem_Click;
				menuItem.DropDownItems.Add( item );
			}
		}

		void HistoryMenuItem_Click( object sender, EventArgs e )
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if( item != null )
			{
				SendCommand( item.Text );
			}
		}

		private void clearHistoryToolStripMenuItem_Click( object sender, EventArgs e )
		{
			CommandHistory.Clear();
		}

		private void favoritesToolStripMenuItem_DropDownOpening( object sender, EventArgs e )
		{

			favoritesToolStripMenuItem.DropDownItems.Clear();

			// Favorites editing menu items

			ToolStripMenuItem edit 
				= new ToolStripMenuItem( "Edit Favorites..." );
			// edit.DropDownOpening += ...;
			favoritesToolStripMenuItem.DropDownItems.Add( edit );

			ToolStripMenuItem item
				= new ToolStripMenuItem( "Add Most Recent Command" );
			item.Click += new EventHandler( addPreviousToFavoritesToolStripMenuItem_Click );
			item.Enabled = ( CommandHistory.Count > 0 );
			edit.DropDownItems.Add( item );

			item = new ToolStripMenuItem( "Add From History..." );
			item.DropDownOpening += new EventHandler( AddFavorite_DropDownOpening );
			item.Enabled = ( CommandHistory.Count > 0 );
			edit.DropDownItems.Add( item );

			item = new ToolStripMenuItem( "Remove Favorite..." );
			item.DropDownOpening += new EventHandler( RemoveFavorite_DropDownOpening );
			item.Enabled = ( CommandFavorites.Count > 0 );
			edit.DropDownItems.Add( item );

			// Favorites menu items

			foreach( string cmd in CommandFavorites.FetchAll() )
			{
				item = new ToolStripMenuItem( cmd );
				item.Click += HistoryMenuItem_Click;
				favoritesToolStripMenuItem.DropDownItems.Add( item );
			}
		}

		void AddFavorite_DropDownOpening( object sender, EventArgs e )
		{
			ToolStripMenuItem menu = sender as ToolStripMenuItem;
			if( menu != null )
			{
				foreach( string cmd in CommandHistory.FetchAll() )
				{
					var item = new ToolStripMenuItem( cmd );
					item.Click += new EventHandler( AddFavorite_Click );
					menu.DropDownItems.Add( item );
				}
			}
		}

		void AddFavorite_Click( object sender, EventArgs e )
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if( item != null )
			{
				CommandFavorites.Add(  item.Text );
			}
		}

		void RemoveFavorite_DropDownOpening( object sender, EventArgs e )
		{
			ToolStripMenuItem menu = sender as ToolStripMenuItem;
			if( menu != null )
			{
				foreach( string cmd in CommandFavorites.FetchAll() )
				{
					var item = new ToolStripMenuItem( cmd );
					item.Click += new EventHandler( RemoveFavorite_Click );
					menu.DropDownItems.Add( item );
				}
			}
		}

		void RemoveFavorite_Click( object sender, EventArgs e )
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if( item != null )
			{
				CommandFavorites.Remove( item.Text );
			}
		}

		private void addPreviousToFavoritesToolStripMenuItem_Click( object sender, EventArgs e )
		{
			string cmd = CommandHistory.FetchMostRecent();

			CommandFavorites.Add( cmd );
#if false		
			MessageBox.Show(
				"Added: " + cmd,
				"Add to Command Favorites",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information );
#endif
		}

		#endregion

		#region // Text Entry /////////////////////////////////////////////////

		private void textEntry_KeyPress( object sender, KeyPressEventArgs e )
		{
			switch( e.KeyChar )
			{
			case (char)Keys.Return:
				SendCommand( CommandEntryTextBox.Text );
				//EntryTextBox.Enabled = true;
				CommandEntryTextBox.Clear();
				e.Handled = true;
				break;
			}
		}

		private void textEntry_KeyDown( object sender, KeyEventArgs e )
		{
			switch( e.KeyCode )
			{
			case Keys.Up:
				CommandHistory.Back();
				break;

			case Keys.Down:
				CommandHistory.Forward();
				break;

			default:
				return;	// NOT handled
			}
			e.Handled = true;	// up and down ARE handled

			CommandEntryTextBox.Text = CommandHistory.Fetch();
			CommandEntryTextBox.SelectionLength = 0;
			CommandEntryTextBox.SelectionStart = CommandEntryTextBox.Text.Length;
		}

		private void SendCommand( string cmd )
		{
			CommandHistory.Add( cmd );

			SendCommandNoHistory( cmd );
		}

		private void SendCommandNoHistory( string cmd )
		{
			if( Port != null )
			{
				AppendTextLine( "Send: " + cmd );
				Stopwatch.Restart();

				try
				{
					Port.Write( cmd + EnterSequence );
				}
				catch( Exception e )
				{
					AppendTextLine( "Exception: " + e.Message );
					ClosePort();
				}
			}
		}

		private void SendInitialCommand()
		{
			SendCommandNoHistory( InitialCommand );
		}

		#endregion

		#region // Transcript Context Menu Actions ////////////////////////////

		private void copyToolStripMenuItem_Click( object sender, EventArgs e )
		{
			TranscriptTextBox.Copy();
		}

		private void clearTextToolStripMenuItem_Click( object sender, EventArgs e )
		{
			TranscriptTextBox.Clear();
			ScrollToEnd();
		}

		private void selectAllToolStripMenuItem_Click( object sender, EventArgs e )
		{
			TranscriptTextBox.SelectAll();
		}

		private void copyToClipboardToolStripMenuItem_Click( object sender, EventArgs e )
		{
			Clipboard.SetText( TranscriptTextBox.Text );
		}

		private void showTimingToolStripMenuItem_Click( object sender, EventArgs e )
		{

		}

		private void SelectFontToolStripMenuItem_Click( object sender, EventArgs e )
		{
			var dlg = new SelectFontForm(){
				SelectedFontName = TranscriptTextBox.Font.FontFamily.ToString(),
				SelectedFontSize = TranscriptTextBox.Font.Size,
				FontBold = TranscriptTextBox.Font.Bold,
				FontItalic = TranscriptTextBox.Font.Italic,
			};

			var result = dlg.ShowDialog( this );

			if( result == System.Windows.Forms.DialogResult.OK )
			{
			}
		}

		private void saveToDiskToolStripMenuItem_Click( object sender, EventArgs e )
		{
			string Timestamp
				= DateTime.Now.ToString()
					.Replace( "/", "-" )
					.Replace( ":", "." );

			var dlg = new SaveFileDialog()
			{
				Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
				FilterIndex = 1,
				RestoreDirectory = true,
				FileName = "Termulator "
					+ Timestamp
					+ ".txt",
				InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.Desktop ),
			};

			var result = dlg.ShowDialog( this );

			if( result == System.Windows.Forms.DialogResult.OK )
			{
				var name = System.IO.Path.GetFullPath( dlg.FileName );

				using( TextWriter writer = File.CreateText( name ) )
				{
					string text = Vis( TranscriptTextBox.Text );
					writer.Write( text );
				}
			}
		}

		private string Vis(string text )
		{
			return text.Replace( "\r", "\\r" );
		}

		private void DownloadFileToolStripMenuItem_Click( object sender, EventArgs e )
		{
			var dlg = new DownloadForm()
			{
				Port = Port,
				AppendTextLine = AppendTextLine,

				Delay = DownloadDelay,
				Filename = DownloadFilename,
				TraceDataSent = DownloadTraceDataSent,
			};

			try
			{
				// this dialog actually performs the necessary downloading, if user selects OK
				var result = dlg.ShowDialog( this );

				// so on Ok, all we need to do is fetch any settings that changed so they can be saved next time
				if( result == System.Windows.Forms.DialogResult.OK )
				{
					DownloadFilename = System.IO.Path.GetFullPath( dlg.Filename );
					DownloadDelay = DownloadDelay = dlg.Delay;
					DownloadTraceDataSent = dlg.TraceDataSent;
				}
			}
			catch( Exception ex )
			{
				AppendTextLine( "Exception: " + ex.Message );
				ClosePort();
			}

		}

		#endregion

		#region // UI Events //////////////////////////////////////////////////

		private void TranscriptTextBox_FocusEnter( object sender, EventArgs e )
		{
			// Debug.WriteLine( "Transcript_Enter" );
		}

		private void CommandEntryTextBox_FocusEnter( object sender, EventArgs e )
		{
			// Debug.WriteLine( "CommandEntryTextBox_Enter" );
		}

		private void HelpPictureBox_Click( object sender, EventArgs e )
		{
			try
			{
				System.Diagnostics.Process.Start( HELP_FILENAME );
			}
			catch( Win32Exception ex )
			{
				MessageBox.Show(
					ex.Message,
					"Exception Opening Help File",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error );
			}
		}

		private void AboutPictureBox_Click( object sender, EventArgs e )
		{
			var box = new AboutBox();
			box.ShowDialog();
		}

		private void UnicodeUnitTestToolStripMenuItem_Click( object sender, EventArgs e )
		{
			// does NOT work in textbox

			string text = CharMap.UnitTest();
			AppendTextLine( text );
		}

		#endregion

		private void TranscriptTextBox_PreviewKeyDown( object sender, PreviewKeyDownEventArgs e )
		{
			CommandEntryTextBox.Focus();
		}

		private void saveScreenAsImageMenuItem_Click( object sender, EventArgs e )
		{

		}
	}
}
