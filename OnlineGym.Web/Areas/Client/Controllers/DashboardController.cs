using Microsoft.AspNetCore.Mvc;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Utilities;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using OnlineGym.Entities.ViewModels;
namespace OnlineGym.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IWebHostEnvironment _webHost;
        public DashboardController(IUnitOfWork context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        [HttpGet]
        public IActionResult PersonalInformation()
        {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var CID = claim.Value;
            OnlineGym.Entities.Models.Client client = _context.Client.GetFirstOrDefualt(c=>c.Id==CID);

            ClientViewModel clientViewModel=new ClientViewModel() 
            { 
            Client = client,
            
            };


            return View(clientViewModel);
        }

        [HttpPost]
        public IActionResult PersonalInformation(ClientViewModel viewModel)
        {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var CID = claim.Value;


            OnlineGym.Entities.Models.Client clientInDB = _context.Client.GetFirstOrDefualt(c => c.Id == CID);


            if (viewModel.Client.Name!=null&& viewModel.Client.Name !="") {

                clientInDB.Name = viewModel.Client.Name;
            }

            if (viewModel.Client.PhoneNumber != null && viewModel.Client.PhoneNumber != "")
            {

                clientInDB.PhoneNumber = viewModel.Client.PhoneNumber;
            }
            clientInDB.ProfilePhoto = GetUploadedFileName(viewModel.formFile);
            _context.Comlete();
            viewModel.Client.ProfilePhoto=clientInDB.ProfilePhoto;
            return View(viewModel);
        }




        public IActionResult Training()
        {
			var clamisIdentity = (ClaimsIdentity)User.Identity;
			var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
			string clientId =claim.Value;
            TrainingPlan Plan = _context.trainingPlan.last(t => t.ClientId == clientId,IncludeWord: "Days");

  
            return View(Plan);  
        }

        public IActionResult AllPlans()
        {
			var clamisIdentity = (ClaimsIdentity)User.Identity;
			var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
			string clientId = claim.Value;

			List<TrainingPlan> plans = _context.trainingPlan.GetAll(p => p.ClientId == clientId).ToList();

         
            return View(plans);
        }

        public IActionResult DayExercises(int id)
        {
            var DayExercise = _context.DayExercis.GetAll(d => d.dayId == id, IncludeWord: "Exercise");


            return View(DayExercise);
        }


      public IActionResult CurrentSubscription()
      {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string clientId = claim.Value;
            ClientSubscription clientSubscription = _context.ClientSubscription.GetFirstOrDefualt(s => s.ClientId == clientId && s.Status==SD.Working, IncludeWord: "Subscription");


            return View(clientSubscription);
      }

      public IActionResult AllSubscription()
      {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string clientId = claim.Value;

            List<ClientSubscription> clientSubscriptions = _context.ClientSubscription.GetAll(s => s.ClientId == clientId,IncludeWord: "Subscription").ToList();
            return View(clientSubscriptions);

      }



      public IActionResult CurrentMentors()
      {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string clientId = claim.Value;


            List<int> ClientSubscriptionIds = _context.ClientSubscription.GetAll(s => s.ClientId == clientId&&s.Status==SD.Working).Select(e => e.ClientSubscriptionId).ToList();

            List<Employee> metors = new List<Employee>();
            foreach (var i in ClientSubscriptionIds)
            {

               
                List<Employee> emps = _context.ClientSubscriptionDetailsEmployee.GetAll(c => c.ClientSubscriptionId == i, IncludeWord: "Employee").Select(o => o.Employee).ToList();

                foreach (var emp in emps)
                {

                    if (!metors.Any(e => e.EmployeeId == emp.EmployeeId))
                    {
                        metors.Add(emp);
                    }

                }

            }
            List<OnlineGym.Entities.Models.Client> Mentors = new List<OnlineGym.Entities.Models.Client>();

            foreach(var item in metors)
            {
                Mentors.Add(_context.Client.GetFirstOrDefualt(c => c.Id == item.UserId));
            }

            return View(Mentors);
      }

        public IActionResult AllMentors()
        {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string clientId = claim.Value;


            List<int> ClientSubscriptionIds = _context.ClientSubscription.GetAll(s => s.ClientId == clientId).Select(e => e.ClientSubscriptionId).ToList();

            List<Employee> metors = new List<Employee>();
            foreach (var i in ClientSubscriptionIds)
            {
                List<Employee> emps = _context.ClientSubscriptionDetailsEmployee.GetAll(c => c.ClientSubscriptionId == i,IncludeWord: "Employee").Select(o => o.Employee).ToList();

                foreach (var emp in emps)
                {

                    if (!metors.Any(e => e.EmployeeId == emp.EmployeeId))
                    {
                        metors.Add(emp);
                    }
                   
                }

            }
            List<OnlineGym.Entities.Models.Client> Mentors = new List<OnlineGym.Entities.Models.Client>();

            foreach (var item in metors)
            {
                Mentors.Add(_context.Client.GetFirstOrDefualt(c => c.Id == item.UserId));
            }


            return View(Mentors);
        }


        private string GetUploadedFileName(IFormFile? imgfile)
        {
            string uniqueFileName = null;

            if (imgfile != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Client/ProfilePhotos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + imgfile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imgfile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}
