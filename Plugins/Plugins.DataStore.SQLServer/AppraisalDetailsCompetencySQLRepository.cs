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
    public class AppraisalDetailsCompetencySQLRepository : IAppraisalDetailsCompetencyRepository
    {
        private readonly AppraisalSystemContext db;

        public AppraisalDetailsCompetencySQLRepository(AppraisalSystemContext db)
        {
            this.db = db;
        }
        public void Add(int empId, int mgrId, string competency, int rating, string feedback)
        {
            var appraisalId = db.Appraisals
           .Where(a => a.EmployeeId == empId && a.ManagerId == mgrId)
           .Select(a => a.AppraisalId);
   

            if (appraisalId.Any())
            {
                var appraisalDetailsCompetency = new AppraisalDetailsCompetency
                {
                    AppraisalId= appraisalId.FirstOrDefault(),
                    EmployeeId = empId,
                    ManagerId = mgrId,
                    Competency = competency,
                    EmployeeRating = rating,
                    EmployeeFeedback = feedback
                };

                db.AppraisalDetailsCompetencies.Add(appraisalDetailsCompetency);
                db.SaveChanges();
            }
        }

        public IEnumerable<AppraisalDetailsCompetency> GetAppraisalDetails(int empId, int mgrId)
        {
            return db.AppraisalDetailsCompetencies
             .Where(ad => ad.EmployeeId == empId && ad.ManagerId == mgrId)
             .ToList();
        }

        public void UpdateManagerAppraised(int empId, int mgrId, string competency, int employeeRating, string employeeFeedback, int managerRating, string managerFeedback)
        {
            var appraisalDetail = db.AppraisalDetailsCompetencies
            .FirstOrDefault(ad => ad.EmployeeId == empId && ad.ManagerId == mgrId && ad.Competency == competency);

            if (appraisalDetail != null)
            {
                appraisalDetail.ManagerRating = managerRating;
                appraisalDetail.ManagerFeedback = managerFeedback;
                db.SaveChanges();
            }
        }
    }
}
