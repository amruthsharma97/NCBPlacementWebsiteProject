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

                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Student"))
                    {
                        Response.Cookies.Add(new HttpCookie("SId", stud.Id.ToString()));
                    }
                }
                
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
        public ActionResult MyProfile(Guid? id)
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
        public ActionResult Edit(Guid? id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "BCA", Text = "BCA" });
            list.Add(new SelectListItem() { Value = "BA", Text = "BA" });
            list.Add(new SelectListItem() { Value = "BSC-CBZ", Text = "BSC-CBZ" });
            list.Add(new SelectListItem() { Value = "BSC-PMCS", Text = "BSC-PMCS" });
            list.Add(new SelectListItem() { Value = "BSC-PME", Text = "BSC-PME" });
            list.Add(new SelectListItem() { Value = "BCOM", Text = "BCOM" });

            ViewBag.Branches = list;
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
                studentProfile.Status = 3;
                db.Entry(studentProfile).State = EntityState.Modified;
                db.SaveChanges();
                Guid Id = Guid.Parse(Request.Cookies["SId"].Value);
                return RedirectToAction("MyProfile", "Home", new { id = Id });
            }
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "BCA", Text = "BCA" });
            list.Add(new SelectListItem() { Value = "BA", Text = "BA" });
            list.Add(new SelectListItem() { Value = "BSC-CBZ", Text = "BSC-CBZ" });
            list.Add(new SelectListItem() { Value = "BSC-PMCS", Text = "BSC-PMCS" });
            list.Add(new SelectListItem() { Value = "BSC-PME", Text = "BSC-PME" });
            list.Add(new SelectListItem() { Value = "BCOM", Text = "BCOM" });

            ViewBag.Branches = list;
            return View(studentProfile);
        }
    }
}