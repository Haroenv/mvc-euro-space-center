using System;
using System.Collections.Generic;
using System.Web.Mvc;

public static class HtmlTimeExtension {
    public static MvcHtmlString Time(this HtmlHelper helper, DateTime time, string format, object htmlAttributes) {
        return Time(helper, time, format, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)
        );
    }

    public static MvcHtmlString Time(this HtmlHelper helper, DateTime time, string format, IDictionary<string, object> htmlAttributes) {
        var builder = new TagBuilder("time");
        builder.InnerHtml = String.Format(format, time);
        builder.Attributes["datetime"] = String.Format("{0:u}", time);
        builder.MergeAttributes(htmlAttributes);
        return MvcHtmlString.Create(builder.ToString());
    }
}
