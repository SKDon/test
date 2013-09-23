using System.IO;
using Alicargo.Services.Abstract;
using OfficeOpenXml;
using Resources;

namespace Alicargo.Services.Excel
{
	internal sealed class ExcelGenerator : IExcelGenerator
	{
		public MemoryStream Get(ApplicationExcelRow[] rows)
		{
			var stream = new MemoryStream();

			using (var pck = new ExcelPackage())
			{
				var ws = pck.Workbook.Worksheets.Add(Pages.Applications);



				pck.SaveAs(stream);
			}

			stream.Position = 0;
			return stream;
		}
	}
}