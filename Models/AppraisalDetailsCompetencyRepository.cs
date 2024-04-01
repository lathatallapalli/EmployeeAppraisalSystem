



using System.Security.AccessControl;

namespace EmployeeAppraisalSystem.Models
{
    public class AppraisalDetailsCompetencyRepository
    {
        private static List<AppraisalDetailsCompetency> _appraisalDetailsCompetencies = new List<AppraisalDetailsCompetency>();
        public static void Add(int empId, int mgrId, string competency, int rating, string feedback)
        {
            var appraisalDetailsCompetency = new AppraisalDetailsCompetency
            {
                EmployeeId = empId,
                ManagerId = mgrId,
                Competency = competency,
                EmployeeRating = rating,
                EmployeeFeedback = feedback
            };
            if (_appraisalDetailsCompetencies != null && _appraisalDetailsCompetencies.Count > 0)
            {
                var maxId = _appraisalDetailsCompetencies.Max(x => x.DetailId);
            }
            else
            {
                appraisalDetailsCompetency.DetailId = 1;
            }
            _appraisalDetailsCompetencies?.Add(appraisalDetailsCompetency);


        }

        internal static List<AppraisalDetailsCompetency> GetSelfAppraisalDetails(int empId, int mgrId)
        {
            List<AppraisalDetailsCompetency> list = new List<AppraisalDetailsCompetency>();
            foreach (var _competency in _appraisalDetailsCompetencies)
            {
                if (_competency.EmployeeId == empId && _competency.ManagerId == mgrId)
                {
                    // Collect objectives
                    list.Add(_competency);
                }
            }
            return list;
        }

        internal static void UpdateManagerAppraised(int empId, int mgrId, string competency, int employeeRating, string employeeFeedback, int managerRating, string managerFeedback)
        {
            //update based on empid, mgrid and competency
            // Find the objective that matches the given criteria
            foreach (var _competency in _appraisalDetailsCompetencies)
            {
                if (_competency.EmployeeId == empId && _competency.ManagerId == mgrId && _competency.Competency == competency)
                {
                    // Update the rating and feedback
                    _competency.ManagerRating = managerRating;
                    _competency.ManagerFeedback = managerFeedback;

                }
            }
            return;
        }
    }
}
