using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Abstract
{
	public interface ITextBuilder
	{
		string GetText(string template, string language, EventType type, ApplicationDetailsData application, byte[] data);
	}
}