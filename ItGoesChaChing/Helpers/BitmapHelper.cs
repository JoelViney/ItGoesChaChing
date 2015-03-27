using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ItGoesChaChing.Helpers
{
	public static class BitmapHelper
	{
		public static Bitmap LoadFromFile(string filePathName)
		{
			Bitmap bitmap = null;

			using (var fileStream = new System.IO.FileStream(filePathName, System.IO.FileMode.Open))
			{
				using (Bitmap temp = new Bitmap(fileStream))
				{
					bitmap = new Bitmap(temp.Width, temp.Height, temp.PixelFormat);
					using (Graphics g = Graphics.FromImage(bitmap))
					{
						Rectangle imageRect = new Rectangle(0, 0, temp.Width, temp.Height);
						g.DrawImage(temp, imageRect);
						g.Flush();
					}
				}
			}

			return bitmap;
		}

		public static void SaveToJpeg(Bitmap bitmap, string filePathName)
		{
			// Encoder parameter for image quality
			EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, 100L);
			ImageCodecInfo jpegCodec = GetEncoderInfo(ImageFormat.Jpeg);

			EncoderParameters encoderParams = new EncoderParameters(1);
			encoderParams.Param[0] = qualityParam;

			bitmap.Save(filePathName, jpegCodec, encoderParams);
		}

		private static ImageCodecInfo GetEncoderInfo(ImageFormat imageFormat)
		{
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

			// Find the correct image codec
			foreach (ImageCodecInfo codec in codecs)
			{
				if (codec.FormatID == imageFormat.Guid)
					return codec;
			}

			return null;
		}

		/*
		// Old method
		public static Bitmap ResizeBitmap(Bitmap image, int width, int height)
		{
			Bitmap result = new Bitmap(width, height);

			// This apparently keeps the quality high.
			using (Graphics graphics = Graphics.FromImage(result))
			{
				//gr.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				//gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.DrawImage(image, new Rectangle(0, 0, width, height));
			}

			return result;
		}
		*/

		public static Bitmap GetBitmapFromByteArray(byte[] bytes)
		{
			ImageConverter imageConverter = new ImageConverter();
			Bitmap bitmap = null;

			using (Image image = (Image)imageConverter.ConvertFrom(bytes))
			{
				bitmap = new Bitmap(image);
				image.Dispose();
			}

			return bitmap;
		}

		public static Bitmap GetBitmapFromByteArray(byte[] bytes, double maxWidth, double maxHeight)
		{
			Bitmap bitmap = GetBitmapFromByteArray(bytes);

			if (bitmap.Height > maxWidth | bitmap.Width > maxHeight)
			{
				Bitmap sizedBitmap = BitmapHelper.GetSizedBitmap(bitmap, maxWidth, maxHeight);
				bitmap.Dispose();
				bitmap = null;

				return sizedBitmap;
			}
			return bitmap;
		}

		/// <summary>
		/// Returns a bitmap with the specified maximum resolutions.
		/// </summary>
		public static Bitmap GetSizedBitmap(Bitmap bitmap, double maxWidth, double maxHeight)
		{
			if (bitmap == null)
				return null;

			double coefHeight = (maxHeight / (double)bitmap.Height);
			double coefWidth = (maxWidth / (double)bitmap.Width);
			double coef;

			if (coefHeight < coefWidth)
				coef = coefHeight;
			else
				coef = coefWidth;

			int height = System.Convert.ToInt32(bitmap.Height * coef);
			int width = System.Convert.ToInt32(bitmap.Width * coef);

			Bitmap result = ResizeBitmap(bitmap, width, height);

			return result;
		}

		private static Bitmap ResizeBitmap(Image image, int width, int height)
		{
			Bitmap result = new Bitmap(width, height);
			// Set the resolutions the same to avoid cropping due to resolution differences 
			result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (Graphics graphics = Graphics.FromImage(result))
			{
				// Set the resize quality modes to high quality 
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;

				graphics.DrawImage(image, 0, 0, result.Width, result.Height);
			}

			return result;
		}


	}
}
