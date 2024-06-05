using Microsoft.AspNetCore.Mvc;
namespace test1.Controllers;

[Route("Error")]
public class ErrorController : Controller
{
    [Route("404/404/404/404")]
    public IActionResult NotFoundPage()
    {
        Console.WriteLine("Hello error");
        return View();
    }
}