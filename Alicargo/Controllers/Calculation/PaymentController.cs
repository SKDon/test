using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation.Admin;

namespace Alicargo.Controllers.Calculation
{
	public partial class PaymentController : Controller
	{
		private readonly IClientRepository _clients;
		private readonly IClientManager _manager;

		public PaymentController(IClientRepository clients, IClientManager manager)
		{
			_clients = clients;
			_manager = manager;
		}

		[Access(RoleType.Admin)]
		[HttpGet]
		public virtual ViewResult Index(long clientId)
		{
			var client = _clients.Get(clientId);

			ViewBag.Nic = client.Nic;
			ViewBag.Balance = client.Balance;
			ViewBag.ClientId = clientId;

			return View();
		}

		[Access(RoleType.Admin)]
		[HttpPost]
		public virtual ActionResult Index(long clientId, PaymentModel model)
		{
			_manager.AddToBalance(clientId, model);

			return RedirectToAction(MVC.Payment.Index(clientId));
		}
	}
}