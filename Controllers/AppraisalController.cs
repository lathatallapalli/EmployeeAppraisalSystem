using CoreBusiness;
using EmployeeAppraisalSystem.Models;
using EmployeeAppraisalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Ocsp;
using System;
using System.Security.AccessControl;
using UseCases.AppraisalUseCases;
using UseCases.Interfaces;
using UseCases.ObjectiveUseCases;
using static EmployeeAppraisalSystem.Program;

namespace EmployeeAppraisalSystem.Controllers
{
    public class AppraisalController : Controller
    {
        private readonly AppraisalRepository _appraisalRepository;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IViewCompetenciesOfRoleUseCase viewCompetenciesOfRoleUseCase;
        private readonly IViewSelectedEmployeeUseCase viewSelectedEmployeeUseCase;
        private readonly IAddObjectiveUseCase addObjectiveUseCase;
        private readonly IViewEmployeeAppraisalObjectivesUseCase viewEmployeeAppraisalObjectivesUseCase;
        private readonly IViewCompetenciesOfEmployeeUseCase viewCompetenciesOfEmployeeUseCase;
        private readonly IUpdateAppraisalStatusUseCase updateAppraisalStatusUseCase;
        private readonly IAddSelfAppraisalCompetenciesUseCase addSelfAppraisalCompetenciesUseCase;
        private readonly IAddSelfAppraisalObjectivesUseCase addSelfAppraisalObjectivesUseCase;
        private readonly IViewAppraisalObjectivesUseCase viewAppraisalObjectivesUseCase;
        private readonly IViewAppraisalCompetenciesUseCase viewAppraisalCompetenciesUseCase;
        private readonly IUpdateManagerAppraisalCompetenciesUseCase updateManagerAppraisalCompetenciesUseCase;
        private readonly IUpdateManagerAppraisalObjectivesUseCase updateManagerAppraisalObjectivesUseCase;
        private readonly IStartAppraisalCycleUseCase startAppraisalCycleUseCase;

        public AppraisalController(AppraisalRepository appraisalRepository,
                                   IHubContext<NotificationHub> hubContext,
                                   IViewCompetenciesOfRoleUseCase viewCompetenciesOfRoleUseCase,
                                   IViewSelectedEmployeeUseCase viewSelectedEmployeeUseCase,
                                   IAddObjectiveUseCase addObjectiveUseCase,
                                   IViewEmployeeAppraisalObjectivesUseCase viewEmployeeAppraisalObjectivesUseCase,
                                   IViewCompetenciesOfEmployeeUseCase viewCompetenciesOfEmployeeUseCase,
                                   IUpdateAppraisalStatusUseCase updateAppraisalStatusUseCase,
                                   IAddSelfAppraisalCompetenciesUseCase addSelfAppraisalCompetenciesUseCase,
                                   IAddSelfAppraisalObjectivesUseCase addSelfAppraisalObjectivesUseCase,
                                   IViewAppraisalObjectivesUseCase viewAppraisalObjectivesUseCase,
                                   IViewAppraisalCompetenciesUseCase viewAppraisalCompetenciesUseCase,
                                   IUpdateManagerAppraisalCompetenciesUseCase updateManagerAppraisalCompetenciesUseCase,
                                   IUpdateManagerAppraisalObjectivesUseCase updateManagerAppraisalObjectivesUseCase,
                                   IStartAppraisalCycleUseCase startAppraisalCycleUseCase)
        {
            _appraisalRepository = appraisalRepository;
            _hubContext = hubContext;
            this.viewCompetenciesOfRoleUseCase = viewCompetenciesOfRoleUseCase;
            this.viewSelectedEmployeeUseCase = viewSelectedEmployeeUseCase;
            this.addObjectiveUseCase = addObjectiveUseCase;
            this.viewEmployeeAppraisalObjectivesUseCase = viewEmployeeAppraisalObjectivesUseCase;
            this.viewCompetenciesOfEmployeeUseCase = viewCompetenciesOfEmployeeUseCase;
            this.updateAppraisalStatusUseCase = updateAppraisalStatusUseCase;
            this.addSelfAppraisalCompetenciesUseCase = addSelfAppraisalCompetenciesUseCase;
            this.addSelfAppraisalObjectivesUseCase = addSelfAppraisalObjectivesUseCase;
            this.viewAppraisalObjectivesUseCase = viewAppraisalObjectivesUseCase;
            this.viewAppraisalCompetenciesUseCase = viewAppraisalCompetenciesUseCase;
            this.updateManagerAppraisalCompetenciesUseCase = updateManagerAppraisalCompetenciesUseCase;
            this.updateManagerAppraisalObjectivesUseCase = updateManagerAppraisalObjectivesUseCase;
            this.startAppraisalCycleUseCase = startAppraisalCycleUseCase;
        }

