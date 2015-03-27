using ItGoesChaChing.Ebay.ClientAlerts.Json;
using ItGoesChaChing.Model;
using ItGoesChaChing.Model.Alerts;
using ItGoesChaChing.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ItGoesChaChing.Converters
{
	public class AlertTemplateSelector : DataTemplateSelector
	{
		/// <summary>A template used to display errors.</summary>
		public DataTemplate ExceptionTemplate { get; set; }

		/// <summary>The generic template for ListingEventTypes.</summary>
		public DataTemplate ListingEventTypesBaseTemplate { get; set; }
		public DataTemplate DefaultAlertTemplate { get; set; }

		public DataTemplate AskSellerQuestionTemplate { get; set; }
		
		public DataTemplate FeedbackReceivedTemplate { get; set; }

		public DataTemplate FixedPriceTransactionTemplate { get; set; }
		public DataTemplate LoginTemplate { get; set; }
		public DataTemplate TickTemplate { get; set; }
		
		
		
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			AlertBase alert = item as AlertBase;

			if (alert == null)
				return this.DefaultAlertTemplate;

			if (alert is LoginAlert)
				return LoginTemplate;

			if (alert is TickAlert)
				return TickTemplate;

			if (alert is ExceptionAlert)
				return ExceptionTemplate;

			if (alert is MessageAlert)
				return AskSellerQuestionTemplate;

			if (alert is FeedbackReceivedAlert)
				return FeedbackReceivedTemplate;

			if (alert is ItemSoldAlert)
				return FixedPriceTransactionTemplate;

			return this.DefaultAlertTemplate;
		}
	}
}
