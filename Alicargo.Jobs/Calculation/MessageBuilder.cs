using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Calculation
{
	internal sealed class MessageBuilder : IMessageBuilder
	{
		public EmailMessage[] Get(EventType type, EventData eventData)
		{
			throw new NotImplementedException();
		}
	}
}