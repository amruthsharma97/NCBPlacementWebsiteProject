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


        [Authorize(Roles = "Student")]
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



        [Authorize(Roles = "Student")]
        // POST: AppraisalFeedback/Add
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


        [Authorize(Roles = "Admin")]
        public ActionResult AppraisalReportByDepartment()
        {
            List<Department> department = new List<Department>();

            department = db.Departments.ToList();

            List<SelectListItem> dlist = new List<SelectListItem>();

            dlist.Add(new SelectListItem() { Value = "All", Text = "All" });

            foreach (var dept in department)
            {
                dlist.Add(new SelectListItem() { Value = dept.DeptName, Text = dept.DeptName });
            }

            ViewBag.deptlist = dlist;

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
        public ActionResult AppraisalReportByDepartment(AppraisalReportByDept appraisalReportByDept)
        {
            List<Lecturers> lect = new List<Lecturers>();
            List<AppraisalFeedbacks> afbyyear = new List<AppraisalFeedbacks>();

            if (appraisalReportByDept.DeptName != "All")
            {
                lect = db.Lecturer.Where(d => d.DeptName == appraisalReportByDept.DeptName).ToList();
            }
            else
            {
                lect = db.Lecturer.ToList();
            }

            afbyyear=db.AppraisalFeedback.Where(a=>a.EYear==appraisalReportByDept.Year).ToList();

            if (ModelState.IsValid && afbyyear.Count() > 0 && lect.Count()>0)
            {
                FeedbackReport feedbackReport = new FeedbackReport();
                byte[] abyte = feedbackReport.PrepareReport(appraisalReportByDept);

                return File(abyte, "application/pdf");
            }
            else if(afbyyear.Count()==0||lect.Count==0)
            {
                List<Department> department = new List<Department>();

                department = db.Departments.ToList();

                List<SelectListItem> dlist = new List<SelectListItem>();

                dlist.Add(new SelectListItem() { Value = "All", Text = "All" });

                foreach (var dept in department)
                {
                    dlist.Add(new SelectListItem() { Value = dept.DeptName, Text = dept.DeptName });
                }

                ViewBag.deptlist = dlist;

                int yr = 1994;

                Dictionary<int, string> dict = new Dictionary<int, string>();

                for (int i = 1; i <= 100; i++)
                {
                    dict.Add(++yr, yr.ToString());
                }

                ViewBag.year = new SelectList(dict, "Key", "Value");

                ViewBag.err = "No Data Exist According To Your Selection!!!";

                return View(appraisalReportByDept);

            }

            List<Department> department2 = new List<Department>();

            department2 = db.Departments.ToList();

            List<SelectListItem> dlist2 = new List<SelectListItem>();

            dlist2.Add(new SelectListItem() { Value = "All", Text = "All" });

            foreach (var dept in department2)
            {
                dlist2.Add(new SelectListItem() { Value = dept.DeptName, Text = dept.DeptName });
            }

            ViewBag.deptlist = dlist2;

            int yr2 = 1994;

            Dictionary<int, string> dict2 = new Dictionary<int, string>();

            for (int i = 1; i <= 100; i++)
            {
                dict2.Add(++yr2, yr2.ToString());
            }

            ViewBag.year = new SelectList(dict2, "Key", "Value");

            return View(appraisalReportByDept);
            
        }


        [Authorize(Roles = "Admin")]
        public ActionResult AppraisalReportByLecturer()
        {
            List<Lecturers> lecturers = new List<Lecturers>();

            lecturers = db.Lecturer.ToList();

            List<SelectListItem> llist = new List<SelectListItem>();

            
            foreach (var lect in lecturers)
            {
                llist.Add(new SelectListItem() { Value = lect.LuctName, Text = lect.LuctName });
            }

            ViewBag.luctlist = llist;

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
        public ActionResult AppraisalReportByLecturer(AppraisalReportByLect appraisalReportByLect)
        {
            List<AppraisalFeedbacks> afbyyear = new List<AppraisalFeedbacks>();

            

            afbyyear = db.AppraisalFeedback.Where(a => a.EYear == appraisalReportByLect.Year).ToList();

            if (ModelState.IsValid && afbyyear.Count() > 0 )
            {
                FeedbackReport feedbackReport = new FeedbackReport();
                byte[] abyte = feedbackReport.PrepareReport(appraisalReportByLect);

                return File(abyte, "application/pdf");
            }
            else if (afbyyear.Count() == 0 )
            {
                List<Lecturers> lecturers = new List<Lecturers>();

                lecturers = db.Lecturer.ToList();

                List<SelectListItem> llist = new List<SelectListItem>();


                foreach (var lect in lecturers)
                {
                    llist.Add(new SelectListItem() { Value = lect.LuctName, Text = lect.LuctName });
                }

                ViewBag.luctlist = llist;

                int yr = 1994;

                Dictionary<int, string> dict = new Dictionary<int, string>();

                for (int i = 1; i <= 100; i++)
                {
                    dict.Add(++yr, yr.ToString());
                }

                ViewBag.year = new SelectList(dict, "Key", "Value");

                ViewBag.err = "No Data Exist According To Your Selection!!!";

                return View(appraisalReportByLect);

            }

            List<Lecturers> lecturers2 = new List<Lecturers>();

            lecturers2 = db.Lecturer.ToList();

            List<SelectListItem> llist2 = new List<SelectListItem>();


            foreach (var lect in lecturers2)
            {
                llist2.Add(new SelectListItem() { Value = lect.LuctName, Text = lect.LuctName });
            }

            ViewBag.luctlist = llist2;

            int yr2 = 1994;

            Dictionary<int, string> dict2 = new Dictionary<int, string>();

            for (int i = 1; i <= 100; i++)
            {
                dict2.Add(++yr2, yr2.ToString());
            }

            ViewBag.year = new SelectList(dict2, "Key", "Value");

            return View(appraisalReportByLect);

        }
        
        
    }
}
