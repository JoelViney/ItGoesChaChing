using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Alerts
{
	public class Media : ModelBase, INotifyPropertyChanged
	{
		public Bitmap Image { get; set; }
		public string Url { get; set; }

		
		#region Constructors...

		public Media() : base()
		{
			// Nothing to do here.
		}

		public Media(Bitmap image, string url) : base()
		{
			this.Image = image;
			this.Url = url;
		}

		#endregion
	}
}
