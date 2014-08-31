using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.Application;
using Alicargo.ViewModels.Application;

namespace Alicargo.Areas.Forwarder.Controllers
{
	[Access(RoleType.Forwarder)]
	public partial class ApplicationsController : Controller
	{
		private readonly ICityRepository _cities;
		private readonly IClientRepository _clients;
		private readonly IForwarderRepository _forwarders;
		private readonly IIdentityService _identity;
		private readonly IApplicationListPresenter _presenter;
		private readonly ITransitRepository _transits;

		public ApplicationsController(
			ICityRepository cities,
			ITransitRepository transits,
			IClientRepository clients,
			IApplicationListPresenter presenter,
			IIdentityService identity,
			IForwarderRepository forwarders)
		{
			_cities = cities;
			_transits = transits;
			_clients = clients;
			_presenter = presenter;
			_identity = identity;
			_forwarders = forwarders;
		}

		[HttpGet]
		public virtual ViewResult Index()
		{
			return View();
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, int skip, Dictionary<string, string>[] group)
		{
			var orders = OrderHelper.Get(group);

			Debug.Assert(_identity.Id != null);

			var forwarderId = _forwarders.GetByUserId(_identity.Id.Value);

			Debug.Assert(forwarderId != null);

			var data = _presenter.List(_identity.Language, take, skip, orders, null, null, forwarderId.Value);

			UpdateDeliveryData(data);

			return Json(data);
		}

		private void UpdateDeliveryData(ApplicationListCollection data)
		{
			var clientTransitIds = _clients.GetAll()
				.ToDictionary(x => x.TransitId, x => x.ClientId);

			var transits = _transits.Get(clientTransitIds.Select(x => x.Key).ToArray())
				.ToDictionary(x => clientTransitIds[x.Id], x => x);

			var cities = _cities.All(_identity.Language).ToDictionary(x => x.Id, x => x.Name);

			foreach(var @group in data.Groups)
			{
				foreach(ApplicationListItem item in @group.items)
				{
					if(item.TransitMethodOfTransitId == (int)MethodOfTransit.Self)
					{
						var transit = transits[item.ClientId];

						item.TransitCity = cities[transit.CityId];
						item.CarrierContact = transit.RecipientName;
						item.CarrierAddress = transit.Address;
						item.CarrierPhone = transit.Phone;
					}
				}
			}
		}
	}
}