using System.Web.Mvc;
using Alicargo.Areas.Admin.Models;
using Alicargo.Areas.Admin.Serivices;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Areas.Admin.Controllers
{
	[Access(RoleType.Admin)]
	public partial class BillController : Controller
	{
		private readonly IBillModelFactory _modelFactory;
		private readonly IBillRepository _bills;
		private readonly ISettingRepository _settings;

		public BillController(ISettingRepository settings, IBillModelFactory modelFactory, IBillRepository bills)
		{
			_settings = settings;
			_modelFactory = modelFactory;
			_bills = bills;
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

			_bills.AddOrReplace(id, new BillData
			{
				Accountant = model.Accountant,
				Bank = model.BankDetails.Bank,
				BIC = model.BankDetails.BIC,
				CorrespondentAccount = model.BankDetails.CorrespondentAccount,
				CurrentAccount = model.BankDetails.CurrentAccount,
				Head = model.Head,
				HeaderText = model.HeaderText,
				Payee = model.BankDetails.Payee,
				Shipper = model.Shipper,
				TaxRegistrationReasonCode = model.BankDetails.TaxRegistrationReasonCode,
				TIN = model.BankDetails.TIN,
				Client = model.Client,
				Count = model.Count,
				Goods = model.Goods
			});

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