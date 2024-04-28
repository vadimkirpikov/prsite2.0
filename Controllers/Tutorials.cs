using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test1.Models;

namespace test1.Cntrollers;

public class Tutorials:Controller
{
    public ApplicationContext db;
    public Tutorials(ApplicationContext context) => db = context;
    [Route("{lang}/{section}/{shortname}")]
    public IActionResult About(string lang,string section, string shortname)
    {
        var tutorial = db.Tutorials.First(
            t => t.LangName.CompareTo(lang) == 0 
                 & t.ShortName.CompareTo(shortname) == 0 
                 & t.Section.CompareTo(section)==0);
        ViewBag.Text = tutorial.TextOfTutorial;
        return View();
    }

    [Route("{lang}")]
    public IActionResult Sections(string lang)
    {
        var sections = db.Tutorials
            .Where(t => t.LangName.CompareTo(lang) == 0)
            .Select(t => t.Section);
        return View();
    }

    [Route("")]
    public IActionResult Main()
    {
        var languages = db.Tutorials
            .Select(t => t.LangName)
            .Distinct()
            .ToList();
        return View(languages);
    }
}