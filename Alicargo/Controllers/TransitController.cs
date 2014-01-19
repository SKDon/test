using System.Web.Mvc;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.ViewModels;
using Antlr.Runtime.Misc;

namespace Alicargo.Controllers
{
	public partial class TransitController : Controller
	{
		private readonly ITransitRepository _transitRepository;

		public TransitController(ITransitRepository transitRepository)
		{
			_transitRepository = transitRepository;
		}

		[ChildActionOnly]
		public virtual PartialViewResult EditByApplication(long? applicationId)
		{
			ViewData.TemplateInfo.HtmlFieldPrefix = "Transit";

			return !applicationId.HasValue
				? PartialView()
				: GetEditPartialView(() => _transitRepository.GetByApplication(applicationId.Value));
		}

		private PartialViewResult GetEditPartialView(Func<TransitData> getData)
		{
			var data = getData();

			var transit = TransitEditModel.GetModel(data);

			ViewBag.TransitId = data.Id;

			return PartialView(transit);
		}

		[ChildActionOnly]
		public virtual PartialViewResult EditByClient(long? clientId)
		{
			ViewData.TemplateInfo.HtmlFieldPrefix = "Transit";

			return !clientId.HasValue
				? PartialView()
				: GetEditPartialView(() => _transitRepository.GetByClient(clientId.Value));
		}
	}
}