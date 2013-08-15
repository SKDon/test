using Alicargo.Contracts.Enums;
using Alicargo.Core.Enums;

namespace Alicargo.Services.Abstract
{
	public interface IIdentityService
	{
		bool IsAuthenticated { get; }
		long? Id { get; set; }
		bool IsInRole(RoleType role);
		string TwoLetterISOLanguageName { get; set; }
	}
}