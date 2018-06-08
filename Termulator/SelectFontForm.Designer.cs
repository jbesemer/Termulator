namespace Termulator
{
	partial class SelectFontForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectFontForm));
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.FontItalicCheckBox = new System.Windows.Forms.CheckBox();
			this.FontBoldCheckBox = new System.Windows.Forms.CheckBox();
			this.FontSizeComboBox = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.FontNameComboBox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.OK_Button = new System.Windows.Forms.Button();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.FontItalicCheckBox);
			this.groupBox3.Controls.Add(this.FontBoldCheckBox);
			this.groupBox3.Controls.Add(this.FontSizeComboBox);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.FontNameComboBox);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Location = new System.Drawing.Point(12, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(370, 47);
			this.groupBox3.TabIndex = 24;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Font";
			// 
			// FontItalicCheckBox
			// 
			this.FontItalicCheckBox.AutoSize = true;
			this.FontItalicCheckBox.Location = new System.Drawing.Point(335, 18);
			this.FontItalicCheckBox.Name = "FontItalicCheckBox";
			this.FontItalicCheckBox.Size = new System.Drawing.Size(29, 17);
			this.FontItalicCheckBox.TabIndex = 5;
			this.FontItalicCheckBox.Text = "I";
			this.FontItalicCheckBox.UseVisualStyleBackColor = true;
			// 
			// FontBoldCheckBox
			// 
			this.FontBoldCheckBox.AutoSize = true;
			this.FontBoldCheckBox.Location = new System.Drawing.Point(296, 19);
			this.FontBoldCheckBox.Name = "FontBoldCheckBox";
			this.FontBoldCheckBox.Size = new System.Drawing.Size(33, 17);
			this.FontBoldCheckBox.TabIndex = 4;
			this.FontBoldCheckBox.Text = "B";
			this.FontBoldCheckBox.UseVisualStyleBackColor = true;
			// 
			// FontSizeComboBox
			// 
			this.FontSizeComboBox.FormattingEnabled = true;
			this.FontSizeComboBox.Location = new System.Drawing.Point(244, 16);
			this.FontSizeComboBox.Name = "FontSizeComboBox";
			this.FontSizeComboBox.Size = new System.Drawing.Size(46, 21);
			this.FontSizeComboBox.TabIndex = 3;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(211, 19);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(27, 13);
			this.label8.TabIndex = 2;
			this.label8.Text = "Size";
			// 
			// FontNameComboBox
			// 
			this.FontNameComboBox.FormattingEnabled = true;
			this.FontNameComboBox.Location = new System.Drawing.Point(49, 18);
			this.FontNameComboBox.Name = "FontNameComboBox";
			this.FontNameComboBox.Size = new System.Drawing.Size(156, 21);
			this.FontNameComboBox.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 22);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(35, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Name";
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(202, 67);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(79, 23);
			this.button2.TabIndex = 26;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// OK_Button
			// 
			this.OK_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.OK_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK_Button.Location = new System.Drawing.Point(303, 67);
			this.OK_Button.Name = "OK_Button";
			this.OK_Button.Size = new System.Drawing.Size(79, 23);
			this.OK_Button.TabIndex = 25;
			this.OK_Button.Text = "OK";
			this.OK_Button.UseVisualStyleBackColor = true;
			this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
			// 
			// SelectFontForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(394, 102);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.OK_Button);
			this.Controls.Add(this.groupBox3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SelectFontForm";
			this.Text = "SelectFontForm";
			this.Load += new System.EventHandler(this.SelectFontForm_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox FontItalicCheckBox;
		private System.Windows.Forms.CheckBox FontBoldCheckBox;
		private System.Windows.Forms.ComboBox FontSizeComboBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox FontNameComboBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button OK_Button;
	}
}