        public IActionResult StartAppraisalCycle(int empId, int mgrId)
        {
            startAppraisalCycleUseCase.Execute();
            return RedirectToAction(actionName: "HrDashboard", controllerName:"Login", new { empId=empId,mgrId = mgrId });
        }
        public async Task<IActionResult> GetAppraisalForm(int empId, int mgrId)
        {
            var employee = viewSelectedEmployeeUseCase.Execute(empId);
            if (employee != null)
            {
                var rolecompetencies = viewCompetenciesOfEmployeeUseCase.Execute(empId);
                ViewData["EmployeeName"] = employee.EmployeeName;
            }
            updateAppraisalStatusUseCase.Execute(empId, mgrId, CoreBusiness.Status.Created);
            await _hubContext.Clients.User(empId.ToString()).SendAsync("ReceiveNotification", "You have an active appraisal!");
            return Ok();
        }

        public IActionResult SetAppraisalObjectives(int empId, int mgrId)
        {
            ViewBag.mgrId = mgrId;
            CoreBusiness.Employee employee = viewSelectedEmployeeUseCase.Execute(empId);
            return View(employee);
        }

        [HttpPost]

        public IActionResult SetAppraisalObjectives(int empId, int mgrId, string[] objects)
        {
            foreach (string _object in objects)
            {
                addObjectiveUseCase.Execute(empId, mgrId, _object);
            }
            return RedirectToAction("directslist", controllerName: "Employee", new { mgrId = mgrId });
        }

        public IActionResult SelfAppraisalForm(int empId, int mgrId)
        {
            SelfAppraisalFormViewModel selfAppraisalFormViewModel = new SelfAppraisalFormViewModel();
            selfAppraisalFormViewModel.EmpId = empId;
            selfAppraisalFormViewModel.MgrId = mgrId;
            IEnumerable<string> objectives = viewEmployeeAppraisalObjectivesUseCase.Execute(empId, mgrId);
            selfAppraisalFormViewModel.ObjectivesSelfAppraised = new List<ObjectiveRatingFeedback>();
            foreach (string _objective in objectives)
            {
                ObjectiveRatingFeedback _objectiveRatingFeedback = new ObjectiveRatingFeedback();
                _objectiveRatingFeedback.Objective = _objective;
                selfAppraisalFormViewModel.ObjectivesSelfAppraised.Add(_objectiveRatingFeedback);
            }
            selfAppraisalFormViewModel.CompetenciesSelfAppraised = new List<CompetencyRatingFeedback>();
            IEnumerable<CoreBusiness.Competency> roleCompetencies = viewCompetenciesOfEmployeeUseCase.Execute(empId);
            foreach (var _rolecompetency in roleCompetencies)
            {
                CompetencyRatingFeedback _competencyRatingFeedback = new CompetencyRatingFeedback();
                _competencyRatingFeedback.Competency = _rolecompetency.CompetencyName;
                selfAppraisalFormViewModel.CompetenciesSelfAppraised.Add(_competencyRatingFeedback);
            }
            return View(selfAppraisalFormViewModel);
        }

