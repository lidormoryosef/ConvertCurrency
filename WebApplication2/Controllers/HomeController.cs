using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using WebApplication2.Models;
using WebApplication2.Service;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private HomeService _homeService;
    private readonly double _epsilon = 1e-10;

    public HomeController(ILogger<HomeController> logger)
    {   
        _homeService = new HomeService();
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> Convert([FromBody]ConvertForm convertForm)
    {
        if (Math.Abs(convertForm.AmountInEUR - 555) < _epsilon)
            throw new Exception("You cannot choose 555");
        ConvertResponse? convertResponse = await _homeService.GetValues(convertForm.Currencies);
        if (convertResponse == null)
            return StatusCode(501);
        return Json(convertResponse);
    }

}