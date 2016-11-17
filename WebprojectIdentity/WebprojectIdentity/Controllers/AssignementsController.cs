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
    public class AssignementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assignements
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Assignements.ToList());
        }

        // GET: Assignements/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignement assignement = db.Assignements.Find(id);
            if (assignement == null)
            {
                return HttpNotFound();
            }
            return View(assignement);
        }

        // GET: Assignements/Create
        [Authorize (Roles ="Admin")]
        public ActionResult Create()
        {
            IQueryable c = db.Courses;// we must send the complete listof corses  to the veiw it can be throw IQueryable
            ViewBag.Course = c;
            return View();
        }

        // POST: Assignements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,assignmentname,periode")] Assignement assignement, string select)
        {
            int selectid = Convert.ToInt32(select);
            if (ModelState.IsValid)
            {

                Course c = db.Courses.Where(p => p.id == selectid).FirstOrDefault();
                db.Assignements.Add(assignement);
                c.assignement.Add(assignement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assignement);
        }

        // GET: Assignements/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignement assignement = db.Assignements.Find(id);
            if (assignement == null)
            {
                return HttpNotFound();
            }
            IQueryable c = db.Courses;// we must send the complete listof corses  to the veiw it can be throw IQueryable
            ViewBag.Course = c;

            return View(assignement);
        }

        // POST: Assignements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string assignmentname, string periode, string select)
        {
            Assignement assign = db.Assignements.Where(c => c.id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                assign.assignmentname = assignmentname;
                assign.periode = periode;
                int selectid = Convert.ToInt32(select);
                Course cou = db.Courses.Where(c => c.id == selectid).FirstOrDefault();
                //cou.assignement.Clear();
                cou.assignement.Add(assign);
                db.Entry(assign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assign);

        }

        // GET: Assignements/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignement assignement = db.Assignements.Find(id);
            if (assignement == null)
            {
                return HttpNotFound();
            }
            return View(assignement);
        }

        // POST: Assignements/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignement assignement = db.Assignements.Find(id);
            db.Assignements.Remove(assignement);
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
