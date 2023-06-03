using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NCBPlacementWebsite.Models;
using OfficeOpenXml;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NCBPlacementWebsite.Controllers
{
    public class CurriculumFeedbackController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        // GET: CurriculumFeedback/Add
        public ActionResult Add()
        {
            CurriculumFeedbacks curriculumFeedbacks = new CurriculumFeedbacks();

            string email = User.Identity.GetUserName();
            StudentProfile stud = new StudentProfile();
            stud = db.StudentProfiles.Where(x => x.EmailId == email).ToList().SingleOrDefault();

            Ncbusers nu = new Ncbusers();

            curriculumFeedbacks.SName = nu.GetFullName(email);
            curriculumFeedbacks.URNo = stud.URNo;
            curriculumFeedbacks.Branch = stud.Branch;

            List<Subjects> subjects = new List<Subjects>();

            subjects = db.Subject.Where(x => x.Branch == stud.Branch).ToList();
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var sub in subjects)
            {
                list.Add(new SelectListItem() { Value = sub.Subject, Text = sub.Subject });
            }

            ViewBag.Subjects = list;
            int yop = 1994;
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            

            for (int i = 0; i < 100; i++)
            {
                dictionary.Add(++yop, yop.ToString());
            }

            ViewBag.syop = new SelectList(dictionary, "Key", "Value");
            ViewBag.eyop = new SelectList(dictionary, "Key", "Value");

            List<SelectListItem> list2 = new List<SelectListItem>();

            list2.Add(new SelectListItem() { Value = "A", Text = "Excellent" });
            list2.Add(new SelectListItem() { Value = "B", Text = "Very Good" });
            list2.Add(new SelectListItem() { Value = "C", Text = "Good" });
            list2.Add(new SelectListItem() { Value = "D", Text = "Average" });
            list2.Add(new SelectListItem() { Value = "E", Text = "Poor" });

            ViewBag.rating = list2;

            return View(curriculumFeedbacks);
        }



        [HttpPost]
        public ActionResult Add(CurriculumFeedbacks curriculumFeedbacks)
        {

            if (ModelState.IsValid)
            {
                db.CurriculumFeedback.Add(curriculumFeedbacks);
                db.SaveChanges();

                return RedirectToAction("Add");
            }

            List<Subjects> subjects1 = new List<Subjects>();

            subjects1 = db.Subject.Where(x => x.Branch == curriculumFeedbacks.Branch).ToList();
            List<SelectListItem> list2 = new List<SelectListItem>();

            foreach (var sub in subjects1)
            {
                list2.Add(new SelectListItem() { Value = sub.Subject, Text = sub.Subject });
            }

            ViewBag.Subjects = list2;
            int yop1 = 1994;
            Dictionary<int, string> dictionary1 = new Dictionary<int, string>();
            Dictionary<int, string> dictionary3 = new Dictionary<int, string>();

            for (int i = 0; i < 100; i++)
            {
                dictionary1.Add(++yop1, yop1.ToString());
            }

            ViewBag.syop = new SelectList(dictionary1, "Key", "Value");
            ViewBag.eyop = new SelectList(dictionary1, "Key", "Value");

            List<SelectListItem> list3 = new List<SelectListItem>();

            list3.Add(new SelectListItem() { Value = "A", Text = "Excellent" });
            list3.Add(new SelectListItem() { Value = "B", Text = "Very Good" });
            list3.Add(new SelectListItem() { Value = "C", Text = "Good" });
            list3.Add(new SelectListItem() { Value = "D", Text = "Average" });
            list3.Add(new SelectListItem() { Value = "E", Text = "Poor" });

            ViewBag.rating = list3;

            return View(curriculumFeedbacks);
        }
    }
}