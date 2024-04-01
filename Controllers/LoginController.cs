using CoreBusiness;
using EmployeeAppraisalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using UseCases.Interfaces;
using UseCases.EmployeeUseCases;

namespace EmployeeAppraisalSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAuthenticationUseCase userAuthenticationUseCase;
        private readonly IViewSelectedEmployeeUseCase viewSelectedEmployeeUseCase;

        public LoginController(IUserAuthenticationUseCase userAuthenticationUseCase,
                               IViewSelectedEmployeeUseCase viewSelectedEmployeeUseCase)
        {
            this.userAuthenticationUseCase = userAuthenticationUseCase;
            this.viewSelectedEmployeeUseCase = viewSelectedEmployeeUseCase;
        }
        public IActionResult Login()
        {
            var userLogin = new CoreBusiness.Employee();
            ViewData["Message"] = "";
            return View("LoginPage", userLogin);
        }

        [HttpPost]
        public IActionResult Login(CoreBusiness.Employee userLogin)
        {
            string key = "auth";
            var employee = userAuthenticationUseCase.Execute(userLogin.Email, userLogin.Password);
            if (employee != null)
            {
                var userAuthorization = employee.AdminPermission;
                switch (userAuthorization)
                {
                    case 3:
                        return RedirectToAction(nameof(EmployeeDashboard),new {empId = employee.EmployeeId, mgrId = employee.ManagerId});
                    /*break;*/
                    case 2:
                            return RedirectToAction(nameof(ManagerDashboard), new {empId = employee.EmployeeId, mgrId = employee.ManagerId});
                    /*break;*/
                    case 1:
                        return RedirectToAction(nameof(HrDashboard), new { empId = employee.EmployeeId, mgrId = employee.ManagerId });

                    default:
                        var _userLogin = new CoreBusiness.Employee();
                        ViewData["Message"] = "Enter Correct Details!";
                        return View("LoginPage", _userLogin);
                }
            }
            ViewData["Message"] = "Enter Correct Details!";
            return View("LoginPage", userLogin);

        }

        public IActionResult EmployeeDashboard(int empId,int mgrId)
        {
            var employee = viewSelectedEmployeeUseCase.Execute(empId);
            return View(employee);
        }

        public IActionResult ManagerDashboard(int empId, int mgrId)
        {
            var manager = viewSelectedEmployeeUseCase.Execute(empId);
            return View(manager);
        }

        public IActionResult HrDashboard(int empId, int mgrId)
        {
            var hr = viewSelectedEmployeeUseCase.Execute(empId);
            return View(hr);
        }

    }
}
