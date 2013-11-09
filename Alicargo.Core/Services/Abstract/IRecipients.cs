﻿using Alicargo.Core.Models;

namespace Alicargo.Core.Services.Abstract
{
	public interface IRecipients
	{
		Recipient[] GetAdminEmails();
		Recipient[] GetSenderEmails();
		Recipient[] GetForwarderEmails();
	}
}