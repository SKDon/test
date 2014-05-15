using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Alicargo.Services
{
	// todo: review (182)
	public sealed class AuthenticationManager
	{
		public const string AccountNameItem = "AccountName";

		#region Implementation of IAuthenticationManager

		public void IssueTicket(string userName, string accountName, params string[] roles)
		{
			FormsAuthentication.SetAuthCookie(userName + "|" + accountName + "|" + string.Join("|", roles), false);
		}

		public void PurgeTicket()
		{
			FormsAuthentication.SignOut();
		}

		public string GetUserName(Controller controller)
		{
			return controller.User.Identity.Name;
		}

		public bool IsUserInRole(Controller controller, string role)
		{
			return controller.User.IsInRole(role);
		}

		#endregion

		public static void Authenticate(HttpApplication application)
		{
			if(application.Request.IsAuthenticated)
			{
				var tokens = application.User.Identity.Name.Split('|');
				var userName = tokens[0];
				var roles = tokens.Skip(2).ToArray();

				application.Context.Items[AccountNameItem] = tokens[1];
				application.Context.User = new GenericPrincipal(new GenericIdentity(userName), roles);
			}
		}
	}
}