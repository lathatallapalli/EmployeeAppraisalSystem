using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Plugins.DataStore.InMemory
{
    public class AppraisalDetailsObjectiveInMemoryRepository : IAppraisalDetailsObjectiveRepository
    {
       
        private static List<AppraisalDetailsObjective> _appraisalDetailsObjectives = new List<AppraisalDetailsObjective>();
        private readonly IAppraisalRepository appraisalRepository;

        public AppraisalDetailsObjectiveInMemoryRepository(IAppraisalRepository appraisalRepository)
        {
            this.appraisalRepository = appraisalRepository;
        }
        public void AddObjective(int empId, int mgrId, string objective)
        {
            var appraisalId = appraisalRepository.GetAppraisalId(empId,mgrId);
            if (appraisalId != 0)
            {
                var appraisalDetailsObjective = new AppraisalDetailsObjective
                {
                    AppraisalId=appraisalId,
                    EmployeeId = empId,
                    ManagerId = mgrId,
                    Objective = objective
                };
                if (_appraisalDetailsObjectives != null && _appraisalDetailsObjectives.Count > 0)
                {
                    var maxId = _appraisalDetailsObjectives.Max(x => x.DetailId);
                    appraisalDetailsObjective.DetailId = maxId + 1;
                }
                else
                {
                    appraisalDetailsObjective.DetailId = 1;
                }
                _appraisalDetailsObjectives?.Add(appraisalDetailsObjective);

            }
        }

        public void UpdateSelfAppraised(int empId, int mgrId, string objective, int rating, string feedback)
        {
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

        public List<string> GetEmployeeAppraisalObjectivesList(int empId, int mgrId)
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

        public IEnumerable<AppraisalDetailsObjective> GetAppraisalDetails(int empId, int mgrId)
        {
            List<AppraisalDetailsObjective> list = new List<AppraisalDetailsObjective>();
            foreach (var _appraisal in _appraisalDetailsObjectives)
            {
                if (_appraisal.EmployeeId == empId && _appraisal.ManagerId == mgrId)
                {
                    // Collect objectives
                    list.Add(_appraisal);
                }
            }
            return list;
        }

        public void UpdateManagerAppraised(int empId, int mgrId, string objective, int employeeRating, string employeeFeedback, int managerRating, string managerFeedback)
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

        IEnumerable<string> IAppraisalDetailsObjectiveRepository.GetEmployeeAppraisalObjectivesList(int empId, int mgrId)
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
    }
}
