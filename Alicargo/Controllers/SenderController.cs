using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;
using Alicargo.ViewModels.User;

namespace Alicargo.Controllers
{
	public partial class SenderController : Controller
    {
		[Access(RoleType.Admin), HttpGet]
		public virtual ActionResult Create()
        {
            return View();
        }

		[Access(RoleType.Admin, RoleType.Sender), HttpGet]
		public virtual ActionResult Edit(long id)
		{
			return View();
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual ActionResult Create(SenderModel model)
		{
			return View();
		}

		[Access(RoleType.Admin, RoleType.Sender), HttpPost]
		public virtual ActionResult Edit(long id, SenderModel model)
		{
			return View();
		}
    }
}
