using EmployeeAppraisalSystem.Models;
using EmployeeAppraisalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using UseCases.EmployeeUseCases;
using UseCases.Interfaces;

namespace EmployeeAppraisalSystem.Controllers
{
    public class Employee : Controller
    {
        private readonly IViewEmployeesUseCase viewEmployeesUseCase;
        private readonly IAddEmployeeUseCase addEmployeeUseCase;
        private readonly IDeleteEmployeeUseCase deleteEmployeeUseCase;
        private readonly IEditEmployeeUseCase editEmployeeUseCase;
        private readonly IViewSelectedEmployeeUseCase viewSelectedEmployeeUseCase;
        private readonly IViewManagerDirectsUseCase viewManagerDirectsUseCase;

        public Employee(IViewEmployeesUseCase viewEmployeesUseCase,
                        IAddEmployeeUseCase addEmployeeUseCase,
                        IDeleteEmployeeUseCase deleteEmployeeUseCase,
                        IEditEmployeeUseCase editEmployeeUseCase,
                        IViewSelectedEmployeeUseCase viewSelectedEmployeeUseCase,
                        IViewManagerDirectsUseCase viewManagerDirectsUseCase
            )
        {
            this.viewEmployeesUseCase = viewEmployeesUseCase;
            this.addEmployeeUseCase = addEmployeeUseCase;
            this.deleteEmployeeUseCase = deleteEmployeeUseCase;
            this.editEmployeeUseCase = editEmployeeUseCase;
            this.viewSelectedEmployeeUseCase = viewSelectedEmployeeUseCase;
            this.viewManagerDirectsUseCase = viewManagerDirectsUseCase;

        }



        public IActionResult EmployeeList()
        {
            var employees = viewEmployeesUseCase.Execute();
            return View(employees);
        }

        public IActionResult DirectsList(int? mgrId)
        {
            ViewBag.Action = "directs";
            ViewBag.ManagerId = mgrId;
            var directs = viewManagerDirectsUseCase.Execute(mgrId.HasValue ? mgrId.Value : 0);
            return View("EmployeeList",directs);
        }
        public IActionResult EditEmployee(int? empId)
        {
            ViewBag.Action = "editemployee";
            var employee = viewSelectedEmployeeUseCase.Execute(empId.HasValue ? empId.Value : 0);
            if (employee.EmployeeName != null)
            {
                EmployeeNameHistoryViewModel.SessionEmployeeName = employee.EmployeeName;
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult EditEmployee(CoreBusiness.Employee employee)
        {
            ViewBag.Action = "editemployee";
            if (ModelState.IsValid)
            {
                editEmployeeUseCase.Execute(employee.EmployeeId, employee);
                return RedirectToAction(nameof(EmployeeList));
            }
            ViewData["SessionEmpName"] = EmployeeNameHistoryViewModel.SessionEmployeeName;
            return View(employee);
        }
        public IActionResult AddEmployee()
        {
            ViewBag.Action = "addemployee";
            return View(); 
        }

        [HttpPost]
        public IActionResult AddEmployee(CoreBusiness.Employee employee)
        {
            ViewBag.Action = "addemployee";
/*            if (ModelState.IsValid)*/
            {
                addEmployeeUseCase.Execute(employee);
                return RedirectToAction(nameof(EmployeeList));
            }
            return View(employee);
        }

        public IActionResult DeleteEmployee(int? empid)
        {
            deleteEmployeeUseCase.Execute(empid.Value);
            return RedirectToAction(nameof(EmployeeList));
        }

    }
}
