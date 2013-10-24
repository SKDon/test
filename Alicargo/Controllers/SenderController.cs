using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.User;

namespace Alicargo.Controllers
{
	public partial class SenderController : Controller
	{
		private readonly ISenderService _senders;

		public SenderController(ISenderService senders)
		{
			_senders = senders;
		}

		[Access(RoleType.Admin), HttpGet]
		public virtual ViewResult Create()
		{
			return View();
		}

		[Access(RoleType.Admin, RoleType.Sender), HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _senders.Get(id);

			return View(model);
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual ActionResult Create(SenderModel model)
		{
			if (!ModelState.IsValid) return View();

			var id = _senders.Add(model);

			return RedirectToAction(MVC.Sender.Edit(id));
		}

		[Access(RoleType.Admin, RoleType.Sender), HttpPost]
		public virtual ActionResult Edit(long id, SenderModel model)
		{
			if (!ModelState.IsValid) return View();

			_senders.Update(id, model);

			return RedirectToAction(MVC.Sender.Edit(id));
		}
	}
}
