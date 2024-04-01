using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class CompetencyInMemoryRepository : ICompetencyRepository
    {
        private static List<Competency> _competencies = new List<Competency>()
        {
            new Competency { CompetencyId = 1, CompetencyName = "CyberSecurity", competencyType = CompetencyType.Technical},
            new Competency { CompetencyId = 2, CompetencyName = "CloudComputing", competencyType = CompetencyType.Technical}
        };
        public void AddCompetency(Competency competency)
        {
            var maxId = _competencies.Max(x => x.CompetencyId);
            competency.CompetencyId = maxId + 1;
            _competencies.Add(competency);
        }

        public void DeleteCompetency(int competencyId)
        {
            var competency = _competencies.FirstOrDefault(x => x.CompetencyId == competencyId);
            if (competency != null)
            {
                _competencies.Remove(competency);
            }
        }

        public IEnumerable<Competency> GetCompetencies() => _competencies;

        public Competency GetCompetencyById(int competencyId)
        {
            var competency = _competencies.FirstOrDefault(x => x.CompetencyId == competencyId);
            if (competency != null)
            {
                return new Competency
                {
                    CompetencyId = competency.CompetencyId,
                    CompetencyName = competency.CompetencyName,
                    competencyType = competency.competencyType
                };
            }
            return null;
        }
    }
}
