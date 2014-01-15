using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Abstract
{
	public interface IUserService
	{
		UserListItem[] List(RoleType role);
		UserModel Get(RoleType role, long id);
		void Update(UserModel model);
		void Add(UserModel model);
	}
}