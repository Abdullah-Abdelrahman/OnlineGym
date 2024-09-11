using Microsoft.AspNetCore.Mvc;

namespace OnlineGym.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
