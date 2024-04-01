using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQLServer
{
    public class RoleCompetencySQLRepository : IRoleCompetencyRepository
    {
        private readonly AppraisalSystemContext db;
        private readonly ICompetencyRepository competencyRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IEmployeeRepository employeeRepository;

        public RoleCompetencySQLRepository(AppraisalSystemContext db,
                                           ICompetencyRepository competencyRepository,
                                           IRoleRepository roleRepository,
                                           IEmployeeRepository employeeRepository)
        {
            this.db = db;
            this.competencyRepository = competencyRepository;
            this.roleRepository = roleRepository;
            this.employeeRepository = employeeRepository;
        }
        public IEnumerable<Competency> GetCompetenciesByEmployeeId(int empId)
        {
            // Get employee designation to determine role
            var employee = employeeRepository.GetEmployeeById(empId);
            string role = employee.EmployeeDesignation;

            // Delegate the retrieval of competencies to the GetCompetenciesByRole method
            return GetCompetenciesByRole(role);
        }

        public IEnumerable<Competency> GetCompetenciesByRole(string role)
        {
            // Retrieve role competency details from the database based on the role name
            var roleId = roleRepository.GetIdByRoleName(role);
            var roleCompetencyDetails = db.RoleCompetencyDetails
                .Where(d => d.RoleId == roleId)
                .ToList();

            // Extract competency IDs
            var competencyIds = roleCompetencyDetails.Select(d => d.CompetencyId);

            // Retrieve competency objects using the competency repository
            var competencies = competencyIds.Select(id => competencyRepository.GetCompetencyById(id));

            return competencies;
        }
    }
}
