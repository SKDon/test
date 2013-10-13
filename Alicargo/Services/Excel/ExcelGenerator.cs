using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using Alicargo.Core.Localization;
using Alicargo.Core.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Resources;

namespace Alicargo.Services.Excel
{
	public sealed class ExcelGenerator : IExcelGenerator
	{
		public MemoryStream Get<T>(T[] rows, string twoLetterISOLanguageName)
		{
			CultureContext.Current.Set(() => twoLetterISOLanguageName);

			var properties = typeof(T).GetProperties();

			var stream = new MemoryStream();

			using (var pck = new ExcelPackage())
			{
				var ws = pck.Workbook.Worksheets.Add(Pages.Applications);

				DrawHeader(properties, ws);

				DrawRows(rows, properties, ws);

				for (var iCol = 1; iCol <= properties.Length; iCol++)
				{
					ws.Column(iCol).AutoFit();
				}

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

					var cell = ws.Cells[iRow, iColumn++];

					cell.Value = value;

					if (property.PropertyType == typeof(float)
						|| property.PropertyType == typeof(decimal)
						|| property.PropertyType == typeof(double)
						|| property.PropertyType == typeof(float?)
						|| property.PropertyType == typeof(decimal?)
						|| property.PropertyType == typeof(double?))
					{
						cell.Style.Numberformat.Format = "0.00";
					}

					RowStyles(cell);
				}

				iRow++;
			}
		}

		private static void RowStyles(ExcelRange cell)
		{
			cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Gray);
		}

		private static void DrawHeader(IEnumerable<PropertyInfo> properties, ExcelWorksheet ws)
		{
			var iColumn = 1;
			foreach (var property in properties)
			{
				var displayName = property.GetCustomAttribute<DisplayNameLocalizedAttribute>();

				var cell = ws.Cells[1, iColumn];

				cell.Value = displayName.DisplayName;

				HeaderStyles(cell);

				iColumn++;
			}
		}

		private static void HeaderStyles(ExcelRange cell)
		{
			cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
			cell.Style.Font.Bold = true;
		}
	}
}