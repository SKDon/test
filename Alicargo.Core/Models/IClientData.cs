using System;

namespace Alicargo.Core.Models
{
	[Obsolete]
	public interface IClientData
	{
		long Id { get; set; }
		long UserId { get; set; }
		string Email { get; set; }
		string Nic { get; set; }
		string LegalEntity { get; set; }
		string Contacts { get; set; }
		string Phone { get; set; }
		string INN { get; set; }
		string KPP { get; set; }
		string OGRN { get; set; }
		string Bank { get; set; }
		string BIC { get; set; }
		string LegalAddress { get; set; }
		string MailingAddress { get; set; }
		string RS { get; set; }
		string KS { get; set; }
		long TransitId { get; set; }
	}
}