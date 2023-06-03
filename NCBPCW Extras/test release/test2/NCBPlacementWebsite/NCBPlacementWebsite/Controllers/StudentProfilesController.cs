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
using PagedList;
using PagedList.Mvc;


namespace NCBPlacementWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        // GET: StudentProfiles
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


        public ActionResult Index(int? page,string branch)
        {
            var studlist = db.StudentProfiles.ToList();

            var studbranlist = new List<StudentProfile>();

            if (branch == null)
            {
                branch = "ALL";
            }

            if (branch == "ALL" )
            {
                foreach (var item in studlist)
                {
                        studbranlist.Add(item);
                }
            }
            else
            {
                foreach (var item in studlist)
                {
                    if (item.Branch == branch)
                    {
                        studbranlist.Add(item);
                    }
                }
            }

            List<StudentProfile> orderbystatus = new List<StudentProfile>();

            orderbystatus=studbranlist.OrderBy(s => s.Status).ToList();

            //var orderbystatus = from s in studbranlist orderby s.Status select s;


            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "ALL", Text = "ALL" });
            list.Add(new SelectListItem() { Value = "BCA", Text = "BCA" });
            list.Add(new SelectListItem() { Value = "BA", Text = "BA" });
            list.Add(new SelectListItem() { Value = "BSC-CBZ", Text = "BSC-CBZ" });
            list.Add(new SelectListItem() { Value = "BSC-PMCS", Text = "BSC-PMCS" });
            list.Add(new SelectListItem() { Value = "BSC-PME", Text = "BSC-PME" });
            list.Add(new SelectListItem() { Value = "BCOM", Text = "BCOM" });

            ViewBag.Branches = list;

            return View(orderbystatus.ToPagedList(page ?? 1, 10));
        }


        


        // GET: StudentProfiles/Details/5
        public ActionResult Details(Guid? id)
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


        public ActionResult Approve(Guid? id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "BCA", Text = "BCA" });
            list.Add(new SelectListItem() { Value = "BA", Text = "BA" });
            list.Add(new SelectListItem() { Value = "BSC-CBZ", Text = "BSC-CBZ" });
            list.Add(new SelectListItem() { Value = "BSC-PMCS", Text = "BSC-PMCS" });
            list.Add(new SelectListItem() { Value = "BSC-PME", Text = "BSC-PME" });
            list.Add(new SelectListItem() { Value = "BCOM", Text = "BCOM" });

            ViewBag.Branches = list;

            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(4, "Approve");
            dictionary.Add(3, "Data Did Not Match!!!");
    

            ViewBag.st = new SelectList(dictionary, "Key", "Value");

            int yop = 1994;
            Dictionary<int, string> dictionary2 = new Dictionary<int, string>();

            for (int i = 0; i < 100; i++)
            {
                dictionary2.Add(++yop, yop.ToString());
            }

            ViewBag.allyop = new SelectList(dictionary2, "Key", "Value");

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


        [HttpPost]
        public ActionResult Approve(StudentProfile studentProfile)
        {
            if (ModelState.IsValid)
            {
                if (studentProfile.SSLCYOP >= studentProfile.PUCYOP || studentProfile.SSLCYOP >= studentProfile.DegreeYOP || studentProfile.SSLCYOP >= studentProfile.PUCYOP && studentProfile.SSLCYOP >= studentProfile.DegreeYOP || studentProfile.PUCYOP - studentProfile.SSLCYOP < 2)
                {
                    ViewBag.errmsg = "SSLC or 10th Year Of Passing is Greater than PUC Year Of Passing Or Degree Year Of Passing Or Difference Between SSLC and PUC is Less Than 2!!";
                }
                else if (studentProfile.PUCYOP >= studentProfile.DegreeYOP || studentProfile.DegreeYOP - studentProfile.PUCYOP < 3)
                {
                    ViewBag.errmsg2 = "PUC Year Of Passing is Greater than Degree Year Of Passing Or Difference Between PUC and Degree is Less Than 3!!";
                }
                else
                {
                    db.Entry(studentProfile).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
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
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(4, "Approve");
            dictionary.Add(3, "Data Did Not Match!!!");


            ViewBag.st = new SelectList(dictionary, "Key", "Value");

            int yop = 1994;
            Dictionary<int, string> dictionary2 = new Dictionary<int, string>();

            for (int i = 0; i < 100; i++)
            {
                dictionary2.Add(++yop, yop.ToString());
            }

            ViewBag.allyop = new SelectList(dictionary2, "Key", "Value");

            return View(studentProfile);
        }

        [AllowAnonymous]
        // GET: StudentProfiles/Create
        public ActionResult Create(ApplicationUser user)
        {
            StudentProfile studentProfile = new StudentProfile();

            studentProfile.EmailId = user.Email;
            studentProfile.MNumber = user.PhoneNumber;


            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() {Value="BCA",Text="BCA"} );
            list.Add(new SelectListItem() {Value="BA",Text="BA"} );
            list.Add(new SelectListItem() {Value="BSC-CBZ",Text="BSC-CBZ"} );
            list.Add(new SelectListItem() {Value="BSC-PMCS",Text="BSC-PMCS"} );
            list.Add(new SelectListItem() {Value="BSC-PME",Text="BSC-PME"} );
            list.Add(new SelectListItem() {Value="BCOM",Text="BCOM"} );

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

        // POST: StudentProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create(StudentProfile studentProfile)
        {
            
            if (ModelState.IsValid)
            {
                if (studentProfile.SSLCYOP >= studentProfile.PUCYOP || studentProfile.SSLCYOP >= studentProfile.DegreeYOP || studentProfile.SSLCYOP >= studentProfile.PUCYOP && studentProfile.SSLCYOP >= studentProfile.DegreeYOP || studentProfile.PUCYOP - studentProfile.SSLCYOP < 2)
                {
                    ViewBag.errmsg = "SSLC or 10th Year Of Passing is Greater than PUC Year Of Passing Or Degree Year Of Passing Or Difference Between SSLC and PUC is Less Than 2!!";
                }
                else if (studentProfile.PUCYOP >= studentProfile.DegreeYOP || studentProfile.DegreeYOP - studentProfile.PUCYOP < 3)
                {
                    ViewBag.errmsg2 = "PUC Year Of Passing is Greater than Degree Year Of Passing Or Difference Between PUC and Degree is Less Than 3!!";
                }
                else
                {
                    studentProfile.Status = 1;
                    db.StudentProfiles.Add(studentProfile);
                    db.SaveChanges();
                    //return RedirectToAction("Index","Home");
                    return RedirectToAction("Login", "Account");

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
                if (studentProfile.SSLCYOP >= studentProfile.PUCYOP || studentProfile.SSLCYOP >= studentProfile.DegreeYOP || studentProfile.SSLCYOP >= studentProfile.PUCYOP && studentProfile.SSLCYOP >= studentProfile.DegreeYOP || studentProfile.PUCYOP - studentProfile.SSLCYOP < 2)
                {
                    ViewBag.errmsg = "SSLC or 10th Year Of Passing is Greater than PUC Year Of Passing Or Degree Year Of Passing Or Difference Between SSLC and PUC is Less Than 2!!";
                }
                else if (studentProfile.PUCYOP >= studentProfile.DegreeYOP || studentProfile.DegreeYOP - studentProfile.PUCYOP < 3)
                {
                    ViewBag.errmsg2 = "PUC Year Of Passing is Greater than Degree Year Of Passing Or Difference Between PUC and Degree is Less Than 3!!";
                }
                else
                {
                    studentProfile.Status = 2;
                    db.Entry(studentProfile).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
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

        // GET: StudentProfiles/Delete/5
        public ActionResult Delete(Guid? id)
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

        // POST: StudentProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            var user = UserManager.FindByEmail(studentProfile.EmailId);
            var dresult = UserManager.Delete(user);
            
            db.StudentProfiles.Remove(studentProfile);
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

        public ActionResult NoData()
        {
            return View();
        }

        public ActionResult CreateExcel()
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
            return View();
        }

        [HttpPost]
        public void CreateExcel(int? yop,string branch,float? min10perc, float? minpuperc )
        {
            List<StudentProfile> studentProfile = new List<StudentProfile>();
            List<StudentProfile> ExcelSP = new List<StudentProfile>();

            studentProfile=db.StudentProfiles.Where(x=>x.DegreeYOP == yop).ToList();

            foreach (var item in studentProfile)
            {
                if (item.Branch == branch && item.SSLCPercentage >= min10perc && item.PUCPercentage >= minpuperc)
                {
                    ExcelSP.Add(item);
                }
            }

            if (ExcelSP.Count!=0)
            {
                ExcelPackage pak = new ExcelPackage();
                ExcelWorksheet ws = pak.Workbook.Worksheets.Add("StudentDetails");


                ws.Cells["A1"].Value = "Sr. No.";
                ws.Cells["B1"].Value = "Aadhar Number";
                ws.Cells["C1"].Value = "University Register Number";
                ws.Cells["D1"].Value = "First Name";
                ws.Cells["E1"].Value = "Middle Name";
                ws.Cells["F1"].Value = "Last Name";
                ws.Cells["G1"].Value = "Mobile Number";
                ws.Cells["H1"].Value = "Email ID";
                ws.Cells["I1"].Value = "Date Of Birth";
                ws.Cells["J1"].Value = "Address Line 1";
                ws.Cells["K1"].Value = "Address Line 2";
                ws.Cells["L1"].Value = "City";
                ws.Cells["M1"].Value = "Pin Code";
                ws.Cells["N1"].Value = "State";
                ws.Cells["O1"].Value = "Country";
                ws.Cells["P1"].Value = "SSLC or 10th Percentage";
                ws.Cells["Q1"].Value = "SSLC or 10th Board";
                ws.Cells["R1"].Value = "Name Of the School";
                ws.Cells["S1"].Value = "Graduating State";
                ws.Cells["T1"].Value = "Graduating Country";
                ws.Cells["U1"].Value = "Year Of Passing";
                ws.Cells["V1"].Value = "PUC or 12th Percentage";
                ws.Cells["W1"].Value = "PUC or 12th Board";
                ws.Cells["X1"].Value = "Name Of the College";
                ws.Cells["Y1"].Value = "Graduating State";
                ws.Cells["Z1"].Value = "Graduating Country";
                ws.Cells["AA1"].Value = "Year Of Passing";
                ws.Cells["AB1"].Value = "Degree Percentage";
                ws.Cells["AC1"].Value = "Branch";
                ws.Cells["AD1"].Value = "University";
                ws.Cells["AE1"].Value = "Name Of the College";
                ws.Cells["AF1"].Value = "Graduating State";
                ws.Cells["AG1"].Value = "Graduating Country";
                ws.Cells["AH1"].Value = "Year Of Passing";

                int rowStart = 2;
                int srno = 0;
                foreach (var item in ExcelSP)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = ++srno;
                    ws.Cells[string.Format("B{0}", rowStart)].Style.Numberformat.Format="0";
                    ws.Cells[string.Format("B{0}", rowStart)].Value = item.Aadnum;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = item.URNo;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = item.FName;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = item.MName;
                    ws.Cells[string.Format("F{0}", rowStart)].Value = item.LName;
                    ws.Cells[string.Format("G{0}", rowStart)].Value = item.MNumber;
                    ws.Cells[string.Format("H{0}", rowStart)].Value = item.EmailId;
                    ws.Cells[string.Format("I{0}", rowStart)].Style.Numberformat.Format = "dd-mm-yyyy";
                    ws.Cells[string.Format("J{0}", rowStart)].Value = item.DOB;
                    ws.Cells[string.Format("K{0}", rowStart)].Value = item.AddrLine1;
                    ws.Cells[string.Format("L{0}", rowStart)].Value = item.AddrLine2;
                    ws.Cells[string.Format("M{0}", rowStart)].Value = item.City;
                    ws.Cells[string.Format("N{0}", rowStart)].Value = item.PostalCode;
                    ws.Cells[string.Format("O{0}", rowStart)].Value = item.State;
                    ws.Cells[string.Format("P{0}", rowStart)].Value = item.Country;
                    ws.Cells[string.Format("Q{0}", rowStart)].Value = item.SSLCPercentage;
                    ws.Cells[string.Format("R{0}", rowStart)].Value = item.SSLCBoard;
                    ws.Cells[string.Format("S{0}", rowStart)].Value = item.NameOfSchool;
                    ws.Cells[string.Format("T{0}", rowStart)].Value = item.SSLCPassingState;
                    ws.Cells[string.Format("U{0}", rowStart)].Value = item.SSLCPassingCOuntry;
                    ws.Cells[string.Format("V{0}", rowStart)].Value = item.SSLCYOP;
                    ws.Cells[string.Format("W{0}", rowStart)].Value = item.PUCPercentage;
                    ws.Cells[string.Format("X{0}", rowStart)].Value = item.PUCBoard;
                    ws.Cells[string.Format("Y{0}", rowStart)].Value = item.NameOfClg;
                    ws.Cells[string.Format("Z{0}", rowStart)].Value = item.PUCPassingState;
                    ws.Cells[string.Format("AA{0}", rowStart)].Value = item.PUCPassingCOuntry;
                    ws.Cells[string.Format("AB{0}", rowStart)].Value = item.PUCYOP;
                    ws.Cells[string.Format("AC{0}", rowStart)].Value = item.DegreePercentage;
                    ws.Cells[string.Format("AD{0}", rowStart)].Value = item.Branch;
                    ws.Cells[string.Format("AE{0}", rowStart)].Value = "Bangalore University";
                    ws.Cells[string.Format("AF{0}", rowStart)].Value = "The National College, Basavanagudi";
                    ws.Cells[string.Format("AG{0}", rowStart)].Value = "India";
                    ws.Cells[string.Format("AH{0}", rowStart)].Value = item.DegreeYOP;

                    rowStart++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();
                Response.Clear();
                //Response.ClearHeaders();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + "StudentDetails.xlsx");
                Response.BinaryWrite(pak.GetAsByteArray());
                Response.End();

                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem() { Value = "BCA", Text = "BCA" });
                list.Add(new SelectListItem() { Value = "BA", Text = "BA" });
                list.Add(new SelectListItem() { Value = "BSC-CBZ", Text = "BSC-CBZ" });
                list.Add(new SelectListItem() { Value = "BSC-PMCS", Text = "BSC-PMCS" });
                list.Add(new SelectListItem() { Value = "BSC-PME", Text = "BSC-PME" });
                list.Add(new SelectListItem() { Value = "BCOM", Text = "BCOM" });

                ViewBag.Branches = list;
                int syop = 1994;
                Dictionary<int, string> dictionary = new Dictionary<int, string>();

                for (int i = 0; i < 100; i++)
                {
                    dictionary.Add(++syop, syop.ToString());
                }

                ViewBag.allyop = new SelectList(dictionary, "Key", "Value");

                
            }
            else
            {
                

                Response.Redirect(Url.Action("NoData","StudentProfiles"));
                
            }
            

            
        }
    }
}