        [HttpPost]
        public IActionResult SelfAppraisalForm(SelfAppraisalFormViewModel selfappraisalFormViewModel)
        {
            for (int i = 0; i < selfappraisalFormViewModel.CompetenciesSelfAppraised.Count(); i++)
            {
                addSelfAppraisalCompetenciesUseCase.Execute(selfappraisalFormViewModel.EmpId,
                                                            selfappraisalFormViewModel.MgrId,
                                                            selfappraisalFormViewModel.CompetenciesSelfAppraised[i].Competency,
                                                            selfappraisalFormViewModel.CompetenciesSelfAppraised[i].Rating,
                                                            selfappraisalFormViewModel.CompetenciesSelfAppraised[i].Feedback);

            }
            for (int i = 0; i < selfappraisalFormViewModel.ObjectivesSelfAppraised.Count(); i++)
            {
                addSelfAppraisalObjectivesUseCase.Execute(selfappraisalFormViewModel.EmpId,
                                                          selfappraisalFormViewModel.MgrId,
                                                          selfappraisalFormViewModel.ObjectivesSelfAppraised[i].Objective,
                                                          selfappraisalFormViewModel.ObjectivesSelfAppraised[i].Rating,
                                                          selfappraisalFormViewModel.ObjectivesSelfAppraised[i].Feedback);

            }
            updateAppraisalStatusUseCase.Execute(selfappraisalFormViewModel.EmpId, selfappraisalFormViewModel.MgrId, CoreBusiness.Status.SelfRated);
            return RedirectToAction("EmployeeDashboard", controllerName: "Login", new { empId = selfappraisalFormViewModel.EmpId, mgrId = selfappraisalFormViewModel.MgrId });
        }



        public IActionResult ManagerAppraisalForm(int? empId, int? mgrId)
        {
            ManagerAppraisalFormViewModel managerAppraisalFormViewModel = new ManagerAppraisalFormViewModel();
            managerAppraisalFormViewModel.EmpId = empId.Value;
            managerAppraisalFormViewModel.MgrId = mgrId.Value;
            IEnumerable<CoreBusiness.AppraisalDetailsObjective> selfObjectiveAppraisalDetails = viewAppraisalObjectivesUseCase.Execute(empId.Value, mgrId.Value);
            managerAppraisalFormViewModel.ObjectivesManagerAppraised = new List<ObjectiveManagerRatingFeedback>();
            foreach (var _selfAppraisalDetail in selfObjectiveAppraisalDetails)
            {
                ObjectiveManagerRatingFeedback _objectiveManagerRatingFeedback = new ObjectiveManagerRatingFeedback();
                _objectiveManagerRatingFeedback.Objective = _selfAppraisalDetail.Objective;
                _objectiveManagerRatingFeedback.EmployeeRating = _selfAppraisalDetail.EmployeeRating;
                _objectiveManagerRatingFeedback.EmployeeFeedback = _selfAppraisalDetail.EmployeeFeedback;
                _objectiveManagerRatingFeedback.ManagerRating = 0;
                _objectiveManagerRatingFeedback.ManagerFeedback = string.Empty;
                managerAppraisalFormViewModel.ObjectivesManagerAppraised.Add(_objectiveManagerRatingFeedback);
            }

            managerAppraisalFormViewModel.CompetenciesManagerAppraised = new List<CompetencyManagerRatingFeedback>();
            IEnumerable<CoreBusiness.AppraisalDetailsCompetency> selfCompetencyAppraisalDetails = viewAppraisalCompetenciesUseCase.Execute(empId.Value, mgrId.Value);
            foreach (var _selfAppraisalDetail in selfCompetencyAppraisalDetails)
            {
                CompetencyManagerRatingFeedback _competencyManagerRatingFeedback = new CompetencyManagerRatingFeedback();
                _competencyManagerRatingFeedback.Competency = _selfAppraisalDetail.Competency;
                _competencyManagerRatingFeedback.EmployeeRating = _selfAppraisalDetail.EmployeeRating;
                _competencyManagerRatingFeedback.EmployeeFeedback = _selfAppraisalDetail.EmployeeFeedback;
                _competencyManagerRatingFeedback.ManagerRating = 0;
                _competencyManagerRatingFeedback.ManagerFeedback = string.Empty;
                managerAppraisalFormViewModel.CompetenciesManagerAppraised.Add(_competencyManagerRatingFeedback);
            }
            return View(managerAppraisalFormViewModel);
        }

