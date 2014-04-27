using System.Web.Mvc;
using Alicargo.Areas.Admin.Serivices;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;

namespace Alicargo.Areas.Admin.Controllers
{
	public partial class BillController : Controller
	{
		private readonly IBillModelFactory _modelFactory;
		private readonly ISettingRepository _settings;

		public BillController(ISettingRepository settings, IBillModelFactory modelFactory)
		{
			_settings = settings;
			_modelFactory = modelFactory;
		}

		public virtual PartialViewResult Preview(long applicationId)
		{
			var billNumber = _settings.GetData<int>(SettingType.ApplicationNumberCounter);
			var model = _modelFactory.GetModel(applicationId);

			ViewBag.BillNumber = billNumber;

			return PartialView(model);
		}
	}
}