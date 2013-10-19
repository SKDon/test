using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Alicargo.MvcHelpers.ModelBinders;
using Alicargo.ViewModels.AirWaybill;
using Alicargo.ViewModels.Application;
using WebGrease.Css.Extensions;

namespace Alicargo.App_Start
{
    internal static class BinderConfig
	{
		public static void RegisterBinders(ModelBinderDictionary binders)
		{
			var types = Assembly.GetCallingAssembly().GetTypes();
			types.Where(x=>x.BaseType == typeof(DefaultModelBinder)).ForEach(x =>
			{
				var binder = (IModelBinder)Activator.CreateInstance(x);
				var modelName = x.Name.Replace("Binder", "");
				var type = types.First(y => y.Name == modelName);

				binders.Add(type, binder);	
			});

			//binders.Add(typeof(AwbAdminModel), new AwbAdminModelBinder());
			//binders.Add(typeof(AwbBrokerModel), new AwbBrokerModelBinder());
			//bindemodelNamers.Add(typeof(AwbSenderModel), new AwbSenderModelBinder());
			//binders.Add(typeof(ApplicationAdminModel), new ApplicationAdminModelBinder());
			//binders.Add(typeof(ApplicationSenderModel), new ApplicationSenderModelBinder());
		}
	}
}