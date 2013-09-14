﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers
{
	public partial class ApplicationListController : Controller
	{
		private readonly IApplicationListPresenter _applicationPresenter;
		private readonly IAwbRepository _awbRepository;
		private readonly IClientRepository _clientRepository;
		private readonly IStateConfig _stateConfig;

		public ApplicationListController(IApplicationListPresenter applicationPresenter,
										 IClientRepository clientRepository, IAwbRepository awbRepository,
										 IStateConfig stateConfig)
		{
			_applicationPresenter = applicationPresenter;
			_clientRepository = clientRepository;
			_awbRepository = awbRepository;
			_stateConfig = stateConfig;
		}

		[HttpPost, Access(RoleType.Admin, RoleType.Client, RoleType.Forwarder, RoleType.Sender)]
		public virtual JsonResult List(int take, int skip, int page, int pageSize, Dictionary<string, string>[] group)
		{
			// todo: 3. use model binder for Order
			var orders = OrderHelper.Get(group);

			var data = _applicationPresenter.List(take, skip, orders);

			return Json(data);
		}

		[Access(RoleType.Admin, RoleType.Client, RoleType.Forwarder, RoleType.Sender)]
		public virtual ViewResult Index()
		{
			var clients = _clientRepository.GetAll()
										   .OrderBy(x => x.Nic)
										   .ToDictionary(x => x.Id, x => x.Nic);

			var model = new ApplicationIndexModel
			{
				Clients = clients,
				AirWaybills = _awbRepository.Get()
											.Where(x => x.StateId == _stateConfig.CargoIsFlewStateId)
											.OrderBy(x => x.Bill)
											.ToDictionary(x => x.Id, x => x.Bill)
			};

			return View(model);
		}
	}
}