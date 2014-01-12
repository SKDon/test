using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Contracts.Client;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Utilities;
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

		[ChildActionOnly]
		public virtual PartialViewResult BalanceButtons()
		{
			var clients = _clients.GetAll().OrderBy(x => x.Nic).ToArray();

			var urls = clients.ToDictionary(
				x => Url.Action(MVC.Balance.Decrease(x.ClientId)) + ";" + Url.Action(MVC.Balance.Increase(x.ClientId)),
				x => x.Nic);

			ViewBag.Urls = urls;

			return PartialView();
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Decrease(long clientId)
		{
			BindBag(clientId);

			return View(new PaymentModel
			{
				Timestamp = DateTimeProvider.Now
			});
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Decrease(long clientId, PaymentModel model)
		{
			Debug.Assert(model.Money != null);

			if (!ModelState.IsValid)
			{
				BindBag(clientId);

				return View(model);
			}

			try
			{				
				_balance.Decrease(clientId, model.Money.Value, model.Comment, model.Timestamp);
			}
			catch (ArgumentException e)
			{
				BindBag(clientId);

				ModelState.AddModelError(e.ParamName, e.Message);

				return View(model);
			}

			return RedirectToAction(MVC.Balance.Decrease(clientId));
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Increase(long clientId)
		{
			BindBag(clientId);

			return View(new PaymentModel
			{
				Timestamp = DateTimeProvider.Now
			});
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Increase(long clientId, PaymentModel model)
		{
			Debug.Assert(model.Money != null);

			if (!ModelState.IsValid)
			{
				BindBag(clientId);

				return View(model);
			}

			try
			{
				_balance.Increase(clientId, model.Money.Value, model.Comment, model.Timestamp);
			}
			catch (ArgumentException e)
			{
				BindBag(clientId);

				ModelState.AddModelError(e.ParamName, e.Message);

				return View(model);
			}

			return RedirectToAction(MVC.Balance.Increase(clientId));
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
	}
}