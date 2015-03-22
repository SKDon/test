using Alicargo.Areas.Admin.Models;
using Alicargo.DataAccess.Contracts.Contracts.Application;

namespace Alicargo.Areas.Admin.Serivices.Abstract
{
	public interface IBillModelFactory
	{
		BillModel GetBillModel(BillData bill);
		BillModel GetBillModelByApplication(long applicationId);
	}
}