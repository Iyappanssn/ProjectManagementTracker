
namespace ProjectManagementTracker.Models
{
    public class ViewTask
    {

        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string TaskName { get; set; }
        public string Deliverables { get; set; }
        public DateTime? TaskStartDate { get; set; }
        public DateTime? TaskEndDate { get; set; }
        public DateTime projectStartDate { get; set; }
        public DateTime projectEndDate { get; set; }
        public int allocationPercentage { get; set; }
    }
}
