using System;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Services.Abstract;
using Alicargo.MvcHelpers.Filters;
using Alicargo.ViewModels.Calculation.Admin;

namespace Alicargo.Controllers.Calculation
{
	public partial class BalanceController : Controller
	{
		private readonly IClientBalance _balance;
		private readonly IClientBalanceRepository _balanceRepository;
		private readonly IClientRepository _clients;

		public BalanceController(
			IClientRepository clients,
			IClientBalance balance,
			IClientBalanceRepository balanceRepository)
		{
			_clients = clients;
			_balance = balance;
			_balanceRepository = balanceRepository;
		}

		[ChildActionOnly]
		public virtual PartialViewResult History(long clientId)
		{
			var items = _balanceRepository.GetHistory(clientId);

			return PartialView(items);
		}

		[Access(RoleType.Admin)]
		[HttpGet]
		public virtual ViewResult Decrease(long clientId)
		{
			BindBag(clientId);

			return View();
		}

		private void BindBag(long clientId)
		{
			var client = _clients.Get(clientId);
			var balance = _balanceRepository.GetBalance(clientId);
			ViewBag.ClientId = clientId;
			ViewBag.Nic = client.Nic;
			ViewBag.Balance = balance;
			ViewBag.ClientId = clientId;
		}

		[Access(RoleType.Admin)]
		[HttpPost]
		public virtual ActionResult Decrease(long clientId, PaymentModel model)
		{
			try
			{
				_balance.Decrease(clientId, model.Money, model.Comment, DateTimeOffset.UtcNow);
			}
			catch (ArgumentException e)
			{
				BindBag(clientId);

				ModelState.AddModelError(e.ParamName, e.Message);

				return View(model);
			}

			return RedirectToAction(MVC.Balance.Decrease(clientId));
		}
	}
}