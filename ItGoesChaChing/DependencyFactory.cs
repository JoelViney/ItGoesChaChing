using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItGoesChaChing.Model;

namespace ItGoesChaChing
{
	/// <summary>
	/// Simple IOC implementation.
	/// </summary>
	public class DependencyFactory
	{
		private class DependencyContainer
		{
			internal Type InstanceType { get; set; }
			internal object Instance { get; set; }
		}

		private static List<DependencyContainer> _items;

		private static List<DependencyContainer> Items
		{
			get 
			{
				// This is put here instead of the constructor because xaml in design time won't load a static constructor.
				if (_items == null)
					_items = new List<DependencyContainer>();
				return _items;  
			}
			set { _items = value; }
		}

		public static void RegisterInstance(object instance)
		{
			DependencyContainer container = new DependencyContainer() { InstanceType = instance.GetType(), Instance = instance };
			Items.Add(container);
		}

		public static void RegisterInstance(Type type, object instance)
		{
			DependencyContainer container = new DependencyContainer() { InstanceType = type, Instance = instance };
			Items.Add(container);
		}
		
		/// <summary>
		/// Resolves the type parameter T to an instance of the appropriate type.
		/// </summary>
		/// <typeparam name="T">Type of object to return</typeparam>
		public static T Resolve<T>()
		{
			T result = default(T);

			foreach (DependencyContainer item in Items)
			{
				if (typeof(T) == item.InstanceType)
				{
					result = (T)item.Instance;
					break;
				}
			}

			return result;
		}
	}
}