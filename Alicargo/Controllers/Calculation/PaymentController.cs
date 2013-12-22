using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Controllers.Calculation
{
	public partial class PaymentController : Controller
    {
		private readonly IClientRepository _clients;

		public PaymentController(IClientRepository clients)
		{
			_clients = clients;
		}

		[Access(RoleType.Admin)]
		public virtual ViewResult Index(long clientId)
		{
			var client = _clients.Get(clientId);



			return View();
        }
    }
}