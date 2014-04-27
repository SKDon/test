using System.Web.Mvc;
using Alicargo.Areas.Admin.Models;
using Alicargo.Areas.Admin.Serivices;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.MvcHelpers.Filters;
using Resources;

namespace Alicargo.Areas.Admin.Controllers
{
	[Access(RoleType.Admin)]
	public partial class BillController : Controller
	{
		private readonly IBillRepository _bills;
		private readonly IBillManager _manager;
		private readonly IBillModelFactory _modelFactory;
		private readonly ISettingRepository _settings;

		public BillController(
			ISettingRepository settings, IBillModelFactory modelFactory, IBillRepository bills, IBillManager manager)
		{
			_settings = settings;
			_modelFactory = modelFactory;
			_bills = bills;
			_manager = manager;
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
				BindDefaultBag(id);

				return View("Preview", model);
			}

			return RedirectToAction(MVC.Admin.Bill.Preview(id));
		}

		[HttpGet]
		public virtual ViewResult Preview(long id)
		{
			var bill = _bills.Get(id);

			BillModel model;
			if(bill == null)
			{
				model = _modelFactory.GetBillModelByApplication(id);
				BindDefaultBag(id);
			}
			else
			{
				model = _modelFactory.GetBillModel(bill);
				ViewBag.ApplicationId = id;
				ViewBag.BillNumber = bill.Number;
			}

			return View(model);
		}

		[HttpPost]
		public virtual ActionResult Save(long id, BillModel model)
		{
			if(!model.PriceRuble.HasValue || model.PriceRuble.Value <= 0)
			{
				ModelState.AddModelError("PriceRuble", Validation.InvalidValue);
			}

			if(!ModelState.IsValid)
			{
				BindDefaultBag(id);

				return View("Preview", model);
			}

			_manager.SaveBill(id, model);

			return RedirectToAction(MVC.Admin.Bill.Preview(id));
		}

		[HttpPost]
		public virtual ActionResult Send(long id, BillModel model)
		{
			if(!ModelState.IsValid)
			{
				BindDefaultBag(id);

				return View("Preview", model);
			}

			return RedirectToAction(MVC.Admin.Bill.Preview(id));
		}

		private void BindDefaultBag(long id)
		{
			ViewBag.ApplicationId = id;
			ViewBag.BillNumber = _settings.GetData<int>(SettingType.BillLastNumber) + 1;
		}
	}
}