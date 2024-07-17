using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using Stripe.Checkout;
using System.Security.Claims;

namespace OnlineGym.Web.Areas.Client.Controllers
{
	[Area("Client")]
	public class HomeController : Controller
	{

		private readonly IUnitOfWork _context;

		public HomeController(IUnitOfWork context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View(_context.Subscription.GetAll(IncludeWord: "Benefits"));
		}


	}
}
