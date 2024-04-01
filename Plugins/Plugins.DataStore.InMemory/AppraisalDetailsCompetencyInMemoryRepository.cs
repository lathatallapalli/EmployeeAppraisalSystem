using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class AppraisalDetailsCompetencyInMemoryRepository : IAppraisalDetailsCompetencyRepository
    {
        private readonly IAppraisalRepository appraisalRepository;
        private List<AppraisalDetailsCompetency> _appraisalDetailsCompetencies = new List<AppraisalDetailsCompetency>();

        public AppraisalDetailsCompetencyInMemoryRepository(IAppraisalRepository appraisalRepository)
        {
            this.appraisalRepository = appraisalRepository;
        }
        public void Add(int empId, int mgrId, string competency, int rating, string feedback)
        {
            var appraisalId = appraisalRepository.GetAppraisalId(empId, mgrId);
            if (appraisalId != 0)
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
                    appraisalDetailsCompetency.DetailId = maxId + 1;
                }
                else
                {
                    appraisalDetailsCompetency.DetailId = 1;
                }
                _appraisalDetailsCompetencies?.Add(appraisalDetailsCompetency);
            }

        }

        public IEnumerable<AppraisalDetailsCompetency> GetAppraisalDetails(int empId, int mgrId)
        {
            List<AppraisalDetailsCompetency> list = new List<AppraisalDetailsCompetency>();
            foreach (var _appraisal in _appraisalDetailsCompetencies)
            {
                if (_appraisal.EmployeeId == empId && _appraisal.ManagerId == mgrId)
                {
                    // Collect objectives
                    list.Add(_appraisal);
                }
            }
            return list;
        }

        public void UpdateManagerAppraised(int empId, int mgrId, string competency, int employeeRating, string employeeFeedback, int managerRating, string managerFeedback)
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
