using System;

namespace Alicargo.DataAccess.Contracts.Contracts.User
{
	public class ClientEditData
	{
		public string[] Emails { get; set; }
		public string Nic { get; set; }
		public string LegalEntity { get; set; }
		public string Contacts { get; set; }
		public string Phone { get; set; }
		public string INN { get; set; }
		public string KPP { get; set; }
		public string OGRN { get; set; }
		public string Bank { get; set; }
		public string BIC { get; set; }
		public string LegalAddress { get; set; }
		public string MailingAddress { get; set; }
		public string RS { get; set; }
		public string KS { get; set; }
		//public string ContractNumber { get; set; }
		//public DateTimeOffset ContractDate { get; set; }
	}
}