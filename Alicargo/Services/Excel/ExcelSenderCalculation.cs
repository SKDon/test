using System.Drawing;
using System.Web;
using Alicargo.Core.Excel;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.Calculation.Sender;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Resources;

namespace Alicargo.Services.Excel
{
	internal sealed class ExcelSenderCalculation
	{
		internal MemoryStream Get(SenderCalculationListCollection data, string language)
		{
			CultureProvider.Set(language);

			var stream = new MemoryStream();

			using (var pck = new ExcelPackage())
			{
				var ws = pck.Workbook.Worksheets.Add(Pages.Applications);
				ws.Cells.Style.Font.Name = ExcelConstants.DefaultFontName;
				ws.Cells.Style.Font.Size = ExcelConstants.DefaultFontSize;
				ws.Cells.Style.Numberformat.Format = "0.00";

				var count = DrawHeader(ws);

				var iRow = 2;
				for (int index = 0; index < data.Groups.Length; index++)
				{
					var group = data.Groups[index];
					var awb = HttpUtility.HtmlDecode(((dynamic)@group.value).text);

					DrawAwb(awb, ws, iRow++, count);

					foreach (var item in group.items)
					{
						ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
						DrawRow(ws, item, iRow++);
					}
					ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
					DrawGroupTotal(ws, group, iRow++);

					iRow = DrawInfo(ws, data.Info[index], iRow, count);
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

		private static void DrawGroupTotal(ExcelWorksheet ws, SenderCalculationGroup @group, int iRow)
		{
			ws.Cells[iRow, 6].Value = @group.aggregates.Value.sum;

			ws.Cells[iRow, 7].Value = group.aggregates.Count.sum;
			ws.Cells[iRow, 8].Value = group.aggregates.Weight.sum;
			ws.Cells[iRow, 10].Value = group.aggregates.TotalSenderRate.sum;
			ws.Cells[iRow, 11].Value = group.aggregates.SenderScotchCost.sum;
			ws.Cells[iRow, 12].Value = group.aggregates.FactureCost.sum;
			ws.Cells[iRow, 13].Value = group.aggregates.PickupCost.sum;
			ws.Cells[iRow, 14].Value = group.aggregates.Profit.sum;
			ws.Cells[iRow, 1, iRow, 14].Style.Font.Bold = true;
		}

		private static int DrawInfo(ExcelWorksheet ws, SenderCalculationAwbInfo info, int iRow, int count)
		{
			var range = ws.Cells[iRow, 1, iRow, count];
			range.Merge = true;
			range.Style.Fill.PatternType = ExcelFillStyle.Solid;
			range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			iRow++;

			ws.Cells[iRow, 1].Value = "sender";
			ws.Cells[iRow, 2].Value = info.CostPerKgOfSender;
			ws.Cells[iRow, 10].Value = info.TotalSenderRate;
			ws.Cells[iRow, 11].Value = info.TotalScotchCost;
			ws.Cells[iRow, 12].Value = info.TotalFactureCost;
			ws.Cells[iRow, 13].Value = info.TotalPickupCost;
			ws.Cells[iRow, 14].Value = info.TotalOfSender;
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			ws.Cells[iRow, 1, iRow, 14].Style.Font.Bold = true;
			iRow++;

			ws.Cells[iRow, 1].Value = "flight";
			ws.Cells[iRow, 2].Value = info.FlightCostPerKg;
			ws.Cells[iRow, 14].Value = info.FlightCost;
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			ws.Cells[iRow, 1, iRow, 14].Style.Font.Bold = true;
			iRow++;

			ws.Cells[iRow, 1].Value = "cost total";
			ws.Cells[iRow, 14].Value = info.Total;
			var rangeCost = ws.Cells[iRow, 1, iRow, count];
			rangeCost.Style.Fill.PatternType = ExcelFillStyle.Solid;
			rangeCost.Style.Fill.BackgroundColor.SetColor(Color.HotPink);
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			ws.Cells[iRow, 1, iRow, 14].Style.Font.Bold = true;
			iRow++;

			return iRow;
		}

		private static void DrawRow(ExcelWorksheet ws, SenderCalculationItem item, int iRow)
		{
			var iColumn = 1;
			ws.Cells[iRow, iColumn++].Value = item.ClientNic;
			ws.Cells[iRow, iColumn++].Value = item.DisplayNumber;
			ws.Cells[iRow, iColumn++].Value = item.Factory;
			ws.Cells[iRow, iColumn++].Value = item.Mark;
			ws.Cells[iRow, iColumn++].Value = item.Invoice;
			ws.Cells[iRow, iColumn++].Value = item.Value;
			ws.Cells[iRow, iColumn++].Value = item.Count;
			ws.Cells[iRow, iColumn++].Value = item.Weight;
			ws.Cells[iRow, iColumn++].Value = item.SenderRate;
			ws.Cells[iRow, iColumn].Style.Font.Bold = true;
			ws.Cells[iRow, iColumn++].Value = item.TotalSenderRate;
			ws.Cells[iRow, iColumn++].Value = item.SenderScotchCost;
			ws.Cells[iRow, iColumn++].Value = item.FactureCost;
			ws.Cells[iRow, iColumn++].Value = item.PickupCost;
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
			ws.Cells[1, iColumn++].Value = Entities.Invoice;
			ws.Cells[1, iColumn++].Value = Entities.Value;
			ws.Cells[1, iColumn++].Value = Entities.Count;
			ws.Cells[1, iColumn++].Value = Entities.Weight;
			ws.Cells[1, iColumn++].Value = Entities.SenderRate;
			ws.Cells[1, iColumn++].Value = Entities.TotalSenderRate;
			ws.Cells[1, iColumn++].Value = Entities.ScotchCost;
			ws.Cells[1, iColumn++].Value = Entities.FactureCost;
			ws.Cells[1, iColumn++].Value = Entities.PickupCost;
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