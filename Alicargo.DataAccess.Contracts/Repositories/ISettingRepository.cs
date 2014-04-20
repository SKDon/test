using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface ISettingRepository
	{
		Setting AddOrReplace(Setting setting);
		Setting Get(SettingType type);
	}
}