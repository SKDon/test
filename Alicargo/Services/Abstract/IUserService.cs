using Alicargo.Contracts.Enums;
using Alicargo.Core.Enums;
using Alicargo.ViewModels;

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
