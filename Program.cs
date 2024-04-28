using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
var connectionString1 = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options=>options.UseMySQL(connectionString1));
var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tutorials}/{action=About}"
);
app.Run();
