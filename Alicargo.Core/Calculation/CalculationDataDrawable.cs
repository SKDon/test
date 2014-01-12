using System.Drawing;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Contracts.Excel;
using Alicargo.Core.Excel;
using Alicargo.Core.Helpers;
using Alicargo.Utilities.Localization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Alicargo.Core.Calculation
{
	public sealed class CalculationDataDrawable : IDrawable
	{
		private readonly string _clientNick;
		private readonly int _columnCount;
		private readonly CalculationData _data;
		private readonly ExcelWorksheet _excel;

		public CalculationDataDrawable(CalculationData data, string clientNick, int columnCount, ExcelWorksheet excel)
		{
			_data = data;
			_clientNick = clientNick;
			_excel = excel;
			_columnCount = columnCount;
		}

		public int Draw(int iRow)
		{
			_excel.Row(iRow++).Height = ExcelConstants.DefaultRowHeight;
			DrawAwb(_data.AirWaybillDisplay, _excel, iRow++, _columnCount);

			_excel.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			DrawRow(iRow++);

			return iRow;
		}

		public long Position
		{
			get { return _data.CreationTimestamp.Ticks; }
		}

		private static void DrawAwb(string currentAwb, ExcelWorksheet excel, int iRow, int count)
		{
			var row = excel.Row(iRow);
			row.Height = ExcelConstants.AwbRowHeight;
			row.Style.Font.Bold = true;

			excel.Cells[iRow, 1].Value = currentAwb;

			var range = excel.Cells[iRow, 1, iRow, count];
			range.Merge = true;
			range.Style.Fill.PatternType = ExcelFillStyle.Solid;
			range.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
		}

		private void DrawRow(int iRow)
		{
			var money = CalculationDataHelper.GetMoney(_data);

			var iColumn = 1;
			_excel.Cells[iRow, iColumn++].Value = _clientNick;
			_excel.Cells[iRow, iColumn++].Value = _data.ApplicationDisplay;
			_excel.Cells[iRow, iColumn++].Value = _data.FactoryName;
			_excel.Cells[iRow, iColumn++].Value = _data.MarkName;
			_excel.Cells[iRow, iColumn++].Value = _data.Class;
			_excel.Cells[iRow, iColumn++].Value = _data.Count;
			_excel.Cells[iRow, iColumn++].Value = _data.Weight;
			_excel.Cells[iRow, iColumn++].Value = _data.Invoice;
			_excel.Cells[iRow, iColumn++].Value = _data.Value;
			_excel.Cells[iRow, iColumn++].Value = _data.TariffPerKg;
			_excel.Cells[iRow, iColumn].Style.Font.Bold = true;
			_excel.Cells[iRow, iColumn++].Value = _data.TariffPerKg * (decimal)_data.Weight;
			_excel.Cells[iRow, iColumn++].Value = _data.ScotchCost;
			_excel.Cells[iRow, iColumn++].Value = _data.InsuranceCost;
			_excel.Cells[iRow, iColumn++].Value = _data.FactureCost;
			_excel.Cells[iRow, iColumn++].Value = _data.PickupCost;
			_excel.Cells[iRow, iColumn++].Value = _data.TransitCost;
			_excel.Cells[iRow, iColumn].Style.Font.Bold = true;
			_excel.Cells[iRow, iColumn].Value = money;

			_excel.Cells[iRow, iColumn + 1].Value = (-money).ToString("N2") + CurrencyName.Euro;
			_excel.Cells[iRow, iColumn + 1].Style.Font.Color.SetColor(Color.Red);
			var cultureInfo = CultureProvider.GetCultureInfo();
			_excel.Cells[iRow, _columnCount + 2].Value = LocalizationHelper.GetDate(_data.CreationTimestamp, cultureInfo);
		}
	}
}