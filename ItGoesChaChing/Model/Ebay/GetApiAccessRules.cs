using eBay.Service.Call;
using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	public class GetApiAccessRules : EbayCommandBase
	{
		public ObservableCollection<AccessRule> AccessRules { get; set; }

		#region Constructors...

		public GetApiAccessRules(EbayContext context)
			: base(context)
		{

		}

		#endregion


		protected override void ExecuteInternal()
		{
			ObservableCollection<AccessRule> list = new ObservableCollection<AccessRule>();

			GetApiAccessRulesCall apicall = new GetApiAccessRulesCall(this.ApiContext);
			
			ApiAccessRuleTypeCollection rules = apicall.GetApiAccessRules();

			foreach (ApiAccessRuleType ruleType in rules)
			{
				AccessRule rule = new AccessRule();
				rule.CallName = ruleType.CallName;
				rule.HourlySoftLimit = ruleType.HourlySoftLimit;
				rule.HourlyHardLimit = ruleType.HourlyHardLimit;
				rule.HourlyUsage = ruleType.HourlyUsage;
				rule.DailySoftLimit = ruleType.DailySoftLimit;
				rule.DailyHardLimit = ruleType.DailyHardLimit;
				rule.DailyUsage = ruleType.DailyUsage;
				rule.CountsTowardAggregate = ruleType.CountsTowardAggregate;
				list.Add(rule);
			}

			this.AccessRules = list;
		}
	}
}
