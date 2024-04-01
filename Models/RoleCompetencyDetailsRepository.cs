using System.Collections;

namespace EmployeeAppraisalSystem.Models
{
    public class RoleCompetencyDetailsRepository
    {
        private static List<RoleCompetencyDetails> _details = new List<RoleCompetencyDetails>()
        {
            new RoleCompetencyDetails {DetailId=1,RoleId=1,CompId=1},
            new RoleCompetencyDetails {DetailId=2,RoleId=2,CompId=1},
            new RoleCompetencyDetails {DetailId=3,RoleId=1,CompId=2},
            new RoleCompetencyDetails {DetailId=4,RoleId=2,CompId=2}
        };

        public static List<Competency> GetCompetenciesByRole(int roleId)
        {
            List<Competency> rolecompetencies = new List<Competency>();
            foreach (RoleCompetencyDetails details in _details)
            {
                if(details.RoleId == roleId)
                {
                    Competency competency = CompetencyRepository.GetCompetencyById(details.CompId);
                    rolecompetencies.Add(competency);
                }
            }            
            return rolecompetencies;
        }

    }
}
