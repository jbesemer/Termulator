using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Termulator
{
	public partial class PortSettingsForm : Form
	{
		SerialPort Port;

		static object[] BaudRates = new object[]{ 
			"110", "300", "600", "1200", "2400", "4800", 
			"9600", "14400", "19200", "28800", "38400", "56000", "57600", "115200",
			// "128000", "153600", "230400", "256000", "460800", "921600",
		};
		static object[] DataBits = new object[] { "5", "6", "7", "8" };		
		static object[] Parities = new object[] { Parity.Even, Parity.Odd, Parity.Mark, Parity.Space, Parity.None };
		static object[] StopBitS = new object[] { StopBits.One, StopBits.OnePointFive, StopBits.Two };
		static object[] Handshakes = new object[] { Handshake.None, Handshake.RequestToSend, Handshake.XOnXOff, Handshake.RequestToSendXOnXOff };

		public static EnterSends EnterSends { get; private set; }

		public PortSettingsForm( SerialPort port )
		{
			InitializeComponent();

			Port = port;

			BaudComboBox.Items.AddRange( BaudRates );
			DataBitsComboBox.Items.AddRange( DataBits );
			ParityComboBox.Items.AddRange( Parities );
			StopBitsComboBox.Items.AddRange( StopBitS );
			HandshakingComboBox.Items.AddRange( Handshakes );
		}

		private void PortSettingsForm_Load( object sender, EventArgs e )
		{
			this.CenterToParent();

			if( Port != null )
			{
				LoadSettingsFromPort( Port );
				Text = Port.PortName + " Settings";
			}
			else
			{
				Text = "Port Settings";
			}
		}

		// !! OK and Cancel buttons use DialogResult properties !!

		private void OK_Button_Click( object sender, EventArgs e )
		{
			SaveSettingsToPort( Port );
		}

		private void ObisModeButton_Click( object sender, EventArgs e )
		{
			DecodeSettings( PortSettings.ObisDefaultPortSettings );
		}

		#region // encode/decode control values to/from PortSettings format

		private void LoadSettingsFromPort( SerialPort Port )
		{
			DecodeSettings( PortSettings.Encode( Port ) );
		}

		private void SaveSettingsToPort( SerialPort Port )
		{
			PortSettings.Decode( Port, EncodeSettings() );
		}

		public string EncodeSettings()
		{
			int[] fields 
#if true
				= new int[]{
					PortSettings.GetInt( (string)BaudComboBox.SelectedItem ),
					PortSettings.GetInt( (string)DataBitsComboBox.SelectedItem ),
					PortSettings.DecodeTimeout( ReadTimeoutTextBox.Text ),
					(int)(Parity)ParityComboBox.SelectedItem,
					(int)(StopBits)StopBitsComboBox.SelectedItem,
					(int)(Handshake)HandshakingComboBox.SelectedItem,
					PortSettings.Encode( NL_CheckBox.Checked ),
					PortSettings.Encode( CR_CheckBox.Checked == true ),
					PortSettings.Encode( ShowNonprintingCheckBox.Checked == true ),
					PortSettings.Encode( ShowWhitespaceCheckBox.Checked == true ),
					(int)EnterSends,
				};
#else
				= new int[ 6 ];

			fields[ 0 ] = PortSettings.GetInt( (string)BaudComboBox.SelectedItem );
			fields[ 1 ] = PortSettings.GetInt( (string)DataBitsComboBox.SelectedItem );
			fields[2] = PortSettings.DecodeTimeout( ReadTimeoutTextBox.Text );
			fields[3] = (int)(Parity)ParityComboBox.SelectedItem;
			fields[4] = (int)(StopBits)StopBitsComboBox.SelectedItem;
			fields[ 5 ] = (int)(Handshake)HandshakingComboBox.SelectedItem;
#endif
			return PortSettings.Encode( fields );
		}

		public EnterSends EncodeEnterSends()
		{
			if( EnterSendsLF_Radio.Checked)
				return EnterSends.LF;

			if( EnterSendsCRLF_Radio.Checked)
				return EnterSends.CRLF;

			return EnterSends.CR;
		}

		public void DecodeSettings( string settings )
		{
			int[] fields = PortSettings.Decode( settings );

			BaudComboBox.SelectedItem = fields[ 0 ].ToString();
			DataBitsComboBox.SelectedItem = fields[ 1 ].ToString();
			ReadTimeoutTextBox.Text = PortSettings.EncodeTimeout( fields[ 2 ] );
			ParityComboBox.SelectedItem = (Parity)fields[ 3 ];
			StopBitsComboBox.SelectedItem = (StopBits)fields[ 4 ];
			HandshakingComboBox.SelectedItem = (Handshake)fields[ 5 ];


			NL_CheckBox.Checked = ( fields.Length >= 7 && fields[ 6 ] != 0 );
			CR_CheckBox.Checked = ( fields.Length >= 8 && fields[ 7 ] != 0 );
			ShowNonprintingCheckBox.Checked = ( fields.Length >= 9 && fields[ 8 ] != 0 );
			ShowWhitespaceCheckBox.Checked = ( fields.Length >= 10 && fields[ 9 ] != 0 );

			if( fields.Length >= 11 )
				EnterSends = (EnterSends)fields[ 10 ];
			else
				EnterSends = EnterSends.CR;

			DecodeEnterSends();
		}

		public void DecodeEnterSends()
		{
			if( EnterSends == EnterSends.CR )
				EnterSendsCR_Radio.Checked = true;
			else if( EnterSends == EnterSends.LF )
				EnterSendsLF_Radio.Checked = true;
			else if( EnterSends == EnterSends.CRLF )
				EnterSendsCRLF_Radio.Checked = true;
		}
		
		#endregion
	}
}
