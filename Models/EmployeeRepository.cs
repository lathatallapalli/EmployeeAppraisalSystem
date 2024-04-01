using System.Reflection;

namespace EmployeeAppraisalSystem.Models
{
    public static class EmployeeRepository
    {
        private static List<Employee> _employees = new List<Employee>()
        {
            new Employee { EmployeeId = 1, EmployeeName = "Jacob", Mobile = "123456", Email="example1@gmail.com", EmployeeDesignation = "Sr.Dev", ManagerId = 4},
            new Employee { EmployeeId = 10, EmployeeName = "John", Mobile = "123456", Email="example1@gmail.com", EmployeeDesignation = "Dev", ManagerId = 1}

        };

        public static void AddEmployee(Employee employee)
        {
            var maxId = _employees.Max(x => x.EmployeeId);
            employee.EmployeeId = maxId + 1;
            _employees.Add(employee);
        }

        public static List<Employee> GetEmployees() => _employees;

        public static Employee? GetEmployeeById(int employeeId)
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
                    ManagerId = employee.ManagerId
                };
            }
            return null;
        }

        public static int? GetManagerIdById(int employeeId)
        {
            var employee = _employees.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (employee != null)
            {
                return employee.ManagerId;
            }
            return null;
        }

        public static List<Employee> GetEmployeesByManagerId(int managerId)
        {
            var _directs = new List<Employee>() ;
            if (managerId != null)
            {
                foreach (var employee in _employees)
                {
                    if (employee.ManagerId == managerId)
                    {
                        _directs.Add(employee);
                    }
                }
                return _directs ;
            }
            return null;
        }
        public static string GetRoleByEmployeeId(int empId)
        {
                foreach (var employee in _employees)
                {
                    if (employee.EmployeeId == empId)
                    {
                        return employee.EmployeeDesignation;
                    }
                }
            return null;
        }

        public static void UpdateEmployee(int employeeId, Employee employee)
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

        public static void DeleteEmployee(int employeeId)
        {
            var employee = _employees.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }

    }
}

