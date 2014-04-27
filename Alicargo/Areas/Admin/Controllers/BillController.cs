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

		[HttpPost]
		public virtual ActionResult Cancel(long id)
		{
			return RedirectToAction(MVC.Admin.Bill.Preview(id));
		}

		[HttpPost]
		public virtual ActionResult Download(long id, BillModel model)
		{
			if(!ModelState.IsValid)
			{
				BindBag(id);

				return View("Preview", model);
			}

			return RedirectToAction(MVC.Admin.Bill.Preview(id));
		}

		[HttpGet]
		public virtual ViewResult Preview(long id)
		{
			var model = _modelFactory.GetModel(id);

			BindBag(id);

			return View(model);
		}

		[HttpPost]
		public virtual ActionResult Save(long id, BillModel model)
		{
			if(!ModelState.IsValid)
			{
				BindBag(id);

				return View("Preview", model);
			}

			return RedirectToAction(MVC.Admin.Bill.Preview(id));
		}

		[HttpPost]
		public virtual ActionResult Send(long id, BillModel model)
		{
			if(!ModelState.IsValid)
			{
				BindBag(id);

				return View("Preview", model);
			}

			return RedirectToAction(MVC.Admin.Bill.Preview(id));
		}

		private void BindBag(long id)
		{
			var billNumber = _settings.GetData<int>(SettingType.ApplicationNumberCounter);
			ViewBag.ApplicationId = id;
			ViewBag.BillNumber = billNumber;
		}
	}
}