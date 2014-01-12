using Alicargo.Contracts.Contracts.User;
using Alicargo.Core.Contracts.Excel;
using OfficeOpenXml;

namespace Alicargo.Core.Calculation
{
	public sealed class ClientBalanceHistoryItemDrawable : IDrawable
	{
		private readonly ClientBalanceHistoryItem _item;
		private readonly string _nick;
		private readonly int _columnCount;
		private readonly ExcelWorksheet _excel;

		public ClientBalanceHistoryItemDrawable(ClientBalanceHistoryItem item)
		{
			_item = item;
		}

		public ClientBalanceHistoryItemDrawable(ClientBalanceHistoryItem item, string nick, int columnCount, ExcelWorksheet excel)
		{
			_item = item;
			_nick = nick;
			_columnCount = columnCount;
			_excel = excel;
		}

		public int Draw(int row)
		{
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