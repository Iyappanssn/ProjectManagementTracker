using System.ComponentModel.DataAnnotations;

namespace ProjectManagementTracker.Models
{
    public class TaskDetails
    {
        [Key]
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string TaskName { get; set; }
        public string Deliverables { get; set; }
        public DateTime? TaskStartDate { get; set; }
        public DateTime? TaskEndDate { get; set; }
    }
}
