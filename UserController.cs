using Book_DataAccess.Data;
using Book_DataAccess.Repository.IRepository;
using Book_Model.Models;
using Books_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Api's
        public IActionResult GetAll()
        {
            var userList = _context.ApplicationUsers.Include(u => u.company).ToList();
            var roles = _context.Roles.ToList();
            var userRoles = _context.UserRoles.ToList();
            foreach (var user in userList)
            {
                var roleId = userRoles.FirstOrDefault(r => r.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
                if (user.company == null)
                {
                    user.company = new Company()
                    {
                        Name = ""
                    };
                }
            }
            var adminUser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
            userList.Remove(adminUser);
            return Json(new { data = userList });
        }
        [HttpPost]
        public IActionResult lockUnlock([FromBody] string id)
        {
            bool isLocked = false;
            var userInDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if(userInDb == null)
            {
                return Json(new { success = false, message = "Something went wrong while lock or unlock the user" });
            }
            if(userInDb !=null && userInDb.LockoutEnd>DateTime.Now)
            {
                userInDb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                userInDb.LockoutEnd = DateTime.Now.AddYears(100);
                isLocked= true;
            }
            _context.SaveChanges();
            return Json(new { success = true, message = isLocked == true ? "User successfully locked" : "User successfully unlocked" });
        }
        #endregion
    }
}
