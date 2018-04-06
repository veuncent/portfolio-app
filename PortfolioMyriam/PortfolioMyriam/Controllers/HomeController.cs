using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PortfolioMyriam.Models;
using PortfolioMyriam.Services;
using System.Diagnostics;

namespace PortfolioMyriam.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStringHelperService _stringHelperService;


        public HomeController(
            IConfiguration configuration,
            IStringHelperService stringHelperService)
        {
            _configuration = configuration;
            _stringHelperService = stringHelperService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            var emailPlainText = _configuration["AppSettings:AdminEmail"];
            ViewData["AdminEmail"] = _stringHelperService.GetBase64EncodedString(emailPlainText);

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
