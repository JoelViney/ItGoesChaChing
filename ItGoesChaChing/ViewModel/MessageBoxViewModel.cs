using ItGoesChaChing.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ItGoesChaChing.ViewModel
{

	public enum FocusedButton
	{
		None = 0,
		One,
		Two,
		Three
	}

	/// <summary>
	/// Implements a custom version of the standard System.Windows.MessageBox
	/// This should be called through the static method MessageBoxEx.Show(...)
	/// </summary>
	public class MessageBoxViewModel : ViewModelBase
	{
		public Action CloseAction { get; set; }

		/// <summary>The return result indicating which button was pressed in the message box.</summary>
		public MessageBoxResult Result { get; private set; }

		private string _text;
		private string _caption;
		private MessageBoxButton _button;
		private MessageBoxImage _iconImage;
		private MessageBoxResult _defaultResult;
		private MessageBoxOptions _options;

		public MessageBoxViewModel()
		{

		}

		public MessageBoxViewModel(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
		{
			this._text = messageBoxText;
			this._caption = caption;
			this._button = button;
			this._iconImage = icon;
			this._defaultResult = defaultResult;
			this._options = options;

			// TODO: Implement the options!
			if (options == MessageBoxOptions.DefaultDesktopOnly
				|| options == MessageBoxOptions.RtlReading
				|| options == MessageBoxOptions.ServiceNotification)
			{
				throw new NotFiniteNumberException("The MessageBoxEx has not implmeneted the Options settings.");
			}
		}

		public string Caption
		{
			get { return this._caption; }
			set { this._caption = value; }
		}

		public string Text
		{
			get { return this._text; }
			set { this._text = value; }
		}

		public HorizontalAlignment TextAlignment
		{
			get
			{
				if (this._options == MessageBoxOptions.RightAlign)
					return HorizontalAlignment.Right;

				return HorizontalAlignment.Left;
			}
		}
		public string Button1Text
		{
			get
			{
				switch (this._button)
				{
					case MessageBoxButton.OK: return "OK";
					case MessageBoxButton.OKCancel: return "OK";
					case MessageBoxButton.YesNoCancel: return "Yes";
					case MessageBoxButton.YesNo: return "Yes";
				}
				return "OK";
			}
		}
		public string Button2Text
		{
			get
			{
				switch (this._button)
				{
					case MessageBoxButton.OKCancel: return "Cancel";
					case MessageBoxButton.YesNoCancel: return "No";
					case MessageBoxButton.YesNo: return "No";
				}
				return "";
			}
		}
		public string Button3Text
		{
			get
			{
				switch (this._button)
				{
					case MessageBoxButton.YesNoCancel: return "Cancel";
				}
				return "OK";
			}
		}

		public bool Button2IsVisible
		{
			get
			{
				return (this._button == MessageBoxButton.OKCancel
					|| this._button == MessageBoxButton.YesNo
					|| this._button == MessageBoxButton.YesNoCancel);
			}
		}
		public bool Button3IsVisible
		{
			get { return (this._button == MessageBoxButton.YesNoCancel); }
		}

		public FocusedButton FocusedButton
		{
			get
			{
				switch (this._defaultResult)
				{
					case MessageBoxResult.OK: return FocusedButton.One;
					case MessageBoxResult.Cancel: return FocusedButton.Three;
					case MessageBoxResult.Yes: return FocusedButton.One;
					case MessageBoxResult.No: return FocusedButton.Two;
					default: return FocusedButton.None;
				}
			}
		}

		public BitmapImage IconImageSource
		{
			get
			{
				BitmapImage image = null;
				Bitmap bitmap = null;
				switch (this._iconImage)
				{
					case MessageBoxImage.Error: bitmap = SystemIcons.Error.ToBitmap(); break;
					case MessageBoxImage.Exclamation: bitmap = SystemIcons.Exclamation.ToBitmap(); break;
					case MessageBoxImage.Information: bitmap = SystemIcons.Information.ToBitmap(); break;
					case MessageBoxImage.Question: bitmap = SystemIcons.Question.ToBitmap(); break;
					default: bitmap = null; break;
				}

				if (bitmap != null)
				{
					image = bitmap.ToImage();
				}
				return image;
			}
		}

		public bool IconImageIsVisible
		{
			get { return (this._iconImage != MessageBoxImage.None); }
		}

		public void Button1()
		{
			switch (this._button)
			{
				case MessageBoxButton.OK: this.Result = MessageBoxResult.OK; break;
				case MessageBoxButton.OKCancel: this.Result = MessageBoxResult.OK; break;
				case MessageBoxButton.YesNoCancel: this.Result = MessageBoxResult.Yes; break;
				case MessageBoxButton.YesNo: this.Result = MessageBoxResult.Yes; break;
				default: this.Result = MessageBoxResult.None; break;
			}
			this.Close();
		}

		public void Button2()
		{
			switch (this._button)
			{
				case MessageBoxButton.OKCancel: this.Result = MessageBoxResult.Cancel; break;
				case MessageBoxButton.YesNoCancel: this.Result = MessageBoxResult.No; break;
				case MessageBoxButton.YesNo: this.Result = MessageBoxResult.No; break;
				default: this.Result = MessageBoxResult.None; break;
			}
			this.Close();
		}

		public void Button3()
		{
			switch (this._button)
			{
				case MessageBoxButton.YesNoCancel: this.Result = MessageBoxResult.Cancel; break;
				default: this.Result = MessageBoxResult.None; break;
			}
			this.Close();
		}

		/// <summary>Closes the related View.</summary>
		private void Close()
		{
			if (this.CloseAction != null)
				this.CloseAction();
		}
	}

}
