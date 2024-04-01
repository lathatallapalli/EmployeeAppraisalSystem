using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UseCases;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQLServer
{
    public class EmployeeSQLRepository : IEmployeeRepository
    {
        private readonly AppraisalSystemContext db;

        public EmployeeSQLRepository(AppraisalSystemContext db) 
        {
            this.db = db;
        }
        public void AddEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = db.Employees.Find(employeeId);
            if (employee == null) return;

            db.Employees.Remove(employee);
            db.SaveChanges();


        }

        public Employee GetEmployeeById(int employeeId, string key)
        {
            var employee = db.Employees.Find(employeeId);
            if (employee != null)
            {
                if (key == "auth")
                {
                    return new Employee
                    {
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.EmployeeName,
                        Mobile = employee.Mobile,
                        Email = employee.Email,
                        EmployeeDesignation = employee.EmployeeDesignation,
                        ManagerId = employee.ManagerId,
                        AdminPermission = employee.AdminPermission
                    };
                }
            }
            return null;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            var employee = db.Employees.Find(employeeId);
            if (employee != null)
            {
                
                return new Employee
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    Mobile = employee.Mobile,
                    Email = employee.Email,
                    EmployeeDesignation = employee.EmployeeDesignation,
                    ManagerId = employee.ManagerId
                };
                
            }
            return null;

        }

        public IEnumerable<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }

        public IEnumerable<Employee> GetEmployeesByManagerId(int managerId)
        {
            return db.Employees.Where(e => e.ManagerId == managerId).ToList();
        }

        public void UpdateEmployee(int employeeId, Employee employee)
        {
            if (employeeId != employee.EmployeeId) return;
            var _employee = db.Employees.Find(employeeId);

            if (_employee == null) return;
            _employee.EmployeeId = employeeId;
            _employee.EmployeeName = employee.EmployeeName;
            _employee.EmployeeDesignation = employee.EmployeeDesignation;
            _employee.Email = employee.Email;
            _employee.Mobile= employee.Mobile;
            _employee.ManagerId=employee.ManagerId;
            db.SaveChanges();

        }

        public Employee UserAuthentication(string username, string password)
        {
            return db.Employees.FirstOrDefault(e => e.Email.ToLower() == username.ToLower() && e.Password == password);
        }
    }
}
