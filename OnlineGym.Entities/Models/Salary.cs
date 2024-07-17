using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineGym.Entities.Models
{
    public class Salary
    {

        [Key]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }


        
        public int Amount { get; set; }

        public int bonus { get; set; }

        public int MounthSalary { get; set; }

    }
}
