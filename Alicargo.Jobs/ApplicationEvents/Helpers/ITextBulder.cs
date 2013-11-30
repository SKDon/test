using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public interface ITextBulder
	{
		string GetText(string template, string language, ApplicationEventType type, ApplicationData application, byte[] data);
	}
}