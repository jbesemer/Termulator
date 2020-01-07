using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Library;

namespace Termulator
{
	// TODO: need to save/restore settings, commit/cancel
	// callback to effect immediate changes, and undo

	public partial class SelectFontForm : Form
	{
		static object[] FontSizes = new object[] { 6F, 8F, 9F, 10F, 11F, 12F, 14F, 16F, 18F, 20F };

		public string SelectedFontName;
		public float SelectedFontSize;
		public bool FontBold;
		public bool FontItalic;
		public string SelectedFontFamily;

		public SelectFontForm()
		{
			InitializeComponent();
		}

		private void SelectFontForm_Load( object sender, EventArgs e )
		{
			this.CenterToParent();

			FontSizeComboBox.Items.AddRange( FontSizes );
			FontSizeComboBox.SelectedIndex = 2;

			var names = InstalledFontFamilies.Keys.ToArray<string>();
			FontNameComboBox.Items.AddRange( names );
			if( FontNameComboBox.Items.Count > 0 )
				FontNameComboBox.SelectedIndex = 0;
			//FontSizeComboBox.Items.AddRange( FontSizes );

			SelectFontName( SelectedFontName );
		}

		private void SelectFontName( string fontName )
		{
			FontNameComboBox.SelectedItem = fontName;
			if( string.IsNullOrEmpty( fontName ) )
				return;

			var family = new FontFamily( fontName );
			var size = SelectedFontSize;
			FontStyle style = GetFontStyle();
			var font = new Font( family, size, style );
		}

		private FontStyle GetFontStyle()
		{
			var style = FontStyle.Regular;
			if( FontBoldCheckBox.Checked )
				style |= FontStyle.Bold;
			if( FontItalicCheckBox.Checked )
				style |= FontStyle.Italic;
			return style;
		}

		private void FontNameComboBox_SelectedValueChanged( object sender, EventArgs e )
		{
			var name = FontNameComboBox.SelectedItem as string;
			SelectFontName( name );
		}

		private void OK_Button_Click( object sender, EventArgs e )
		{
			SelectedFontName = (string)FontNameComboBox.SelectedItem;
			SelectedFontSize = (float)FontSizeComboBox.SelectedItem;
			FontBold = FontBoldCheckBox.Checked;
			FontItalic = FontItalicCheckBox.Checked;
		}

		private static Dictionary<string, FontFamily> _InstalledFontFamilies;
		public static Dictionary<string, FontFamily> InstalledFontFamilies
		{
			get
			{
				if( _InstalledFontFamilies == null )
				{
					_InstalledFontFamilies = new Dictionary<string, FontFamily>();
					foreach( var family in FontFamily.Families )
						_InstalledFontFamilies[ family.Name ] = family;
				}
				return _InstalledFontFamilies;
			}
		}
	}
}
