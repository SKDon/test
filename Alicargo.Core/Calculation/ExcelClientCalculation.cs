using System.Drawing;
using System.IO;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Excel;
using Alicargo.Core.Resources;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Utilities.Localization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Alicargo.Core.Calculation
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

			var client = _clients.Get(clientId);

			var balance = _balance.GetBalance(clientId);

			var calculations = _calculations.GetByClient(clientId);

			var history = _balance.GetHistory(clientId);

			return Get(client, balance, calculations, history);
		}

		private static MemoryStream Get(ClientData client, decimal balance, CalculationData[] calculations,
			ClientBalanceHistoryItem[] history)
		{
			var stream = new MemoryStream();

			using(var pck = new ExcelPackage())
			{
				var excel = pck.Workbook.Worksheets.Add(Entities.Application);
				excel.Cells.Style.Font.Name = ExcelConstants.DefaultFontName;
				excel.Cells.Style.Font.Size = ExcelConstants.DefaultFontSize;
				excel.Cells.Style.Numberformat.Format = "0.00";

				var columnCount = DrawHeader(excel, balance);

				var iRow = DrawRows(client, calculations, history, columnCount, 4, excel);

				AdjustExcel(columnCount, excel, iRow);

				pck.SaveAs(stream);
			}

			stream.Position = 0;

			return stream;
		}

		private static int DrawRows(ClientData client, CalculationData[] calculations,
			ClientBalanceHistoryItem[] history, int columnCount, int iRow, ExcelWorksheet excel)
		{
			var drawables = DrawableMapper.Get(history, excel, client, columnCount)
				.Union(DrawableMapper.Get(calculations, excel, client, columnCount))
				.OrderBy(x => x.Position)
				.ToArray();

			foreach(var drawable in drawables)
			{
				iRow = drawable.Draw(iRow);
			}

			return iRow;
		}

		private static void AdjustExcel(int count, ExcelWorksheet excel, int iRow)
		{
			for(var j = 1; j <= count; j++)
			{
				excel.Column(j).AutoFit();
				for(var i = 1; i < iRow; i++)
				{
					excel.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Gray);
				}
			}
		}

		private static int DrawHeader(ExcelWorksheet excel, decimal balance)
		{
			var iColumn = 1;
			const int iRow = 2;

			excel.Cells[iRow, iColumn++].Value = Entities.Client;
			excel.Cells[iRow, iColumn++].Value = Entities.DisplayNumber;
			excel.Cells[iRow, iColumn++].Value = Entities.FactoryName;
			excel.Cells[iRow, iColumn++].Value = Entities.Mark;
			excel.Cells[iRow, iColumn++].Value = Entities.Class;
			excel.Cells[iRow, iColumn++].Value = Entities.Count;
			excel.Cells[iRow, iColumn++].Value = Entities.Weight;
			excel.Cells[iRow, iColumn++].Value = Entities.Invoice;
			excel.Cells[iRow, iColumn++].Value = Entities.Value;
			excel.Cells[iRow, iColumn++].Value = Entities.TariffPerKg;
			excel.Cells[iRow, iColumn++].Value = Entities.TotalTariffCost;
			excel.Cells[iRow, iColumn++].Value = Entities.ScotchCost;
			excel.Cells[iRow, iColumn++].Value = Entities.Insurance;
			excel.Cells[iRow, iColumn++].Value = Entities.FactureCost;
			excel.Cells[iRow, iColumn++].Value = Entities.PickupCost;
			excel.Cells[iRow, iColumn++].Value = Entities.TransitCost;
			excel.Cells[iRow, iColumn].Value = Entities.Total;
			excel.Cells[iRow, iColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

			excel.Cells[1, iColumn - 1].Value = Entities.Balance;
			var balanceCell = excel.Cells[1, iColumn];
			balanceCell.Value = balance.ToString("N2") + CurrencyName.Euro;
			balanceCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
			balanceCell.Style.Font.Color.SetColor(Color.Red);

			var headerCells = excel.Cells[1, 1, iRow, iColumn];
			headerCells.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
			headerCells.Style.Font.Bold = true;

			excel.View.FreezePanes(3, iColumn);
			excel.Row(1).Height = ExcelConstants.DefaultRowHeight;
			excel.Row(2).Height = ExcelConstants.DefaultRowHeight;

			return iColumn;
		}
	}
}