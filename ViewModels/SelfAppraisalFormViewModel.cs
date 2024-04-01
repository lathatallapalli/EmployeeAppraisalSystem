using EmployeeAppraisalSystem.Models;

namespace EmployeeAppraisalSystem.ViewModels
{
    public class SelfAppraisalFormViewModel
    {
        public int EmpId { get; set; }
        public int MgrId { get; set; }

        public List<CompetencyRatingFeedback> CompetenciesSelfAppraised { get; set; }
        public List<ObjectiveRatingFeedback> ObjectivesSelfAppraised { get; set; }
    }

    public class CompetencyRatingFeedback
    {
        public string Competency { get; set; }
        public int Rating { get; set; }
        public string Feedback { get; set; }
    }
    
    public class ObjectiveRatingFeedback
    {
        public string Objective { get; set; }
        public int Rating { get; set; }
        public string Feedback { get; set; }
    }
}
