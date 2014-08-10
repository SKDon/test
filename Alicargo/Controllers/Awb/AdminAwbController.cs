using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.AirWaybill;
using Alicargo.ViewModels.AirWaybill;
using Resources;

namespace Alicargo.Controllers.Awb
{
	[Access(RoleType.Admin, RoleType.Manager)]
	public partial class AdminAwbController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly IAwbManager _awbManager;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly IBrokerRepository _brokers;
		private readonly ISenderRepository _senders;

		public AdminAwbController(
			IIdentityService identity,
			IAwbManager awbManager,
			IAwbPresenter awbPresenter,
			IAwbUpdateManager awbUpdateManager,
			IBrokerRepository brokers,
			ISenderRepository senders)
		{
			_identity = identity;
			_awbManager = awbManager;
			_awbPresenter = awbPresenter;
			_awbUpdateManager = awbUpdateManager;
			_brokers = brokers;
			_senders = senders;
		}

		private void BindBag(long? awbId)
		{
			ViewBag.AwbId = awbId;

			ViewBag.Brokers = _brokers.GetAll().ToDictionary(x => (long?)x.Id, x => x.Name);
			ViewBag.Senders = _senders.GetAll().ToDictionary(x => (long?)x.UserId, x => x.Name);
		}

		#region Edit

		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _awbPresenter.Get(id);

			BindBag(id);

			return View(model);
		}

		[HttpPost]
		public virtual ActionResult Edit(long id, AwbAdminModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					_awbUpdateManager.Update(id, model);

					return RedirectToAction(MVC.AdminAwb.Edit(id));
				}
			}
			catch(DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
			}

			BindBag(id);

			return View(model);
		}

		#endregion

		#region Create

		[HttpGet]
		public virtual ActionResult Create(long? applicationId)
		{
			BindBag(null);

			return View();
		}

		[HttpPost]
		public virtual ActionResult Create(long? applicationId, AwbAdminModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var data = AwbMapper.GetData(model);

					Debug.Assert(_identity.Id != null);
					var id = _awbManager.Create(applicationId, data, _identity.Id.Value);

					return RedirectToAction(MVC.AdminAwb.Edit(id));
				}
			}
			catch(DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
			}

			BindBag(null);

			return View(model);
		}

		#endregion
	}
}