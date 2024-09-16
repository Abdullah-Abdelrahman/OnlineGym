using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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

		private IMemoryCache _cache;

		private readonly SemaphoreSlim _semaphore =new SemaphoreSlim(1,1);

		public HomeController(IUnitOfWork context, IMemoryCache cache)
		{
			_context = context;
			_cache = cache;
		}
		public async Task<IActionResult> Index()
		{

			if(_cache.TryGetValue("Subscription",out IEnumerable<Subscription> subscriptions))
			{
				Console.WriteLine("from Cache !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			}
			else
			{

				try
				{
					await _semaphore.WaitAsync();
                    if (_cache.TryGetValue("Subscription", out  subscriptions))
                    {
                        Console.WriteLine("from Cache !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
					else
					{
                        subscriptions = await _context.Subscription.GetAllAsync(s => s.IsActive == true, IncludeWord: "Benefits");


                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                            .SetPriority(CacheItemPriority.High);

                        _cache.Set("Subscription", subscriptions, cacheEntryOptions);
                        Console.WriteLine("from DB !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                }
				finally
				{
					_semaphore.Release();
				}
				

            }
            return View(subscriptions);
		}


	}
}
