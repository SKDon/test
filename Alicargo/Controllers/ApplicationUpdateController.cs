using System;
using System.Net;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Enums;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class ApplicationUpdateController : Controller
	{
		private readonly IApplicationPresenter _applicationPresenter;
		private readonly IApplicationManager _applicationManager;
		private readonly IStateConfig _stateConfig;

		public ApplicationUpdateController(IApplicationPresenter applicationPresenter,
			IApplicationManager applicationManager, IStateConfig stateConfig)
		{
			_applicationPresenter = applicationPresenter;
			_applicationManager = applicationManager;
			_stateConfig = stateConfig;
		}

		#region Set state

		// todo: test
		[HttpPost]
		[Access(RoleType.Client, RoleType.Admin)]
		public virtual HttpStatusCodeResult Close(long id)
		{
			_applicationManager.SetState(id, _stateConfig.CargoReceivedStateId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		// todo: test
		[HttpPost]
		[Access(RoleType.Admin, RoleType.Brocker, RoleType.Forwarder, RoleType.Sender)]
		public virtual HttpStatusCodeResult SetState(long id, long stateId)
		{
			_applicationManager.SetState(id, stateId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpPost]
		public virtual JsonResult States(long id)
		{
			return Json(_applicationPresenter.GetAvailableStates(id));
		}

		#endregion

		[Access(RoleType.Admin, RoleType.Forwarder), HttpPost]
		public virtual HttpStatusCodeResult SetTransitReference(long id, string transitReference)
		{
			_applicationManager.SetTransitReference(id, transitReference);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin, RoleType.Forwarder), HttpPost]
		public virtual HttpStatusCodeResult SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_applicationManager.SetDateOfCargoReceipt(id, dateOfCargoReceipt);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}
	}
}
