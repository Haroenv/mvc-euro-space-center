using System.Collections.Generic;
using System.Web.Mvc;

public static class HtmlButtonExtension {

    public static MvcHtmlString Button(this HtmlHelper helper,
                                       string innerHtml,
                                       object htmlAttributes) {
        return Button(helper, innerHtml,
                      HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)
        );
    }

    public static MvcHtmlString Button(this HtmlHelper helper,
                                       string innerHtml,
                                       IDictionary<string, object> htmlAttributes) {
        var builder = new TagBuilder("button");
        builder.InnerHtml = innerHtml;
        builder.MergeAttributes(htmlAttributes);
        return MvcHtmlString.Create(builder.ToString());
    }
}