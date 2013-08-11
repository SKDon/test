using System.ComponentModel.DataAnnotations;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.ViewModels
{
	public sealed class ClientModel
	{
		#region Contatct info

		[Required]
		[DataType(DataType.MultilineText)]
		[DisplayNameLocalized(typeof(Entities), "Contacts")]
		public string Contacts { get; set; }

		[DataType(DataType.PhoneNumber)]
		[DisplayNameLocalized(typeof(Entities), "OfficePhone")]
		public string Phone { get; set; }

		[DataType(DataType.EmailAddress)]
		[Required]
		[MaxLength(320)]
		[DisplayNameLocalized(typeof(Entities), "Email")]
		public string Email { get; set; }

		#endregion

		#region Legal entry attributes

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Nic")]
		public string Nic { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "LegalEntity")]
		public string LegalEntity { get; set; }

		[DisplayNameLocalized(typeof(Entities), "INN")]
		public string INN { get; set; }

		[DisplayNameLocalized(typeof(Entities), "KPP")]
		public string KPP { get; set; }

		[DisplayNameLocalized(typeof(Entities), "OGRN")]
		public string OGRN { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Bank")]
		public string Bank { get; set; }

		[DisplayNameLocalized(typeof(Entities), "BIC")]
		public string BIC { get; set; }

		[DisplayNameLocalized(typeof(Entities), "LegalAddress")]
		public string LegalAddress { get; set; }

		[DisplayNameLocalized(typeof(Entities), "MailingAddress")]
		public string MailingAddress { get; set; }

		[DisplayNameLocalized(typeof(Entities), "RS")]
		public string RS { get; set; }

		[DisplayNameLocalized(typeof(Entities), "KS")]
		public string KS { get; set; }

		#endregion

		public static ClientModel GetModel(ClientData client)
		{
			return new ClientModel
			{
				BIC = client.BIC,
				Phone = client.Phone,
				Email = client.Email,
				LegalEntity = client.LegalEntity,
				Bank = client.Bank,
				Contacts = client.Contacts,
				INN = client.INN,
				KPP = client.KPP,
				KS = client.KS,
				LegalAddress = client.LegalAddress,
				MailingAddress = client.MailingAddress,
				Nic = client.Nic,
				OGRN = client.OGRN,
				RS = client.RS
			};
		}
	}
}