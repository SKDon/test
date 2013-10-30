using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Abstract
{
	public interface IUserService
	{
		UserData[] List(RoleType role);
		UserModel Get(RoleType role, long id);
		void Update(UserModel model);
		void Add(UserModel model);
	}
}
