using System.Web.Mvc;
using Alicargo.Areas.Admin.Models;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Utilities;

namespace Alicargo.Areas.Admin.Controllers
{
	public partial class BillController : Controller
	{
		private readonly IApplicationRepository _applications;
		private readonly IBillRepository _bills;
		private readonly IClientRepository _clients;
		private readonly ISerializer _serializer;
		private readonly ISettingRepository _settings;

		public BillController(
			ISettingRepository settings,
			ISerializer serializer,
			IClientRepository clients,
			IApplicationRepository applications,
			IBillRepository bills)
		{
			_settings = settings;
			_serializer = serializer;
			_clients = clients;
			_applications = applications;
			_bills = bills;
		}

		public virtual PartialViewResult Preview(long applicationId)
		{
			var billSettings = GetData<BillSettings>(SettingType.Bill);
			var application = _applications.Get(applicationId);
			var billNumber = GetData<int>(SettingType.ApplicationNumberCounter);
			var bill = _bills.Get(applicationId);

			ViewBag.BillNumber = billNumber;

			return PartialView(GetModel(billSettings, application, bill));
		}

		private string GetClientString(ApplicationEditData application)
		{
			var client = _clients.Get(application.ClientId);

			return string.Format("{0}, ИНН {1}, {2}, {3}", client.LegalEntity, client.INN, client.LegalAddress, client.Contacts);
		}

		private T GetData<T>(SettingType type)
		{
			return _serializer.Deserialize<T>(_settings.Get(type).Data);
		}

		private BillModel GetModel(BillSettings settings, ApplicationData application, BillEditData bill)
		{
			if(bill != null)
			{
				return new BillModel
				{
					BankDetails = new BankDetails
					{
						Bank = bill.Bank,
						BIC = bill.BIC,
						CorrespondentAccount = bill.CorrespondentAccount,
						CurrentAccount = bill.CurrentAccount,
						Payee = bill.Payee,
						TaxRegistrationReasonCode = bill.TaxRegistrationReasonCode,
						TIN = bill.TIN
					},
					Accountant = bill.Accountant,
					Head = bill.Head,
					HeaderText = bill.HeaderText,
					Shipper = bill.Shipper,
					Client = bill.Client,
					Count = bill.Count,
					Goods = bill.Goods,
					Price = bill.Price,
					Total = bill.Total
				};
			}

			var price = (application.Value * (settings.VAT + 1)).ToString("n2");

			return new BillModel
			{
				BankDetails = new BankDetails
				{
					Bank = settings.Bank,
					BIC = settings.BIC,
					CorrespondentAccount = settings.CorrespondentAccount,
					CurrentAccount = settings.CurrentAccount,
					Payee = settings.Payee,
					TaxRegistrationReasonCode = settings.TaxRegistrationReasonCode,
					TIN = settings.TIN
				},
				Count = "1",
				Client = GetClientString(application),
				Goods = GetGoodsString(application),
				Price = price,
				Total = price,
				Accountant = settings.Accountant,
				Head = settings.Head,
				HeaderText = settings.HeaderText,
				Shipper = settings.Shipper
			};
		}

		private static string GetGoodsString(ApplicationData application)
		{
			return string.Format(
				"Оплата по договору {0} от {1} года. За одежду, обувь и другие непродоволдьственные товары.",
				application.GetApplicationDisplay(),
				application.CreationTimestamp.ToString("dd MMMM yyyy"));
		}
	}
}