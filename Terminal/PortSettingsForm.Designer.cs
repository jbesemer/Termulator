namespace Terminal
{
	partial class PortSettingsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.BaudComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.ParityComboBox = new System.Windows.Forms.ComboBox();
			this.DataBitsComboBox = new System.Windows.Forms.ComboBox();
			this.StopBitsComboBox = new System.Windows.Forms.ComboBox();
			this.HandshakingComboBox = new System.Windows.Forms.ComboBox();
			this.ReadTimeoutTextBox = new System.Windows.Forms.TextBox();
			this.OK_Button = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.ObisModeButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ShowWhitespaceCheckBox = new System.Windows.Forms.CheckBox();
			this.ShowNonprintingCheckBox = new System.Windows.Forms.CheckBox();
			this.CR_CheckBox = new System.Windows.Forms.CheckBox();
			this.NL_CheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.EnterSendsCRLF_Radio = new System.Windows.Forms.RadioButton();
			this.EnterSendsCR_Radio = new System.Windows.Forms.RadioButton();
			this.EnterSendsLF_Radio = new System.Windows.Forms.RadioButton();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// BaudComboBox
			// 
			this.BaudComboBox.FormattingEnabled = true;
			this.BaudComboBox.Location = new System.Drawing.Point(91, 29);
			this.BaudComboBox.Name = "BaudComboBox";
			this.BaudComboBox.Size = new System.Drawing.Size(60, 21);
			this.BaudComboBox.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Baud Rate";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Parity";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(22, 89);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(50, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Data Bits";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(161, 33);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(49, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Stop Bits";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(161, 61);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(62, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Handshake";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(161, 89);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(74, 13);
			this.label7.TabIndex = 7;
			this.label7.Text = "Read Timeout";
			// 
			// ParityComboBox
			// 
			this.ParityComboBox.FormattingEnabled = true;
			this.ParityComboBox.Location = new System.Drawing.Point(91, 57);
			this.ParityComboBox.Name = "ParityComboBox";
			this.ParityComboBox.Size = new System.Drawing.Size(60, 21);
			this.ParityComboBox.TabIndex = 9;
			// 
			// DataBitsComboBox
			// 
			this.DataBitsComboBox.FormattingEnabled = true;
			this.DataBitsComboBox.Location = new System.Drawing.Point(91, 85);
			this.DataBitsComboBox.Name = "DataBitsComboBox";
			this.DataBitsComboBox.Size = new System.Drawing.Size(60, 21);
			this.DataBitsComboBox.TabIndex = 10;
			// 
			// StopBitsComboBox
			// 
			this.StopBitsComboBox.FormattingEnabled = true;
			this.StopBitsComboBox.Location = new System.Drawing.Point(216, 29);
			this.StopBitsComboBox.Name = "StopBitsComboBox";
			this.StopBitsComboBox.Size = new System.Drawing.Size(95, 21);
			this.StopBitsComboBox.TabIndex = 11;
			// 
			// HandshakingComboBox
			// 
			this.HandshakingComboBox.FormattingEnabled = true;
			this.HandshakingComboBox.Location = new System.Drawing.Point(228, 57);
			this.HandshakingComboBox.Name = "HandshakingComboBox";
			this.HandshakingComboBox.Size = new System.Drawing.Size(83, 21);
			this.HandshakingComboBox.TabIndex = 12;
			// 
			// ReadTimeoutTextBox
			// 
			this.ReadTimeoutTextBox.Location = new System.Drawing.Point(238, 85);
			this.ReadTimeoutTextBox.Name = "ReadTimeoutTextBox";
			this.ReadTimeoutTextBox.Size = new System.Drawing.Size(73, 20);
			this.ReadTimeoutTextBox.TabIndex = 14;
			// 
			// OK_Button
			// 
			this.OK_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.OK_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK_Button.Location = new System.Drawing.Point(217, 303);
			this.OK_Button.Name = "OK_Button";
			this.OK_Button.Size = new System.Drawing.Size(79, 23);
			this.OK_Button.TabIndex = 16;
			this.OK_Button.Text = "OK";
			this.OK_Button.UseVisualStyleBackColor = true;
			this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(116, 303);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(79, 23);
			this.button2.TabIndex = 17;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// ObisModeButton
			// 
			this.ObisModeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ObisModeButton.Location = new System.Drawing.Point(15, 302);
			this.ObisModeButton.Name = "ObisModeButton";
			this.ObisModeButton.Size = new System.Drawing.Size(79, 23);
			this.ObisModeButton.TabIndex = 18;
			this.ObisModeButton.Text = "OBIS Mode";
			this.ObisModeButton.UseVisualStyleBackColor = true;
			this.ObisModeButton.Click += new System.EventHandler(this.ObisModeButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ShowWhitespaceCheckBox);
			this.groupBox1.Controls.Add(this.ShowNonprintingCheckBox);
			this.groupBox1.Controls.Add(this.CR_CheckBox);
			this.groupBox1.Controls.Add(this.NL_CheckBox);
			this.groupBox1.Location = new System.Drawing.Point(15, 125);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(302, 56);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Input Translations";
			// 
			// ShowWhitespaceCheckBox
			// 
			this.ShowWhitespaceCheckBox.AutoSize = true;
			this.ShowWhitespaceCheckBox.Enabled = false;
			this.ShowWhitespaceCheckBox.Location = new System.Drawing.Point(147, 34);
			this.ShowWhitespaceCheckBox.Name = "ShowWhitespaceCheckBox";
			this.ShowWhitespaceCheckBox.Size = new System.Drawing.Size(113, 17);
			this.ShowWhitespaceCheckBox.TabIndex = 3;
			this.ShowWhitespaceCheckBox.Text = "Show Whitespace";
			this.ShowWhitespaceCheckBox.UseVisualStyleBackColor = true;
			// 
			// ShowNonprintingCheckBox
			// 
			this.ShowNonprintingCheckBox.AutoSize = true;
			this.ShowNonprintingCheckBox.Enabled = false;
			this.ShowNonprintingCheckBox.Location = new System.Drawing.Point(147, 16);
			this.ShowNonprintingCheckBox.Name = "ShowNonprintingCheckBox";
			this.ShowNonprintingCheckBox.Size = new System.Drawing.Size(110, 17);
			this.ShowNonprintingCheckBox.TabIndex = 2;
			this.ShowNonprintingCheckBox.Text = "Show Nonprinting";
			this.ShowNonprintingCheckBox.UseVisualStyleBackColor = true;
			// 
			// CR_CheckBox
			// 
			this.CR_CheckBox.AutoSize = true;
			this.CR_CheckBox.Location = new System.Drawing.Point(21, 16);
			this.CR_CheckBox.Name = "CR_CheckBox";
			this.CR_CheckBox.Size = new System.Drawing.Size(94, 17);
			this.CR_CheckBox.TabIndex = 1;
			this.CR_CheckBox.Text = "CR -> Newline";
			this.CR_CheckBox.UseVisualStyleBackColor = true;
			// 
			// NL_CheckBox
			// 
			this.NL_CheckBox.AutoSize = true;
			this.NL_CheckBox.Location = new System.Drawing.Point(21, 34);
			this.NL_CheckBox.Name = "NL_CheckBox";
			this.NL_CheckBox.Size = new System.Drawing.Size(91, 17);
			this.NL_CheckBox.TabIndex = 0;
			this.NL_CheckBox.Text = "LF -> Newline";
			this.NL_CheckBox.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.EnterSendsCRLF_Radio);
			this.groupBox2.Controls.Add(this.EnterSendsCR_Radio);
			this.groupBox2.Controls.Add(this.EnterSendsLF_Radio);
			this.groupBox2.Location = new System.Drawing.Point(15, 182);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(302, 39);
			this.groupBox2.TabIndex = 20;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Enter Sends";
			// 
			// EnterSendsCRLF_Radio
			// 
			this.EnterSendsCRLF_Radio.AutoSize = true;
			this.EnterSendsCRLF_Radio.Location = new System.Drawing.Point(166, 16);
			this.EnterSendsCRLF_Radio.Name = "EnterSendsCRLF_Radio";
			this.EnterSendsCRLF_Radio.Size = new System.Drawing.Size(52, 17);
			this.EnterSendsCRLF_Radio.TabIndex = 2;
			this.EnterSendsCRLF_Radio.TabStop = true;
			this.EnterSendsCRLF_Radio.Text = "CRLF";
			this.EnterSendsCRLF_Radio.UseVisualStyleBackColor = true;
			// 
			// EnterSendsCR_Radio
			// 
			this.EnterSendsCR_Radio.AutoSize = true;
			this.EnterSendsCR_Radio.Location = new System.Drawing.Point(20, 16);
			this.EnterSendsCR_Radio.Name = "EnterSendsCR_Radio";
			this.EnterSendsCR_Radio.Size = new System.Drawing.Size(40, 17);
			this.EnterSendsCR_Radio.TabIndex = 1;
			this.EnterSendsCR_Radio.TabStop = true;
			this.EnterSendsCR_Radio.Text = "CR";
			this.EnterSendsCR_Radio.UseVisualStyleBackColor = true;
			// 
			// EnterSendsLF_Radio
			// 
			this.EnterSendsLF_Radio.AutoSize = true;
			this.EnterSendsLF_Radio.Location = new System.Drawing.Point(90, 16);
			this.EnterSendsLF_Radio.Name = "EnterSendsLF_Radio";
			this.EnterSendsLF_Radio.Size = new System.Drawing.Size(37, 17);
			this.EnterSendsLF_Radio.TabIndex = 0;
			this.EnterSendsLF_Radio.TabStop = true;
			this.EnterSendsLF_Radio.Text = "LF";
			this.EnterSendsLF_Radio.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Location = new System.Drawing.Point(15, 11);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(302, 108);
			this.groupBox4.TabIndex = 22;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "RS-232 Settings";
			// 
			// PortSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(338, 336);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ObisModeButton);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.OK_Button);
			this.Controls.Add(this.ReadTimeoutTextBox);
			this.Controls.Add(this.HandshakingComboBox);
			this.Controls.Add(this.StopBitsComboBox);
			this.Controls.Add(this.DataBitsComboBox);
			this.Controls.Add(this.ParityComboBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BaudComboBox);
			this.Controls.Add(this.groupBox4);
			this.Name = "PortSettingsForm";
			this.Text = "PortSettingsForm";
			this.Load += new System.EventHandler(this.PortSettingsForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox BaudComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox ParityComboBox;
		private System.Windows.Forms.ComboBox DataBitsComboBox;
		private System.Windows.Forms.ComboBox StopBitsComboBox;
		private System.Windows.Forms.ComboBox HandshakingComboBox;
		private System.Windows.Forms.TextBox ReadTimeoutTextBox;
		private System.Windows.Forms.Button OK_Button;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button ObisModeButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox CR_CheckBox;
		private System.Windows.Forms.CheckBox NL_CheckBox;
		private System.Windows.Forms.CheckBox ShowNonprintingCheckBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton EnterSendsCRLF_Radio;
		private System.Windows.Forms.RadioButton EnterSendsCR_Radio;
		private System.Windows.Forms.RadioButton EnterSendsLF_Radio;
		private System.Windows.Forms.CheckBox ShowWhitespaceCheckBox;
		private System.Windows.Forms.GroupBox groupBox4;
	}
}