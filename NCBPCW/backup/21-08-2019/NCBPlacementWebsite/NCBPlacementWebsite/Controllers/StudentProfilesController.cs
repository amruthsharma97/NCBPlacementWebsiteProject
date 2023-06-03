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

namespace NCBPlacementWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentProfiles
        
        public ActionResult Index()
        {
            return View(db.StudentProfiles.ToList());
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
            dictionary.Add(1, "Approve");
            dictionary.Add(2, "Data Did Not Match!!!");
    

            ViewBag.st = new SelectList(dictionary, "Key", "Value");
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
                db.Entry(studentProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            dictionary.Add(1, "Approve");
            dictionary.Add(2, "Data Did Not Match!!!");


            ViewBag.st = new SelectList(dictionary, "Key", "Value");
            return View(studentProfile);
        }

        [AllowAnonymous]
        // GET: StudentProfiles/Create
        public ActionResult Create()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() {Value="BCA",Text="BCA"} );
            list.Add(new SelectListItem() {Value="BA",Text="BA"} );
            list.Add(new SelectListItem() {Value="BSC-CBZ",Text="BSC-CBZ"} );
            list.Add(new SelectListItem() {Value="BSC-PMCS",Text="BSC-PMCS"} );
            list.Add(new SelectListItem() {Value="BSC-PME",Text="BSC-PME"} );
            list.Add(new SelectListItem() {Value="BCOM",Text="BCOM"} );

            ViewBag.Branches = list;
            return View();
        }

        // POST: StudentProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentProfile studentProfile)
        {
            if (ModelState.IsValid)
            {
                studentProfile.Status = 0;
                db.StudentProfiles.Add(studentProfile);
                db.SaveChanges();
                //return RedirectToAction("Index","Home");
                return RedirectToAction("Login", "Account");
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
                return RedirectToAction("Index");
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
            return View();
        }

        [HttpPost]
        public ActionResult CreateExcel(int yop,string branch,float min10perc, float minpuperc )
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

            ExcelPackage pak = new ExcelPackage();
            ExcelWorksheet ws = pak.Workbook.Worksheets.Add("StudentDetails");

            
            ws.Cells["A1"].Value = "Sr. No.";
            ws.Cells["B1"].Value = "University Register Number";
            ws.Cells["C1"].Value = "First Name";
            ws.Cells["D1"].Value = "Middle Name";
            ws.Cells["E1"].Value = "Last Name";
            ws.Cells["F1"].Value = "Mobile Number";
            ws.Cells["G1"].Value = "Email ID";
            ws.Cells["H1"].Value = "Date Of Birth";
            ws.Cells["I1"].Value = "Address Line 1";
            ws.Cells["J1"].Value = "Address Line 2";
            ws.Cells["K1"].Value = "City";
            ws.Cells["L1"].Value = "Pin Code";
            ws.Cells["M1"].Value = "State";
            ws.Cells["N1"].Value = "Country";
            ws.Cells["O1"].Value = "SSLC or 10th Percentage";
            ws.Cells["P1"].Value = "SSLC or 10th Board";
            ws.Cells["Q1"].Value = "Name Of the School";
            ws.Cells["R1"].Value = "Graduating State";
            ws.Cells["S1"].Value = "Graduating Country";
            ws.Cells["T1"].Value = "Year Of Passing";
            ws.Cells["U1"].Value = "PUC or 12th Percentage";
            ws.Cells["V1"].Value = "PUC or 12th Board";
            ws.Cells["W1"].Value = "Name Of the College";
            ws.Cells["X1"].Value = "Graduating State";
            ws.Cells["Y1"].Value = "Graduating Country";
            ws.Cells["Z1"].Value = "Year Of Passing";
            ws.Cells["AA1"].Value = "Degree Percentage";
            ws.Cells["AB1"].Value = "Branch";
            ws.Cells["AC1"].Value = "University";
            ws.Cells["AD1"].Value = "Name Of the College";
            ws.Cells["AE1"].Value = "Graduating State";
            ws.Cells["AF1"].Value = "Graduating Country";
            ws.Cells["AG1"].Value = "Year Of Passing";

            int rowStart = 2;
            int srno=0;
            foreach (var item in ExcelSP)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = ++srno;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.URNo;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.FName;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.MName;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.LName;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.MNumber;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.EmailId;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Numberformat.Format = "dd-mm-yyyy";
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.DOB;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.AddrLine1;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.AddrLine2;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.City;
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.PostalCode;
                ws.Cells[string.Format("M{0}", rowStart)].Value = item.State;
                ws.Cells[string.Format("N{0}", rowStart)].Value = item.Country;
                ws.Cells[string.Format("O{0}", rowStart)].Value = item.SSLCPercentage;
                ws.Cells[string.Format("P{0}", rowStart)].Value = item.SSLCBoard;
                ws.Cells[string.Format("Q{0}", rowStart)].Value = item.NameOfSchool;
                ws.Cells[string.Format("R{0}", rowStart)].Value = item.SSLCPassingState;
                ws.Cells[string.Format("S{0}", rowStart)].Value = item.SSLCPassingCOuntry;
                ws.Cells[string.Format("T{0}", rowStart)].Value = item.SSLCYOP;
                ws.Cells[string.Format("U{0}", rowStart)].Value = item.PUCPercentage;
                ws.Cells[string.Format("V{0}", rowStart)].Value = item.PUCBoard;
                ws.Cells[string.Format("W{0}", rowStart)].Value = item.NameOfClg;
                ws.Cells[string.Format("X{0}", rowStart)].Value = item.PUCPassingState;
                ws.Cells[string.Format("Y{0}", rowStart)].Value = item.PUCPassingCOuntry;
                ws.Cells[string.Format("Z{0}", rowStart)].Value = item.PUCYOP;
                ws.Cells[string.Format("AA{0}", rowStart)].Value = item.DegreePercentage;
                ws.Cells[string.Format("AB{0}", rowStart)].Value = item.Branch;
                ws.Cells[string.Format("AC{0}", rowStart)].Value = "Bangalore University";
                ws.Cells[string.Format("AD{0}", rowStart)].Value = "The National College, Basavanagudi";
                ws.Cells[string.Format("AE{0}", rowStart)].Value = "Karnataka";
                ws.Cells[string.Format("AF{0}", rowStart)].Value = "India";
                ws.Cells[string.Format("AG{0}", rowStart)].Value = item.DegreeYOP;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "StudentDetails.xlsx");
            Response.BinaryWrite(pak.GetAsByteArray());
            Response.End();

            return RedirectToAction("Index");
        }
    }
}
