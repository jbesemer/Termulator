using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Microsoft.Win32;

namespace Library
{
	//http://www.grumpydev.com/2009/01/03/taking-wpf-screenshots/
	//http://stackoverflow.com/questions/9094494/how-make-a-screenshot-of-uielement-in-wpf
	//http://blogs.msdn.com/b/kirillosenkov/archive/2009/10/12/saving-images-bmp-png-etc-in-wpf-silverlight.aspx
	//http://blogs.msdn.com/b/saveenr/archive/2008/09/18/wpf-xaml-saving-a-window-or-canvas-as-a-png-bitmap.aspx
	
	public class ScreenShot
	{
		#region // Constants //////////////////////////////////////////////////

		public const string Filter
			= "PNG Files|*.png"
				+ "|Jpeg files|.jpg"
				+ "|Bmp Files|*.bmp"
				+ "|Gif Files|*.gif";
				// + "|Tiff Files|*.tif"

		public const string DefaultExtension = "png";

		#endregion

		#region // Save Screen Shot to File ///////////////////////////////////

		// prompt user for destination filename with common dialog then save to file

		[API( @"prompt user for destination filename and format 
using standard SaveFileDialog.
Save to the file if user clicks OK.

Caller determines UIElement to digitize, and default filename for image file." )]
		public static void SaveScreenShotDialog( UIElement element, string filename )
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				InitialDirectory 
					= Environment.GetFolderPath(
						Environment.SpecialFolder
							.DesktopDirectory ),
				Title = "Choose  filename and location",
				DefaultExt = DefaultExtension,
				FileName = filename,
				Filter = Filter,
				AddExtension = true,
			};

			bool? result = dialog.ShowDialog();

			if( result.HasValue && result.Value )
				SaveToFile( element, dialog.FileName );
		}

		#endregion

		#region // Render UIElement into External File ////////////////////////

		[API( @"Render UIElement into External File. 
Filename extension determines file format." )]
		public static void SaveToFile( UIElement element, string filename )
		{
			string ext = Path.GetExtension( filename );
			BitmapEncoder encoder = null;

			switch( ext.ToLower() )
			{
			case ".jpg": encoder = new JpegBitmapEncoder(); break;
			case ".bmp": encoder = new BmpBitmapEncoder(); break;
			case ".png": encoder = new PngBitmapEncoder(); break;
			case ".gif": encoder = new GifBitmapEncoder(); break;
//			case ".tif": encoder = new TifJpegBitmapEncoder();	break;
			}

			if( encoder == null )
				return;

			var target = ScreenShot.RenderElement( element, 1.0 );

			encoder.Frames.Add( BitmapFrame.Create( target ) );

			using( FileStream fstream = File.OpenWrite( filename ) )
				encoder.Save( fstream );
		}

		#endregion

		#region // Render UIElement to Bitmap /////////////////////////////////

		[API( @"Render UIElement to Bitmap. ")]
		public static RenderTargetBitmap RenderElement( UIElement element )
		{
			double actualHeight = element.RenderSize.Height;
			double actualWidth = element.RenderSize.Width;

			RenderTargetBitmap bitmap
				= new RenderTargetBitmap(
					(int)actualWidth, (int)actualHeight,
					96, 96,
					PixelFormats.Pbgra32 );

			bitmap.Render( element );

			return bitmap;
		}

		[API( @"Render UIElement to Bitmap, applying arbitrary scale factor (while retaining aspect ratio). " )]
		public static RenderTargetBitmap RenderElement( UIElement element, double scale )
		{
			double actualHeight = element.RenderSize.Height;
			double actualWidth = element.RenderSize.Width;

			double renderHeight = actualHeight * scale;
			double renderWidth = actualWidth * scale;

			RenderTargetBitmap bitmap
				= new RenderTargetBitmap(
					(int)renderWidth, (int)renderHeight,
					96, 96,
					PixelFormats.Pbgra32 );

			VisualBrush sourceBrush = new VisualBrush( element );

			DrawingVisual drawingVisual = new DrawingVisual();
			DrawingContext drawingContext = drawingVisual.RenderOpen();

			using( drawingContext )
			{
				drawingContext.PushTransform( new ScaleTransform( scale, scale ) );

				drawingContext.DrawRectangle(
					Brushes.White,
					null,
					new Rect(
						new Point( 0, 0 ),
						new Point( actualWidth, actualHeight ) ) );

				drawingContext.DrawRectangle(
					sourceBrush,
					null,
					new Rect(
						new Point( 0, 0 ),
						new Point( actualWidth, actualHeight ) ) );
			}
			bitmap.Render( drawingVisual );

			return bitmap;
		}

#if OBSOLETE
		public static RenderTargetBitmap RenderVisaulToBitmap( Visual visual, int width, int height )
		{
			RenderTargetBitmap rtb
				= new RenderTargetBitmap(
					width, height,
					96, 96,
					PixelFormats.Pbgra32 );

			rtb.Render( visual );

			return rtb;
		}

		public static MemoryStream GeneratePng( Visual visual, int width, int height )
		{
			BitmapEncoder encoder = new PngBitmapEncoder();
			if( encoder == null ) return null;

			RenderTargetBitmap rtb = RenderVisaulToBitmap( visual, width, height );
			MemoryStream stream = new MemoryStream();
			encoder.Frames.Add( BitmapFrame.Create( rtb ) );
			encoder.Save( stream );

			return stream;
		}
#endif
		#endregion
	}
}
