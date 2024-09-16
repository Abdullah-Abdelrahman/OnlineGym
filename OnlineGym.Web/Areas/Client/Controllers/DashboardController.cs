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
        public async Task<IActionResult> PersonalInformation()
        {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var CID = claim.Value;
            OnlineGym.Entities.Models.Client client =await _context.Client.GetFirstOrDefualtAsync(c=>c.Id==CID);

            ClientViewModel clientViewModel=new ClientViewModel() 
            { 
            Client = client,
            
            };


            return View(clientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PersonalInformation(ClientViewModel viewModel)
        {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var CID = claim.Value;


            OnlineGym.Entities.Models.Client clientInDB = await _context.Client.GetFirstOrDefualtAsync(c => c.Id == CID);


            if (viewModel.Client.Name!=null&& viewModel.Client.Name !="") {

                clientInDB.Name = viewModel.Client.Name;
            }

            if (viewModel.Client.PhoneNumber != null && viewModel.Client.PhoneNumber != "")
            {

                clientInDB.PhoneNumber = viewModel.Client.PhoneNumber;
            }
            clientInDB.ProfilePhoto = await GetUploadedFileName(viewModel.formFile);
            _context.Comlete();
            viewModel.Client.ProfilePhoto=clientInDB.ProfilePhoto;

            TempData["Updated"] = "true";
            return View(viewModel);
        }




        public async Task<IActionResult> Training()
        {
			var clamisIdentity = (ClaimsIdentity)User.Identity;
			var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
			string clientId =claim.Value;



            int orderId=-1;
            var ClientSubscriptions = (await _context.ClientSubscription.GetAllAsync(c => c.ClientId == clientId && c.Status == SD.Working));

            if(ClientSubscriptions.Any())
            {
                orderId= ClientSubscriptions.First().ClientSubscriptionId;
            }


            TrainingPlan Plan = _context.trainingPlan.GetFirstOrDefualt(t => t.ClientSubscriptionId == orderId, IncludeWord: "Days");

  
            return View(Plan);  
        }

        public async Task<IActionResult> AllPlans()
        {
			var clamisIdentity = (ClaimsIdentity)User.Identity;
			var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
			string clientId = claim.Value;

            var list= await _context.trainingPlan.GetAllAsync(p => p.ClientId == clientId);
            List<TrainingPlan> plans = list.ToList();

         
            return View(plans);
        }

        public async Task<IActionResult> DayExercises(int id,bool canUpdate)
        {
            var DayExercise = await _context.DayExercis.GetAllAsync(d => d.dayId == id, IncludeWord: "Exercise");

            List<DayExerciseViewModel> viewModels = new List<DayExerciseViewModel>();

            foreach(var i in DayExercise)
            {
                if (i.Exercise.VideoUrl != null)
                {
                    Video video = await _context.Video.GetFirstOrDefualtAsync(v => v.Id.ToString() == i.Exercise.VideoUrl);
                    viewModels.Add(new DayExerciseViewModel() { DayExercise = i, url = video?.Url, CanUpdate = canUpdate });
                }
       

            }

           

            return View(viewModels);
        }


        public async Task<IActionResult> MarkDayExerciseASDoneOrOpsite(int dayId,int ExerciseId)
        {

            var DayExercise = await _context.DayExercis.GetFirstOrDefualtAsync(d => d.dayId == dayId && d.ExerciseId==ExerciseId);

            if (DayExercise.isDone == true)
            {
                DayExercise.isDone = false;

            }
            else
            {
                DayExercise.isDone = true;

            }
            

            _context.Comlete();

            return RedirectToAction("DayExercises", new { id =dayId, canUpdate =true});
        }

      public async Task<IActionResult> CurrentSubscription()
      {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string clientId = claim.Value;


            
            ClientSubscription? clientSubscription=null;
            var Orders = (await _context.ClientSubscription.GetAllAsync(s => s.ClientId == clientId && s.Status == SD.Working, IncludeWord: "Subscription,ClientSubscriptionDetails"));

            if (Orders.Any())
            {
                clientSubscription = Orders.First();
            }



            return View(clientSubscription);
      }

      public async Task<IActionResult> AllSubscription()
      {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string clientId = claim.Value;



            List<ClientSubscription> clientSubscriptions = (await _context.ClientSubscription.GetAllAsync(s => s.ClientId == clientId,IncludeWord: "Subscription")).ToList();
            return View(clientSubscriptions);

      }



      public async Task<IActionResult> CurrentMentors()
      {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string clientId = claim.Value;


            List<int> ClientSubscriptionIds = (await _context.ClientSubscription.GetAllAsync(s => s.ClientId == clientId&&s.Status==SD.Working)).Select(e => e.ClientSubscriptionId).ToList();

            List<Employee> metors = new List<Employee>();
            foreach (var i in ClientSubscriptionIds)
            {

               
                List<Employee> emps =(await _context.ClientSubscriptionDetailsEmployee.GetAllAsync(c => c.ClientSubscriptionId == i, IncludeWord: "Employee")).Select(o => o.Employee).ToList();

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
                Mentors.Add(await _context.Client.GetFirstOrDefualtAsync(c => c.Id == item.UserId));
            }

            return View(Mentors);
      }

        public async Task<IActionResult> AllMentors()
        {
            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string clientId = claim.Value;


            List<int> ClientSubscriptionIds =(await _context.ClientSubscription.GetAllAsync(s => s.ClientId == clientId)).Select(e => e.ClientSubscriptionId).ToList();

            List<Employee> metors = new List<Employee>();
            foreach (var i in ClientSubscriptionIds)
            {
                List<Employee> emps = (await _context.ClientSubscriptionDetailsEmployee.GetAllAsync(c => c.ClientSubscriptionId == i,IncludeWord: "Employee")).Select(o => o.Employee).ToList();

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
                Mentors.Add(await _context.Client.GetFirstOrDefualtAsync(c => c.Id == item.UserId));
            }


            return View(Mentors);
        }


        private async Task<string> GetUploadedFileName(IFormFile? imgfile)
        {
            string uniqueFileName = null;

            if (imgfile != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Client/ProfilePhotos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + imgfile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                   await imgfile.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}
