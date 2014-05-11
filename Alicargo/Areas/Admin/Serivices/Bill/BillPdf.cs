using System;
using System.Globalization;
using System.IO;
using Alicargo.Areas.Admin.Serivices.Abstract;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Alicargo.Areas.Admin.Serivices.Bill
{
	internal sealed class BillPdf : IBillPdf
	{
		private static readonly Font SmallFont;
		private static readonly Font DefaultFont;
		private static readonly Font TitleFont;
		private static readonly Font BoldFont;
		private readonly IBillRepository _bills;

		static BillPdf()
		{
			FontFactory.RegisterDirectories();

			SmallFont = FontFactory.GetFont("Arial", BaseFont.IDENTITY_H, 8);
			DefaultFont = FontFactory.GetFont("Arial", BaseFont.IDENTITY_H, 9.5f);
			TitleFont = FontFactory.GetFont("Arial Bold", BaseFont.IDENTITY_H, 13.5f);
			BoldFont = FontFactory.GetFont("Arial Bold", BaseFont.IDENTITY_H, 9.5f);
		}

		public BillPdf(IBillRepository bills)
		{
			_bills = bills;
		}

		public FileHolder Get(long applicationId)
		{
			var bill = _bills.Get(applicationId);

			using(var stream = new MemoryStream())
			using(var document = new Document(PageSize.A4, 30, 30, 30, 30))
			{
				PdfWriter.GetInstance(document, stream);
				document.Open();
				Fill(document, bill);
				document.Close();

				return new FileHolder
				{
					Name = "bill-" + bill.Number + ".pdf",
					Data = stream.ToArray()
				};
			}
		}

		private static void AddBankDetails(Document document, BillData bill)
		{
			var table = new PdfPTable(4) { WidthPercentage = 100 };
			table.SetWidths(new[]
			{
				220, 220, 60, 220
			});

			var bankPhrase = (bill.Bank + Environment.NewLine + Environment.NewLine).Phrase(DefaultFont);
			bankPhrase.Add("Банк получателя".Phrase(SmallFont));
			table.AddCell(new PdfPCell(bankPhrase)
			{
				Colspan = 2,
				Rowspan = 2
			});
			table.AddCell("БИК".Phrase(DefaultFont));
			table.AddCell(bill.BIC.Phrase(DefaultFont));
			table.AddCell("Сч. №".Phrase(DefaultFont));
			table.AddCell(bill.CorrespondentAccount.Phrase(DefaultFont));

			table.AddCell(("ИНН " + bill.TIN).Phrase(DefaultFont));
			table.AddCell(("КПП " + bill.TaxRegistrationReasonCode).Phrase(DefaultFont));
			table.AddCell(new PdfPCell("Сч. №".Phrase(DefaultFont))
			{
				Rowspan = 2
			});
			table.AddCell(new PdfPCell(bill.CurrentAccount.Phrase(DefaultFont))
			{
				Rowspan = 2
			});

			var payeePhrase = (bill.Payee + Environment.NewLine + Environment.NewLine).Phrase(DefaultFont);
			payeePhrase.Add("Получатель".Phrase(SmallFont));
			table.AddCell(new PdfPCell(payeePhrase)
			{
				Colspan = 2
			});

			document.Add(table);

			document.Add(Environment.NewLine.Phrase(DefaultFont));
		}

		private static void AddGoods(Document document, BillData bill)
		{
			var table = new PdfPTable(6) { WidthPercentage = 100 };
			table.SetWidths(new[]
			{
				30, 370, 60, 60, 100, 100
			});

			table.AddCell("№".Phrase(BoldFont).CenterCell());
			table.AddCell("Товары (работы, услуги)".Phrase(BoldFont).CenterCell());
			table.AddCell("Кол-во".Phrase(BoldFont).CenterCell());
			table.AddCell("Ед.".Phrase(BoldFont).CenterCell());
			table.AddCell("Цена".Phrase(BoldFont).CenterCell());
			table.AddCell("Сумма".Phrase(BoldFont).CenterCell());

			table.AddCell("1".Phrase(DefaultFont).CenterCell());
			table.AddCell(bill.Goods.Phrase(DefaultFont));
			table.AddCell(bill.Count.ToString(CultureInfo.InvariantCulture).Phrase(DefaultFont));
			table.AddCell("шт.".Phrase(DefaultFont).CenterCell());
			var price = (bill.Price * bill.EuroToRuble).ToString("N2");
			table.AddCell(price.Phrase(DefaultFont).RightCell());
			table.AddCell(price.Phrase(DefaultFont).RightCell());

			document.Add(table);
		}

		private static void AddHeadAndAccountant(Document document, BillData bill)
		{
			var table = new PdfPTable(4) { WidthPercentage = 100 };
			table.SetWidths(new[]
			{
				120, 240, 120, 240
			});
			table.DefaultCell.Border = Rectangle.NO_BORDER;

			table.AddCell("Руководитель".Phrase(BoldFont));
			var headCell = bill.Head.Phrase(BoldFont).RightCell();
			headCell.Border = Rectangle.BOTTOM_BORDER;
			table.AddCell(headCell);

			table.AddCell("Бухгалтер".Phrase(BoldFont));
			var accountantCell = bill.Accountant.Phrase(BoldFont).RightCell();
			accountantCell.Border = Rectangle.BOTTOM_BORDER;
			table.AddCell(accountantCell);

			document.Add(table);
		}

		private static void AddHeader(Document document, BillData bill)
		{
			var header = new Paragraph(bill.HeaderText.Phrase(SmallFont))
			{
				SpacingAfter = 30,
				Alignment = Element.ALIGN_CENTER,
				IndentationLeft = 50,
				IndentationRight = 50
			};

			document.Add(header);
		}

		private static void AddMoney(Document document, BillData bill)
		{
			var table = new PdfPTable(2) { WidthPercentage = 100 };
			table.SetWidths(new[]
			{
				600, 120
			});
			table.DefaultCell.Border = Rectangle.NO_BORDER;

			var price = (bill.Price * bill.EuroToRuble);
			var priceVat = (price * bill.VAT).ToString("N2");

			table.AddCell("Итого:".Phrase(BoldFont).RightCell().NoBorder());
			table.AddCell(price.ToString("N2").Phrase(BoldFont).RightCell().NoBorder());
			table.AddCell("В том числе НДС:".Phrase(BoldFont).RightCell().NoBorder());
			table.AddCell(priceVat.Phrase(BoldFont).RightCell().NoBorder());
			table.AddCell("Всего к оплате:".Phrase(BoldFont).RightCell().NoBorder());
			table.AddCell(price.ToString("N2").Phrase(BoldFont).RightCell().NoBorder());

			document.Add(table);

			document.Add(string.Format(
				"Всего наименований {0}, на сумму {1} руб." + Environment.NewLine,
				bill.Count,
				price.ToString("N2"))
				.Phrase(DefaultFont));
			document.Add((NumByWords.RurPhrase(price) + Environment.NewLine).Phrase(BoldFont));
			Line(document);
		}

		private static void AddParticipants(Document document, BillData bill)
		{
			var table = new PdfPTable(2) { WidthPercentage = 100 };
			table.SetWidths(new[]
			{
				100, 620
			});
			table.DefaultCell.Border = Rectangle.NO_BORDER;
			table.SpacingAfter = 6;

			table.AddCell("Поставщик:".Phrase(DefaultFont));
			table.AddCell(bill.Shipper.Phrase(BoldFont));
			table.AddCell("Покупатель:".Phrase(DefaultFont));
			table.AddCell(bill.Client.Phrase(BoldFont));

			document.Add(table);
		}

		private static void AddTitle(Document document, BillData bill)
		{
			document.Add(string.Format("Счет на оплату № {0} от {1}",
				bill.Number,
				bill.SaveDate.ToString("dd MMMM yyyy")).Phrase(TitleFont));
			Line(document);
		}

		private static void Fill(Document document, BillData bill)
		{
			AddHeader(document, bill);

			AddBankDetails(document, bill);

			AddTitle(document, bill);

			AddParticipants(document, bill);

			AddGoods(document, bill);

			AddMoney(document, bill);

			AddHeadAndAccountant(document, bill);
		}

		private static void Line(Document document)
		{
			document.Add(("_______________________________________________________________________" + Environment.NewLine)
				.Phrase(TitleFont));
		}
	}

	internal static class PdfHepler
	{
		public static PdfPCell CenterCell(this Phrase phrase)
		{
			return new PdfPCell(phrase)
			{
				HorizontalAlignment = Element.ALIGN_CENTER
			};
		}

		public static Phrase Phrase(this string text, Font font)
		{
			return new Phrase(text, font)
			{
				Leading = 12
			};
		}

		public static PdfPCell RightCell(this Phrase phrase)
		{
			return new PdfPCell(phrase)
			{
				HorizontalAlignment = Element.ALIGN_RIGHT
			};
		}

		public static PdfPCell NoBorder(this PdfPCell cell)
		{
			cell.Border = Rectangle.NO_BORDER;
			return cell;
		}
	}
}