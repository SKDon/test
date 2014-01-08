using System.Drawing;
using System.IO;
using System.Web;
using Alicargo.Core.Calculation;
using Alicargo.Core.Calculation.Entities;
using Alicargo.Core.Enums;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Resources;

namespace Alicargo.Services.Excel
{
	internal sealed class ExcelClientCalculation : IExcelClientCalculation
	{
		public MemoryStream Get(ClientCalculationGroup[] groups, decimal balance, string language)
		{
			CultureProvider.Current.Set(() => language);

			var stream = new MemoryStream();

			using(var pck = new ExcelPackage())
			{
				var ws = pck.Workbook.Worksheets.Add(Pages.Applications);
				ws.Cells.Style.Font.Name = ExcelConstants.DefaultFontName;
				ws.Cells.Style.Font.Size = ExcelConstants.DefaultFontSize;
				ws.Cells.Style.Numberformat.Format = "0.00";

				var count = DrawHeader(ws, balance);

				var iRow = 3;
				foreach(var @group in groups)
				{
					var awb = HttpUtility.HtmlDecode(@group.value);

					DrawAwb(awb, ws, iRow++, count);

					foreach(var item in @group.items)
					{
						ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
						DrawRow(ws, item, iRow++);
					}
					ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
					DrawGroupTotal(ws, @group, iRow++);
				}

				for(var j = 1; j <= count; j++)
				{
					ws.Column(j).AutoFit();
					for(var i = 1; i < iRow; i++)
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

		private static int DrawHeader(ExcelWorksheet ws, decimal balance)
		{
			var iColumn = 1;
			const int iRow = 2;

			ws.Cells[iRow, iColumn++].Value = Pages.Client;
			ws.Cells[iRow, iColumn++].Value = Entities.DisplayNumber;
			ws.Cells[iRow, iColumn++].Value = Entities.FactoryName;
			ws.Cells[iRow, iColumn++].Value = Entities.Mark;
			ws.Cells[iRow, iColumn++].Value = Entities.Class;
			ws.Cells[iRow, iColumn++].Value = Entities.Count;
			ws.Cells[iRow, iColumn++].Value = Entities.Weight;
			ws.Cells[iRow, iColumn++].Value = Entities.Invoice;
			ws.Cells[iRow, iColumn++].Value = Entities.Value;
			ws.Cells[iRow, iColumn++].Value = Entities.TariffPerKg;
			ws.Cells[iRow, iColumn++].Value = Entities.TotalTariffCost;
			ws.Cells[iRow, iColumn++].Value = Entities.ScotchCost;
			ws.Cells[iRow, iColumn++].Value = Entities.Insurance;
			ws.Cells[iRow, iColumn++].Value = Entities.FactureCost;
			ws.Cells[iRow, iColumn++].Value = Entities.PickupCost;
			ws.Cells[iRow, iColumn++].Value = Entities.TransitCost;
			ws.Cells[iRow, iColumn].Value = Entities.Total;
			ws.Cells[iRow, iColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

			ws.Cells[1, iColumn - 1].Value = Entities.Balance;
			ws.Cells[1, iColumn].Value = balance.ToString("N2") + CurrencyName.Euro;
			ws.Cells[1, iColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

			var cell = ws.Cells[1, 1, iRow, iColumn];
			cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
			cell.Style.Font.Bold = true;
			ws.View.FreezePanes(3, iColumn);
			ws.Row(1).Height = ExcelConstants.DefaultRowHeight;
			ws.Row(2).Height = ExcelConstants.DefaultRowHeight;

			return iColumn;
		}
	}
}