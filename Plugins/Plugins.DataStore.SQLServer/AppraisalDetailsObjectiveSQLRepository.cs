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
    public class AppraisalDetailsObjectiveSQLRepository : IAppraisalDetailsObjectiveRepository
    {
        private readonly AppraisalSystemContext db;

        public AppraisalDetailsObjectiveSQLRepository(AppraisalSystemContext db)
        {
            this.db = db;
        }
        public void AddObjective(int empId, int mgrId, string objective)
        {
            var appraisalId = db.Appraisals
            .FirstOrDefault(a => a.EmployeeId == empId && a.ManagerId == mgrId)?.AppraisalId;

            if (appraisalId != null)
            {
                var appraisalDetailsObjective = new AppraisalDetailsObjective
                {
                    AppraisalId = appraisalId.Value,
                    EmployeeId = empId,
                    ManagerId = mgrId,
                    Objective = objective
                };

                db.AppraisalDetailsObjectives.Add(appraisalDetailsObjective);
                db.SaveChanges();
            }
        }

        public IEnumerable<CoreBusiness.AppraisalDetailsObjective> GetAppraisalDetails(int empId, int mgrId)
        {
            return db.AppraisalDetailsObjectives
             .Where(ad => ad.EmployeeId == empId && ad.ManagerId == mgrId)
             .ToList();
        }

        public IEnumerable<string> GetEmployeeAppraisalObjectivesList(int empId, int mgrId)
        {
            return db.AppraisalDetailsObjectives
            .Where(a => a.EmployeeId == empId && a.ManagerId == mgrId)
            .Select(a => a.Objective)
            .ToList();
        }

        public void UpdateManagerAppraised(int empId, int mgrId, string objective, int employeeRating, string employeeFeedback, int managerRating, string managerFeedback)
        {
            var appraisal = db.AppraisalDetailsObjectives
            .FirstOrDefault(a => a.EmployeeId == empId && a.ManagerId == mgrId && a.Objective == objective);

            if (appraisal != null)
            {
                appraisal.EmployeeRating = employeeRating;
                appraisal.EmployeeFeedback = employeeFeedback;
                appraisal.ManagerRating = managerRating;
                appraisal.ManagerFeedback = managerFeedback;

                db.SaveChanges();
            }
        }

        public void UpdateSelfAppraised(int empId, int mgrId, string objective, int rating, string feedback)
        {
            var appraisal = db.AppraisalDetailsObjectives
            .FirstOrDefault(a => a.EmployeeId == empId && a.ManagerId == mgrId && a.Objective == objective);

            if (appraisal != null)
            {
                appraisal.EmployeeRating = rating;
                appraisal.EmployeeFeedback = feedback;

                db.SaveChanges();
            }
        }
    }
}
