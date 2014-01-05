using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.ApplicationEvents
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