using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Areas.Admin.Serivices.Abstract
{
	public interface IBillPdf
	{
		FileHolder Get(long applicationId);
	}
}