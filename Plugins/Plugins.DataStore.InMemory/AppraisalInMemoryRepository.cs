using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class AppraisalInMemoryRepository : IAppraisalRepository
    {
        private List<Appraisal> _appraisals = new List<Appraisal>();
        private readonly IEmployeeRepository employeeRepository;

        public AppraisalInMemoryRepository(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }


        public void Update(int empId, int mgrId, Status status)
        {
            // Find the appraisal based on empId and mgrId
            var appraisal = _appraisals.FirstOrDefault(a => a.EmployeeId == empId && a.ManagerId == mgrId);

            if (appraisal != null)
            {
                // Update the status
                appraisal.status = status;
            }
            else
            {
                // Appraisal not found, you might want to handle this scenario
            }
        }

        public void NewAppraisals()
        {
            foreach ( var employee in employeeRepository.GetEmployees())
            {
                Appraisal appraisal = new Appraisal
                {
                    EmployeeId = employee.EmployeeId,
                    ManagerId = employee.ManagerId.Value,
                    status = Status.New
                };
                if (_appraisals.Count > 0)
                {
                    var max = _appraisals.Max(x => x.AppraisalId);
                    appraisal.AppraisalId = max + 1;
                }
                else appraisal.AppraisalId = 1;
                _appraisals.Add(appraisal);
            }
        }

        public int GetAppraisalId(int empId, int mgrId)
        {
            foreach ( var appraisal in _appraisals)
            {
                if (appraisal.EmployeeId == empId && appraisal.ManagerId == mgrId)
                {
                    return appraisal.AppraisalId;
                }
            }
            return 0;
        }
    }
}
