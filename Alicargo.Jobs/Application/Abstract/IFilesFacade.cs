using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.Application.Abstract
{
	public interface IFilesFacade
	{
		FileHolder[] GetFiles(EventType type, EventDataForEntity data);
	}
}