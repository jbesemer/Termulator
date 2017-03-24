namespace Termulator
{
	partial class DownloadForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadForm));
			this.BrowseButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.FilenameTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.DelayTextBox = new System.Windows.Forms.TextBox();
			this.TraceDataCheckBox = new System.Windows.Forms.CheckBox();
			this.DownloadButton = new System.Windows.Forms.Button();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// BrowseButton
			// 
			this.BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BrowseButton.Location = new System.Drawing.Point(331, 33);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(64, 27);
			this.BrowseButton.TabIndex = 0;
			this.BrowseButton.Text = "Browse";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Filename";
			// 
			// FilenameTextBox
			// 
			this.FilenameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FilenameTextBox.Location = new System.Drawing.Point(67, 6);
			this.FilenameTextBox.Name = "FilenameTextBox";
			this.FilenameTextBox.Size = new System.Drawing.Size(258, 20);
			this.FilenameTextBox.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Inter-Line Delay";
			// 
			// DelayTextBox
			// 
			this.DelayTextBox.Location = new System.Drawing.Point(99, 37);
			this.DelayTextBox.Name = "DelayTextBox";
			this.DelayTextBox.Size = new System.Drawing.Size(67, 20);
			this.DelayTextBox.TabIndex = 4;
			// 
			// TraceDataCheckBox
			// 
			this.TraceDataCheckBox.AutoSize = true;
			this.TraceDataCheckBox.Location = new System.Drawing.Point(172, 39);
			this.TraceDataCheckBox.Name = "TraceDataCheckBox";
			this.TraceDataCheckBox.Size = new System.Drawing.Size(102, 17);
			this.TraceDataCheckBox.TabIndex = 5;
			this.TraceDataCheckBox.Text = "Echo Data Sent";
			this.TraceDataCheckBox.UseVisualStyleBackColor = true;
			// 
			// DownloadButton
			// 
			this.DownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DownloadButton.Location = new System.Drawing.Point(331, 2);
			this.DownloadButton.Name = "DownloadButton";
			this.DownloadButton.Size = new System.Drawing.Size(64, 26);
			this.DownloadButton.TabIndex = 6;
			this.DownloadButton.Text = "Download";
			this.DownloadButton.UseVisualStyleBackColor = true;
			this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
			// 
			// ProgressBar
			// 
			this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ProgressBar.Location = new System.Drawing.Point(15, 63);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(380, 8);
			this.ProgressBar.Step = 1;
			this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.ProgressBar.TabIndex = 7;
			// 
			// DownloadForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(407, 77);
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.DownloadButton);
			this.Controls.Add(this.TraceDataCheckBox);
			this.Controls.Add(this.DelayTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.FilenameTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BrowseButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DownloadForm";
			this.Text = "DownloadForm";
			this.Load += new System.EventHandler(this.DownloadForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BrowseButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox FilenameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox DelayTextBox;
		private System.Windows.Forms.CheckBox TraceDataCheckBox;
		private System.Windows.Forms.Button DownloadButton;
		private System.Windows.Forms.ProgressBar ProgressBar;
	}
}