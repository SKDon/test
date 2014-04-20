using System.Web.Mvc;
using Alicargo.Areas.Admin.Models;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Utilities;

namespace Alicargo.Areas.Admin.Controllers
{
	[Access(RoleType.Admin)]
	public partial class BillSettingsController : Controller
	{
		private readonly ISerializer _serializer;
		private readonly ISettingRepository _settings;

		public BillSettingsController(ISerializer serializer, ISettingRepository settings)
		{
			_serializer = serializer;
			_settings = settings;
		}

		[HttpGet]
		public virtual ActionResult Index()
		{
			var setting = _settings.Get(SettingType.Bill);

			var data = _serializer.Deserialize<BillSettings>(setting.Data) ?? new BillSettings();

			var model = new BillSettingsModel
			{
				Data = data,
				Version = setting.RowVersion
			};

			return View(model);
		}

		[HttpPost]
		public virtual ActionResult Index(BillSettingsModel model)
		{
			if(!ModelState.IsValid)
			{
				return View(model);
			}

			var data = _serializer.Serialize(model.Data);

			_settings.AddOrReplace(new Setting
			{
				Data = data,
				RowVersion = model.Version,
				Type = SettingType.Bill
			});

			return RedirectToAction(MVC.Admin.BillSettings.Index());
		}
	}
}