using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineGym.Entities.Models
{
    public class Leave
    {
        public int LeaveId { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public DateTime? Date { get; set; }

        public string? reason { get; set; }
    }
}
