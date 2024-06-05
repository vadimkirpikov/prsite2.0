using Microsoft.AspNetCore.Mvc;
using test1.TestModels;

namespace test1.Controllers;

public class Tutorials : Controller
{
    private ProgrammSiteContext db;
    public Tutorials(ProgrammSiteContext context) => db = context;
    
    [Route("{lang}/{section}/{shortname}")]
    public IActionResult About(string lang,string section, string shortname)
    {
        var url = $"/{lang}/{section}/{shortname}";
        var tutorials = db.Tutorials.Where(t => t.Url.Contains($"/{lang}/{section}")).ToList();
        var tutorial = db.Tutorials.First(t => t.Url.Equals(url));
        var indexOfTutorial = tutorials.IndexOf(tutorial);
        var nextTutorialUrl = indexOfTutorial < tutorials.Count - 1 ? tutorials[indexOfTutorial + 1].Url : "invalid";
        var previousTutorialUrl = indexOfTutorial > 0 ? tutorials[indexOfTutorial - 1].Url : "invalid";
        
        ViewBag.Sections = db.Sections.ToList();
        ViewBag.Title = tutorial.Title;
        ViewBag.Tutorial = tutorial;
        ViewBag.Tutorials = tutorials;
        ViewBag.BackLink = previousTutorialUrl;
        ViewBag.ForwardLink = nextTutorialUrl;
        
        
        return View();
    }

    [Route("{lang}")]
    public IActionResult Main(string lang)
    {
        var Lang = db.Langs.First(l => l.Name.Equals(lang));
        
        ViewBag.Tutorials = db.Sections.ToList();
        ViewBag.Sections = db.Sections.ToList();
        ViewBag.Title = Lang.Title;
        
        var sections = db.Sections.Where(s=>s.LangId==Lang.Id);
        
        return View(sections);
    }

    [Route("")]
    public IActionResult Main()
    {
        ViewBag.Sections = db.Sections.ToList();
        ViewBag.Tutorials = db.Langs.ToList();
        ViewBag.Title = "PROGA.RU";
        IEnumerable<IDescription> langs = db.Langs.ToList();
        return View(langs);
    }

    [Route("{lang}/{section}")]
    public IActionResult Main(string lang, string section)
    {

        var tutorials = db.Tutorials
            .Where(t => t.Url.Contains($"/{lang}/{section}"))
            .ToList();
        var firstTutorial = tutorials[0];
        
        return Redirect(firstTutorial.Url);
    }
}