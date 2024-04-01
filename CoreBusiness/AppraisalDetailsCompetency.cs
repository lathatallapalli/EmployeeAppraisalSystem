namespace CoreBusiness
{
    public class AppraisalDetailsCompetency
    {
        public int DetailId { get; set; }
        public int AppraisalId { get; set; }
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public string Competency { get; set; }
        public int EmployeeRating {  get; set; }
        public string? EmployeeFeedback {  get; set; }
        public int ManagerRating { get; set; }
        public string? ManagerFeedback { get; set; }


        //Navigation properties

        public Appraisal Appraisal { get; set; } // Navigation property for Appraisal
        public Employee Employee { get; set; } // Navigation property for Employee
        public Employee Manager { get; set; } // Navigation property for Manager

    }
}
