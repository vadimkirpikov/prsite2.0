using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using test1.Models;

namespace test1.Controllers;

public class Tutorials : Controller
{
    private ProgrammSiteContext db;
    private readonly IMemoryCache _cache;
    public Tutorials(ProgrammSiteContext context, IMemoryCache cache)
    {
        db = context;
        _cache = cache;
    }
    [Route("{lang}/{section}/{shortname}")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client, VaryByQueryKeys = new[] { "lang", "section", "shortname" })]
    public IActionResult About(string lang,string section, string shortname)
    {

        var url = $"/{lang}/{section}/{shortname}";
        Console.WriteLine("Uncached in browser " + url);
        var tutorials = GetTutorialsFromCache($"/{lang}/{section}");
        var sectionId = db.Sections.First(s => s.Url.Equals($"/{lang}/{section}")).Id;
        if (!_cache.TryGetValue(sectionId, out Dictionary<int, string> chaptersMap))
        {
            var chapters = db.Chapters.Where(ch => ch.SectionId.Equals(sectionId));
            chaptersMap = new();
            foreach (var chapter in chapters)
                chaptersMap[chapter.Id] = chapter.Title;
            _cache.Set(sectionId, chaptersMap, TimeSpan.FromMinutes(30));
        }
        var tutorial = tutorials.First(t => t.Url.Equals(url));
        var indexOfTutorial = tutorials.IndexOf(tutorial);
        var nextTutorialUrl = indexOfTutorial < tutorials.Count - 1 ? tutorials[indexOfTutorial + 1].Url : "invalid";
        var previousTutorialUrl = indexOfTutorial > 0 ? tutorials[indexOfTutorial - 1].Url : "invalid";

        ViewBag.Chapters = chaptersMap;
        ViewBag.Sections = db.Sections.ToList();
        ViewBag.Title = tutorial.Title;
        ViewBag.Tutorial = tutorial;
        ViewBag.Tutorials = tutorials;
        ViewBag.BackLink = previousTutorialUrl;
        ViewBag.ForwardLink = nextTutorialUrl;
        ViewBag.Description = tutorial.Description;
        ViewBag.KeyWords = tutorial.Keywords;
        ViewBag.IsTutorial = true;
        return View();
    }

    [Route("{lang}")]
    public IActionResult Main(string lang)
    {
        var Lang = db.Langs.First(l => l.Name.Equals(lang));
        
        ViewBag.Tutorials = db.Sections.ToList();
        ViewBag.Sections = db.Sections.ToList();
        ViewBag.Title = Lang.Title;
        ViewBag.IsTutorial = false;
        if (!_cache.TryGetValue(Lang.Id, out IQueryable<Section>? sections))
        {
            sections = db.Sections.Where(s=>s.LangId==Lang.Id);
            _cache.Set(Lang.Id, sections, TimeSpan.FromMinutes(30));
        }
        
        return View(sections);
    }

    [Route("")]
    public IActionResult Main()
    {
        ViewBag.Sections = db.Sections.ToList();
        ViewBag.Tutorials = db.Langs.ToList();
        ViewBag.Title = "PROGA.RU";
        ViewBag.IsTutorial = false;
        
        IEnumerable<IDescription> langs = db.Langs.ToList();
        return View(langs);
    }

    [Route("{lang}/{section}")]
    public IActionResult Main(string lang, string section)
    {

        var tutorials = GetTutorialsFromCache($"/{lang}/{section}");
        var firstTutorial = tutorials[0];
        
        return Redirect(firstTutorial.Url);
    }
    
    [NonAction]
    private List<Tutorial> GetTutorialsFromCache(string url)
    {
        if (!_cache.TryGetValue(url, out List<Tutorial>? tutorials))
        {
            Console.WriteLine("Uncached url "+url);
            tutorials = db.Tutorials.Where(t => t.Url.Contains(url)).ToList();
            _cache.Set(url, tutorials, TimeSpan.FromMinutes(30));
        }
        return tutorials;
    }
}