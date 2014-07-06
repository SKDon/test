using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.Application;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers.Application
{
	public partial class ApplicationListController : Controller
	{
		private readonly IAwbRepository _awbs;
		private readonly ICarrierRepository _carriers;
		private readonly IClientRepository _clients;
		private readonly IForwarderRepository _forwarders;
		private readonly IIdentityService _identity;
		private readonly IApplicationListPresenter _presenter;
		private readonly ISenderRepository _senders;
		private readonly IAdminRepository _admins;
		private readonly IManagerRepository _managers;
		private readonly IStateConfig _stateConfig;

		public ApplicationListController(
			IApplicationListPresenter presenter,
			IClientRepository clients,
			ISenderRepository senders,
			IAdminRepository admins,
			IManagerRepository managers,
			IAwbRepository awbs,
			ICarrierRepository carriers,
			IStateConfig stateConfig,
			IIdentityService identity,
			IForwarderRepository forwarders)
		{
			_presenter = presenter;
			_clients = clients;
			_senders = senders;
			_admins = admins;
			_managers = managers;
			_awbs = awbs;
			_carriers = carriers;
			_stateConfig = stateConfig;
			_identity = identity;
			_forwarders = forwarders;
		}

		[Access(RoleType.Admin, RoleType.Manager, RoleType.Client, RoleType.Forwarder, RoleType.Sender, RoleType.Carrier)]
		public virtual ViewResult Index()
		{
			var clients = _clients.GetAll()
				.OrderBy(x => x.Nic)
				.ToDictionary(x => x.ClientId, x => x.Nic);

			var awbs = GetAwbs();

			var model = new ApplicationIndexModel
			{
				Clients = clients,
				AirWaybills = awbs.OrderBy(x => x.Bill).ToDictionary(x => x.Id, x => x.Bill)
			};

			return View(model);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Client, RoleType.Forwarder, RoleType.Sender, RoleType.Carrier)]
		public virtual JsonResult List(int take, int skip, Dictionary<string, string>[] group)
		{
			var orders = OrderHelper.Get(group);

			Debug.Assert(_identity.Id != null);

			var senderId = _senders.GetByUserId(_identity.Id.Value);

			var forwarderId = _forwarders.GetByUserId(_identity.Id.Value);

			var client = _clients.GetByUserId(_identity.Id.Value);

			var carrierId = _carriers.GetByUserId(_identity.Id.Value);

			var clientId = client != null
				? client.ClientId
				: (long?)null;

			var data = _presenter.List(_identity.Language, take, skip, orders, clientId, senderId, forwarderId, carrierId);

			return Json(data);
		}

		private IEnumerable<AirWaybillData> GetAwbs()
		{
			var cargoIsFlewStateId = _stateConfig.CargoIsFlewStateId;
			var awbs = _awbs.Get().Where(x => x.StateId == cargoIsFlewStateId && x.IsActive);

			if(_identity.IsInRole(RoleType.Sender))
			{
				var adminUserIds = _admins.GetAll().Select(x => x.UserId).ToArray();
				var managerUserIds = _managers.GetAll().Select(x => x.UserId).ToArray();

				var currentUserId = _identity.Id;
				awbs = awbs.Where(x => x.CreatorUserId == currentUserId || adminUserIds.Contains(x.CreatorUserId) || managerUserIds.Contains(x.CreatorUserId));
			}

			return awbs;
		}
	}
}