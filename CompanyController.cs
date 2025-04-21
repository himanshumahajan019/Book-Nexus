using Book_DataAccess.Repository.IRepository;
using Book_Model.Models;
using Books_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? Id)
        {
            Company company = new Company();
            if (Id == null) return View(company);
            company = _unitOfWork.Company.Get(Id.GetValueOrDefault());
            if (company == null) return NotFound();
            return View(company);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (company == null) return BadRequest();
            if (!ModelState.IsValid) return View(company);
            if (company.Id == 0)
                _unitOfWork.Company.Add(company);
            else
                _unitOfWork.Company.Update(company);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        #region API's
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Company.GetAll() });
        }
        #endregion
    }
}
