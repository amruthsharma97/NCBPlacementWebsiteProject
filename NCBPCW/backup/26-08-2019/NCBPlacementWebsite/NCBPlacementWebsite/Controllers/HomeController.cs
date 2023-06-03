using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NCBPlacementWebsite.Models;
using System.Data.Entity;


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

                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        if (User.IsInRole("Student"))
                        {
                            Response.Cookies.Add(new HttpCookie("SId", stud.Id.ToString()));
                        }
                    }
                }
                catch
                {
                    if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("SId"))
                    {
                        HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["SId"];
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    }
                    ViewBag.Profileerr="User Registered but Student Prifile Not Created. Contact Placement Officer.";
                    return View();

                }
                
                
                return View();
            }

            
            return View();
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            

            return View(db.ContactLists.ToList());
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
            int yop = 1994;
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            for (int i = 0; i < 100; i++)
            {
                dictionary.Add(++yop, yop.ToString());
            }

            ViewBag.allyop = new SelectList(dictionary, "Key", "Value");
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
                if (studentProfile.SSLCYOP >= studentProfile.PUCYOP || studentProfile.SSLCYOP >= studentProfile.DegreeYOP || studentProfile.SSLCYOP >= studentProfile.PUCYOP && studentProfile.SSLCYOP >= studentProfile.DegreeYOP || studentProfile.PUCYOP-studentProfile.SSLCYOP < 2)
                {
                    ViewBag.errmsg = "SSLC or 10th Year Of Passing is Greater than PUC Year Of Passing Or Degree Year Of Passing Or Difference Between SSLC and PUC is Less Than 2!!";
                }
                else if (studentProfile.PUCYOP >= studentProfile.DegreeYOP || studentProfile.DegreeYOP-studentProfile.PUCYOP<3)
                {
                    ViewBag.errmsg2 = "PUC Year Of Passing is Greater than Degree Year Of Passing Or Difference Between PUC and Degree is Less Than 3!!";
                }
                else
                {
                    studentProfile.Status = 2;
                    db.Entry(studentProfile).State = EntityState.Modified;
                    db.SaveChanges();
                    Guid Id = Guid.Parse(Request.Cookies["SId"].Value);
                    return RedirectToAction("MyProfile", "Home", new { id = Id });
                }
            }
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "BCA", Text = "BCA" });
            list.Add(new SelectListItem() { Value = "BA", Text = "BA" });
            list.Add(new SelectListItem() { Value = "BSC-CBZ", Text = "BSC-CBZ" });
            list.Add(new SelectListItem() { Value = "BSC-PMCS", Text = "BSC-PMCS" });
            list.Add(new SelectListItem() { Value = "BSC-PME", Text = "BSC-PME" });
            list.Add(new SelectListItem() { Value = "BCOM", Text = "BCOM" });

            ViewBag.Branches = list;
            int yop = 1994;
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            for (int i = 0; i < 100; i++)
            {
                dictionary.Add(++yop, yop.ToString());
            }

            ViewBag.allyop = new SelectList(dictionary, "Key", "Value");
            return View(studentProfile);
        }
    }
}