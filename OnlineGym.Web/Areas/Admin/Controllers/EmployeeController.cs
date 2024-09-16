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


        public async Task<IActionResult> GetData()
        {
            var employees =await _context.Employee.GetAllAsync(IncludeWord: "JobTitle");
           
           //return the employees as json
            return Json(new { data = employees });
        }

        public IActionResult Index()
        {
         
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                jobTitles =(await _context.Jobs.GetAllAsync()).Select(j => new SelectListItem { Text = j.JopName, Value = j.JobTitleId.ToString() })
            };
            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
		
          
            //you can modify the condetions to allow create
            if (employeeViewModel.Employee.Email!=null)
            {
                //Add an temporray UserId to insert in the database
                //did that to add the Salary record
                employeeViewModel.Employee.UserId = "temp";
                await _context.Employee.AddAsync(employeeViewModel.Employee);

                _context.Comlete();

                //Add the salary record
                //Get the last inserted Employee Which is one needed
                employeeViewModel.Salary.EmployeeId = _context.Employee.last().EmployeeId;
                //make the next (time to take the salary) is equal 
                //to the curent time + the days to work
                employeeViewModel.Salary.nextSalaryDate = DateTime.Now.AddDays(30);
                //the first state which mean that you dont have to pay to that employee
                employeeViewModel.Salary.Status = SD.Pending;
				await _context.Salary.AddAsync(employeeViewModel.Salary);
				_context.Comlete();






                // make a new user for that employee
                OnlineGym.Entities.Models.Client client = new OnlineGym.Entities.Models.Client()
                {
                    Email = employeeViewModel.Employee.Email,
                    Name = employeeViewModel.Employee.Name,
                    UserName=employeeViewModel.Employee.Email,
                    PhoneNumber=employeeViewModel.Employee.Phone
                };

                //try to create the user
                IdentityResult result = await _userManager.CreateAsync(client, SD.DefoultPassword);

                if (result.Succeeded)
                {
                    Console.WriteLine("User Created Succesfuley");

                    // get the job of the employee to use its name to add the role
                    string? job = (await _context.Jobs.GetFirstOrDefualtAsync(j => j.JobTitleId == employeeViewModel.Employee.JobTitleId))?.JopName;


                    //Add the role base on the job name
                    //so you must make the names of the jobs in the system match the
                    //corsponding role
                    
                    await _userManager.AddToRoleAsync(client, job);

                    _context.Comlete();

                    //finally after creating the user add the created id to the employee.UserId
                    _context.Employee.last().UserId =(await _context.Client.GetFirstOrDefualtAsync(c=>c.Email== employeeViewModel.Employee.Email)).Id;
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
        public async Task<IActionResult> Update(int id)
        {
			EmployeeViewModel employeeViewModel = new EmployeeViewModel()
			{

				Employee =await _context.Employee.GetFirstOrDefualtAsync(e=>e.EmployeeId==id),
				Salary =await _context.Salary.GetFirstOrDefualtAsync(s=>s.EmployeeId==id),
				jobTitles =(await _context.Jobs.GetAllAsync()).Select(j => new SelectListItem { Text = j.JopName, Value = j.JobTitleId.ToString() })
			};
			return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployeeViewModel employeeViewModel)
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
        public async Task<IActionResult> Delete(int id)
        {
            //remove the employee
            Employee employee=await _context.Employee.GetFirstOrDefualtAsync(e=>e.EmployeeId == id);

            //must remove its salary
            Salary salary=await _context.Salary.GetFirstOrDefualtAsync(s=>s.EmployeeId == id);


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
        public async Task<IActionResult> Details(int id)
        {
           

			EmployeeDetailsViewModel EmpD = new EmployeeDetailsViewModel();
            EmpD.EmployeeRate=await _context.EmployeeRate.GetFirstOrDefualtAsync(e => e.EmployeeId == id);

            EmpD.Employee = await _context.Employee.GetFirstOrDefualtAsync(e => e.EmployeeId == id);
            EmpD.Salary =await _context.Salary.GetFirstOrDefualtAsync(s => s.EmployeeId == id);
			EmpD.Employee.JobTitle=await _context.Jobs.GetFirstOrDefualtAsync(j=>j.JobTitleId==EmpD.Employee.JobTitleId);
            
			return View(EmpD);
        }


        [HttpGet]
        public async Task<IActionResult> Bouns(int id)
        {
            Salary salary=await _context.Salary.GetFirstOrDefualtAsync(s=>s.EmployeeId==id);
            return View(salary);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Bouns(int id,int Amount)
		{
			return View();
		}


       
        
        public async Task<IActionResult> Salary()
        {
            List<Salary> salaries=(await _context.Salary.GetAllAsync(IncludeWord: "Employee")).ToList();
          
            return View(salaries);
        }

        public async Task<IActionResult> PaySalary(int id)
        {
            Salary salary = (await _context.Salary.GetFirstOrDefualtAsync(s=>s.EmployeeId==id));

            salary.Status = SD.Paid;

            // update the Salary Aquired date
            salary.nextSalaryDate = salary.nextSalaryDate.AddDays(30);
            SalaryHistory salaryHistory = new SalaryHistory(salary);

            _context.SalaryHistory.Add(salaryHistory);
            
            _context.Comlete();
            TempData["Created"] = "true";
            return RedirectToAction("Salary");
        }

    }
}
