using System.Drawing;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Contracts.Excel;
using Alicargo.Core.Helpers;
using Alicargo.Utilities.Localization;
using OfficeOpenXml;

namespace Alicargo.Core.Calculation
{
	public sealed class ClientBalanceHistoryItemDrawable : IDrawable
	{
		private readonly ClientBalanceHistoryItem _item;
		private readonly int _columnCount;
		private readonly ExcelWorksheet _excel;

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

		public int Draw(int row)
		{
			var cultureInfo = CultureProvider.GetCultureInfo();
			var money = _item.EventType == EventType.BalanceDecreased ? -_item.Money : _item.Money;

			_excel.Cells[row, _columnCount].Value = _item.EventType.ToLocalString();
			_excel.Cells[row, _columnCount + 1].Value = money.ToString("N2") + CurrencyName.Euro;
			if(_item.EventType == EventType.BalanceDecreased)
			{
				_excel.Cells[row, _columnCount + 1].Style.Font.Color.SetColor(Color.Red);
			}
			_excel.Cells[row, _columnCount + 2].Value = LocalizationHelper.GetDate(_item.Timestamp, cultureInfo);
			_excel.Cells[row, _columnCount + 3].Value = _item.Comment;

			return ++row;
		}

		public long Position
		{
			get
			{
				return _item.Timestamp.Ticks;
			}
		}
	}
}