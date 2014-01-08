using System.Drawing;
using System.IO;
using System.Web;
using Alicargo.Core.Calculation;
using Alicargo.Core.Calculation.Entities;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Resources;

namespace Alicargo.Services.Excel
{
	internal sealed class ExcelClientCalculation : IExcelClientCalculation
	{
		public MemoryStream Get(ClientCalculationGroup[] groups, string language)
		{
			CultureProvider.Current.Set(() => language);

			var stream = new MemoryStream();

			using (var pck = new ExcelPackage())
			{
				var ws = pck.Workbook.Worksheets.Add(Pages.Applications);
				ws.Cells.Style.Font.Name = ExcelConstants.DefaultFontName;
				ws.Cells.Style.Font.Size = ExcelConstants.DefaultFontSize;
				ws.Cells.Style.Numberformat.Format = "0.00";

				var count = DrawHeader(ws);

				var iRow = 2;
				foreach (var @group in groups)
				{
					var awb = HttpUtility.HtmlDecode(@group.value);

					DrawAwb(awb, ws, iRow++, count);

					foreach (var item in @group.items)
					{
						ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
						DrawRow(ws, item, iRow++);
					}
					ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
					DrawGroupTotal(ws, @group, iRow++);
				}

				for (int j = 1; j <= count; j++)
				{
					ws.Column(j).AutoFit();
					for (int i = 1; i < iRow; i++)
					{
						ws.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Gray);
					}
				}

				pck.SaveAs(stream);
			}

			stream.Position = 0;

			return stream;
		}

		private static void DrawGroupTotal(ExcelWorksheet ws, ClientCalculationGroup @group, int iRow)
		{
			ws.Cells[iRow, 17].Value = group.aggregates.Profit.sum;
			ws.Cells[iRow, 1, iRow, 17].Style.Font.Bold = true;
		}

		private static void DrawRow(ExcelWorksheet ws, ClientCalculationItem item, int iRow)
		{
			var iColumn = 1;
			ws.Cells[iRow, iColumn++].Value = item.ClientNic;
			ws.Cells[iRow, iColumn++].Value = item.DisplayNumber;
			ws.Cells[iRow, iColumn++].Value = item.Factory;
			ws.Cells[iRow, iColumn++].Value = item.Mark;
			ws.Cells[iRow, iColumn++].Value = item.ClassName;
			ws.Cells[iRow, iColumn++].Value = item.Count;
			ws.Cells[iRow, iColumn++].Value = item.Weight;
			ws.Cells[iRow, iColumn++].Value = item.Invoice;			
			ws.Cells[iRow, iColumn++].Value = item.Value;
			ws.Cells[iRow, iColumn++].Value = item.TariffPerKg;
			ws.Cells[iRow, iColumn].Style.Font.Bold = true;
			ws.Cells[iRow, iColumn++].Value = item.TotalTariffCost;
			ws.Cells[iRow, iColumn++].Value = item.ScotchCost;
			ws.Cells[iRow, iColumn++].Value = item.InsuranceCost;
			ws.Cells[iRow, iColumn++].Value = item.FactureCost;
			ws.Cells[iRow, iColumn++].Value = item.PickupCost;
			ws.Cells[iRow, iColumn++].Value = item.TransitCost;
			ws.Cells[iRow, iColumn].Style.Font.Bold = true;
			ws.Cells[iRow, iColumn].Value = item.Profit;
		}

		private static void DrawAwb(string currentAwb, ExcelWorksheet ws, int iRow, int count)
		{
			var row = ws.Row(iRow);
			row.Height = ExcelConstants.AwbRowHeight;
			row.Style.Font.Bold = true;

			ws.Cells[iRow, 1].Value = currentAwb;

			var range = ws.Cells[iRow, 1, iRow, count];
			range.Merge = true;
			range.Style.Fill.PatternType = ExcelFillStyle.Solid;
			range.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
		}

		private static int DrawHeader(ExcelWorksheet ws)
		{
			var iColumn = 1;

			ws.Cells[1, iColumn++].Value = Pages.Client;
			ws.Cells[1, iColumn++].Value = Entities.DisplayNumber;
			ws.Cells[1, iColumn++].Value = Entities.FactoryName;
			ws.Cells[1, iColumn++].Value = Entities.Mark;
			ws.Cells[1, iColumn++].Value = Entities.Class;
			ws.Cells[1, iColumn++].Value = Entities.Count;
			ws.Cells[1, iColumn++].Value = Entities.Weight;
			ws.Cells[1, iColumn++].Value = Entities.Invoice;
			ws.Cells[1, iColumn++].Value = Entities.Value;
			ws.Cells[1, iColumn++].Value = Entities.TariffPerKg;
			ws.Cells[1, iColumn++].Value = Entities.TotalTariffCost;
			ws.Cells[1, iColumn++].Value = Entities.ScotchCost;
			ws.Cells[1, iColumn++].Value = Entities.Insurance;
			ws.Cells[1, iColumn++].Value = Entities.FactureCost;
			ws.Cells[1, iColumn++].Value = Entities.PickupCost;
			ws.Cells[1, iColumn++].Value = Entities.TransitCost;
			ws.Cells[1, iColumn].Value = Entities.Total;

			HeaderStyles(ws, ws.Cells[1, 1, 1, iColumn]);

			return iColumn;
		}

		private static void HeaderStyles(ExcelWorksheet ws, ExcelRange cell)
		{
			cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
			cell.Style.Font.Bold = true;
			cell.Style.Locked = true;
			ws.View.FreezePanes(2, 1);
			ws.Row(1).Height = ExcelConstants.DefaultRowHeight;
		}
	}
}