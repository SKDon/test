using System.Web.Mvc;
using Alicargo.Areas.Admin.Models;
using Alicargo.Areas.Admin.Serivices;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Areas.Admin.Controllers
{
	[Access(RoleType.Admin)]
	public partial class BillController : Controller
	{
		private readonly IBillModelFactory _modelFactory;
		private readonly ISettingRepository _settings;

		public BillController(ISettingRepository settings, IBillModelFactory modelFactory)
		{
			_settings = settings;
			_modelFactory = modelFactory;
		}

		[HttpGet]
		public virtual ViewResult Preview(long id)
		{
			var billNumber = _settings.GetData<int>(SettingType.ApplicationNumberCounter);
			var model = _modelFactory.GetModel(id);

			ViewBag.BillNumber = billNumber;

			return View(model);
		}

		[HttpPost]
		public virtual ActionResult Preview(long id, BillModel model)
		{
			if(!ModelState.IsValid)
			{
				return View(model);
			}

			return RedirectToAction(MVC.Admin.Bill.Preview(id));
		}
	}
}