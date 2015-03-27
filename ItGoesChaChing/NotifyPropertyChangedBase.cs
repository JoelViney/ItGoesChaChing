using System;
using System.ComponentModel;
using System.Linq.Expressions;
using ItGoesChaChing.Helpers;

namespace ItGoesChaChing
{
	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		///   Notifies subscribers of the property change.
		/// </summary>
		/// <typeparam name = "TProperty">The type of the property.</typeparam>
		/// <param name = "property">The property expression.</param>
		public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
		{
			NotifyOfPropertyChange(property.GetMemberInfo().Name);
		}

		/// <summary>
		///   Notifies subscribers of the property change.
		/// </summary>
		/// <param name = "propertyName">Name of the property.</param>
		public virtual void NotifyOfPropertyChange(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		///   Raises the <see cref="E:PropertyChanged" /> event directly.
		/// </summary>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		#endregion
	}
}
