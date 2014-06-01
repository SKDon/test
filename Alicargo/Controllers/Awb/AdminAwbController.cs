using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.AirWaybill;
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
	public partial class AdminAwbController : Controller
	{
		private readonly IAwbManager _awbManager;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly IBrokerRepository _brokers;


		public AdminAwbController(
			IAwbManager awbManager,
			IAwbPresenter awbPresenter,
			IAwbUpdateManager awbUpdateManager,
			IBrokerRepository brokers)
		{
			_awbManager = awbManager;
			_awbPresenter = awbPresenter;
			_awbUpdateManager = awbUpdateManager;
			_brokers = brokers;
		}

		private void BindBag(long? awbId)
		{
			ViewBag.AwbId = awbId;

			ViewBag.Brokers = _brokers.GetAll().ToDictionary(x => (long?)x.Id, x => x.Name);
		}

		#region Edit

		[Access(RoleType.Admin, RoleType.Manager)]
		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _awbPresenter.Get(id);

			BindBag(id);

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager)]
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

		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult Create(long? applicationId)
		{
			BindBag(null);

			return View();
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult Create(long? applicationId, AwbAdminModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var data = AwbMapper.GetData(model);

					var id = _awbManager.Create(applicationId, data);

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