using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alicargo.Controllers
{
	public partial class StateController : Controller
    {
        //
        // GET: /State/

		public virtual ActionResult Index()
        {
            return View();
        }

    }
}
