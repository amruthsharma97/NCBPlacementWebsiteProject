using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NCBPlacementWebsite.Models;

namespace NCBPlacementWebsite.Controllers
{
    public class SubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subjects
        public ActionResult Index()
        {

            return View(db.Subject.ToList());
        }

        // GET: Subjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects subjects = db.Subject.Find(id);
            if (subjects == null)
            {
                return HttpNotFound();
            }
            return View(subjects);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Course> course = new List<Course>();

            course = db.Courses.ToList();

            foreach (var item in course)
            {
                list.Add(new SelectListItem() { Value = item.Crse.ToString(), Text = item.Crse.ToString() });
            }

            ViewBag.Branches = list;
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Subject,Branch")] Subjects subjects)
        {
            if (ModelState.IsValid)
            {
                db.Subject.Add(subjects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> list = new List<SelectListItem>();
            List<Course> course = new List<Course>();

            course = db.Courses.ToList();

            foreach (var item in course)
            {
                list.Add(new SelectListItem() { Value = item.Crse.ToString(), Text = item.Crse.ToString() });
            }

            ViewBag.Branches = list;
            return View(subjects);
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int? id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Course> course = new List<Course>();

            course = db.Courses.ToList();

            foreach (var item in course)
            {
                list.Add(new SelectListItem() { Value = item.Crse.ToString(), Text = item.Crse.ToString() });
            }

            ViewBag.Branches = list;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects subjects = db.Subject.Find(id);
            if (subjects == null)
            {
                return HttpNotFound();
            }
            return View(subjects);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Subject,Branch")] Subjects subjects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> list = new List<SelectListItem>();
            List<Course> course = new List<Course>();

            course = db.Courses.ToList();

            foreach (var item in course)
            {
                list.Add(new SelectListItem() { Value = item.Crse.ToString(), Text = item.Crse.ToString() });
            }

            ViewBag.Branches = list;
            return View(subjects);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects subjects = db.Subject.Find(id);
            if (subjects == null)
            {
                return HttpNotFound();
            }
            return View(subjects);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subjects subjects = db.Subject.Find(id);
            db.Subject.Remove(subjects);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
