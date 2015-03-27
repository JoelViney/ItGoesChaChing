using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ItGoesChaChing.Helpers
{
	public static class BitmapExtensions
	{
		/// <summary>
		/// Converts the Bitmap to a BitMapImage and returns the results.
		/// </summary>
		public static BitmapImage ToImage(this Bitmap bitmap)
		{
			BitmapImage bitmapImage = null;
			using (MemoryStream memory = new MemoryStream())
			{
				bitmap.Save(memory, ImageFormat.Png);
				memory.Position = 0;
				bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				// ms.Seek(0, SeekOrigin.Begin);
				bitmapImage.StreamSource = memory;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();

			}
			return bitmapImage;
		}
	}
}
