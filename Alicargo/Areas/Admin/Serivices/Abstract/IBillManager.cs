using System;
using Alicargo.Areas.Admin.Models;

namespace Alicargo.Areas.Admin.Serivices.Abstract
{
	public interface IBillManager
	{
		void SaveBill(long id, int number, BillModel model, DateTimeOffset saveDate);
	}
}