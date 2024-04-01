namespace EmployeeAppraisalSystem.ViewModels
{
    public class ManagerAppraisalFormViewModel
    {
            public int EmpId { get; set; }
            public int MgrId { get; set; }

            public List<CompetencyManagerRatingFeedback> CompetenciesManagerAppraised { get; set; }
            public List<ObjectiveManagerRatingFeedback> ObjectivesManagerAppraised { get; set; }
    }

    public class CompetencyManagerRatingFeedback
    {
        public string Competency { get; set; }
        public int EmployeeRating { get; set; }
        public string EmployeeFeedback { get; set; }
        public int ManagerRating { get; set; }
        public string ManagerFeedback { get; set; }
    }

    public class ObjectiveManagerRatingFeedback
    {
        public string Objective { get; set; }
        public int EmployeeRating { get; set; }
        public string EmployeeFeedback { get; set; }
        public int ManagerRating { get; set; }
        public string ManagerFeedback { get; set; }
    }

}
