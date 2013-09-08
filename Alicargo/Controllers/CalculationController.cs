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

			var data = fixture.Build<CalculationAwb>()
							  .With(x => x.Rows, fixture.CreateMany<CalculationListItem>(100).ToArray())
							  .CreateMany(10);

			return Json(data);
		}
	}
}