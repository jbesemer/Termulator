using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Terminal
{
	public partial class SelectFontForm : Form
	{
		static string[] FontNames = new string[] {
			"Calibri", 
			"Consolas", 
			"Comic Sans MS", 
			"Courier New", 
			"Courier Std", 
			"Lucidia Console Regular",
			"Tahoma",
			"Times New Roman",
		};

		static object[] FontSizes = new object[] { 8, 9, 10, 11, 12, 14, 16 };

		public string SelectedFontName;
		public float SelectedFontSize;
		public bool FontBold;
		public bool FontItalic;

		public SelectFontForm()
		{
			InitializeComponent();

			FontNameComboBox.Items.AddRange( FontNames );
			FontSizeComboBox.Items.AddRange( FontSizes );
		}

		private void SelectFontForm_Load( object sender, EventArgs e )
		{
			FontNameComboBox.SelectedItem = SelectedFontName;
			FontSizeComboBox.SelectedItem = (int)SelectedFontSize;
			FontBoldCheckBox.Checked = FontBold;
			FontItalicCheckBox.Checked = FontItalic;
		}

		private void OK_Button_Click( object sender, EventArgs e )
		{
			SelectedFontName = (string)FontNameComboBox.SelectedItem;
			SelectedFontSize = (int)FontSizeComboBox.SelectedItem;
			FontBold = FontBoldCheckBox.Checked;
			FontItalic = FontItalicCheckBox.Checked;
		}
	}
}
