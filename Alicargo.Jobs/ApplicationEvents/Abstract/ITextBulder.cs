using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Abstract
{
	public interface ITextBulder
	{
		string GetText(string template, string language, EventType type, ApplicationDetailsData application, byte[] data);
	}
}