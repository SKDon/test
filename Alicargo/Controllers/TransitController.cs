using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
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
			return GetEditPartialView(applicationId, id => _transitRepository.GetByApplication(id));
		}

		private PartialViewResult GetEditPartialView(long? id, Func<long, TransitData> getData)
		{
			ViewData.TemplateInfo.HtmlFieldPrefix = "Transit";

			if (!id.HasValue) return PartialView();

			var data = getData(id.Value);

			var transit = TransitEditModel.GetModel(data);

			ViewBag.TransitId = data.Id;

			return PartialView(transit);
		}

		[ChildActionOnly]
		public virtual PartialViewResult EditByClient(long? clientId)
		{
			return GetEditPartialView(clientId, id => _transitRepository.GetByClient(id));
		}
	}
}