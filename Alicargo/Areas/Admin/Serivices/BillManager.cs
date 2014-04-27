using System.Diagnostics;
using Alicargo.Areas.Admin.Models;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Utilities;

namespace Alicargo.Areas.Admin.Serivices
{
	internal sealed class BillManager : IBillManager
	{
		private readonly IBillRepository _bills;
		private readonly ISerializer _serializer;
		private readonly ISettingRepository _settings;

		public BillManager(ISettingRepository settings, ISerializer serializer, IBillRepository bills)
		{
			_settings = settings;
			_serializer = serializer;
			_bills = bills;
		}

		public void SaveBill(long id, BillModel model)
		{
			var settings = _settings.GetData<BillSettings>(SettingType.Bill);

			var number = GetNextNumber();

			Debug.Assert(model.PriceRuble != null, "model.PriceRuble != null");
			_bills.AddOrReplace(id,
				new BillData
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
					Goods = model.Goods,
					EuroToRuble = settings.EuroToRuble,
					VAT = settings.VAT,
					Price = model.PriceRuble.Value / settings.EuroToRuble,
					Number = number
				});
		}

		private int GetNextNumber()
		{
			while(true)
			{
				try
				{
					var data = _settings.Get(SettingType.BillLastNumber);

					var number = _serializer.Deserialize<int>(data.Data);

					data.Data = _serializer.Serialize(number + 1);

					_settings.AddOrReplace(data);

					return number;
				}
				catch(UpdateConflictException)
				{
				}
			}
		}
	}
}