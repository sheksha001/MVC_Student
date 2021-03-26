using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3;

namespace WebApplication3.Controllers
{
    public class CoursesController : Controller
    {
        private StudentDBEntities db = new StudentDBEntities();

        public async Task<ActionResult> List()
        {
            return View(await db.Courses.ToListAsync());
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            if (db.Departments.ToList() != null)
                ViewBag.ddlDList = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction("List");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course != null)
            {
                ViewBag.DeptName = (from d in db.Departments
                                    where d.Deptid == course.DeptID
                                    select d.DeptName).FirstOrDefault();
                return View(course);
            }
            return HttpNotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Course _course)
        {
            if (ModelState.IsValid)
            {
                var c = db.Courses.FirstOrDefault(co => co.CourseId == _course.CourseId);
                if (c != null)
                {
                    c.CourseName = _course.CourseName;
                    // db.Cities.Update(city);
                    await db.SaveChangesAsync();
                    return RedirectToAction("List");
                }
                return View(_course);
            }
            return View(_course);
        }

        // GET: Courses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Course course = await db.Courses.FindAsync(id);
            db.Courses.Remove(course);
            await db.SaveChangesAsync();
            return RedirectToAction("List");
        }

    }
}
