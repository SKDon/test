using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using Alicargo.Core.Localization;
using Alicargo.Core.Services;
using Alicargo.Services.Excel.Rows;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Resources;

namespace Alicargo.Services.Excel
{
	public sealed class ExcelGenerator<T> : IExcelGenerator<T>
		where T : BaseApplicationExcelRow
	{
		public MemoryStream Get(T[] rows, string twoLetterISOLanguageName)
		{
			CultureContext.Current.Set(() => twoLetterISOLanguageName);

			var properties = rows.GetType().GetElementType().GetProperties();

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

		private static void DrawRows(IEnumerable<T> rows, IList<PropertyInfo> properties, ExcelWorksheet ws)
		{
			var iRow = 2;
			string currentAwb = null;
			foreach (var row in rows)
			{
				if (currentAwb != row.AirWaybillDisplay)
				{
					currentAwb = row.AirWaybillDisplay;
					if (!string.IsNullOrWhiteSpace(currentAwb))
					{
						DrawAwb(currentAwb, ws, iRow++);
					}
				}

				DrawColumns(properties, ws, row, iRow++);
			}
		}

		private static void DrawAwb(string currentAwb, ExcelWorksheet ws, int iRow)
		{
			var row = ws.Row(iRow);
			row.Style.Fill.PatternType = ExcelFillStyle.Solid;
			row.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
			row.Height = 20;
			row.Style.Font.Bold = true;

			var cell = ws.Cells[iRow, 1];
			cell.Value = currentAwb;
		}

		private static void DrawColumns(IEnumerable<PropertyInfo> properties, ExcelWorksheet ws, T row, int iRow)
		{
			var iColumn = 1;
			foreach (var property in properties)
			{
				var displayName = property.GetCustomAttribute<DisplayNameLocalizedAttribute>();
				if (displayName != null)
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
			}
		}

		private static void DrawHeader(IEnumerable<PropertyInfo> properties, ExcelWorksheet ws)
		{
			var iColumn = 1;
			foreach (var property in properties)
			{
				var displayName = property.GetCustomAttribute<DisplayNameLocalizedAttribute>();

				if (displayName != null)
				{
					var cell = ws.Cells[1, iColumn++];

					cell.Value = displayName.DisplayName;

					HeaderStyles(cell);
				}
			}

			ws.View.FreezePanes(2, 1);
		}

		// ReSharper disable SuggestBaseTypeForParameter

		private static void RowStyles(ExcelRange cell)
		{
			cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Gray);
		}

		private static void HeaderStyles(ExcelRange cell) // ReSharper restore SuggestBaseTypeForParameter
		{
			cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
			cell.Style.Font.Bold = true;
			cell.Style.Locked = true;
		}
	}
}