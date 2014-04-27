using Alicargo.Areas.Admin.Models;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;

namespace Alicargo.Areas.Admin.Serivices
{
	internal sealed class BillModelFactory : IBillModelFactory
	{
		private readonly IApplicationRepository _applications;
		private readonly ICalculationRepository _calculations;
		private readonly IClientRepository _clients;
		private readonly ISettingRepository _settings;

		public BillModelFactory(
			IApplicationRepository applications,
			ICalculationRepository calculations,
			IClientRepository clients,
			ISettingRepository settings)
		{
			_applications = applications;
			_calculations = calculations;
			_clients = clients;
			_settings = settings;
		}

		public BillModel GetBillModelByApplication(long applicationId)
		{
			var application = _applications.Get(applicationId);
			var settings = _settings.GetData<BillSettings>(SettingType.Bill);
			var money = GetMoney(applicationId, settings.EuroToRuble);

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
				Count = 1,
				Client = GetClientString(application),
				Goods = GetGoodsString(application),
				PriceRuble = money,
				Accountant = settings.Accountant,
				Head = settings.Head,
				HeaderText = settings.HeaderText,
				Shipper = settings.Shipper
			};
		}

		public BillModel GetBillModel(BillData bill)
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
				PriceRuble = bill.Price * bill.EuroToRuble
			};
		}		

		private string GetClientString(ApplicationEditData application)
		{
			var client = _clients.Get(application.ClientId);

			return string.Format("{0}, ИНН {1}, {2}, {3}", client.LegalEntity, client.INN, client.LegalAddress, client.Contacts);
		}

		private decimal? GetMoney(long applicationId, decimal euroToRuble)
		{
			var calculation = _calculations.GetByApplication(applicationId);

			return calculation != null
				? CalculationDataHelper.GetMoney(calculation) * euroToRuble
				: (decimal?)null;
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