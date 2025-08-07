using System.Globalization;

namespace MyRecipeBook.API.Middleware;

public class Culture
{
    private readonly RequestDelegate _next;
    public Culture(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var requestedCulture = context.Request.Headers.AcceptLanguage.ToString();
        
        if (!string.IsNullOrEmpty(requestedCulture))
        {
            var idx = requestedCulture.IndexOf(',');
            if (idx > 0)
                requestedCulture = requestedCulture.Substring(0, idx);
        }
        
        var cultureInfo = new CultureInfo(requestedCulture ?? "pt-BR");
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}