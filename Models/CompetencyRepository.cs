namespace EmployeeAppraisalSystem.Models
{
    public class CompetencyRepository
    {
        private static List<Competency> _competencies = new List<Competency>()
        {
            new Competency { CompetencyId = 1, CompetencyName = "CyberSecurity", competencyType = CompetencyType.Technical},
            new Competency { CompetencyId = 2, CompetencyName = "CloudComputing", competencyType = CompetencyType.Technical}
        };

        public static void AddCompetency(Competency competency)
        {
            var maxId = _competencies.Max(x => x.CompetencyId);
            competency.CompetencyId = maxId + 1;
            _competencies.Add(competency);
        }

        public static List<Competency> GetCompetencies() => _competencies;

        public static Competency? GetCompetencyById(int competencyId)
        {
            var competency = _competencies.FirstOrDefault(x => x.CompetencyId == competencyId);
            if (competency != null)
            {
                return new Competency
                {
                    CompetencyId = competency.CompetencyId,
                    CompetencyName= competency.CompetencyName,
                    competencyType = competency.competencyType
                };
            }
            return null;
        }

        public static void DeleteCompetency(int competencyId)
        {
            var competency = _competencies.FirstOrDefault(x => x.CompetencyId == competencyId);
            if (competency != null)
            {
                _competencies.Remove(competency);
            }
        }

    }
}
