using System;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.Application
{
	internal sealed class ApplicationStateHistoryProcessor : IEventProcessor
	{
		public void ProcessEvent(EventType type, EventData data)
		{
			if (type != EventType.ApplicationSetState)
			{
				throw new InvalidOperationException(type + " is not supported");
			}
		}
	}
}