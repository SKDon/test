﻿using System.Diagnostics;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Calculation;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers.Calculation
{
	public partial class ClientCalculationController : Controller
	{
		private readonly IClientBalanceRepository _balance;
		private readonly IClientRepository _clients;
		private readonly IExcelClientCalculation _excel;
		private readonly IIdentityService _identity;
		private readonly IClientCalculationPresenter _presenter;

		public ClientCalculationController(
			IClientCalculationPresenter presenter,
			IClientBalanceRepository balance,
			IExcelClientCalculation excel,
			IClientRepository clients,
			IIdentityService identity)
		{
			_presenter = presenter;
			_balance = balance;
			_excel = excel;
			_clients = clients;
			_identity = identity;
		}

		[Access(RoleType.Client)]
		[HttpGet]
		public virtual ActionResult Index()
		{
			Debug.Assert(_identity.Id != null);

			var client = _clients.GetByUserId(_identity.Id.Value);

			var balance = _balance.GetBalance(client.ClientId);

			ViewBag.Balance = balance;
			ViewBag.ClientId = client.ClientId;

			return View();
		}

		[HttpGet]
		[Access(RoleType.Client)]
		public virtual ViewResult History(long clientId)
		{
			var balance = _balance.GetBalance(clientId);

			ViewBag.Balance = balance;

			return View(clientId);
		}

		[HttpPost]
		[Access(RoleType.Client)]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, long skip)
		{
			Debug.Assert(_identity.Id != null);

			var client = _clients.GetByUserId(_identity.Id.Value);

			var data = _presenter.List(client.ClientId, take, skip);

			return Json(data);
		}

		[Access(RoleType.Client)]
		public virtual FileResult Excel()
		{
			Debug.Assert(_identity.Id != null);

			var client = _clients.GetByUserId(_identity.Id.Value);

			var data = _presenter.List(client.ClientId, int.MaxValue, 0);

			var balance = _balance.GetBalance(client.ClientId);

			var stream = _excel.Get(data.Groups, balance, _identity.Language);

			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "calculation.xlsx");
		}
	}
}