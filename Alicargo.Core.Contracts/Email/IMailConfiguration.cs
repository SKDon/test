using System.Net.Configuration;

namespace Alicargo.Core.Contracts.Email
{
	public interface IMailConfiguration
	{
		SmtpSection GetConfiguration(long? userId);
	}
}