namespace EmployeeAppraisalSystem.Models
{
    public class AppraisalDetailsCompetency
    {
        public int DetailId { get; set; }
        /*        public int AppraisalId { get; set; }*/
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public string Competency { get; set; }
        public int EmployeeRating {  get; set; }
        public string? EmployeeFeedback {  get; set; }
        public int ManagerRating { get; set; }
        public string? ManagerFeedback { get; set; }

    }
}
