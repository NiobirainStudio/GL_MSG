using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GL_APP.Helpers
{
    public static class MyHelpers
    {
        public static IHtmlContent RenderName(this IHtmlHelper html, string name) =>
            html.Raw("<p>Hello <strong>" + name + "</strong>!</p>");
    }
}
