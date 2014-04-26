using System.Web.Mvc;
using Alicargo.Areas.Admin.Models;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Utilities;
using Alicargo.Utilities.Localization;

namespace Alicargo.Areas.Admin.Controllers
{
	public partial class BillController : Controller
	{
		private readonly IApplicationRepository _applications;
		private readonly IBillRepository _bills;
		private readonly ISerializer _serializer;
		private readonly ISettingRepository _settings;

		public BillController(
			ISettingRepository settings, ISerializer serializer,
			IApplicationRepository applications, IBillRepository bills)
		{
			_settings = settings;
			_serializer = serializer;
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

		private T GetData<T>(SettingType type)
		{
			return _serializer.Deserialize<T>(_settings.Get(type).Data);
		}

		private static BillModel GetModel(BillSettings billSettings, ApplicationData application, BillEditData bill)
		{
			if(bill != null)
			{
				return new BillModel
				{
					Settings = new BillSettings
					{
						Accountant = bill.Accountant,
						Bank = bill.Bank,
						BIC = bill.BIC,
						CorrespondentAccount = bill.CorrespondentAccount,
						CurrentAccount = bill.CurrentAccount,
						Head = bill.Head,
						HeaderText = bill.HeaderText,
						Payee = bill.Payee,
						Shipper = bill.Shipper,
						TaxRegistrationReasonCode = bill.TaxRegistrationReasonCode,
						TIN = bill.TIN
					},
					Client = bill.Client,
					Count = bill.Count,
					Goods = bill.Goods,
					Price = bill.Price,
					Total = bill.Total
				};
			}

			var price = LocalizationHelper.GetValueString(application.Value, application.CurrencyId, CultureProvider.GetCultureInfo());

			return new BillModel
			{
				Settings = billSettings,
				Count = "1",
				Client = application.ClientLegalEntity,
				Goods = application.FactoryName,
				Price = price,
				Total = price
			};
		}
	}
}