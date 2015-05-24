using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.Application;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers.Application
{
	[Access(RoleType.Admin, RoleType.Manager, RoleType.Client, RoleType.Sender, RoleType.Carrier)]
	public partial class ApplicationListController : Controller
	{
		private readonly IAwbRepository _awbs;
		private readonly ICarrierRepository _carriers;
		private readonly IClientRepository _clients;
		private readonly IForwarderRepository _forwarders;
		private readonly IIdentityService _identity;
		private readonly IApplicationListPresenter _presenter;
		private readonly ISenderRepository _senders;
		private readonly IStateConfig _stateConfig;

		public ApplicationListController(
			IApplicationListPresenter presenter,
			IClientRepository clients,
			ISenderRepository senders,
			IAwbRepository awbs,
			ICarrierRepository carriers,
			IStateConfig stateConfig,
			IIdentityService identity,
			IForwarderRepository forwarders)
		{
			_presenter = presenter;
			_clients = clients;
			_senders = senders;
			_awbs = awbs;
			_carriers = carriers;
			_stateConfig = stateConfig;
			_identity = identity;
			_forwarders = forwarders;
		}

		[HttpGet]
		public virtual ViewResult Index()
		{
			var clients = _clients.GetAll().AsEnumerable();

			clients = FilterClientsForSender(clients);

			var awbs = GetAwbs();

			var model = new ApplicationIndexModel
			{
				Clients = clients.OrderBy(x => x.Nic).ToDictionary(x => x.ClientId, x => x.Nic),
				AirWaybills = awbs.OrderBy(x => x.Bill).ToDictionary(x => x.Id, x => x.Bill)
			};

			return View(model);
		}

		private IEnumerable<ClientData> FilterClientsForSender(IEnumerable<ClientData> clients)
		{
			var senderId = _senders.GetByUserId(_identity.Id);

			return senderId.HasValue
				? clients.Where(x => !x.DefaultSenderId.HasValue || x.DefaultSenderId.Value == senderId.Value)
				: clients;
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, int skip, Dictionary<string, string>[] group)
		{
			var orders = OrderHelper.Get(group);

			var senderId = _senders.GetByUserId(_identity.Id);

			var forwarderId = _forwarders.GetByUserId(_identity.Id);

			var client = _clients.GetByUserId(_identity.Id);

			var carrierId = _carriers.GetByUserId(_identity.Id);

			var clientId = client != null
				? client.ClientId
				: (long?)null;

			var data = _presenter.List(_identity.Language, take, skip, orders, clientId, senderId, forwarderId, carrierId);

			var result = Json(data);
			result.MaxJsonLength = int.MaxValue;

			return result;
		}

		private IEnumerable<AirWaybillData> GetAwbs()
		{
			var cargoIsFlewStateId = _stateConfig.CargoIsFlewStateId;
			var awbs = _awbs.Get().Where(x => x.StateId == cargoIsFlewStateId && x.IsActive);

			if(_identity.IsInRole(RoleType.Sender))
			{
				var id = _identity.Id;
				awbs = awbs.Where(x => x.SenderUserId == id);
			}

			return awbs;
		}
	}
}