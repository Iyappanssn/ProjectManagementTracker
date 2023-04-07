using System.ComponentModel.DataAnnotations;

namespace ProjectManagementTracker.Models
{
    public class MemberDetails
    {
        [Key]
        public int memberID { get; set; }
        public string memberName { get; set; }
        public int numberOfYearsOfExperience { get; set; }
        public string skillset { get; set; }
        public string currentProfileDescription { get; set; }
        public DateTime projectStartDate { get; set; }
        public DateTime projectEndDate { get; set; }
        public int allocationPercentage { get; set; }
    }
}
