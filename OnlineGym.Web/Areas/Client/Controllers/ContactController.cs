using Microsoft.AspNetCore.Mvc;

namespace OnlineGym.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
