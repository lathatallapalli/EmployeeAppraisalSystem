using EmployeeAppraisalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using UseCases.Interfaces;

namespace EmployeeAppraisalSystem.Controllers
{
    public class Competencies : Controller
    {
        private readonly IAddCompetencyUseCase addCompetencyUseCase;
        private readonly IDeleteCompetencyUseCase deleteCompetencyUseCase;
        private readonly IGetCompetencyUseCase getCompetencyUseCase;
        private readonly IViewCompetenciesUseCase viewCompetenciesUseCase;

        public Competencies(IAddCompetencyUseCase addCompetencyUseCase,
                            IDeleteCompetencyUseCase deleteCompetencyUseCase,
                            IGetCompetencyUseCase getCompetencyUseCase,
                            IViewCompetenciesUseCase viewCompetenciesUseCase)
        {
            this.addCompetencyUseCase = addCompetencyUseCase;
            this.deleteCompetencyUseCase = deleteCompetencyUseCase;
            this.getCompetencyUseCase = getCompetencyUseCase;
            this.viewCompetenciesUseCase = viewCompetenciesUseCase;
        }
        public IActionResult CompetencyList()
        {
            {
                var competencies = viewCompetenciesUseCase.Execute();
                return View(competencies);
            }
        }

        public IActionResult AddCompetency()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCompetency(CoreBusiness.Competency competency)
        {

            {
                addCompetencyUseCase.Execute(competency);
                return RedirectToAction(nameof(CompetencyList));
            }
            return View(competency);
        }

        public IActionResult DeleteCompetency(int competencyid)
        {
            deleteCompetencyUseCase.Execute(competencyid);
            return RedirectToAction(nameof(CompetencyList));
        }
    }
}
