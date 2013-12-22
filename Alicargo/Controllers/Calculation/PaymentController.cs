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
		private readonly IClientBalanceRepository _balance;
		private readonly IClientManager _manager;

		public PaymentController(
			IClientRepository clients,
			IClientBalanceRepository balance,
			IClientManager manager)
		{
			_clients = clients;
			_balance = balance;
			_manager = manager;
		}

		[Access(RoleType.Admin)]
		[HttpGet]
		public virtual ViewResult Index(long clientId)
		{
			var client = _clients.Get(clientId);
			var balance = _balance.GetBalance(clientId);

			ViewBag.Nic = client.Nic;
			ViewBag.Balance = balance;
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