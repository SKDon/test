using System;
using System.Net;
using System.Web.Mvc;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers.Application
{
	public partial class ApplicationUpdateController : Controller
	{
		private readonly IAdminApplicationManager _manager;
		private readonly IApplicationStateManager _states;
		private readonly IApplicationPresenter _presenter;
		private readonly IStateConfig _config;

		public ApplicationUpdateController(
			IApplicationStateManager states,
			IApplicationPresenter presenter,
			IAdminApplicationManager manager,
			IStateConfig config)
		{
			_states = states;
			_presenter = presenter;
			_manager = manager;
			_config = config;
		}

		#region Set state

		[HttpPost]
		[Access(RoleType.Client, RoleType.Admin)]
		public virtual HttpStatusCodeResult Close(long id)
		{
			_states.SetState(id, _config.CargoReceivedStateId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Broker, RoleType.Forwarder, RoleType.Sender, RoleType.Carrier)]
		public virtual HttpStatusCodeResult SetState(long id, long stateId)
		{
			_states.SetState(id, stateId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult States(long id)
		{
			return Json(_presenter.GetStateAvailability(id));
		}

		#endregion

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual HttpStatusCodeResult SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_manager.SetDateOfCargoReceipt(id, dateOfCargoReceipt);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpPost]
		[Access(RoleType.Forwarder)]
		public virtual HttpStatusCodeResult SetTransitCost(long id, decimal? transitCost)
		{
			_manager.SetTransitCost(id, transitCost);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Forwarder)]
		public virtual HttpStatusCodeResult SetTransitReference(long id, string transitReference)
		{
			_manager.SetTransitReference(id, transitReference);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}
	}
}