using System.Drawing;
using Alicargo.Core.Contracts.Excel;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Utilities.Localization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Alicargo.Core.Excel.Client
{
	public sealed class ClientBalanceHistoryItemDrawable : IDrawable
	{
		private readonly int _columnCount;
		private readonly ExcelWorksheet _excel;
		private readonly ClientBalanceHistoryItem _item;

		public ClientBalanceHistoryItemDrawable(ClientBalanceHistoryItem item)
		{
			_item = item;
		}

		public ClientBalanceHistoryItemDrawable(ClientBalanceHistoryItem item, int columnCount, ExcelWorksheet excel)
		{
			_item = item;
			_columnCount = columnCount;
			_excel = excel;
		}

		public int Draw(int iRow)
		{
			_excel.Row(iRow++).Height = ExcelConstants.DefaultRowHeight;
			var cultureInfo = CultureProvider.GetCultureInfo();

			var money = _item.EventType == EventType.BalanceDecreased ? -_item.Money : _item.Money;

			_excel.Cells[iRow, _columnCount].Value = _item.EventType.ToLocalString();
			var moneyCell = _excel.Cells[iRow, _columnCount + 1];
			moneyCell.Value = money.ToString("N2") + CurrencyName.Euro;
			moneyCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
			if(_item.EventType == EventType.BalanceDecreased)
			{
				moneyCell.Style.Font.Color.SetColor(Color.Red);
			}
			_excel.Cells[iRow, _columnCount + 2].Value = LocalizationHelper.GetDate(_item.Timestamp, cultureInfo);
			_excel.Cells[iRow, _columnCount + 3].Value = _item.Comment;

			_excel.Row(iRow).Height = ExcelConstants.DefaultRowHeight;
			var range = _excel.Cells[iRow, 1, iRow, _columnCount];
			range.Style.Fill.PatternType = ExcelFillStyle.Solid;
			range.Style.Fill.BackgroundColor.SetColor(_item.EventType == EventType.BalanceDecreased
				? Color.Yellow
				: Color.LawnGreen);

			return ++iRow;
		}

		public long Position
		{
			get { return _item.Timestamp.Ticks; }
		}
	}
}