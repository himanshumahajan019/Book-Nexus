using Book_DataAccess.Data;
using Book_DataAccess.Repository.IRepository;
using Book_Model.Models;
using Book_DataAccess.Repository;
using Books_Utility;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Book_Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin+","+SD.Role_Employe)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null) return View(category);
            DynamicParameters param = new DynamicParameters();
            param.Add("id", id.GetValueOrDefault());
            category = _unitOfWork.SP_Call.OneRecord<Category>(SD.Proc_GetCategory, param);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (category == null) return NotFound();
            if (!ModelState.IsValid) return View(category);
            DynamicParameters param = new DynamicParameters();
            param.Add("@name", category.Name);
            if (category.Id == 0)
                _unitOfWork.SP_Call.Execute(SD.Proc_CreateCategory, param);
            // _unitOfWork.coverType.Add(coverType);
            else
            {
                param.Add("@id", category.Id);
                _unitOfWork.SP_Call.Execute(SD.Proc_UpdateCategory, param);
            }
            //_unitOfWork.Category.Update(category);
            //_unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }




        //<---------------------------------------------------------API's------------------------------------------------------->

        #region API's
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.SP_Call.List<Category>(SD.Proc_GetCategorys) });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("id", id);
            var categoryInDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_GetCategory, param);
            // var CoverTypeInDb=_unitOfWork.coverType.Get(Id);
            if (categoryInDb == null)
                return Json(new { success = false, Message = "Somthing Went Wrong While Delete Data" });
            _unitOfWork.SP_Call.Execute(SD.Proc_DeleteCategory, param);
            // _unitOfWork.coverType.Remove(CoverTypeInDb);
            // _unitOfWork.Save();
            return Json(new { success = true, message = "Data Deleted Successfuly?" });
        }
        #endregion
    }
}
