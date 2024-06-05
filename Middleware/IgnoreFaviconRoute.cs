namespace test1.Middleware;

/*public class IgnoreFaviconMiddleware
{
    private readonly RequestDelegate _next;

    public IgnoreFaviconMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Проверяем, запрашивается ли favicon.ico
        if (context.Request.Path.Value == "/favicon.ico")
        {
            // Просто возвращаем пустой ответ
            context.Response.StatusCode = 204; // 204 No Content
            return;
        }

        // Передаем управление следующему компоненту middleware
        await _next(context);
    }
}*/