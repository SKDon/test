using System;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Microsoft.Ajax.Utilities;

namespace Alicargo.MvcHelpers.Extensions
{
    public static class PageHelpers
	{
		public static string JavaScriptStringEncode(this string html)
		{
			return HttpUtility.JavaScriptStringEncode(html);
		}

		public static string HtmlEncode(this string html)
		{
			return HttpUtility.HtmlEncode(html);
		}

		public static string EncodeForKendo(this string html)
		{
			return html.HtmlEncode().JavaScriptStringEncode().Replace("#", "\\#");
		}

		public static MvcHtmlString EditPage(this HtmlHelper html, string actionUrl, string buttonTitle, MvcHtmlString additionalHtml = null)
		{
			var builder = new StringBuilder();

			var div = new TagBuilder("div");
			div.AddCssClass("entity-edit");

			var form = new TagBuilder("form");
			form.Attributes.Add("action", actionUrl);
			form.Attributes.Add("enctype", "multipart/form-data");
			form.Attributes.Add("method", FormMethod.Post.ToString());

			var input = new TagBuilder("input");
			input.Attributes.Add("type", "submit");
			input.Attributes.Add("value", buttonTitle);
			input.AddCssClass("btn btn-primary");

			var cancel = new TagBuilder("a");
			cancel.AddCssClass("btn");
			cancel.InnerHtml = Resources.Pages.Cancel;
			cancel.Attributes.Add("href", actionUrl);

			builder.Append(div.ToString(TagRenderMode.StartTag));

			builder.Append(form.ToString(TagRenderMode.StartTag))
			.Append(html.AntiForgeryToken().ToHtmlString())
			.Append(html.EditorForModel())
			.Append(new TagBuilder("hr").ToString(TagRenderMode.SelfClosing))
			.Append(input.ToString(TagRenderMode.SelfClosing))
			.Append("&nbsp;")
			.Append(cancel.ToString(TagRenderMode.Normal));

			if (additionalHtml != null)
			{
				builder.Append("&nbsp;").Append(additionalHtml.ToHtmlString());
			}

			builder.Append(html.ValidationSummary(false).ToHtmlString())
				.Append(form.ToString(TagRenderMode.EndTag))
				.Append(div.ToString(TagRenderMode.EndTag));

			return new MvcHtmlString(builder.ToString());
		}

		public static MvcHtmlString EditorWithLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
		{
			var labelFor = html.LabelFor(expression).ToHtmlString();
			var editorFor = html.EditorFor(expression).ToHtmlString();

			return new MvcHtmlString(labelFor + editorFor);
		}

		public static MvcHtmlString DisplayWithLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
		{
			var labelFor = html.LabelFor(expression).ToHtmlString();
			var editorFor = html.DisplayFor(expression).ToHtmlString();

			return new MvcHtmlString(labelFor + (editorFor.IsNullOrWhiteSpace() ? "&nbsp;" : editorFor));
		}
	}
}