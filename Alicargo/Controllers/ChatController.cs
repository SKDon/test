using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Repositories.User;

namespace Alicargo.Controllers
{
	public partial class ChatController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly IUserRepository _users;

		public ChatController(
			IIdentityService identity,
			IUserRepository users)
		{
			_identity = identity;
			_users = users;
		}

		public virtual ActionResult Index()
		{
			return View();
		}
	}
}