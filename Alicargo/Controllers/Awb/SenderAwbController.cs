using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;
using Resources;

namespace Alicargo.Controllers.Awb
{
	public partial class SenderAwbController : Controller
	{
		private readonly IAwbManager _awbManager;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbUpdateManager _awbUpdateManager;

		public SenderAwbController(IAwbUpdateManager awbUpdateManager, IAwbManager awbManager, IAwbPresenter awbPresenter)
		{
			_awbUpdateManager = awbUpdateManager;
			_awbManager = awbManager;
			_awbPresenter = awbPresenter;
		}

		[Access(RoleType.Sender), HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _awbPresenter.GetSenderAwbModel(id);

			ViewBag.AwbId = id;

			return View(model);
		}

		[Access(RoleType.Sender), HttpPost]
		public virtual ActionResult Edit(long id, AwbSenderModel model)
		{
			if (!ModelState.IsValid) return View(model);

			try
			{
				_awbUpdateManager.Update(id, model);
			}
			catch (DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
				return View(model);
			}

			return RedirectToAction(MVC.SenderAwb.Edit(id));
		}

		[Access(RoleType.Sender)]
		public virtual ActionResult Create(long? id)
		{
			return View();
		}

		[HttpPost, Access(RoleType.Sender)]
		public virtual ActionResult Create(long? id, AwbSenderModel model)
		{
			if (!ModelState.IsValid) return View(model);

			try
			{
				_awbManager.Create(id, model);

				return RedirectToAction(MVC.AirWaybill.Index());
			}
			catch (DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);

				return View(model);
			}			
		}
	}
}
