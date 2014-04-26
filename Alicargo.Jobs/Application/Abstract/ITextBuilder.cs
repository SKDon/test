using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.Application.Abstract
{
	public interface ITextBuilder
	{
		string GetText(string template, string language, EventType type, ApplicationData application, byte[] data);
	}
}