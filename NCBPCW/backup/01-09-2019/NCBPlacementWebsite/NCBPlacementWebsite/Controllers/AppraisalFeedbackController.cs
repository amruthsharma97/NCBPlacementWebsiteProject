using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NCBPlacementWebsite.Models;
using NCBPlacementWebsite.Report;
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
    public class AppraisalFeedbackController : Controller
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
        
        // GET: AppraisalFeedback/Add
        public ActionResult Add()
        {
            AppraisalFeedbacks appraisalFeedbacks = new AppraisalFeedbacks();
            
            
            
            string email = User.Identity.GetUserName();
            StudentProfile stud = new StudentProfile();
            stud = db.StudentProfiles.Where(x => x.EmailId == email).ToList().SingleOrDefault();



            appraisalFeedbacks.Branch = stud.Branch;

            List<Lecturers> lecturers = new List<Lecturers>();
            lecturers = db.Lecturer.ToList();

            List<SelectListItem> list2 = new List<SelectListItem>();

            foreach (var lec in lecturers)
            {
                list2.Add(new SelectListItem() { Value = lec.LuctName, Text = lec.LuctName });
            }

            ViewBag.lecturer = list2;


            List<Subjects> subjects =new List<Subjects>();

            subjects = db.Subject.Where(x => x.Branch == stud.Branch).ToList();
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var sub in subjects)
            {
                list.Add(new SelectListItem() { Value = sub.Subject, Text = sub.Subject });
            }

            ViewBag.Subjects = list;
            int yop = 1994;
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            Dictionary<int, string> dictionary2 = new Dictionary<int, string>();

            for (int i = 0; i < 100; i++)
            {
                dictionary.Add(++yop, yop.ToString());
            }

            ViewBag.syop = new SelectList(dictionary, "Key", "Value");
            ViewBag.eyop = new SelectList(dictionary, "Key", "Value");

            for (int i = 1; i <= 10; i++)
            {
                dictionary2.Add(i, i.ToString());
            }


            ViewBag.rating = new SelectList(dictionary2, "Key", "Value");
            
            return View(appraisalFeedbacks);
        }

        // POST: AppraisalFeedback/Create
        [HttpPost]
        public ActionResult Add(AppraisalFeedbacks appraisalFeedbacks)
        {
             if (ModelState.IsValid)
                {
                    db.AppraisalFeedback.Add(appraisalFeedbacks);
                    db.SaveChanges();

                    
                    return RedirectToAction("Add");
                }


             


             List<Subjects> subjects1 = new List<Subjects>();

             subjects1 = db.Subject.Where(x => x.Branch == appraisalFeedbacks.Branch).ToList();
             List<SelectListItem> list2 = new List<SelectListItem>();

             foreach (var sub in subjects1)
             {
                 list2.Add(new SelectListItem() { Value = sub.Subject, Text = sub.Subject });
             }

             ViewBag.Subjects = list2;

             List<Lecturers> lecturers = new List<Lecturers>();
             lecturers = db.Lecturer.ToList();

             List<SelectListItem> list3 = new List<SelectListItem>();

             foreach (var lec in lecturers)
             {
                 list3.Add(new SelectListItem() { Value = lec.LuctName, Text = lec.LuctName });
             }

             ViewBag.lecturer = list3;

             int yop1 = 1994;
             Dictionary<int, string> dictionary1 = new Dictionary<int, string>();
             Dictionary<int, string> dictionary3 = new Dictionary<int, string>();

             for (int i = 0; i < 100; i++)
             {
                 dictionary1.Add(++yop1, yop1.ToString());
             }

             ViewBag.syop = new SelectList(dictionary1, "Key", "Value");
             ViewBag.eyop = new SelectList(dictionary1, "Key", "Value");

             for (int i = 1; i <= 10; i++)
             {
                 dictionary3.Add(i, i.ToString());
             }


             ViewBag.rating = new SelectList(dictionary3, "Key", "Value");

             return View(appraisalFeedbacks);
        }


        public ActionResult AppraisalReport()
        {
            FeedbackReport feedbackReport = new FeedbackReport();
            byte[] abyte=feedbackReport.PrepareReport();

            return File(abyte, "application/pdf");
        }
        
    }
}
