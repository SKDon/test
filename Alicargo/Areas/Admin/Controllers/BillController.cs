using System.Web.Mvc;
using Alicargo.Areas.Admin.Models;
using Alicargo.Areas.Admin.Serivices.Abstract;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.MvcHelpers.Extensions;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Utilities;
using Resources;

namespace Alicargo.Areas.Admin.Controllers
{
	[Access(RoleType.Admin)]
	public partial class BillController : Controller
	{
		private readonly IApplicationRepository _applications;
		private readonly IBillRepository _bills;
		private readonly IBillManager _manager;
		private readonly IBillModelFactory _modelFactory;
		private readonly IBillPdf _pdf;

		public BillController(
			IBillPdf pdf,
			IBillModelFactory modelFactory,
			IApplicationRepository applications,
			IBillRepository bills,
			IBillManager manager)
		{
			_pdf = pdf;
			_modelFactory = modelFactory;
			_applications = applications;
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
				var application = _applications.Get(id);
				ViewBag.BillNumber = application.DisplayNumber;
				ViewBag.Date = DateTimeProvider.Now;
			}
			else
			{
				model = _modelFactory.GetBillModel(bill);
				ViewBag.BillNumber = bill.Number;
				ViewBag.Date = bill.SaveDate;
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
			if(!SaveImpl(id, model))
			{
				return View("Preview", model);
			}

			_manager.Send(id);

			return RedirectToAction(MVC.Admin.Bill.Sent(id));
		}

		[HttpGet]
		public virtual ActionResult Sent(long id)
		{
			var bill = _bills.Get(id);

			return View(bill);
		}

		private bool SaveImpl(long id, BillModel model)
		{
			if(!model.PriceRuble.HasValue || model.PriceRuble.Value <= 0)
			{
				ModelState.AddModelError("PriceRuble", Validation.InvalidValue);
			}

			var bill = _bills.Get(id);
			var application = _applications.Get(id);
			var number = bill != null ? bill.Number : application.DisplayNumber;
			var date = bill != null ? bill.SaveDate : DateTimeProvider.Now;

			if(!ModelState.IsValid)
			{
				ViewBag.ApplicationId = id;
				ViewBag.BillNumber = number;
				ViewBag.Date = date;

				return false;
			}

			_manager.Save(id, number, model, date);

			return true;
		}
	}
}