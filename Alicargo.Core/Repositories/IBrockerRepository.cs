using Alicargo.Core.Models;

namespace Alicargo.Core.Repositories
{
	public interface IBrockerRepository
	{
		Brocker Get(long brockerId);
		Brocker GetByUserId(long userId);
		Brocker[] GetAll();
	}
}