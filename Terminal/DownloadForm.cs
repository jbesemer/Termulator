#define SHOW_PROGRESS

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace Terminal
{
	public partial class DownloadForm : Form
	{
		public Action<string> AppendTextLine;	// append line to transcript window
		public SerialPort Port { get; set; }

		public bool TraceDataSent;
		public string Filename;
		public uint Delay;

		public DownloadForm()
		{
			InitializeComponent();
		}

		private void DownloadForm_Load( object sender, EventArgs e )
		{
			this.CenterToParent();
			FilenameTextBox.Text = Filename;
			DelayTextBox.Text = Delay.ToString();
			TraceDataCheckBox.Checked = TraceDataSent;
			ProgressBar.Value = 0;
		}

		private void BrowseButton_Click( object sender, EventArgs e )
		{
			Browse();
		}

		private void DownloadButton_Click( object sender, EventArgs e )
		{
			Filename = FilenameTextBox.Text;
			uint.TryParse( DelayTextBox.Text, out Delay );
			TraceDataSent = TraceDataCheckBox.Checked;

			// Download launches a separate thread
			Download();

			// this dialog remains visible until thread closes it
		}

		public void Browse()
		{
			if( string.IsNullOrWhiteSpace( Filename ) )
				Filename = "Filename.txt";

			string folder = Path.GetDirectoryName( Filename );
			if( string.IsNullOrWhiteSpace( folder ))
				folder = Environment.GetFolderPath( Environment.SpecialFolder.Desktop );

			var dlg = new OpenFileDialog()
			{
				Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
				FilterIndex = 1,
				RestoreDirectory = true,
				FileName=Filename,
				InitialDirectory = folder,
			};

			var result = dlg.ShowDialog( this );

			if( result == System.Windows.Forms.DialogResult.OK )
			{
				FilenameTextBox.Text 
					= Filename 
					= System.IO.Path.GetFullPath( dlg.FileName );
			}
		}

		Thread Thread;

		public void Download()
		{
			Thread = new Thread( () => DownloadThread() );
			Thread.Name = "Downloader";
			//Thread.IsBackground = true;
			Thread.Start();
		}

		public void UpdateProgress( int percent )
		{
			ProgressBar.Value = percent; 
		}

		void InvokeUpdateProgress( int percent )
		{
			Invoke(
				new Action<int>( UpdateProgress ),
				new object[] { percent } );
		}

		public void DownloadThread()
		{
			var fi = new FileInfo( Filename );
			long FileLength= fi.Length;
#if SHOW_PROGRESS
			long BytesRead = 0;
			int Percent = 0;
#endif

			try
			{
				using( TextReader reader = File.OpenText( Filename ) )
				{
					for( ; ; )
					{
						string cmd = reader.ReadLine();
						if( cmd == null )
							break;

						SendCommandNoHistory( cmd );

#if SHOW_PROGRESS
						BytesRead += cmd.Length + 2;
						int percent = (int)( BytesRead * 100.0 / FileLength );
						if( percent != Percent )
						{
							Percent = percent;
							InvokeUpdateProgress( percent );
#if false
						Invoke(
							new Action<int>( UpdateProgress ),
							new object[] { percent } );
#endif

						}
#endif
					}
				}
			}
			catch( Exception ex )
			{
				Debug.WriteLine( "Download thread exception: " + ex.Message );
				Invoke( new Action( Close ) );
			}

			Invoke( new Action( Success ) );
		}

		private void Success()
		{
			DialogResult = DialogResult.OK;
		}

		private void SendCommandNoHistory( string data )
		{
			if( Port != null )
			{
				if( TraceDataSent )
					EchoDataSent( "Send: " + data );

				Port.Write( data + TerminalForm.EnterSequence );

				if( Delay > 0 )
					Thread.Sleep( (int)Delay );
			}
		}

		private void EchoDataSent( string data )
		{
			Invoke( 
				new Action<string>( AppendTextLine ),
				new object[]{ data } );
		}
	}
}
