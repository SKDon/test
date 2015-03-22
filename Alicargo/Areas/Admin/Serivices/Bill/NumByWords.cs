namespace Alicargo.Areas.Admin.Serivices.Bill
{
	public sealed class NumByWords
	{
		public static string NumPhrase(ulong value, bool isMale)
		{
			if(value == 0UL) return "Ноль";

			string[] dek1 =
			{
				"", " од", " дв", " три", " четыре", " пять", " шесть", " семь", " восемь", " девять", " десять",
				" одиннадцать", " двенадцать", " тринадцать", " четырнадцать", " пятнадцать", " шестнадцать", " семнадцать",
				" восемнадцать", " девятнадцать"
			};
			string[] dek2 =
			{
				"", "", " двадцать", " тридцать", " сорок", " пятьдесят", " шестьдесят", " семьдесят",
				" восемьдесят", " девяносто"
			};
			string[] dek3 =
			{
				"", " сто", " двести", " триста", " четыреста", " пятьсот", " шестьсот", " семьсот", " восемьсот",
				" девятьсот"
			};
			string[] th = { "", "", " тысяч", " миллион", " миллиард", " триллион", " квадрилион", " квинтилион" };
			string str = "";

			for(byte i = 1; value > 0; i++)
			{
				var gr = (ushort)(value % 1000);
				value = (value - gr) / 1000;
				if(gr > 0)
				{
					var d3 = (byte)((gr - gr % 100) / 100);
					var d1 = (byte)(gr % 10);
					var d2 = (byte)((gr - d3 * 100 - d1) / 10);
					if(d2 == 1) d1 += 10;
					bool ismale = (i > 2) || ((i == 1) && isMale);
					str = dek3[d3] + dek2[d2] + dek1[d1] + EndDek1(d1, ismale) + th[i] + EndTh(i, d1) + str;
				}
			}
			str = str.Substring(1, 1).ToUpper() + str.Substring(2);
			return str;
		}

		public static string RurPhrase(decimal money)
		{
			return CurPhrase(money, "рубль", "рубля", "рублей", "копейка", "копейки", "копеек");
		}

		public static string UsdPhrase(decimal money)
		{
			return CurPhrase(money, "доллар США", "доллара США", "долларов США", "цент", "цента", "центов");
		}

		#region Private members

		private static string CurPhrase(
			decimal money, string word1, string word234, string wordmore, string sword1, string sword234, string swordmore)
		{
			money = decimal.Round(money, 2);
			decimal decintpart = decimal.Truncate(money);
			ulong intpart = decimal.ToUInt64(decintpart);
			string str = NumPhrase(intpart, true) + " ";
			var endpart = (byte)(intpart % 100UL);
			if(endpart > 19) endpart = (byte)(endpart % 10);
			switch(endpart)
			{
				case 1:
					str += word1;
					break;
				case 2:
				case 3:
				case 4:
					str += word234;
					break;
				default:
					str += wordmore;
					break;
			}
			byte fracpart = decimal.ToByte((money - decintpart) * 100M);
			str += " " + ((fracpart < 10) ? "0" : "") + fracpart + " ";
			if(fracpart > 19) fracpart = (byte)(fracpart % 10);
			switch(fracpart)
			{
				case 1:
					str += sword1;
					break;
				case 2:
				case 3:
				case 4:
					str += sword234;
					break;
				default:
					str += swordmore;
					break;
			}

			return str;
		}

		private static string EndDek1(byte dek, bool isMale)
		{
			if((dek > 2) || (dek == 0)) return "";
			if(dek == 1)
			{
				if(isMale) return "ин";
				return "на";
			}
			if(isMale) return "а";
			return "е";
		}

		private static string EndTh(byte thNum, byte dek)
		{
			bool in234 = ((dek >= 2) && (dek <= 4));
			bool more4 = ((dek > 4) || (dek == 0));
			if(((thNum > 2) && in234) || ((thNum == 2) && (dek == 1))) return "а";
			if((thNum > 2) && more4) return "ов";
			if((thNum == 2) && in234) return "и";
			return "";
		}

		#endregion
	}
}