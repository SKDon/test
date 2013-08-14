using System.Web.Mvc;
using Alicargo.ModelBinders;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Alicargo.ViewModels.Application;

namespace Alicargo.App_Start
{
	public static class BinderConfig
	{
		public static void RegisterBinders(ModelBinderDictionary binders)
		{
			binders.Add(typeof(AirWaybillEditModel), new AirWaybillModelBinder());
			binders.Add(typeof(BrockerAWBModel), new BrockerAWBModelBinder());
			binders.Add(typeof(ApplicationEditModel), new ApplicationModelBinder());
			binders.Add(typeof(ApplicationSenderEdit), new ApplicationSenderEditModelBinder());
		}
	}
}