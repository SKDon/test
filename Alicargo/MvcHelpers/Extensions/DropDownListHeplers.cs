using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Alicargo.MvcHelpers.Extensions
{
	public static class DropDownListHeplers
	{
		public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IDictionary<long, string> data)
		{
			var list = data.Select(x => new SelectListItem
			{
				Text = x.Value,
				Value = x.Key.ToString(CultureInfo.InvariantCulture)
			}).ToList();

			return helper.DropDownListFor(expression, list);
		}
	}
}