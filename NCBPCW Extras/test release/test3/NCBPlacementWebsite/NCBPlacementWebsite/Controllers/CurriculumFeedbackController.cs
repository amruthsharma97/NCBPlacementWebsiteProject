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


        [Authorize(Roles = "Student")]
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


        [Authorize(Roles = "Student")]
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



        [Authorize(Roles = "Admin")]
        public ActionResult CurriculumReportByCourse()
        {
            List<Course> course = new List<Course>();

            course = db.Courses.ToList();

            List<SelectListItem> clist = new List<SelectListItem>();

            clist.Add(new SelectListItem() { Value = "All", Text = "All" });

            foreach (var crse in course)
            {
                clist.Add(new SelectListItem() { Value = crse.Crse, Text = crse.Crse });
            }

            ViewBag.crselist = clist;

            int yr = 1994;

            Dictionary<int, string> dict = new Dictionary<int, string>();

            for (int i = 1; i <= 100; i++)
            {
                dict.Add(++yr, yr.ToString());
            }

            ViewBag.year = new SelectList(dict, "Key", "Value");

            return View();
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CurriculumReportByCourse(CurriculumReportByCrse curriculumReportByCrse)
        {

            List<Subjects> subj = new List<Subjects>();
            List<CurriculumFeedbacks> cfbyyear = new List<CurriculumFeedbacks>();

            if (curriculumReportByCrse.Crse != "All")
            {
                subj = db.Subject.Where(d => d.Branch == curriculumReportByCrse.Crse).ToList();
            }
            else
            {
                subj = db.Subject.ToList();
            }

            cfbyyear = db.CurriculumFeedback.Where(a => a.EYear == curriculumReportByCrse.Year).ToList();

            if (ModelState.IsValid && cfbyyear.Count() > 0 && subj.Count() > 0)
            {
                FeedbackReport feedbackReport = new FeedbackReport();
                byte[] abyte = feedbackReport.PrepareReport(curriculumReportByCrse);

                return File(abyte, "application/pdf");
            }
            else if (cfbyyear.Count() == 0 || subj.Count == 0)
            {
                List<Course> course = new List<Course>();

                course = db.Courses.ToList();

                List<SelectListItem> clist = new List<SelectListItem>();

                clist.Add(new SelectListItem() { Value = "All", Text = "All" });

                foreach (var crse in course)
                {
                    clist.Add(new SelectListItem() { Value = crse.Crse, Text = crse.Crse });
                }

                ViewBag.crselist = clist;

                int yr = 1994;

                Dictionary<int, string> dict = new Dictionary<int, string>();

                for (int i = 1; i <= 100; i++)
                {
                    dict.Add(++yr, yr.ToString());
                }

                ViewBag.year = new SelectList(dict, "Key", "Value");

                ViewBag.err = "No Data Exist According To Your Selection!!!";

                return View(curriculumReportByCrse);

            }

            List<Course> course2 = new List<Course>();

            course2 = db.Courses.ToList();

            List<SelectListItem> clist2 = new List<SelectListItem>();

            clist2.Add(new SelectListItem() { Value = "All", Text = "All" });

            foreach (var crse in course2)
            {
                clist2.Add(new SelectListItem() { Value = crse.Crse, Text = crse.Crse });
            }

            ViewBag.crselist = clist2;

            int yr2 = 1994;

            Dictionary<int, string> dict2 = new Dictionary<int, string>();

            for (int i = 1; i <= 100; i++)
            {
                dict2.Add(++yr2, yr2.ToString());
            }

            ViewBag.year = new SelectList(dict2, "Key", "Value");

            return View(curriculumReportByCrse);
        }




        [Authorize(Roles = "Admin")]
        public ActionResult CurriculumReportBySubject()
        {
            List<Subjects> subjects = new List<Subjects>();

            subjects = db.Subject.ToList();

            List<SelectListItem> slist = new List<SelectListItem>();



            foreach (var subj in subjects)
            {
                slist.Add(new SelectListItem() { Value = subj.Subject, Text = subj.Subject });
            }

            ViewBag.subjlist = slist;

            int yr = 1994;

            Dictionary<int, string> dict = new Dictionary<int, string>();

            for (int i = 1; i <= 100; i++)
            {
                dict.Add(++yr, yr.ToString());
            }

            ViewBag.year = new SelectList(dict, "Key", "Value");

            return View();
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CurriculumReportBySubject(CurriculumReportBySubj curriculumReportBySubj)
        {

            List<CurriculumFeedbacks> cfbyyear = new List<CurriculumFeedbacks>();

            
            cfbyyear = db.CurriculumFeedback.Where(a => a.EYear == curriculumReportBySubj.Year).ToList();

            if (ModelState.IsValid && cfbyyear.Count() > 0 )
            {
                FeedbackReport feedbackReport = new FeedbackReport();
                byte[] abyte = feedbackReport.PrepareReport(curriculumReportBySubj);

                return File(abyte, "application/pdf");
            }
            else if (cfbyyear.Count() == 0 )
            {
                List<Subjects> subjects = new List<Subjects>();

                subjects = db.Subject.ToList();

                List<SelectListItem> slist = new List<SelectListItem>();

                

                foreach (var subj in subjects)
                {
                    slist.Add(new SelectListItem() { Value = subj.Subject, Text = subj.Subject });
                }

                ViewBag.subjlist = slist;

                int yr = 1994;

                Dictionary<int, string> dict = new Dictionary<int, string>();

                for (int i = 1; i <= 100; i++)
                {
                    dict.Add(++yr, yr.ToString());
                }

                ViewBag.year = new SelectList(dict, "Key", "Value");

                ViewBag.err = "No Data Exist According To Your Selection!!!";

                return View(curriculumReportBySubj);

            }

            List<Subjects> subjects2 = new List<Subjects>();

            subjects2 = db.Subject.ToList();

            List<SelectListItem> slist2 = new List<SelectListItem>();



            foreach (var subj in subjects2)
            {
                slist2.Add(new SelectListItem() { Value = subj.Subject, Text = subj.Subject });
            }

            ViewBag.subjlist = slist2;

            int yr2 = 1994;

            Dictionary<int, string> dict2 = new Dictionary<int, string>();

            for (int i = 1; i <= 100; i++)
            {
                dict2.Add(++yr2, yr2.ToString());
            }

            ViewBag.year = new SelectList(dict2, "Key", "Value");

            return View(curriculumReportBySubj);
        }



        [Authorize(Roles = "Admin")]
        public ActionResult RemoveCurriculumFeedback()
        {
            int yr = 1994;

            Dictionary<int, string> dict = new Dictionary<int, string>();

            for (int i = 1; i <= 100; i++)
            {
                dict.Add(++yr, yr.ToString());
            }

            ViewBag.year = new SelectList(dict, "Key", "Value");

            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RemoveCurriculumFeedback(RemoveFeedback removeFeedback)
        {
            List<CurriculumFeedbacks> cfbyyear = new List<CurriculumFeedbacks>();



            cfbyyear = db.CurriculumFeedback.Where(a => a.EYear == removeFeedback.Year).ToList();

            if (ModelState.IsValid && cfbyyear.Count() > 0)
            {
                foreach (var item in cfbyyear)
                {
                    db.CurriculumFeedback.Remove(item);
                    db.SaveChanges();
                }

                int yr = 1994;

                Dictionary<int, string> dict = new Dictionary<int, string>();

                for (int i = 1; i <= 100; i++)
                {
                    dict.Add(++yr, yr.ToString());
                }

                ViewBag.year = new SelectList(dict, "Key", "Value");

                ViewBag.err = "Data Removed";

                return View();
            }
            else if (cfbyyear.Count() == 0)
            {
                int yr = 1994;

                Dictionary<int, string> dict = new Dictionary<int, string>();

                for (int i = 1; i <= 100; i++)
                {
                    dict.Add(++yr, yr.ToString());
                }

                ViewBag.year = new SelectList(dict, "Key", "Value");

                ViewBag.err = "No Data Exist According To Your Selection!!!";

                return View(removeFeedback);

            }

            int yr2 = 1994;

            Dictionary<int, string> dict2 = new Dictionary<int, string>();

            for (int i = 1; i <= 100; i++)
            {
                dict2.Add(++yr2, yr2.ToString());
            }

            ViewBag.year = new SelectList(dict2, "Key", "Value");

            ViewBag.err = "Error While Updating The Database!!!";

            return View(removeFeedback);
        }
    }
}