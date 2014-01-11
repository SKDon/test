using System.Drawing;
using System.Web;
using Alicargo.Contracts.Resources;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.Calculation.Admin;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Resources;

namespace Alicargo.Services.Excel
{
	internal sealed class ExcelAdminCalculation
	{
		internal MemoryStream Get(CalculationListCollection data, string language)
		{
			CultureProvider.Set(() => language);

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

		private static void DrawGroupTotal(ExcelWorksheet ws, CalculationGroup @group, int iRow)
		{
			ws.Cells[iRow, 6].Value = group.aggregates.Count.sum;
			ws.Cells[iRow, 7].Value = group.aggregates.Weight.sum;
			ws.Cells[iRow, 9].Value = group.aggregates.TotalSenderRate.sum;
			ws.Cells[iRow, 11].Value = group.aggregates.Value.sum;
			ws.Cells[iRow, 13].Value = group.aggregates.TotalTariffCost.sum;
			ws.Cells[iRow, 14].Value = group.aggregates.ScotchCost.sum;
			ws.Cells[iRow, 15].Value = group.aggregates.InsuranceCost.sum;
			ws.Cells[iRow, 18].Value = group.aggregates.TransitCost.sum;
			ws.Cells[iRow, 19].Value = group.aggregates.Profit.sum;
			ws.Cells[iRow, 1, iRow, 19].Style.Font.Bold = true;
		}

		private static int DrawInfo(ExcelWorksheet ws, CalculationAwbInfo info, int iRow, int count)
		{
			var range = ws.Cells[iRow, 1, iRow, count];
			range.Merge = true;
			range.Style.Fill.PatternType = ExcelFillStyle.Solid;
			range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			iRow++;

			ws.Cells[iRow, 1].Value = "sender";
			ws.Cells[iRow, 1].Style.Font.Bold = true;
			ws.Cells[iRow, 8].Value = info.CostPerKgOfSender;
			ws.Cells[iRow, 9].Style.Font.Bold = true;
			ws.Cells[iRow, 9].Value = info.TotalSenderRate;
			ws.Cells[iRow, 14].Value = info.TotalScotchCost;
			ws.Cells[iRow, 16].Value = info.TotalFactureCost;
			ws.Cells[iRow, 17].Value = info.TotalPickupCost;
			ws.Cells[iRow, 19].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Value = info.TotalOfSender;
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			iRow++;

			ws.Cells[iRow, 1].Value = "flight";
			ws.Cells[iRow, 1].Style.Font.Bold = true;
			ws.Cells[iRow, 9].Style.Font.Bold = true;
			ws.Cells[iRow, 9].Value = info.FlightCostPerKg;
			ws.Cells[iRow, 19].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Value = info.FlightCost;
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			iRow++;

			ws.Cells[iRow, 1].Value = "custom";
			ws.Cells[iRow, 1].Style.Font.Bold = true;
			ws.Cells[iRow, 9].Style.Font.Bold = true;
			ws.Cells[iRow, 9].Value = info.CustomCostPerKg;
			ws.Cells[iRow, 19].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Value = info.CustomCost;
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			iRow++;

			ws.Cells[iRow, 1].Value = "broker";
			ws.Cells[iRow, 1].Style.Font.Bold = true;
			ws.Cells[iRow, 9].Style.Font.Bold = true;
			ws.Cells[iRow, 9].Value = info.BrokerCostPerKg;
			ws.Cells[iRow, 19].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Value = info.BrokerCost;
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			iRow++;

			ws.Cells[iRow, 1].Value = "insurance";
			ws.Cells[iRow, 1].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Value = info.TotalInsuranceCost;
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			iRow++;

			ws.Cells[iRow, 1].Value = "forwarder";
			ws.Cells[iRow, 1].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Value = info.TotalTransitCost;
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			iRow++;

			ws.Cells[iRow, 1].Value = "additional";
			ws.Cells[iRow, 1].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Value = info.AdditionalCost;
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			iRow++;

			ws.Cells[iRow, 1].Value = "cost total";
			ws.Cells[iRow, 1].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Value = info.TotalExpenses;
			var rangeCost = ws.Cells[iRow, 1, iRow, count];
			rangeCost.Style.Fill.PatternType = ExcelFillStyle.Solid;
			rangeCost.Style.Fill.BackgroundColor.SetColor(Color.HotPink);
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			iRow++;

			ws.Cells[iRow, 18].Value = info.ProfitPerKg;
			if (info.ProfitPerKg <= 0)
			{
				ws.Cells[iRow, 18].Style.Font.Color.SetColor(Color.Red);
			}
			ws.Cells[iRow, 19].Style.Font.Bold = true;
			ws.Cells[iRow, 19].Value = info.Profit;
			if (info.Profit <= 0)
			{
				ws.Cells[iRow, 19].Style.Font.Color.SetColor(Color.Red);
			}
			ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			iRow++;

			return iRow;
		}

		private static void DrawRow(ExcelWorksheet ws, CalculationItem item, int iRow)
		{
			var iColumn = 1;
			ws.Cells[iRow, iColumn++].Value = item.ClientNic;
			ws.Cells[iRow, iColumn++].Value = item.DisplayNumber;
			ws.Cells[iRow, iColumn++].Value = item.Factory;
			ws.Cells[iRow, iColumn++].Value = item.Mark;
			ws.Cells[iRow, iColumn++].Value = item.ClassId.HasValue ? item.ClassId.Value.ToLocalString() : "";
			ws.Cells[iRow, iColumn++].Value = item.Count;
			ws.Cells[iRow, iColumn++].Value = item.Weight;
			ws.Cells[iRow, iColumn++].Value = item.SenderRate;
			ws.Cells[iRow, iColumn].Style.Font.Bold = true;
			ws.Cells[iRow, iColumn++].Value = item.TotalSenderRate;
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
			ws.Cells[1, iColumn++].Value = Entities.SenderRate;
			ws.Cells[1, iColumn++].Value = Entities.TotalSenderRate;
			ws.Cells[1, iColumn++].Value = Entities.Invoice;
			ws.Cells[1, iColumn++].Value = Entities.Value;
			ws.Cells[1, iColumn++].Value = Entities.TariffPerKg;
			ws.Cells[1, iColumn++].Value = Entities.TotalTariffCost;
			ws.Cells[1, iColumn++].Value = Entities.ScotchCost;
			ws.Cells[1, iColumn++].Value = Entities.Insurance;
			ws.Cells[1, iColumn++].Value = Entities.FactureCost;
			ws.Cells[1, iColumn++].Value = Entities.PickupCost;
			ws.Cells[1, iColumn++].Value = Entities.TransitCost;
			ws.Cells[1, iColumn].Value = Entities.Profit;

			HeaderStyles(ws.Cells[1, 1, 1, iColumn]);
			ws.View.FreezePanes(2, 1);
			ws.Row(1).Height = ExcelConstants.DefaultRowHeight;

			return iColumn;
		}

		private static void HeaderStyles(ExcelRange cell)
		{
			cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
			cell.Style.Font.Bold = true;
			cell.Style.Locked = true;
		}
	}
}