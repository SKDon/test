﻿using System.Web.Mvc;
using Alicargo.ModelBinders;
using Alicargo.ViewModels.AirWaybill;
using Alicargo.ViewModels.Application;

namespace Alicargo.App_Start
{
    internal static class BinderConfig
	{
		public static void RegisterBinders(ModelBinderDictionary binders)
		{
			binders.Add(typeof(AirWaybillEditModel), new AirWaybillModelBinder());
			binders.Add(typeof(BrockerAwbModel), new BrockerAWBModelBinder());
			binders.Add(typeof(SenderAwbModel), new SenderAwbModelBinder());
			binders.Add(typeof(ApplicationEditModel), new ApplicationModelBinder());
			binders.Add(typeof(ApplicationSenderEdit), new ApplicationSenderEditModelBinder());
		}
	}
}