using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NCBPlacementWebsite.Models;
using System.Data.Entity;
using System.Data;

namespace NCBPlacementWebsite.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                string email = User.Identity.GetUserName();
                StudentProfile stud = new StudentProfile();
                stud = db.StudentProfiles.Where(x => x.EmailId == email).ToList().SingleOrDefault();

                Response.Cookies.Add(new HttpCookie("SId", stud.Id.ToString()));
                
                return View();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult MyProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            if (studentProfile == null)
            {
                return HttpNotFound();
            }
            return View(studentProfile);
        }


        // GET: StudentProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            if (studentProfile == null)
            {
                return HttpNotFound();
            }
            return View(studentProfile);
        }

        // POST: StudentProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentProfile studentProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentProfile);
        }
    }
}