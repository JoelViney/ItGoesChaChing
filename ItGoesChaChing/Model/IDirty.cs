using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model
{
	public interface IDirty
	{
		bool Dirty { get; set; }
	}
}
