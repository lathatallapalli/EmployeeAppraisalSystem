using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQLServer
{
    public class AppraisalSQLRepository : IAppraisalRepository
    {

        private readonly AppraisalSystemContext db;

        public AppraisalSQLRepository(AppraisalSystemContext db)
        {
            this.db = db;
        }
        public int GetAppraisalId(int empId, int mgrId)
        {
            var appraisal = db.Appraisals
            .FirstOrDefault(a => a.EmployeeId == empId && a.ManagerId == mgrId);

            return appraisal?.AppraisalId ?? 0;
        }

        public void NewAppraisals()
        {
            var employees = db.Employees.ToList();

            foreach (var employee in employees)
            {
                var appraisal = new Appraisal
                {
                    EmployeeId = employee.EmployeeId,
                    ManagerId = employee.ManagerId ?? 0, // Assuming ManagerId is nullable
                    status = Status.New
                };

                db.Appraisals.Add(appraisal);
            }

            db.SaveChanges();
        }

        public void Update(int empId, int mgrId, Status status)
        {
            var appraisal = db.Appraisals
            .FirstOrDefault(a => a.EmployeeId == empId && a.ManagerId == mgrId);

            if (appraisal != null)
            {
                appraisal.status = status;
               db.SaveChanges();
            }
        }
    }
}
