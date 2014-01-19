using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Core.Contracts.Common
{
	public interface IIdentityService
	{
		bool IsAuthenticated { get; }
		long? Id { get; set; }
		bool IsInRole(RoleType role);
		string Language { get; }
	    void SetLanguage(string value);
	}
}