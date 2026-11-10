using Microsoft.AspNetCore.Mvc;

namespace SurveyLive.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
