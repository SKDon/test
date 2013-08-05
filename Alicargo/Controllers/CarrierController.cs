using System.Web.Mvc;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class CarrierController : Controller
	{
		private readonly ICarrierService _carriers;

		public CarrierController(ICarrierService carriers)
		{
			_carriers = carriers;
		}

		[ChildActionOnly]
		// todo: use the SelecetModel
		public virtual PartialViewResult Select(long? selectedId)
		{
			var model = _carriers.Get(selectedId);

			return PartialView(model);
		}
	}
}
