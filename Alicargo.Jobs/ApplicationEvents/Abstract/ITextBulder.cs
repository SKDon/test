using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Abstract
{
	public interface ITextBulder
	{
		string GetText(string template, string language, ApplicationEventType type, ApplicationDetailsData application, byte[] data);
	}
}