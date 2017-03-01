using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using WebUI.wwwroot.Helpers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace QuickstartIdentityServer.Controllers
{
    [SecurityHeaders]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;

        public HomeController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
