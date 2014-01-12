using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Contracts;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers.Application
{
	public partial class ApplicationListController : Controller
	{
		private readonly IApplicationListPresenter _applicationPresenter;
		private readonly IAwbRepository _awbRepository;
		private readonly IClientRepository _clients;
		private readonly IIdentityService _identity;
		private readonly ISenderRepository _senders;
		private readonly IStateConfig _stateConfig;

		public ApplicationListController(IApplicationListPresenter applicationPresenter,
			IClientRepository clients, ISenderRepository senders, IAwbRepository awbRepository,
			IStateConfig stateConfig, IIdentityService identity)
		{
			_applicationPresenter = applicationPresenter;
			_clients = clients;
			_senders = senders;
			_awbRepository = awbRepository;
			_stateConfig = stateConfig;
			_identity = identity;
		}

		[HttpPost, Access(RoleType.Admin, RoleType.Client, RoleType.Forwarder, RoleType.Sender),
		 OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, int skip, Dictionary<string, string>[] group)
		{
			var orders = OrderHelper.Get(group);

			Debug.Assert(_identity.Id != null);

			var client = _clients.GetByUserId(_identity.Id.Value);

			var senderId = _senders.GetByUserId(_identity.Id.Value);

			var isForwarder = _identity.IsInRole(RoleType.Forwarder);

			var data = _applicationPresenter.List(take, skip, orders, client != null
				? client.ClientId
				: (long?) null, senderId,
				isForwarder);

			return Json(data);
		}

		[Access(RoleType.Admin, RoleType.Client, RoleType.Forwarder, RoleType.Sender)]
		public virtual ViewResult Index()
		{
			var clients = _clients.GetAll()
				.OrderBy(x => x.Nic)
				.ToDictionary(x => x.ClientId, x => x.Nic);

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