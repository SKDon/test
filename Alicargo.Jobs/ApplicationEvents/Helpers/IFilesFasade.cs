﻿using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public interface IFilesFasade
	{
		FileHolder[] GetFiles(long applicationId, long? awbId, ApplicationEventType type, byte[] data);
	}
}