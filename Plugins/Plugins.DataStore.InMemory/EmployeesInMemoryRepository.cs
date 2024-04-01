using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class EmployeesInMemoryRepository : IEmployeeRepository
    {
        private static List<Employee> _employees = new List<Employee>()
        {
            new Employee { EmployeeId = 1, EmployeeName = "Jacob", Mobile = "654321", Email="jacob@gmail.com", EmployeeDesignation = "Sr.Dev", ManagerId = 4, Password="jacob", AdminPermission=2},
            new Employee { EmployeeId = 10, EmployeeName = "John", Mobile = "321456", Email="john@gmail.com", EmployeeDesignation = "Dev", ManagerId = 1, Password="jacob", AdminPermission=3}

        };
        public void AddEmployee(Employee employee)
        {
            var maxId = _employees.Max(x => x.EmployeeId);
            employee.EmployeeId = maxId + 1;
            _employees.Add(employee);
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = _employees.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }

        public void UpdateEmployee(int employeeId, Employee employee)
        {
            if (employeeId != employee.EmployeeId) return;

            var employeeToUpdate = _employees.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (employeeToUpdate != null)
            {
                employeeToUpdate.EmployeeName = employee.EmployeeName;
                employeeToUpdate.Mobile = employee.Mobile;
                employeeToUpdate.Email = employee.Email;
                employeeToUpdate.EmployeeDesignation = employee.EmployeeDesignation;
                employeeToUpdate.ManagerId = employee.ManagerId;
            }
        }

        public Employee GetEmployeeById(int employeeId, string key)
        {
            var employee = _employees.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (employee != null)
            {
                if (key == "auth")
                {
                    return new Employee
                    {
                        EmployeeId = employeeId,
                        EmployeeName = employee.EmployeeName,
                        Mobile = employee.Mobile,
                        Email = employee.Email,
                        EmployeeDesignation = employee.EmployeeDesignation,
                        ManagerId = employee.ManagerId,
                        AdminPermission = employee.AdminPermission

                    };
                }
                return new Employee
                {
                    EmployeeId = employeeId,
                    EmployeeName = employee.EmployeeName,
                    Mobile = employee.Mobile,
                    Email = employee.Email,
                    EmployeeDesignation = employee.EmployeeDesignation,
                    ManagerId = employee.ManagerId,
                    
                };
            }
            return null;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            var employee = _employees.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (employee != null)
            {
                return new Employee
                {
                    EmployeeId = employeeId,
                    EmployeeName = employee.EmployeeName,
                    Mobile = employee.Mobile,
                    Email = employee.Email,
                    EmployeeDesignation = employee.EmployeeDesignation,
                    ManagerId = employee.ManagerId,

                };
            }
            return null;
        }

        public IEnumerable<Employee> GetEmployees() => _employees;

        public IEnumerable<Employee> GetEmployeesByManagerId(int managerId)
        {
            var _directs = new List<Employee>();
            if (managerId != null)
            {
                foreach (var employee in _employees)
                {
                    if (employee.ManagerId == managerId)
                    {
                        _directs.Add(employee);
                    }
                }
                return _directs;
            }
            return null;
        }

        public Employee UserAuthentication(string username, string password)
        {
            foreach(var employee in _employees)
            {
                if (employee.Email.ToLower() ==username.ToLower() && employee.Password == password)
                {
                    string key = "auth";
                    return GetEmployeeById(employee.EmployeeId, key);
                }
            }
            return null;
        }
    }
}
