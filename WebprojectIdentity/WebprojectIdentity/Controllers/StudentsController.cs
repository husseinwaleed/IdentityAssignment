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
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [Authorize]
        public ActionResult Create()
        {

            IQueryable c = db.Courses;// we must send the complete listof corses  to the veiw it can be throw IQueryable
            ViewBag.Course = c;
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,secondname,city,phone")] Student student, string select)
        {
            int selectid = Convert.ToInt32(select);

            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                Course c = db.Courses.Where(p => p.id == selectid).FirstOrDefault();
                c.student.Add(student);
                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            IQueryable c = db.Courses;// we must send the complete listof corses  to the veiw it can be throw IQueryable
            ViewBag.Course = c;
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string firstname, string secondname, string city, string phone, string select)
        {

            Student students = db.Students.Where(c => c.id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                Student stu = db.Students.Where(p => p.id == id).FirstOrDefault();
                stu.city = city;
                stu.phone = phone;
                stu.firstname = firstname;
                stu.secondname = secondname;
                stu.ownedcourse.Clear();
                int selectid = Convert.ToInt32(select);

                Course cou = db.Courses.Where(c => c.id == selectid).FirstOrDefault();
                cou.student.Add(stu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(students);
        }

        // GET: Students/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
