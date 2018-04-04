using Microsoft.AspNetCore.Mvc;
using PortfolioMyriam.Models;
using PortfolioMyriam.Services;
using System.Diagnostics;

namespace PortfolioMyriam.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigurationService _configurationService;
        private readonly IStringHelperService _stringHelperService;


        public HomeController(
            IConfigurationService configurationService,
            IStringHelperService stringHelperService)
        {
            _configurationService = configurationService;
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
            var emailPlainText = _configurationService.GetConfigurationItem("AppSettings:AdminEmail");
            ViewData["AdminEmail"] = _stringHelperService.GetBase64EncodedString(emailPlainText);

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