        [HttpPost]
        public IActionResult ManagerAppraisalForm(ManagerAppraisalFormViewModel managerappraisalFormViewModel)
        {
            for (int i = 0; i < managerappraisalFormViewModel.CompetenciesManagerAppraised.Count(); i++)
            {
                updateManagerAppraisalCompetenciesUseCase.Execute(managerappraisalFormViewModel.EmpId,
                                                                  managerappraisalFormViewModel.MgrId,
                                                                  managerappraisalFormViewModel.CompetenciesManagerAppraised[i].Competency,
                                                                  managerappraisalFormViewModel.CompetenciesManagerAppraised[i].EmployeeRating,
                                                                  managerappraisalFormViewModel.CompetenciesManagerAppraised[i].EmployeeFeedback,
                                                                  managerappraisalFormViewModel.CompetenciesManagerAppraised[i].ManagerRating,
                                                                  managerappraisalFormViewModel.CompetenciesManagerAppraised[i].ManagerFeedback);

/*                AppraisalDetailsCompetencyRepository.UpdateManagerAppraised(
                    managerappraisalFormViewModel.EmpId,
                    managerappraisalFormViewModel.MgrId,
                    managerappraisalFormViewModel.CompetenciesManagerAppraised[i].Competency,
                    managerappraisalFormViewModel.CompetenciesManagerAppraised[i].EmployeeRating,
                    managerappraisalFormViewModel.CompetenciesManagerAppraised[i].EmployeeFeedback,
                    managerappraisalFormViewModel.CompetenciesManagerAppraised[i].ManagerRating,
                    managerappraisalFormViewModel.CompetenciesManagerAppraised[i].ManagerFeedback
                    );*/
            }
            for (int i = 0; i < managerappraisalFormViewModel.ObjectivesManagerAppraised.Count(); i++)
            {
                updateManagerAppraisalObjectivesUseCase.Execute(managerappraisalFormViewModel.EmpId,
                                                                managerappraisalFormViewModel.MgrId,
                                                                managerappraisalFormViewModel.ObjectivesManagerAppraised[i].Objective,
                                                                managerappraisalFormViewModel.ObjectivesManagerAppraised[i].EmployeeRating,
                                                                managerappraisalFormViewModel.ObjectivesManagerAppraised[i].EmployeeFeedback,
                                                                managerappraisalFormViewModel.ObjectivesManagerAppraised[i].ManagerRating,
                                                                managerappraisalFormViewModel.ObjectivesManagerAppraised[i].ManagerFeedback);
/*                AppraisalDetailsObjectiveRepository.UpdateManagerAppraised(
                    managerappraisalFormViewModel.EmpId,
                    managerappraisalFormViewModel.MgrId,
                    managerappraisalFormViewModel.ObjectivesManagerAppraised[i].Objective,
                    managerappraisalFormViewModel.ObjectivesManagerAppraised[i].EmployeeRating,
                    managerappraisalFormViewModel.ObjectivesManagerAppraised[i].EmployeeFeedback,
                    managerappraisalFormViewModel.ObjectivesManagerAppraised[i].ManagerRating,
                    managerappraisalFormViewModel.ObjectivesManagerAppraised[i].ManagerFeedback
                    );*/
            }
            updateAppraisalStatusUseCase.Execute(managerappraisalFormViewModel.EmpId, managerappraisalFormViewModel.MgrId, CoreBusiness.Status.Rated);
            return RedirectToAction("directslist", controllerName: "Employee", new { mgrId = managerappraisalFormViewModel.MgrId });

        }

