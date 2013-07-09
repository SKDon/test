using System.Web.Mvc;
using Alicargo.ModelBinders;
using Alicargo.ViewModels;

namespace Alicargo.App_Start
{
	public static class BinderConfig
	{
		public static void RegisterBinders(ModelBinderDictionary binders)
		{
			binders.Add(typeof(ReferenceModel), new ReferenceModelBinder());
			binders.Add(typeof(BrockerAWBModel), new BrockerAWBModelBinder());
			binders.Add(typeof(ApplicationModel), new ApplicationModelBinder());
			binders.Add(typeof(ApplicationSenderEdit), new ApplicationSenderEditModelBinder());
		}
	}
}