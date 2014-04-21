﻿using System.Web.Mvc;
using Alicargo.Areas.Admin.Models;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Utilities;

namespace Alicargo.Areas.Admin.Controllers
{
	public partial class BillController : Controller
	{
		private readonly ISettingRepository _settings;
		private readonly ISerializer _serializer;
		private readonly IApplicationRepository _applications;

		public BillController(ISettingRepository settings, ISerializer serializer, IApplicationRepository applications)
		{
			_settings = settings;
			_serializer = serializer;
			_applications = applications;
		}

		public virtual PartialViewResult Preview(long applicationId)
		{
			var billSettings = GetData<BillSettings>(SettingType.Bill);
			var application = _applications.Get(applicationId);
			var billNumber = GetData<int>(SettingType.ApplicationNumberCounter);

			ViewBag.BillNumber = billNumber;

			return PartialView(new BillModel
			{
				Settings = billSettings
			});
		}

		private T GetData<T>(SettingType type)
		{
			return _serializer.Deserialize<T>(_settings.Get(type).Data);
		}
	}
}