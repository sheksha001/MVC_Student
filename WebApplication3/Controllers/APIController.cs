using System;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class APIController : Controller
    {
        private StudentDBEntities db = new StudentDBEntities();
        bool status = false;


        //---> Check Dept Name
        public async Task<JsonResult> IsAlreadyDeptNameAsync(string DeptName)
        {
            if (await db.Departments.AnyAsync(x => x.DeptName.ToLower() == DeptName.ToLower()))
            {
                status = false;
            }
            else
            {
                status = true;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }


        //---> Check Dept Name
        public async Task<JsonResult> IsAlreadyCourseNameAsync(Course _course)
        {
            if (await db.Courses.AnyAsync(x => x.CourseName.ToLower() == _course.CourseName.ToLower() && x.DeptID== _course.DeptID))
            {
                status = false;
            }
            else
            {
                status = true;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

    }
}