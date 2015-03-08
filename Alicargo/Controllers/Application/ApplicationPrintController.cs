using System.Globalization;
using System.Web.Mvc;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers.Application
{
    public partial class ApplicationPrintController : Controller
    {
        public virtual ActionResult Index(long id)
        {
            return View(new ApplicationPrintModel
            {
                City = "city",
                Text = id.ToString(CultureInfo.InvariantCulture)
            });
        }
    }
}