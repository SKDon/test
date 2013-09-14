using System.Web;
using System.Web.Mvc;
using Alicargo.MvcHelpers;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.ModelBinders
{
	internal sealed class SenderAwbModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var model = (SenderAwbModel)base.BindModel(controllerContext, bindingContext);

			ReadFiles(controllerContext.HttpContext.Request, model);

			return model;
		}

		private static void ReadFiles(HttpRequestBase request, SenderAwbModel model)
		{
			if (model.PackingFile == null && model.PackingFileName == null)
				request.ReadFile("PackingFile", (name, bytes) =>
				{
					model.PackingFileName = name;
					model.PackingFile = bytes;
				});

			if (model.AWBFile == null && model.AWBFileName == null)
				request.ReadFile("AWBFile", (name, bytes) =>
				{
					model.AWBFileName = name;
					model.AWBFile = bytes;
				});
		}
	}
}