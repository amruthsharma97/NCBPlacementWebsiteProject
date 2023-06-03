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
    public class LecturersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lecturers
        public ActionResult Index()
        {
            return View(db.Lecturer.ToList());
        }

        // GET: Lecturers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturers lecturers = db.Lecturer.Find(id);
            if (lecturers == null)
            {
                return HttpNotFound();
            }
            return View(lecturers);
        }

        // GET: Lecturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lecturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LuctName,DeptName")] Lecturers lecturers)
        {
            if (ModelState.IsValid)
            {
                db.Lecturer.Add(lecturers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lecturers);
        }

        // GET: Lecturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturers lecturers = db.Lecturer.Find(id);
            if (lecturers == null)
            {
                return HttpNotFound();
            }
            return View(lecturers);
        }

        // POST: Lecturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LuctName,DeptName")] Lecturers lecturers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lecturers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lecturers);
        }

        // GET: Lecturers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturers lecturers = db.Lecturer.Find(id);
            if (lecturers == null)
            {
                return HttpNotFound();
            }
            return View(lecturers);
        }

        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lecturers lecturers = db.Lecturer.Find(id);
            db.Lecturer.Remove(lecturers);
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
