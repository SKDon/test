using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Alicargo.Core.Localization;
using Alicargo.Services.Abstract;
using OfficeOpenXml;
using Resources;

namespace Alicargo.Services.Excel
{
	internal sealed class ExcelGenerator : IExcelGenerator
	{
		public MemoryStream Get<T>(T[] rows)
		{
			var properties = typeof(T).GetProperties();

			var stream = new MemoryStream();

			using (var pck = new ExcelPackage())
			{
				var ws = pck.Workbook.Worksheets.Add(Pages.Applications);

				DrawHeader(properties, ws);

				DrawRows(rows, properties, ws);

				pck.SaveAs(stream);
			}

			stream.Position = 0;
			return stream;
		}

		private static void DrawRows<T>(IEnumerable<T> rows, IList<PropertyInfo> properties, ExcelWorksheet ws)
		{
			var iRow = 2;
			foreach (var row in rows)
			{
				var iColumn = 1;
				foreach (var property in properties)
				{
					var value = property.GetValue(row);

					ws.Cells[iRow, iColumn++].Value = value;
				}
				iRow++;
			}
		}

		private static void DrawHeader(IEnumerable<PropertyInfo> properties, ExcelWorksheet ws)
		{
			var iColumn = 1;
			foreach (var property in properties)
			{
				var displayName = property.GetCustomAttribute<DisplayNameLocalizedAttribute>();

				ws.Cells[1, iColumn++].Value = displayName.DisplayName;
			}
		}
	}
}