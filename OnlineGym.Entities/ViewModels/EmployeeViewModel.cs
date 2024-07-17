using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGym.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OnlineGym.Entities.ViewModels
{
    [NotMapped]
    public class EmployeeViewModel
    {

        public Employee Employee { get; set; }

        public IEnumerable<SelectListItem>? jobTitles { get; set; }

        public Salary Salary { get; set; }
    }
}
