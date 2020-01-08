namespace Termulator
{
	partial class TerminalForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalForm));
			this.SettingsButton = new System.Windows.Forms.Button();
			this.CommandEntryTextBox = new System.Windows.Forms.TextBox();
			this.CommandContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.favoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveScreenAsImageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TranscriptTextBox = new System.Windows.Forms.TextBox();
			this.TranscriptContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.clearTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.autoScrollToEndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showTimingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SelectFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.UnicodeUnitTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PortNameCombo = new System.Windows.Forms.ComboBox();
			this.OpenCloseButton = new System.Windows.Forms.Button();
			this.AboutPictureBox = new System.Windows.Forms.PictureBox();
			this.HelpPictureBox = new System.Windows.Forms.PictureBox();
			this.CommandContextMenu.SuspendLayout();
			this.TranscriptContextMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AboutPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HelpPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// SettingsButton
			// 
			this.SettingsButton.Location = new System.Drawing.Point(286, 6);
			this.SettingsButton.Name = "SettingsButton";
			this.SettingsButton.Size = new System.Drawing.Size(60, 24);
			this.SettingsButton.TabIndex = 7;
			this.SettingsButton.Text = "Settings";
			this.SettingsButton.UseVisualStyleBackColor = true;
			this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
			// 
			// CommandEntryTextBox
			// 
			this.CommandEntryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CommandEntryTextBox.ContextMenuStrip = this.CommandContextMenu;
			this.CommandEntryTextBox.Location = new System.Drawing.Point(9, 451);
			this.CommandEntryTextBox.Name = "CommandEntryTextBox";
			this.CommandEntryTextBox.Size = new System.Drawing.Size(402, 20);
			this.CommandEntryTextBox.TabIndex = 4;
			this.CommandEntryTextBox.Enter += new System.EventHandler(this.CommandEntryTextBox_FocusEnter);
			this.CommandEntryTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommandEntryTextBox_KeyDown);
			this.CommandEntryTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CommandEntryTextBox_KeyPress);
			// 
			// CommandContextMenu
			// 
			this.CommandContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.historyToolStripMenuItem,
            this.favoritesToolStripMenuItem,
            this.saveScreenAsImageMenuItem});
			this.CommandContextMenu.Name = "contextMenuStrip2";
			this.CommandContextMenu.Size = new System.Drawing.Size(196, 70);
			this.CommandContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.CommandContextMenu_Opening);
			// 
			// historyToolStripMenuItem
			// 
			this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
			this.historyToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.historyToolStripMenuItem.Tag = "0";
			this.historyToolStripMenuItem.Text = "History...";
			this.historyToolStripMenuItem.DropDownOpening += new System.EventHandler(this.historyToolStripMenuItem_DropDownOpening);
			// 
			// favoritesToolStripMenuItem
			// 
			this.favoritesToolStripMenuItem.Name = "favoritesToolStripMenuItem";
			this.favoritesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.favoritesToolStripMenuItem.Text = "Favorites...";
			this.favoritesToolStripMenuItem.DropDownOpening += new System.EventHandler(this.favoritesToolStripMenuItem_DropDownOpening);
			// 
			// saveScreenAsImageMenuItem
			// 
			this.saveScreenAsImageMenuItem.Name = "saveScreenAsImageMenuItem";
			this.saveScreenAsImageMenuItem.Size = new System.Drawing.Size(195, 22);
			this.saveScreenAsImageMenuItem.Text = "Save Screen as Image...";
			this.saveScreenAsImageMenuItem.Click += new System.EventHandler(this.saveScreenAsImageMenuItem_Click);
			// 
			// TranscriptTextBox
			// 
			this.TranscriptTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TranscriptTextBox.ContextMenuStrip = this.TranscriptContextMenu;
			this.TranscriptTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TranscriptTextBox.HideSelection = false;
			this.TranscriptTextBox.Location = new System.Drawing.Point(9, 37);
			this.TranscriptTextBox.Multiline = true;
			this.TranscriptTextBox.Name = "TranscriptTextBox";
			this.TranscriptTextBox.ReadOnly = true;
			this.TranscriptTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TranscriptTextBox.Size = new System.Drawing.Size(401, 407);
			this.TranscriptTextBox.TabIndex = 4;
			this.TranscriptTextBox.Enter += new System.EventHandler(this.TranscriptTextBox_FocusEnter);
			// 
			// TranscriptContextMenu
			// 
			this.TranscriptContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearTextToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.copyToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem,
            this.toolStripSeparator1,
            this.autoScrollToEndToolStripMenuItem,
            this.showTimingToolStripMenuItem,
            this.SelectFontToolStripMenuItem,
            this.toolStripSeparator3,
            this.loadFileToolStripMenuItem,
            this.saveToDiskToolStripMenuItem,
            this.UnicodeUnitTestToolStripMenuItem});
			this.TranscriptContextMenu.Name = "contextMenuStrip1";
			this.TranscriptContextMenu.Size = new System.Drawing.Size(265, 264);
			// 
			// clearTextToolStripMenuItem
			// 
			this.clearTextToolStripMenuItem.Name = "clearTextToolStripMenuItem";
			this.clearTextToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.clearTextToolStripMenuItem.Text = "Clear All";
			this.clearTextToolStripMenuItem.Click += new System.EventHandler(this.clearTextToolStripMenuItem_Click);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.selectAllToolStripMenuItem.Text = "Select All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(261, 6);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.copyToolStripMenuItem.Text = "Copy Selection to Clipboard";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// copyToClipboardToolStripMenuItem
			// 
			this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
			this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.copyToClipboardToolStripMenuItem.Text = "Copy All to Clipboard";
			this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(261, 6);
			// 
			// autoScrollToEndToolStripMenuItem
			// 
			this.autoScrollToEndToolStripMenuItem.Checked = true;
			this.autoScrollToEndToolStripMenuItem.CheckOnClick = true;
			this.autoScrollToEndToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.autoScrollToEndToolStripMenuItem.Name = "autoScrollToEndToolStripMenuItem";
			this.autoScrollToEndToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.autoScrollToEndToolStripMenuItem.Text = "Auto Scroll to End";
			// 
			// showTimingToolStripMenuItem
			// 
			this.showTimingToolStripMenuItem.CheckOnClick = true;
			this.showTimingToolStripMenuItem.Name = "showTimingToolStripMenuItem";
			this.showTimingToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.showTimingToolStripMenuItem.Text = "Show Command Timing";
			this.showTimingToolStripMenuItem.Click += new System.EventHandler(this.showTimingToolStripMenuItem_Click);
			// 
			// SelectFontToolStripMenuItem
			// 
			this.SelectFontToolStripMenuItem.Name = "SelectFontToolStripMenuItem";
			this.SelectFontToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.SelectFontToolStripMenuItem.Text = "Select Font";
			this.SelectFontToolStripMenuItem.Click += new System.EventHandler(this.SelectFontToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(261, 6);
			// 
			// loadFileToolStripMenuItem
			// 
			this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
			this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.loadFileToolStripMenuItem.Text = "Load Commands From File...";
			this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.DownloadFileToolStripMenuItem_Click);
			// 
			// saveToDiskToolStripMenuItem
			// 
			this.saveToDiskToolStripMenuItem.Name = "saveToDiskToolStripMenuItem";
			this.saveToDiskToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.saveToDiskToolStripMenuItem.Text = "Save All To File...";
			this.saveToDiskToolStripMenuItem.Click += new System.EventHandler(this.saveToDiskToolStripMenuItem_Click);
			// 
			// UnicodeUnitTestToolStripMenuItem
			// 
			this.UnicodeUnitTestToolStripMenuItem.Name = "UnicodeUnitTestToolStripMenuItem";
			this.UnicodeUnitTestToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.UnicodeUnitTestToolStripMenuItem.Text = "Unicode Unit Test";
			this.UnicodeUnitTestToolStripMenuItem.Click += new System.EventHandler(this.UnicodeUnitTestToolStripMenuItem_Click);
			// 
			// PortNameCombo
			// 
			this.PortNameCombo.DropDownWidth = 300;
			this.PortNameCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PortNameCombo.FormattingEnabled = true;
			this.PortNameCombo.Location = new System.Drawing.Point(9, 6);
			this.PortNameCombo.Name = "PortNameCombo";
			this.PortNameCombo.Size = new System.Drawing.Size(204, 24);
			this.PortNameCombo.TabIndex = 8;
			this.PortNameCombo.SelectedIndexChanged += new System.EventHandler(this.PortNameCombo_SelectedIndexChanged);
			// 
			// OpenCloseButton
			// 
			this.OpenCloseButton.Location = new System.Drawing.Point(219, 6);
			this.OpenCloseButton.Name = "OpenCloseButton";
			this.OpenCloseButton.Size = new System.Drawing.Size(62, 24);
			this.OpenCloseButton.TabIndex = 11;
			this.OpenCloseButton.Text = "<- Select Port";
			this.OpenCloseButton.UseVisualStyleBackColor = true;
			this.OpenCloseButton.Click += new System.EventHandler(this.OpenCloseButton_Click);
			// 
			// AboutPictureBox
			// 
			this.AboutPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AboutPictureBox.BackgroundImage = global::Termulator.Properties.Resources.info;
			this.AboutPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.AboutPictureBox.Location = new System.Drawing.Point(370, 10);
			this.AboutPictureBox.Name = "AboutPictureBox";
			this.AboutPictureBox.Size = new System.Drawing.Size(16, 16);
			this.AboutPictureBox.TabIndex = 10;
			this.AboutPictureBox.TabStop = false;
			this.AboutPictureBox.Click += new System.EventHandler(this.AboutPictureBox_Click);
			// 
			// HelpPictureBox
			// 
			this.HelpPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.HelpPictureBox.BackgroundImage = global::Termulator.Properties.Resources.help_16x16;
			this.HelpPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.HelpPictureBox.InitialImage = null;
			this.HelpPictureBox.Location = new System.Drawing.Point(392, 10);
			this.HelpPictureBox.Name = "HelpPictureBox";
			this.HelpPictureBox.Size = new System.Drawing.Size(16, 16);
			this.HelpPictureBox.TabIndex = 9;
			this.HelpPictureBox.TabStop = false;
			this.HelpPictureBox.Click += new System.EventHandler(this.HelpPictureBox_Click);
			// 
			// TerminalForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(417, 483);
			this.Controls.Add(this.OpenCloseButton);
			this.Controls.Add(this.AboutPictureBox);
			this.Controls.Add(this.HelpPictureBox);
			this.Controls.Add(this.PortNameCombo);
			this.Controls.Add(this.SettingsButton);
			this.Controls.Add(this.CommandEntryTextBox);
			this.Controls.Add(this.TranscriptTextBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(276, 167);
			this.Name = "TerminalForm";
			this.Text = "Termulator";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TerminalForm_FormClosed);
			this.Load += new System.EventHandler(this.TerminalForm_Load);
			this.CommandContextMenu.ResumeLayout(false);
			this.TranscriptContextMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.AboutPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HelpPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button SettingsButton;
		private System.Windows.Forms.TextBox CommandEntryTextBox;
		private System.Windows.Forms.TextBox TranscriptTextBox;
		private System.Windows.Forms.ComboBox PortNameCombo;
		private System.Windows.Forms.ContextMenuStrip TranscriptContextMenu;
		private System.Windows.Forms.ToolStripMenuItem clearTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem saveToDiskToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem autoScrollToEndToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.PictureBox HelpPictureBox;
		private System.Windows.Forms.PictureBox AboutPictureBox;
		private System.Windows.Forms.ContextMenuStrip CommandContextMenu;
		private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem favoritesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem UnicodeUnitTestToolStripMenuItem;
		private System.Windows.Forms.Button OpenCloseButton;
		private System.Windows.Forms.ToolStripMenuItem showTimingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SelectFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveScreenAsImageMenuItem;
    }
}

