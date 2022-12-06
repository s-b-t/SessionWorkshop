using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        HttpContext.Session.SetInt32("MyNum", 22);
        return View();
    }

    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        if(HttpContext.Session.GetString("UserName") == null)
        {
            return RedirectToAction("Index");
        }
        int? Num = HttpContext.Session.GetInt32("MyNum");
        return View();
    }

    [HttpPost("Login")]
    public IActionResult Login(string NewName)
    {
        if(NewName == null)
        {
            return RedirectToAction("Index");
        }
        HttpContext.Session.SetString("UserName", NewName);

        return RedirectToAction("Dashboard");
    }

    [HttpPost("AddOne")]
    public IActionResult AddOne()
    {
        int? Num = HttpContext.Session.GetInt32("MyNum");
        Num += 1;
        HttpContext.Session.SetInt32("MyNum", (int)Num);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("MinusOne")]
    public IActionResult MinusOne()
    {
        int? Num = HttpContext.Session.GetInt32("MyNum");
        Num -= 1;
        HttpContext.Session.SetInt32("MyNum", (int)Num);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("TimesTwo")]
    public IActionResult TimesTwo()
    {
        int? Num = HttpContext.Session.GetInt32("MyNum");
        Num *= 2;
        HttpContext.Session.SetInt32("MyNum", (int)Num);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("AddRandom")]
    public IActionResult Random()
    {
        Random rand = new Random();
        int Random = rand.Next(1, 11);
        int? Num = HttpContext.Session.GetInt32("MyNum");
        Num += Random;
        HttpContext.Session.SetInt32("MyNum", (int)Num);
        return RedirectToAction("Dashboard");
    }

    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }


    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