        public IActionResult EmployeeAppraisalReview(int? empId, int? mgrId)
        {
            ManagerAppraisalFormViewModel managerAppraisalFormViewModel = new ManagerAppraisalFormViewModel();
            managerAppraisalFormViewModel.EmpId = empId.Value;
            managerAppraisalFormViewModel.MgrId = mgrId.Value;
            IEnumerable<CoreBusiness.AppraisalDetailsObjective> selfObjectiveAppraisalDetails = viewAppraisalObjectivesUseCase.Execute(empId.Value, mgrId.Value);
            managerAppraisalFormViewModel.ObjectivesManagerAppraised = new List<ObjectiveManagerRatingFeedback>();
            foreach (var _selfAppraisalDetail in selfObjectiveAppraisalDetails)
            {
                ObjectiveManagerRatingFeedback _objectiveManagerRatingFeedback = new ObjectiveManagerRatingFeedback();
                _objectiveManagerRatingFeedback.Objective = _selfAppraisalDetail.Objective;
                _objectiveManagerRatingFeedback.EmployeeRating = _selfAppraisalDetail.EmployeeRating;
                _objectiveManagerRatingFeedback.EmployeeFeedback = _selfAppraisalDetail.EmployeeFeedback;
                _objectiveManagerRatingFeedback.ManagerRating = _selfAppraisalDetail.ManagerRating;
                _objectiveManagerRatingFeedback.ManagerFeedback = _selfAppraisalDetail.ManagerFeedback;
                managerAppraisalFormViewModel.ObjectivesManagerAppraised.Add(_objectiveManagerRatingFeedback);
            }
            /*            var role = EmployeeRepository.GetRoleByEmployeeId(empId.Value);
                        var roleid = RoleRepository.GetIdByRoleName(role.ToLower());*/
            managerAppraisalFormViewModel.CompetenciesManagerAppraised = new List<CompetencyManagerRatingFeedback>();
            IEnumerable<CoreBusiness.AppraisalDetailsCompetency> selfCompetencyAppraisalDetails = viewAppraisalCompetenciesUseCase.Execute(empId.Value, mgrId.Value);
            foreach (var _selfAppraisalDetail in selfCompetencyAppraisalDetails)
            {
                CompetencyManagerRatingFeedback _competencyManagerRatingFeedback = new CompetencyManagerRatingFeedback();
                _competencyManagerRatingFeedback.Competency = _selfAppraisalDetail.Competency;
                _competencyManagerRatingFeedback.EmployeeRating = _selfAppraisalDetail.EmployeeRating;
                _competencyManagerRatingFeedback.EmployeeFeedback = _selfAppraisalDetail.EmployeeFeedback;
                _competencyManagerRatingFeedback.ManagerRating = _selfAppraisalDetail.ManagerRating;
                _competencyManagerRatingFeedback.ManagerFeedback = _selfAppraisalDetail.ManagerFeedback;
                managerAppraisalFormViewModel.CompetenciesManagerAppraised.Add(_competencyManagerRatingFeedback);
            }
            return View(managerAppraisalFormViewModel);
        }

        [HttpPost]
        public IActionResult EmployeeAppraisalReview(ManagerAppraisalFormViewModel managerappraisalFormViewModel)
        {
            updateAppraisalStatusUseCase.Execute(managerappraisalFormViewModel.EmpId, managerappraisalFormViewModel.MgrId, CoreBusiness.Status.Completed);
            return RedirectToAction("EmployeeDashboard", controllerName: "Login", new { empId = managerappraisalFormViewModel.EmpId, mgrId = managerappraisalFormViewModel.MgrId });
        }

    }
}   



