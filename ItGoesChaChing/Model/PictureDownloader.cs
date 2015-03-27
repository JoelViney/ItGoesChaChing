using System;
using System.IO;
using System.Drawing;
using ItGoesChaChing.Helpers;

namespace ItGoesChaChing.Model
{
	/// <summary>
	/// This class is used to download images from the provided URL.
	/// </summary>
	public class PictureDownloader
	{
		private const int MAX_STORAGE_HEIGHT = 100;
		private const int MAX_STORAGE_WIDTH = 100;

		#region Constructor...

		public PictureDownloader()
		{

		}

		#endregion

		/// <summary>
		/// Attempts three times to download an image from the provided URL.
		/// </summary>
		public Bitmap DownloadImage(string url)
		{
			// Sanity check
			if (url == null || url == "")
			{
				return null;
			}

			int attempts = 0;

			while (attempts <= 3)
			{
				attempts++;
				try
				{
					byte[] byteArray = GetRemoteImage(url);

					if (byteArray == null)
					{
						// No bytes returned.
					}
					else
					{
						Bitmap bitmap = BitmapHelper.GetBitmapFromByteArray(byteArray);

						if (bitmap.Height > MAX_STORAGE_HEIGHT || bitmap.Width > MAX_STORAGE_WIDTH)
						{
							Bitmap sizedBitmap = BitmapHelper.GetSizedBitmap(bitmap, MAX_STORAGE_WIDTH, MAX_STORAGE_HEIGHT);
							bitmap.Dispose();
							bitmap = null;

							return sizedBitmap;
						}

						return bitmap;
					}

				}
				catch
				{
					// :S Do we throw or ignore?
				}
			}

			return null;
		}


		private byte[] GetRemoteImage(string galleryPictureUrl)
		{
			if (galleryPictureUrl == null || galleryPictureUrl == "")
			{
				return null; // No remote image to grab
			}

			// Get the image from the Url
			byte[] bytes;

			System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(galleryPictureUrl);
			webRequest.Timeout = 3000; // Set the timeout to 3 seconds. TODO: Make this a parameter
			System.Net.WebResponse webResponse = webRequest.GetResponse();

			Stream stream = webResponse.GetResponseStream();

			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				bytes = binaryReader.ReadBytes(500000);
				binaryReader.Close();
			}

			webResponse.Close();

			return bytes;
		}

	}
}
