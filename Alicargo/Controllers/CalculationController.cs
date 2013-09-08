using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Helpers;
using Alicargo.ViewModels;
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

		[Access(RoleType.Admin), HttpPost]
		public virtual JsonResult List()
		{
			// todo: 3. remove fixture from references
			var fixture = new Fixture();

			fixture.Register(() => fixture.Build<CurrencyModel>().With(x => x.CurrencyId, fixture.Create<int>() % 3 + 1).Create());

			var data = fixture.Build<CalculationAwb>()
							  .With(x => x.Rows, fixture.Build<CalculationListItem>()
														.With(x => x.ValueCurrencyId, fixture.Create<int>() % 3 + 1)
														.CreateMany(10).ToArray())
							  .CreateMany(10);

			return Json(data);
		}
	}
}