using System;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Services.Abstract;
using Alicargo.MvcHelpers.Filters;
using Alicargo.ViewModels.Calculation.Admin;

namespace Alicargo.Controllers.Calculation
{
	public partial class PaymentController : Controller
	{
		private readonly IClientRepository _clients;
		private readonly IClientBalance _balance;
		private readonly IClientBalanceRepository _balanceRepository;

		public PaymentController(
			IClientRepository clients,
			IClientBalance balance,
			IClientBalanceRepository balanceRepository)
		{
			_clients = clients;
			_balance = balance;
			_balanceRepository = balanceRepository;
		}

		[Access(RoleType.Admin)]
		[HttpGet]
		public virtual ViewResult Index(long clientId)
		{
			var client = _clients.Get(clientId);
			var balance = _balanceRepository.GetBalance(clientId);
			var items = _balanceRepository.GetHistory(clientId);

			ViewBag.Nic = client.Nic;
			ViewBag.Balance = balance;
			ViewBag.ClientId = clientId;

			return View(items);
		}

		[ChildActionOnly]
		public virtual PartialViewResult Payment(long clientId)
		{
			ViewBag.ClientId = clientId;

			return PartialView();
		}

		[Access(RoleType.Admin)]
		[HttpPost]
		public virtual ActionResult Payment(long clientId, PaymentModel model)
		{
			_balance.Add(clientId, model.Money, model.Comment, DateTimeOffset.UtcNow);

			return RedirectToAction(MVC.Payment.Index(clientId));
		}
	}
}