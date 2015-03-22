using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Alicargo.Utilities.Localization;

namespace Alicargo.MvcHelpers.Extensions
{
	public static class DropDownListHelpers
	{
		public static MvcHtmlString DropDownListFor<TModel, TProperty>(
			this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
			IDictionary<TProperty, string> data, string optionLabel = null,
			object htmlAttributes = null)
		{
			var list = data.Select(x => new SelectListItem
			{
				Text = x.Value,
				Value = x.Key.ToString()
			}).ToList();

			return helper.DropDownListFor(expression, list, optionLabel, htmlAttributes);
		}

		public static MvcHtmlString DropDownList<TModel, TProperty>(
			this HtmlHelper<TModel> helper, string name,
			IDictionary<TProperty, string> data, string optionLabel = null,
			object htmlAttributes = null)
		{
			var list = data.Select(x => new SelectListItem
			{
				Text = x.Value,
				Value = x.Key.ToString()
			}).ToList();

			return helper.DropDownList(name, list, optionLabel, htmlAttributes);
		}

		public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(
			this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
			where TProperty : struct, IComparable, IFormattable, IConvertible
		{
			var enumType = typeof(TProperty);

			if (!enumType.IsEnum)
				throw new NotSupportedException();

			var list = Enum.GetValues(enumType)
						   .Cast<TProperty>()
						   .Select(x => new SelectListItem
						   {
							   Text = x.ToLocalString(),
							   Value = x.ToString(CultureInfo.InvariantCulture)
						   });

			return helper.DropDownListFor(expression, list);
		}
	}
}