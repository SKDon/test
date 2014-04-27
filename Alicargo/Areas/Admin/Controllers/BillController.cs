using System.Web.Mvc;
using Alicargo.Areas.Admin.Models;
using Alicargo.Areas.Admin.Serivices.Abstract;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.MvcHelpers.Extensions;
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
		private readonly IBillPdf _pdf;
		private readonly ISettingRepository _settings;

		public BillController(
			ISettingRepository settings,
			IBillPdf pdf,
			IBillModelFactory modelFactory,
			IBillRepository bills,
			IBillManager manager)
		{
			_settings = settings;
			_pdf = pdf;
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
			if(!SaveImpl(id, model))
			{
				return View("Preview", model);
			}

			var file = _pdf.Get(id);

			return file.GetFileResult();
		}

		[HttpGet]
		public virtual ViewResult Preview(long id)
		{
			var bill = _bills.Get(id);

			BillModel model;
			if(bill == null)
			{
				model = _modelFactory.GetBillModelByApplication(id);
				ViewBag.BillNumber = _settings.GetData<int>(SettingType.BillLastNumber) + 1;
			}
			else
			{
				model = _modelFactory.GetBillModel(bill);
				ViewBag.BillNumber = bill.Number;
			}

			ViewBag.ApplicationId = id;

			return View(model);
		}

		[HttpPost]
		public virtual ActionResult Save(long id, BillModel model)
		{
			if(!SaveImpl(id, model))
			{
				return View("Preview", model);
			}

			return RedirectToAction(MVC.Admin.Bill.Preview(id));
		}

		[HttpPost]
		public virtual ActionResult Send(long id, BillModel model)
		{
			Save(id, model);

			return RedirectToAction(MVC.Admin.Bill.Preview(id));
		}

		private bool SaveImpl(long id, BillModel model)
		{
			if(!model.PriceRuble.HasValue || model.PriceRuble.Value <= 0)
			{
				ModelState.AddModelError("PriceRuble", Validation.InvalidValue);
			}

			var bill = _bills.Get(id);
			var number = bill != null ? bill.Number : _settings.GetNextBillNumber();

			if(!ModelState.IsValid)
			{
				ViewBag.ApplicationId = id;
				ViewBag.BillNumber = number;

				return false;
			}

			_manager.SaveBill(id, number, model);

			return true;
		}
	}
}