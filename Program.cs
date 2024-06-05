using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test1.TestModels;
using test1.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
string? connectionString1 = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProgrammSiteContext>(options=>options.UseMySQL(connectionString1));
var app = builder.Build();


app.UseExceptionHandler("/Error/404/404/404/404");
app.UseHsts();

//app.UseIgnoreFavicon();
app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();
app.MapControllerRoute(
    name: "default",
    pattern: ""
);
app.Use(async (context, next) =>
    {
        await next();
        Console.WriteLine(context.Response.StatusCode+" "+context.Request.Path);
        if (context.Response.StatusCode == 404)
        {
            Console.WriteLine("ZHOPA");
            context.Response.Redirect("/Error/404/404/404/404");
        }
    }
);
app.Run();

/*public static class IgnoreFaviconMiddlewareExtensions
{
    public static IApplicationBuilder UseIgnoreFavicon(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<IgnoreFaviconMiddleware>();
    }
}*/