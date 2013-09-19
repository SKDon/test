using System.Web.Mvc;
using Alicargo.ModelBinders;
using Alicargo.ViewModels.AirWaybill;
using Alicargo.ViewModels.Application;

namespace Alicargo.App_Start
{
    internal static class BinderConfig
	{
		public static void RegisterBinders(ModelBinderDictionary binders)
		{
			binders.Add(typeof(AwbAdminModel), new AwbAdminModelBinder());
			binders.Add(typeof(AwbBrokerModel), new AwbBrokerModelBinder());
			binders.Add(typeof(AwbSenderModel), new AwbSenderModelBinder());
			binders.Add(typeof(ApplicationAdminModel), new ApplicationAdminModelBinder());
			binders.Add(typeof(ApplicationSenderModel), new ApplicationSenderModelBinder());
		}
	}
}