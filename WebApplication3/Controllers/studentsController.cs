using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class studentsController : Controller
    {
        private StudentDBEntities db = new StudentDBEntities();

        // GET: students
        public async Task<ActionResult> index(string search)
        {

            if (search == null)
            {
                return View(await db.Students.ToListAsync());
            }
            else
            {
                return View(db.Students.Where(s => s.sname.Contains(search)));
            }
        }

        // GET: students/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: students/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: students/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: students/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student student = await db.Students.FindAsync(id);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        //-----------> Ajax Calls <-----------//

        //-- Get Student Course
        [HttpPost]
        public ActionResult GetStudentCourses(int stuId)
        {
            CreateStudentData(stuId);
            return PartialView("_StudentCourrse", db.Students.Find(stuId));
        }

        //-- Add Student Course
        [HttpPost]
        public ActionResult AddStudentCourse(int stuId, int couId)
        {
            StudentCourse s = new StudentCourse();
            s.CourseId = couId;
            s.sid = stuId;
            db.StudentCourses.Add(s);
            db.SaveChanges();
            CreateStudentData(stuId);
            return PartialView("_StudentCourrse", db.Students.Find(stuId));
        }
        //-- Delete Student Course
        [HttpPost]
        public ActionResult DeleteStudentCourse(int stuId, int couId)
        {
            StudentCourse s = db.StudentCourses.SingleOrDefault(m => m.sid == stuId && m.CourseId == couId);
            if (s == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                db.StudentCourses.Remove(s);
                db.SaveChanges();
                CreateStudentData(stuId);
            }
            return PartialView("_StudentCourrse", db.Students.Find(stuId));
        }

        internal void CreateStudentData(int stuId)
        {
            var Courselist = db.Courses.ToList();
            var matchingCourselist = (from c in db.Courses
                                      from sc in db.StudentCourses
                                      where (sc.CourseId == c.CourseId && sc.sid == stuId)
                                      select c).ToList();

            ViewBag.StdentCourseList = matchingCourselist;



            var nonMatchingCourselist = Courselist.Except(matchingCourselist).ToList();
            ViewBag.ddlStudentCoursesList = (from c in nonMatchingCourselist
                                             select new SelectListItem
                                             {
                                                 Text = c.CourseName,
                                                 Value = c.CourseId.ToString(),
                                             }).ToList();
        }
    }
}
