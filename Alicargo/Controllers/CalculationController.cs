using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Helpers;
using Alicargo.ViewModels.Calculation;
using Ploeh.AutoFixture;

namespace Alicargo.Controllers
{
	public partial class CalculationController : Controller
    {
		[Access(RoleType.Admin), HttpGet]
		public virtual ActionResult Index()
        {
            return View();
        }

		[Access(RoleType.Admin), HttpGet]
		public virtual JsonResult List()
		{
			// todo: 3. remove fixture from references
			var fixture = new Fixture();
			var items = fixture.CreateMany<CalculationListItem>(100).ToArray();

			return Json(items);
		}
    }
}
