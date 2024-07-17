using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Entities.ViewModels;
using Stripe.Checkout;
using System.Security.Claims;

namespace OnlineGym.Web.Areas.Client.Controllers
{
	[Area("Client")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _context;

		public CartController(IUnitOfWork context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Authorize]
		public IActionResult Checkout(int subscriptionId)
		{

			var clamisIdentity = (ClaimsIdentity)User.Identity;
			var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
			// claim.value;
			ClientSubscriptionDetails clientSubscriptionDetails = new ClientSubscriptionDetails
			{
				clientSubscription = new ClientSubscription()
				{
					Subscription= _context.Subscription.GetFirstOrDefualt(s => s.SubscriptionId == subscriptionId, IncludeWord: "Benefits")
				}
			};



			clientSubscriptionDetails.clientSubscription.SubscriptionId = subscriptionId;

			clientSubscriptionDetails.clientSubscription.Client = _context.Client.GetFirstOrDefualt(c => c.Id == claim.Value);


			return View(clientSubscriptionDetails);
		}


		[HttpPost]
		public IActionResult Checkout(ClientSubscriptionDetails clientSubscriptionDetails)
		{
			
			if (ModelState.IsValid)
			{
				clientSubscriptionDetails.clientSubscription.Subscription = _context.Subscription.GetFirstOrDefualt(s => s.SubscriptionId == clientSubscriptionDetails.clientSubscription.SubscriptionId, IncludeWord: "Benefits");




				ClientSubscription clientSubscription = new ClientSubscription();

				//1
				clientSubscription.SubscriptionId = clientSubscriptionDetails.clientSubscription.SubscriptionId;
				//2
				var clamisIdentity = (ClaimsIdentity)User.Identity;
				var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
				clientSubscription.ClientId = claim.Value;
				//3
				clientSubscription.price =
					clientSubscriptionDetails.clientSubscription.Subscription.Price;

				//4
				clientSubscription.PaymentStatus = "Pendenig";

				clientSubscription.Status = "Pendenig";

				_context.ClientSubscription.Add(clientSubscription);
				_context.Comlete();






				ClientSubscriptionDetails clientSubscriptionDetails1 = new ClientSubscriptionDetails();


				//1
				clientSubscriptionDetails1.ClientEmail = clientSubscriptionDetails.ClientEmail;

				//2

				clientSubscriptionDetails1.ClientPhone = clientSubscriptionDetails.ClientPhone;
				_context.Comlete();
				//3
				

				ClientSubscription Cs = _context.ClientSubscription.last();



				clientSubscriptionDetails1.ClientSubscriptionId = Cs.ClientSubscriptionId;

				_context.ClientSubscriptionDetails.Add(clientSubscriptionDetails1);

				_context.Comlete();
			}
			else
			{
				var clamisIdentity = (ClaimsIdentity)User.Identity;
				var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
				// claim.value;


				clientSubscriptionDetails.clientSubscription.Subscription = _context.Subscription.GetFirstOrDefualt(s => s.SubscriptionId == clientSubscriptionDetails.clientSubscription.SubscriptionId, IncludeWord: "Benefits");


				clientSubscriptionDetails.clientSubscription.Client = _context.Client.GetFirstOrDefualt(c => c.Id == claim.Value);
				return View(clientSubscriptionDetails);
			}



			ClientSubscription clientSubscription1 = _context.ClientSubscription.last();
			var domain = "https://localhost:7078/";

			var options = new SessionCreateOptions
			{
				LineItems = new List<SessionLineItemOptions>(),

				Mode = "payment",
				SuccessUrl = domain + $"Client/Cart/OrderConfirmation?id={clientSubscription1.ClientSubscriptionId}",
				CancelUrl = domain + $"Client/Cart/OrderCancel?id={clientSubscription1.ClientSubscriptionId}",
			};


			var sessionLineItemOptions = new SessionLineItemOptions
			{
				PriceData = new SessionLineItemPriceDataOptions
				{
					UnitAmount = (long)(clientSubscriptionDetails.clientSubscription.Subscription.Price*100),
					Currency = "usd",
					ProductData = new SessionLineItemPriceDataProductDataOptions
					{
						Name = clientSubscriptionDetails.clientSubscription.Subscription.SubscriptionName,
					},
				},
				Quantity = 1,
			};

			options.LineItems.Add(sessionLineItemOptions);

			var service = new Stripe.Checkout.SessionService();
			Session session = service.Create(options);


			

			clientSubscription1.SessionId = session.Id;
		


			_context.ClientSubscription.Uppdate(clientSubscription1);
			_context.Comlete();

			Response.Headers.Add("Location", session.Url);
			return new StatusCodeResult(303);


		}





		public IActionResult OrderConfirmation(int id)
		{

			ClientSubscription clientSubscription = _context.ClientSubscription.GetFirstOrDefualt(c=>c.ClientSubscriptionId==id);

			
			ClientSubscriptionDetails csd=_context.ClientSubscriptionDetails.GetFirstOrDefualt(c=>c.ClientSubscriptionId == id);

			Console.WriteLine("bfiuegfuehfbeuihf");
			var service = new SessionService();
			Session session = service.Get(clientSubscription.SessionId);

			if (session.PaymentStatus.ToLower() == "paid")
			{
				clientSubscription.PaymentStatus = "paid";
				csd.paymentDate = DateTime.Now;
				clientSubscription.PaymentIntentId = session.PaymentIntentId;
				clientSubscription.Status = "Aproved";
				clientSubscription.orderDate = DateAndTime.Now;
				_context.ClientSubscription.Uppdate(clientSubscription);
				_context.ClientSubscriptionDetails.Update(csd);
				_context.Comlete();
			}
			return View(id);
		}




		public IActionResult  OrderCancel(int id)
		{
			_context.ClientSubscription.DeleteById(id);

			return RedirectToAction("Index", "Home");
		}
	}
}
