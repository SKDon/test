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
	[Access(RoleType.Sender)]
	public partial class SenderAwbController : Controller
	{
		private readonly IAwbManager _awbManager;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly IBrokerRepository _brokers;
		private readonly IIdentityService _identity;

		public SenderAwbController(
			IIdentityService identity,
			IAwbUpdateManager awbUpdateManager,
			IAwbManager awbManager,
			IAwbPresenter awbPresenter,
			IBrokerRepository brokers)
		{
			_identity = identity;
			_awbUpdateManager = awbUpdateManager;
			_awbManager = awbManager;
			_awbPresenter = awbPresenter;
			_brokers = brokers;
		}

		[HttpGet]
		public virtual ActionResult Create(long? id)
		{
			BindBag(null);

			return View();
		}

		[HttpPost]
		public virtual ActionResult Create(long? id, AwbSenderModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var airWaybillData = AwbMapper.GetData(model);

					Debug.Assert(_identity.Id != null);
					_awbManager.Create(id, airWaybillData, _identity.Id.Value);

					return RedirectToAction(MVC.AirWaybill.Index());
				}
			}
			catch(DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
			}

			BindBag(null);

			return View(model);
		}

		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _awbPresenter.GetSenderAwbModel(id);

			BindBag(id);

			return View(model);
		}

		[HttpPost]
		public virtual ActionResult Edit(long id, AwbSenderModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					_awbUpdateManager.Update(id, model);

					return RedirectToAction(MVC.SenderAwb.Edit(id));
				}
			}
			catch(DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
			}

			BindBag(id);

			return View(model);
		}

		private void BindBag(long? awbId)
		{
			ViewBag.AwbId = awbId;
		}
	}
}