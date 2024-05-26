using Microsoft.AspNetCore.Mvc;

namespace Olya.controller;

[Route("web")]
public class WebController : Controller
{
    
    [HttpGet("page")]
    public IActionResult Index()
    {
        return View("Index");
    }
}