using Alicargo.Contracts.Enums;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Abstract
{
	public interface IUserService
	{
		UserModel[] List(RoleType role);
		UserModel Get(RoleType role, long id);
		void Update(UserModel model);
		void Add(UserModel model);
	}
}
