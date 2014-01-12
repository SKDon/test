using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Contracts.Resources;
using Alicargo.Core.Calculation;
using Alicargo.Core.Helpers;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Alicargo.Services.Excel
{
	public sealed class ExcelClientCalculation : IExcelClientCalculation
	{
		private readonly IClientBalanceRepository _balance;
		private readonly ICalculationRepository _calculations;
		private readonly IClientRepository _clients;

		public ExcelClientCalculation(
			ICalculationRepository calculations,
			IClientBalanceRepository balance,
			IClientRepository clients)
		{
			_balance = balance;
			_calculations = calculations;
			_clients = clients;
		}

		public MemoryStream Get(long clientId, string language)
		{
			CultureProvider.Set(() => language);

			var history = _balance.GetHistory(clientId);

			var client = _clients.Get(clientId);

			var balance = _balance.GetBalance(clientId);

			IReadOnlyDictionary<string, CalculationData[]> groups = _calculations.GetByClient(clientId)
				.GroupBy(x => x.AirWaybillDisplay)
				.ToDictionary(x => x.Key, x => x.ToArray());

			var stream = new MemoryStream();

			using(var pck = new ExcelPackage())
			{
				var ws = pck.Workbook.Worksheets.Add(Entities.Application);
				ws.Cells.Style.Font.Name = ExcelConstants.DefaultFontName;
				ws.Cells.Style.Font.Size = ExcelConstants.DefaultFontSize;
				ws.Cells.Style.Numberformat.Format = "0.00";

				var count = DrawHeader(ws, balance);

				var iRow = 3;
				foreach(var @group in groups)
				{
					var awb = @group.Key;

					DrawAwb(awb, ws, iRow++, count);

					decimal totalProfit = 0;
					foreach(var item in @group.Value)
					{
						ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
						var money = CalculationDataHelper.GetMoney(item);

						DrawRow(iRow++, ws, item, client.Nic, money);
						totalProfit += money;
					}
					ws.Row(iRow).Height = ExcelConstants.DefaultRowHeight;

					DrawGroupTotal(ws, totalProfit, iRow++);
				}

				AdjustExcel(count, ws, iRow);

				pck.SaveAs(stream);
			}

			stream.Position = 0;

			return stream;
		}

		private static void AdjustExcel(int count, ExcelWorksheet ws, int iRow)
		{
			for(var j = 1; j <= count; j++)
			{
				ws.Column(j).AutoFit();
				for(var i = 1; i < iRow; i++)
				{
					ws.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Gray);
				}
			}
		}

		private static void DrawGroupTotal(ExcelWorksheet ws, decimal totalProfit, int iRow)
		{
			const int iColor = 17;
			ws.Cells[iRow, iColor].Value = totalProfit;
			ws.Cells[iRow, 1, iRow, iColor].Style.Font.Bold = true;
		}

		private static void DrawRow(int iRow, ExcelWorksheet ws, CalculationData item, string nic, decimal money)
		{
			var iColumn = 1;
			ws.Cells[iRow, iColumn++].Value = nic;
			ws.Cells[iRow, iColumn++].Value = item.ApplicationDisplay;
			ws.Cells[iRow, iColumn++].Value = item.FactoryName;
			ws.Cells[iRow, iColumn++].Value = item.MarkName;
			ws.Cells[iRow, iColumn++].Value = item.Class;
			ws.Cells[iRow, iColumn++].Value = item.Count;
			ws.Cells[iRow, iColumn++].Value = item.Weight;
			ws.Cells[iRow, iColumn++].Value = item.Invoice;
			ws.Cells[iRow, iColumn++].Value = item.Value;
			ws.Cells[iRow, iColumn++].Value = item.TariffPerKg;
			ws.Cells[iRow, iColumn].Style.Font.Bold = true;
			ws.Cells[iRow, iColumn++].Value = item.TariffPerKg * (decimal)item.Weight;
			ws.Cells[iRow, iColumn++].Value = item.ScotchCost;
			ws.Cells[iRow, iColumn++].Value = item.InsuranceCost;
			ws.Cells[iRow, iColumn++].Value = item.FactureCost;
			ws.Cells[iRow, iColumn++].Value = item.PickupCost;
			ws.Cells[iRow, iColumn++].Value = item.TransitCost;
			ws.Cells[iRow, iColumn].Style.Font.Bold = true;
			ws.Cells[iRow, iColumn].Value = money;
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

			ws.Cells[iRow, iColumn++].Value = Entities.Client;
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
			var balanceCell = ws.Cells[1, iColumn];
			balanceCell.Value = balance.ToString("N2") + CurrencyName.Euro;
			balanceCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
			balanceCell.Style.Font.Color.SetColor(Color.Red);

			var headerCells = ws.Cells[1, 1, iRow, iColumn];
			headerCells.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
			headerCells.Style.Font.Bold = true;

			ws.View.FreezePanes(3, iColumn);
			ws.Row(1).Height = ExcelConstants.DefaultRowHeight;
			ws.Row(2).Height = ExcelConstants.DefaultRowHeight;

			return iColumn;
		}
	}
}