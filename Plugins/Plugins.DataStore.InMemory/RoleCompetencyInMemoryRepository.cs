using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class RoleCompetencyInMemoryRepository : IRoleCompetencyRepository
    {
        private readonly ICompetencyRepository competencyRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IEmployeeRepository employeeRepository;

        public RoleCompetencyInMemoryRepository(ICompetencyRepository competencyRepository, 
                                                IRoleRepository roleRepository,
                                                IEmployeeRepository employeeRepository)
        {
            this.competencyRepository = competencyRepository;
            this.roleRepository = roleRepository;
            this.employeeRepository = employeeRepository;
        }
        private static List<RoleCompetencyDetails> _details = new List<RoleCompetencyDetails>()
        {
            new RoleCompetencyDetails {DetailId=1,RoleId=1,CompetencyId=1},
            new RoleCompetencyDetails {DetailId=2,RoleId=2,CompetencyId=1},
            new RoleCompetencyDetails {DetailId=3,RoleId=1,CompetencyId=2},
            new RoleCompetencyDetails {DetailId=4,RoleId=2,CompetencyId=2}
        };
        

/*        public IEnumerable<Competency> GetCompetenciesByRole(int roleId)
        {
            List<Competency> rolecompetencies = new List<Competency>();
            foreach (RoleCompetencyDetails details in _details)
            {
                if (details.RoleId == roleId)
                {
                    Competency competency = competencyRepository.GetCompetencyById(details.CompId);
                    rolecompetencies.Add(competency);
                }
            }
            return rolecompetencies;
        }*/

        public IEnumerable<Competency> GetCompetenciesByRole(string role)
        {
            List<Competency> rolecompetencies = new List<Competency>();
            int roleid = roleRepository.GetIdByRoleName(role);
            foreach (RoleCompetencyDetails details in _details)
            {
                if (details.RoleId == roleid)
                {
                    Competency competency = competencyRepository.GetCompetencyById(details.CompetencyId);
                    rolecompetencies.Add(competency);
                }
            }
            return rolecompetencies;
        }

        public IEnumerable<Competency> GetCompetenciesByEmployeeId(int empId)
        {
            List<Competency> rolecompetencies = new List<Competency>();
            var role = employeeRepository.GetEmployeeById(empId).EmployeeDesignation;
            int roleid = roleRepository.GetIdByRoleName(role);
            foreach (RoleCompetencyDetails details in _details)
            {
                if (details.RoleId == roleid)
                {
                    Competency competency = competencyRepository.GetCompetencyById(details.CompetencyId);
                    rolecompetencies.Add(competency);
                }
            }
            return rolecompetencies;
        }
    }
}
