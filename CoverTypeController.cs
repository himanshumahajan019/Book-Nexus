using Book_DataAccess.Repository.IRepository;
using Book_Model.Models;
using Books_Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employe)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null) return View(coverType);
            //coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", id.GetValueOrDefault());
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_GetCoverType, param);
            if (coverType == null) return NotFound();
            return View(coverType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType == null) return NotFound();
            if (!ModelState.IsValid) return View(coverType);
            DynamicParameters param = new DynamicParameters();
            param.Add("@name", coverType.Name);
            if (coverType.Id == 0)
                _unitOfWork.SP_Call.Execute(SD.Proc_CreateCoverType, param);
            // _unitOfWork.coverType.Add(coverType);
            else
            {
                param.Add("@id", coverType.Id);
                _unitOfWork.SP_Call.Execute(SD.Proc_UpdateCoverType, param);
            }
            //    _unitOfWork.CoverType.Update(coverType);
            //_unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }





        //<------------------------------------------------------------------------------------API's-------------------------------------------------------------------------------->
        #region Api's
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_GetCoverTypes) });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("id", id);
            var CoverTypeInDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_GetCoverType, param);
            // var CoverTypeInDb=_unitOfWork.coverType.Get(Id);
            if (CoverTypeInDb == null)
                return Json(new { success = false, Message = "Somthing Went Wrong While Delete Data" });
            _unitOfWork.SP_Call.Execute(SD.Proc_DeleteCoverType, param);
            // _unitOfWork.coverType.Remove(CoverTypeInDb);
            // _unitOfWork.Save();
            return Json(new { success = true, message = "Data Deleted Successfuly?" });

        }
        #endregion
    }
}
