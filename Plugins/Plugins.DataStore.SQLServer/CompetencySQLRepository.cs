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
    public class CompetencySQLRepository : ICompetencyRepository
    {
        private readonly AppraisalSystemContext dbContext; // Replace YourDbContext with your actual DbContext

        public CompetencySQLRepository(AppraisalSystemContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddCompetency(Competency competency)
        {
            dbContext.Competencies.Add(competency);
            dbContext.SaveChanges();
        }

        public void DeleteCompetency(int competencyId)
        {
            var competency = dbContext.Competencies.Find(competencyId);
            if (competency != null)
            {
                dbContext.Competencies.Remove(competency);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Competency> GetCompetencies()
        {
            return dbContext.Competencies.ToList();
        }

        public Competency GetCompetencyById(int competencyId)
        {
            return dbContext.Competencies.Find(competencyId);
        }
    }
}
