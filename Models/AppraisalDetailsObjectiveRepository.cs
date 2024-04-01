
using System.Security.AccessControl;
using System.Transactions;

namespace EmployeeAppraisalSystem.Models
{
    public class AppraisalDetailsObjectiveRepository
    {
        private static List<AppraisalDetailsObjective> _appraisalDetailsObjectives = new List<AppraisalDetailsObjective>();
        public static void AddObjective(int empId, int mgrId, string @object)
        {
            var appraisalDetailsObjective = new AppraisalDetailsObjective
            {
                EmployeeId = empId,
                ManagerId = mgrId,
                Objective = @object
            };
            if (_appraisalDetailsObjectives!=null && _appraisalDetailsObjectives.Count > 0)
            {
                var maxId = _appraisalDetailsObjectives.Max(x => x.DetailId);
            }
            else
            {
                appraisalDetailsObjective.DetailId = 1;
            }
            _appraisalDetailsObjectives?.Add(appraisalDetailsObjective);           

        }

        public static void UpdateSelfAppraised(int empId, int mgrId, string objective, int rating, string feedback)
        {
            //update based on empid, mgrid and competency
            // Find the objective that matches the given criteria
            foreach (var _objective in _appraisalDetailsObjectives)
            {
                if (_objective.EmployeeId == empId && _objective.ManagerId == mgrId && _objective.Objective == objective)
                {
                    // Update the rating and feedback
                    _objective.EmployeeRating = rating;
                    _objective.EmployeeFeedback = feedback;
                }
            }

            // Exit the loop once the objective is updated
            return;
        }

        internal static List<string> GetEmployeeAppraisalObjectivesList(int empId, int mgrId)
        {
            List<string> list = new List<string>();
            foreach (var _objective in _appraisalDetailsObjectives)
            {
                if (_objective.EmployeeId == empId && _objective.ManagerId == mgrId)
                {
                    // Collect objectives
                    list.Add(_objective.Objective);
                }
            }
            return list;
        }

        internal static List<AppraisalDetailsObjective> GetSelfAppraisalDetails(int empId, int mgrId)
        {
            List<AppraisalDetailsObjective> list = new List<AppraisalDetailsObjective>();
            foreach (var _objective in _appraisalDetailsObjectives)
            {
                if (_objective.EmployeeId == empId && _objective.ManagerId == mgrId)
                {
                    // Collect objectives
                    list.Add(_objective);
                }
            }
            return list;
        }

        internal static void UpdateManagerAppraised(int empId, int mgrId, string objective, int employeeRating, string employeeFeedback, int managerRating, string managerFeedback)
        {
            //update based on empid, mgrid and competency
            // Find the objective that matches the given criteria
            foreach (var _objective in _appraisalDetailsObjectives)
            {
                if (_objective.EmployeeId == empId && _objective.ManagerId == mgrId && _objective.Objective == objective)
                {
                    // Update the rating and feedback
                    _objective.ManagerRating = managerRating;
                    _objective.ManagerFeedback = managerFeedback;
                }
            }

            // Exit the loop once the objective is updated
            return;
        }
    }
}
