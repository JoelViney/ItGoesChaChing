using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	public class AccessRule
	{
		public string CallName { get; set; }
		public long HourlySoftLimit { get; set; }
		public long HourlyHardLimit { get; set; }
		public long HourlyUsage { get; set; }
		public long DailySoftLimit { get; set; }
		public long DailyHardLimit { get; set; }
		public long DailyUsage { get; set; }
		public bool CountsTowardAggregate { get; set; }
	}
}
