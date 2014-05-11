using System;
using System.Diagnostics;
using System.Linq;
using Alicargo.Areas.Admin.Models;
using Alicargo.Areas.Admin.Serivices.Abstract;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Contracts.Email;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;

namespace Alicargo.Areas.Admin.Serivices.Bill
{
	internal sealed class BillManager : IBillManager
	{
		private readonly IAdminRepository _admins;
		private readonly IApplicationRepository _applications;
		private readonly IBillRepository _bills;
		private readonly IMailSender _mail;
		private readonly IBillPdf _pdf;
		private readonly ISettingRepository _settings;

		public BillManager(
			IApplicationRepository applications,
			ISettingRepository settings,
			IBillRepository bills,
			IBillPdf pdf,
			IAdminRepository admins,
			IMailSender mail)
		{
			_applications = applications;
			_settings = settings;
			_bills = bills;
			_pdf = pdf;
			_admins = admins;
			_mail = mail;
		}

		public void Save(long applicationId, int number, BillModel model, DateTimeOffset saveDate, DateTimeOffset? sendDate)
		{
			var settings = _settings.GetData<BillSettings>(SettingType.Bill);

			Debug.Assert(model.PriceRuble != null, "model.PriceRuble != null");
			_bills.AddOrReplace(applicationId,
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
					Number = number,
					SaveDate = saveDate,
					SendDate = sendDate
				});
		}

		public void Send(long applicationId)
		{
			var bill = _bills.Get(applicationId);
			var application = _applications.Get(applicationId);

			var subject = string.Format("Счет на оплату № {0} от {1}", bill.Number, bill.SaveDate.ToString("dd MMMM yyyy"));
			var body = subject;
			var from = EmailsHelper.DefaultFrom;
			var to = _admins.GetAll().Select(x => x.Email).ToArray()
				.Union(EmailsHelper.SplitAndTrimEmails(application.ClientEmails))
				.Distinct()
				.ToArray();
			var files = new[] { _pdf.Get(applicationId) };

			foreach(var item in to)
			{
				_mail.Send(new EmailMessage(subject, body, from, item)
				{
					Files = files,
				});
			}
		}
	}
}