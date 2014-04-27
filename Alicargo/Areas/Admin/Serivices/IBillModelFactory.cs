using Alicargo.Areas.Admin.Models;

namespace Alicargo.Areas.Admin.Serivices
{
	public interface IBillModelFactory
	{
		BillModel GetModel(long applicationId);
	}
}