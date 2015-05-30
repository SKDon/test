using System;
using Alicargo.Areas.Admin.Models;

namespace Alicargo.Areas.Admin.Serivices.Abstract
{
	public interface IBillManager
	{
		void Save(long applicationId, int number, BillModel model, DateTimeOffset saveDate, DateTimeOffset? sendDate);
		void Send(long applicationId, long userId);
	}
}