using System.Linq;
using System.Web.Mvc;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers.Application
{
	public partial class ApplicationPrintController : Controller
	{
		private readonly IApplicationRepository _applications;
		private readonly ICityRepository _cities;

		public ApplicationPrintController(IApplicationRepository applications, ICityRepository cities)
		{
			_applications = applications;
			_cities = cities;
		}

		public virtual ActionResult Index(long id)
		{
			var data = _applications.Get(id);

			var city = _cities.All(TwoLetterISOLanguageName.English).Single(x => x.Id == data.TransitCityId);

			return View(
				new ApplicationPrintModel
				{
					City = city.Name,
					ClientNic = data.ClientNic,
					Text = data.GetApplicationDisplay()
				});
		}
	}
}