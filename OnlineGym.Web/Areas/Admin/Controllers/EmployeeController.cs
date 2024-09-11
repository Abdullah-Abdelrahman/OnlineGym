using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGym.DataAccess.Data;
using OnlineGym.DataAccess.RepositoryImplementation;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Entities.ViewModels;
using OnlineGym.Utilities;


namespace OnlineGym.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _context;
        private UserManager<IdentityUser> _userManager;
        public EmployeeController(IUnitOfWork context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult GetData()
        {
            var employees = _context.Employee.GetAll(IncludeWord: "JobTitle");
           
           //Console.WriteLine(employees.First().JobTitle.JopName);
            return Json(new { data = employees });
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {

                Employee = new Employee(),
                Salary = new Salary(),
                jobTitles = _context.Jobs.GetAll().Select(j => new SelectListItem { Text = j.JopName, Value = j.JobTitleId.ToString() })
            };
            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
		
          
            if (employeeViewModel.Employee.Email!=null)
            {
                employeeViewModel.Employee.UserId = "temp";
                _context.Employee.Add(employeeViewModel.Employee);
               

                _context.Comlete();

                employeeViewModel.Salary.EmployeeId = _context.Employee.last().EmployeeId;
				_context.Salary.Add(employeeViewModel.Salary);
				_context.Comlete();



                string job = _context.Jobs.GetFirstOrDefualt(j => j.JobTitleId == employeeViewModel.Employee.JobTitleId).JopName;



                OnlineGym.Entities.Models.Client client = new OnlineGym.Entities.Models.Client()
                {
                    Email = employeeViewModel.Employee.Email,
                    Name = employeeViewModel.Employee.Name,
                    UserName=employeeViewModel.Employee.Email,
                    PhoneNumber=employeeViewModel.Employee.Phone
                };


                IdentityResult result = await _userManager.CreateAsync(client, SD.DefoultPassword);

                if (result.Succeeded)
                {


                    Console.WriteLine("User Created Succesfuley");
                    if (job == SD.CoachRole)
                    {
                        await _userManager.AddToRoleAsync(client, SD.CoachRole);

                    }
            
                    _context.Comlete();
                    _context.Employee.last().UserId = _context.Client.GetFirstOrDefualt(c=>c.Email== employeeViewModel.Employee.Email).Id;
                    _context.Comlete();
                }
                else
                {
                    Console.WriteLine("there is a problem in Creating the account for Employee");

                }


                TempData["Created"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
				
			
              
                return View(employeeViewModel);
            }


        }




        [HttpGet]
        public IActionResult Update(int id)
        {
			EmployeeViewModel employeeViewModel = new EmployeeViewModel()
			{

				Employee = _context.Employee.GetFirstOrDefualt(e=>e.EmployeeId==id),
				Salary = _context.Salary.GetFirstOrDefualt(s=>s.EmployeeId==id),
				jobTitles = _context.Jobs.GetAll().Select(j => new SelectListItem { Text = j.JopName, Value = j.JobTitleId.ToString() })
			};
			return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(EmployeeViewModel employeeViewModel)
        {

            if (ModelState.IsValid)
            {
                _context.Employee.Update(employeeViewModel.Employee);
                _context.Salary.Update(employeeViewModel.Salary);
                _context.Comlete();
                TempData["Updated"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                return View(employeeViewModel);
            }


        }


       

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Employee employee=_context.Employee.GetFirstOrDefualt(e=>e.EmployeeId == id);

            Salary salary=_context.Salary.GetFirstOrDefualt(s=>s.EmployeeId == id);


            if (employee != null)
            {
                _context.Employee.Delete(employee);
            }

            if(salary != null)
            {
                _context.Salary.Delete(salary);
            }

           
            _context.Comlete();
          
            return Json(new { success=true,message="file has been deleted" });

        }




        [HttpGet]
        public IActionResult Details(int id)
        {
           

			EmployeeDetailsViewModel EmpD = new EmployeeDetailsViewModel();
            EmpD.EmployeeRate= _context.EmployeeRate.GetFirstOrDefualt(e => e.EmployeeId == id);

            EmpD.Employee = _context.Employee.GetFirstOrDefualt(e => e.EmployeeId == id);
            EmpD.Salary = _context.Salary.GetFirstOrDefualt(s => s.EmployeeId == id);
			EmpD.Employee.JobTitle=_context.Jobs.GetFirstOrDefualt(j=>j.JobTitleId==EmpD.Employee.JobTitleId);
            
			return View(EmpD);
        }


        [HttpGet]
        public IActionResult Bouns(int id)
        {
            Salary salary=_context.Salary.GetFirstOrDefualt(s=>s.EmployeeId==id);
            return View(salary);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Bouns(int id,int Amount)
		{
			return View();
		}


       
        


    }
}
