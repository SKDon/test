using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IApplicationEventRepository
	{
		void Add(long applicationId, ApplicationEventType eventType);
		ApplicationEventData GetNext(DateTimeOffset olderThan);
		byte[] Touch(long id, byte[] rowVersion);
		void Delete(long id, byte[] rowVersion);
	}
}
