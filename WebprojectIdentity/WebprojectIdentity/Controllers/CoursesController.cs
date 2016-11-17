using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebprojectIdentity.Models;

namespace WebprojectIdentity.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Include(p => p.teacher).SingleOrDefault(p => p.id == id);
            if (course == null)
            {
                return HttpNotFound();
            }

            //Teacher c =course.teacher; 
            //ViewBag.Course = c.firstname;
            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            IQueryable c = db.Teachers;// we must send the complete listof corses  to the veiw it can be throw IQueryable
            ViewBag.Course = c;
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,coursename,periode")] Course course, string select)
        {
            int selectid = Convert.ToInt32(select);
            if (ModelState.IsValid)
            {
                Teacher t = db.Teachers.Where(p => p.id == selectid).FirstOrDefault();
                course.teacher = t;
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);

        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            IQueryable c = db.Teachers;// we must send the complete listof corses  to the veiw it can be throw IQueryable
            ViewBag.teacher = c;
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string coursename, string periode, string select)
        {
            Course cours = db.Courses.Where(c => c.id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                cours.coursename = coursename;
                cours.periode = periode;
                int selectid = Convert.ToInt32(select);
                Teacher teach = db.Teachers.Where(c => c.id == selectid).FirstOrDefault();
                cours.teacher = teach;
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cours);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Include(p => p.teacher).SingleOrDefault(p => p.id == id);

            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
