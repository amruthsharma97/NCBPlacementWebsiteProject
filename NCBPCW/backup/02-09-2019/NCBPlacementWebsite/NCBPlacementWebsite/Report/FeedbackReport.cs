using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NCBPlacementWebsite.Models;

namespace NCBPlacementWebsite.Report
{
    public class FeedbackReport
    {
        #region Decleration
        ApplicationDbContext db = new ApplicationDbContext();
        int _totalColumn = 1;
        Document _document;
        Font _fontStyle;
        
        
        MemoryStream _memoryStream = new MemoryStream();

        #endregion


        public byte[] PrepareReport(AppraisalReportByDept appraisalReportByDept)
        {
            #region

            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4.Rotate());
            _document.SetMargins(20f, 20f, 20f, 20f);
            
            
            _fontStyle = FontFactory.GetFont("Times", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();
            
            
            #endregion
                       

            this.ReportBody(appraisalReportByDept);

            _document.Close();
            return _memoryStream.ToArray();
        }


        public byte[] PrepareReport(AppraisalReportByLect appraisalReportByLect)
        {
            #region

            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4.Rotate());
            _document.SetMargins(20f, 20f, 20f, 20f);


            _fontStyle = FontFactory.GetFont("Times", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();


            #endregion


            this.ReportBody(appraisalReportByLect);

            _document.Close();
            return _memoryStream.ToArray();
        }


        public byte[] PrepareReport(CurriculumReportByCrse curriculumReportByCrse)
        {
            #region

            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4.Rotate());
            _document.SetMargins(20f, 20f, 20f, 20f);


            _fontStyle = FontFactory.GetFont("Times", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();


            #endregion


            this.ReportBody(curriculumReportByCrse);

            _document.Close();
            return _memoryStream.ToArray();
        }


        public byte[] PrepareReport(CurriculumReportBySubj curriculumReportBySubj)
        {
            #region

            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4.Rotate());
            _document.SetMargins(20f, 20f, 20f, 20f);


            _fontStyle = FontFactory.GetFont("Times", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();


            #endregion


            this.ReportBody(curriculumReportBySubj);

            _document.Close();
            return _memoryStream.ToArray();
        }

        public void ReportHeader()
        {
            PdfPTable _pdfPTable = new PdfPTable(2);

            PdfPCell _pdfPCell;

            _pdfPTable.SetWidths(new float[] { 10f,90f });

            _pdfPTable.WidthPercentage = 100;
            _pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;

            
            string imgurl = HttpContext.Current.Server.MapPath("~/1459933172national-colllege-basavanagudi_logo.png");
            Image png = Image.GetInstance(imgurl);

            png.ScaleToFit(52.325f, 59.8f);
            png.Alignment = Image.TEXTWRAP;
            //png.SetAbsolutePosition(_document.Left, _document.Top);



            _pdfPCell = new PdfPCell(png);
            _pdfPCell.Rowspan = 3;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);

           

            _fontStyle = FontFactory.GetFont("Times", 25f, 1);
            _pdfPCell = new PdfPCell(new Phrase("THE NATIONAL COLLEGE", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();


            _fontStyle = FontFactory.GetFont("Times", 13f, 1);
            _pdfPCell = new PdfPCell(new Phrase("AUTONOMOUS", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Basavanagudi, Bangalore - 04.", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();

            _document.Add(_pdfPTable);

            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));

            _document.Add(p);

            
            

        }




        public void ReportBody(AppraisalReportByDept appraisalReportByDept)
        {
            List<AppraisalFeedbacks> appfeed = new List<AppraisalFeedbacks>();
            List<Lecturers> lect = new List<Lecturers>();

            List<AppraisalFeedbacks> afbyyear = new List<AppraisalFeedbacks>();

            afbyyear=db.AppraisalFeedback.Where(a=>a.EYear==appraisalReportByDept.Year).ToList();

            if (appraisalReportByDept.DeptName != "All")
            {
                lect = db.Lecturer.Where(d => d.DeptName == appraisalReportByDept.DeptName).ToList();
            }
            else
            {
                lect = db.Lecturer.ToList();
            }

            

            List<string> lname =new List<string>();


            foreach (var item in lect)
            {
                lname.Add(item.LuctName);
            }

            foreach (var name in lname)
            {
                //appfeed = db.AppraisalFeedback.Where(s => s.LuctName == name).ToList();
                appfeed = afbyyear.Where(s => s.LuctName == name).ToList();

                int totalcount = appfeed.Count();

                this.ReportHeader();

                Font phfs = new Font();
                phfs = FontFactory.GetFont("Times", 16f, 1);

                Paragraph phas = new Paragraph("Appraisal By Students", phfs);
                phas.Alignment = Element.ALIGN_CENTER;
                phas.SpacingAfter = 10f;
                _document.Add(phas);

                PdfPTable _ptable = new PdfPTable(12);
                PdfPCell _pcell;
                _ptable.WidthPercentage = 100;
                _ptable.HorizontalAlignment = Element.ALIGN_LEFT;
                _ptable.SetWidths(new float[] { 15f, 90f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f });

                Font ln = new Font();
                ln = FontFactory.GetFont("Times", 16f, 1);

                Paragraph ph = new Paragraph("Lecturer Name: " + name, ln);
                ph.Alignment = Element.ALIGN_LEFT;
                ph.SpacingAfter = 10f;
                _document.Add(ph);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Sl. No.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Criteria \\ Rating", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("1", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("2", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("3", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("4", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("5", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("6", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("7", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("8", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("9", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("10", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();

                //criteria 1
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("1.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Regularity in conducting the classes", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q11cnt = appfeed.Where(q => q.Q1 == 1).Count();
                float q11avg = (float)q11cnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q11avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q12cnt = appfeed.Where(q => q.Q1 == 2).Count();
                float q12avg = ((float)q12cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q12avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q13cnt = appfeed.Where(q => q.Q1 == 3).Count();
                float q13avg = ((float)q13cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q13avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q14cnt = appfeed.Where(q => q.Q1 == 4).Count();
                float q14avg = ((float)q14cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q14avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q15cnt = appfeed.Where(q => q.Q1 == 5).Count();
                float q15avg = ((float)q15cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q15avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q16cnt = appfeed.Where(q => q.Q1 == 6).Count();
                float q16avg = ((float)q16cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q16avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q17cnt = appfeed.Where(q => q.Q1 == 7).Count();
                float q17avg = ((float)q17cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q17avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q18cnt = appfeed.Where(q => q.Q1 == 8).Count();
                float q18avg = ((float)q18cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q18avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q19cnt = appfeed.Where(q => q.Q1 == 9).Count();
                float q19avg = ((float)q19cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q19avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q110cnt = appfeed.Where(q => q.Q1 == 10).Count();
                float q110avg = ((float)q110cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q110avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();





                //criteria 2
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("2.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Punctuality", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q21cnt = appfeed.Where(q => q.Q2 == 1).Count();
                float q21avg = (float)q21cnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q21avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q22cnt = appfeed.Where(q => q.Q2 == 2).Count();
                float q22avg = ((float)q22cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q22avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q23cnt = appfeed.Where(q => q.Q2 == 3).Count();
                float q23avg = ((float)q23cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q23avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q24cnt = appfeed.Where(q => q.Q2 == 4).Count();
                float q24avg = ((float)q24cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q24avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q25cnt = appfeed.Where(q => q.Q2 == 5).Count();
                float q25avg = ((float)q25cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q25avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q26cnt = appfeed.Where(q => q.Q2 == 6).Count();
                float q26avg = ((float)q26cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q26avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q27cnt = appfeed.Where(q => q.Q2 == 7).Count();
                float q27avg = ((float)q27cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q27avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q28cnt = appfeed.Where(q => q.Q2 == 8).Count();
                float q28avg = ((float)q28cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q28avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q29cnt = appfeed.Where(q => q.Q2 == 9).Count();
                float q29avg = ((float)q29cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q29avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q210cnt = appfeed.Where(q => q.Q2 == 10).Count();
                float q210avg = ((float)q210cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q210avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();







                //criteria 3
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("3.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Preparation for the classes", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q31cnt = appfeed.Where(q => q.Q3 == 1).Count();
                float q31avg = (float)q31cnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q31avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q32cnt = appfeed.Where(q => q.Q3 == 2).Count();
                float q32avg = ((float)q32cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q32avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q33cnt = appfeed.Where(q => q.Q3 == 3).Count();
                float q33avg = ((float)q33cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q33avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q34cnt = appfeed.Where(q => q.Q3 == 4).Count();
                float q34avg = ((float)q34cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q34avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q35cnt = appfeed.Where(q => q.Q3 == 5).Count();
                float q35avg = ((float)q35cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q35avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q36cnt = appfeed.Where(q => q.Q3 == 6).Count();
                float q36avg = ((float)q36cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q36avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q37cnt = appfeed.Where(q => q.Q3 == 7).Count();
                float q37avg = ((float)q37cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q37avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q38cnt = appfeed.Where(q => q.Q3 == 8).Count();
                float q38avg = ((float)q38cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q38avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q39cnt = appfeed.Where(q => q.Q3 == 9).Count();
                float q39avg = ((float)q39cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q39avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q310cnt = appfeed.Where(q => q.Q3 == 10).Count();
                float q310avg = ((float)q310cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q310avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();







                //criteria 4
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("4.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Completion of syllabus on time", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q41cnt = appfeed.Where(q => q.Q4 == 1).Count();
                float q41avg = (float)q41cnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q41avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q42cnt = appfeed.Where(q => q.Q4 == 2).Count();
                float q42avg = ((float)q42cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q42avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q43cnt = appfeed.Where(q => q.Q4 == 3).Count();
                float q43avg = ((float)q43cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q43avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q44cnt = appfeed.Where(q => q.Q4 == 4).Count();
                float q44avg = ((float)q44cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q44avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q45cnt = appfeed.Where(q => q.Q4 == 5).Count();
                float q45avg = ((float)q45cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q45avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q46cnt = appfeed.Where(q => q.Q4 == 6).Count();
                float q46avg = ((float)q46cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q46avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q47cnt = appfeed.Where(q => q.Q4 == 7).Count();
                float q47avg = ((float)q47cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q47avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q48cnt = appfeed.Where(q => q.Q4 == 8).Count();
                float q48avg = ((float)q48cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q48avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q49cnt = appfeed.Where(q => q.Q4 == 9).Count();
                float q49avg = ((float)q49cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q49avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q410cnt = appfeed.Where(q => q.Q4 == 10).Count();
                float q410avg = ((float)q410cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q410avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();







                //criteria 5
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("5.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Competency to handle to the subject", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q51cnt = appfeed.Where(q => q.Q5 == 1).Count();
                float q51avg = (float)q51cnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q51avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q52cnt = appfeed.Where(q => q.Q5 == 2).Count();
                float q52avg = ((float)q52cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q52avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q53cnt = appfeed.Where(q => q.Q5 == 3).Count();
                float q53avg = ((float)q53cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q53avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q54cnt = appfeed.Where(q => q.Q5 == 4).Count();
                float q54avg = ((float)q54cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q54avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q55cnt = appfeed.Where(q => q.Q5 == 5).Count();
                float q55avg = ((float)q55cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q55avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q56cnt = appfeed.Where(q => q.Q5 == 6).Count();
                float q56avg = ((float)q56cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q56avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q57cnt = appfeed.Where(q => q.Q5 == 7).Count();
                float q57avg = ((float)q57cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q57avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q58cnt = appfeed.Where(q => q.Q5 == 8).Count();
                float q58avg = ((float)q58cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q58avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q59cnt = appfeed.Where(q => q.Q5 == 9).Count();
                float q59avg = ((float)q59cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q59avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q510cnt = appfeed.Where(q => q.Q5 == 10).Count();
                float q510avg = ((float)q510cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q510avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();





                //criteria 6
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("6.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Presentation skills like Voice, clarity and language", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q61cnt = appfeed.Where(q => q.Q6 == 1).Count();
                float q61avg = (float)q61cnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q61avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q62cnt = appfeed.Where(q => q.Q6 == 2).Count();
                float q62avg = ((float)q62cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q62avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q63cnt = appfeed.Where(q => q.Q6 == 3).Count();
                float q63avg = ((float)q63cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q63avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q64cnt = appfeed.Where(q => q.Q6 == 4).Count();
                float q64avg = ((float)q64cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q64avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q65cnt = appfeed.Where(q => q.Q6 == 5).Count();
                float q65avg = ((float)q65cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q65avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q66cnt = appfeed.Where(q => q.Q6 == 6).Count();
                float q66avg = ((float)q66cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q66avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q67cnt = appfeed.Where(q => q.Q6 == 7).Count();
                float q67avg = ((float)q67cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q67avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q68cnt = appfeed.Where(q => q.Q6 == 8).Count();
                float q68avg = ((float)q68cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q68avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q69cnt = appfeed.Where(q => q.Q6 == 9).Count();
                float q69avg = ((float)q69cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q69avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q610cnt = appfeed.Where(q => q.Q6 == 10).Count();
                float q610avg = ((float)q610cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q610avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();






                //criteria 7
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("7.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Methodology used to import the knowledge", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q71cnt = appfeed.Where(q => q.Q7 == 1).Count();
                float q71avg = (float)q71cnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q71avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q72cnt = appfeed.Where(q => q.Q7 == 2).Count();
                float q72avg = ((float)q72cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q72avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q73cnt = appfeed.Where(q => q.Q7 == 3).Count();
                float q73avg = ((float)q73cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q73avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q74cnt = appfeed.Where(q => q.Q7 == 4).Count();
                float q74avg = ((float)q74cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q74avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q75cnt = appfeed.Where(q => q.Q7 == 5).Count();
                float q75avg = ((float)q75cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q75avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q76cnt = appfeed.Where(q => q.Q7 == 6).Count();
                float q76avg = ((float)q76cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q76avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q77cnt = appfeed.Where(q => q.Q7 == 7).Count();
                float q77avg = ((float)q77cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q77avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q78cnt = appfeed.Where(q => q.Q7 == 8).Count();
                float q78avg = ((float)q78cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q78avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q79cnt = appfeed.Where(q => q.Q7 == 9).Count();
                float q79avg = ((float)q79cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q79avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q710cnt = appfeed.Where(q => q.Q7 == 10).Count();
                float q710avg = ((float)q710cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q710avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();







                //criteria 8
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("8.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Interaction with the students", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q81cnt = appfeed.Where(q => q.Q8 == 1).Count();
                float q81avg = (float)q81cnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q81avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q82cnt = appfeed.Where(q => q.Q8 == 2).Count();
                float q82avg = ((float)q82cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q82avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q83cnt = appfeed.Where(q => q.Q8 == 3).Count();
                float q83avg = ((float)q83cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q83avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q84cnt = appfeed.Where(q => q.Q8 == 4).Count();
                float q84avg = ((float)q84cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q84avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q85cnt = appfeed.Where(q => q.Q8 == 5).Count();
                float q85avg = ((float)q85cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q85avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q86cnt = appfeed.Where(q => q.Q8 == 6).Count();
                float q86avg = ((float)q86cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q86avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q87cnt = appfeed.Where(q => q.Q8 == 7).Count();
                float q87avg = ((float)q87cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q87avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q88cnt = appfeed.Where(q => q.Q8 == 8).Count();
                float q88avg = ((float)q88cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q88avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q89cnt = appfeed.Where(q => q.Q8 == 9).Count();
                float q89avg = ((float)q89cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q89avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q810cnt = appfeed.Where(q => q.Q8 == 10).Count();
                float q810avg = ((float)q810cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q810avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();







                //criteria 9
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("9.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Accessibility", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q91cnt = appfeed.Where(q => q.Q9 == 1).Count();
                float q91avg = (float)q91cnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q91avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q92cnt = appfeed.Where(q => q.Q9 == 2).Count();
                float q92avg = ((float)q92cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q92avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q93cnt = appfeed.Where(q => q.Q9 == 3).Count();
                float q93avg = ((float)q93cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q93avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q94cnt = appfeed.Where(q => q.Q9 == 4).Count();
                float q94avg = ((float)q94cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q94avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q95cnt = appfeed.Where(q => q.Q9 == 5).Count();
                float q95avg = ((float)q95cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q95avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q96cnt = appfeed.Where(q => q.Q9 == 6).Count();
                float q96avg = ((float)q96cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q96avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q97cnt = appfeed.Where(q => q.Q9 == 7).Count();
                float q97avg = ((float)q97cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q97avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q98cnt = appfeed.Where(q => q.Q9 == 8).Count();
                float q98avg = ((float)q98cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q98avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q99cnt = appfeed.Where(q => q.Q9 == 9).Count();
                float q99avg = ((float)q99cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q99avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q910cnt = appfeed.Where(q => q.Q9 == 10).Count();
                float q910avg = ((float)q910cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q910avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();







                //criteria 10
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("10", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Role as a Mentor", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q101cnt = appfeed.Where(q => q.Q10 == 1).Count();
                float q101avg = (float)q101cnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q101avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q102cnt = appfeed.Where(q => q.Q10 == 2).Count();
                float q102avg = ((float)q102cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q102avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q103cnt = appfeed.Where(q => q.Q10 == 3).Count();
                float q103avg = ((float)q103cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q103avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q104cnt = appfeed.Where(q => q.Q10 == 4).Count();
                float q104avg = ((float)q104cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q104avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q105cnt = appfeed.Where(q => q.Q10 == 5).Count();
                float q105avg = ((float)q105cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q105avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q106cnt = appfeed.Where(q => q.Q10 == 6).Count();
                float q106avg = ((float)q106cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q106avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q107cnt = appfeed.Where(q => q.Q10 == 7).Count();
                float q107avg = ((float)q107cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q107avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q108cnt = appfeed.Where(q => q.Q10 == 8).Count();
                float q108avg = ((float)q108cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q108avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q109cnt = appfeed.Where(q => q.Q10 == 9).Count();
                float q109avg = ((float)q109cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q109avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q1010cnt = appfeed.Where(q => q.Q10 == 10).Count();
                float q1010avg = ((float)q1010cnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q1010avg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();







                _document.Add(_ptable);

                _document.NewPage();



            }
            
        }


        public void ReportBody(AppraisalReportByLect appraisalReportByLect)
        {
            List<AppraisalFeedbacks> appfeed = new List<AppraisalFeedbacks>();
            List<Lecturers> lect = new List<Lecturers>();

            List<AppraisalFeedbacks> afbyyear = new List<AppraisalFeedbacks>();

            afbyyear = db.AppraisalFeedback.Where(a => a.EYear == appraisalReportByLect.Year).ToList();

            appfeed = afbyyear.Where(s => s.LuctName == appraisalReportByLect.LuctName).ToList();


            int totalcount = appfeed.Count();

            this.ReportHeader();

            Font phfs = new Font();
            phfs = FontFactory.GetFont("Times", 16f, 1);

            Paragraph phas = new Paragraph("Appraisal By Students", phfs);
            phas.Alignment = Element.ALIGN_CENTER;
            phas.SpacingAfter = 10f;
            _document.Add(phas);

            PdfPTable _ptable = new PdfPTable(12);
            PdfPCell _pcell;
            _ptable.WidthPercentage = 100;
            _ptable.HorizontalAlignment = Element.ALIGN_LEFT;
            _ptable.SetWidths(new float[] { 15f, 90f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f, 15f });

            Font ln = new Font();
            ln = FontFactory.GetFont("Times", 16f, 1);

            Paragraph ph = new Paragraph("Lecturer Name: " + appraisalReportByLect.LuctName, ln);
            ph.Alignment = Element.ALIGN_LEFT;
            ph.SpacingAfter = 10f;
            _document.Add(ph);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Sl. No.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Criteria \\ Rating", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("1", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("2", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("3", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("4", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("5", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("6", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("7", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("8", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("9", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("10", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();

            //criteria 1
            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("1.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Regularity in conducting the classes", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q11cnt = appfeed.Where(q => q.Q1 == 1).Count();
            float q11avg = (float)q11cnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q11avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q12cnt = appfeed.Where(q => q.Q1 == 2).Count();
            float q12avg = ((float)q12cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q12avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q13cnt = appfeed.Where(q => q.Q1 == 3).Count();
            float q13avg = ((float)q13cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q13avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q14cnt = appfeed.Where(q => q.Q1 == 4).Count();
            float q14avg = ((float)q14cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q14avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q15cnt = appfeed.Where(q => q.Q1 == 5).Count();
            float q15avg = ((float)q15cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q15avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q16cnt = appfeed.Where(q => q.Q1 == 6).Count();
            float q16avg = ((float)q16cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q16avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q17cnt = appfeed.Where(q => q.Q1 == 7).Count();
            float q17avg = ((float)q17cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q17avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q18cnt = appfeed.Where(q => q.Q1 == 8).Count();
            float q18avg = ((float)q18cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q18avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q19cnt = appfeed.Where(q => q.Q1 == 9).Count();
            float q19avg = ((float)q19cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q19avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q110cnt = appfeed.Where(q => q.Q1 == 10).Count();
            float q110avg = ((float)q110cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q110avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();





            //criteria 2
            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("2.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Punctuality", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q21cnt = appfeed.Where(q => q.Q2 == 1).Count();
            float q21avg = (float)q21cnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q21avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q22cnt = appfeed.Where(q => q.Q2 == 2).Count();
            float q22avg = ((float)q22cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q22avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q23cnt = appfeed.Where(q => q.Q2 == 3).Count();
            float q23avg = ((float)q23cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q23avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q24cnt = appfeed.Where(q => q.Q2 == 4).Count();
            float q24avg = ((float)q24cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q24avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q25cnt = appfeed.Where(q => q.Q2 == 5).Count();
            float q25avg = ((float)q25cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q25avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q26cnt = appfeed.Where(q => q.Q2 == 6).Count();
            float q26avg = ((float)q26cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q26avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q27cnt = appfeed.Where(q => q.Q2 == 7).Count();
            float q27avg = ((float)q27cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q27avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q28cnt = appfeed.Where(q => q.Q2 == 8).Count();
            float q28avg = ((float)q28cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q28avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q29cnt = appfeed.Where(q => q.Q2 == 9).Count();
            float q29avg = ((float)q29cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q29avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q210cnt = appfeed.Where(q => q.Q2 == 10).Count();
            float q210avg = ((float)q210cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q210avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();







            //criteria 3
            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("3.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Preparation for the classes", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q31cnt = appfeed.Where(q => q.Q3 == 1).Count();
            float q31avg = (float)q31cnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q31avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q32cnt = appfeed.Where(q => q.Q3 == 2).Count();
            float q32avg = ((float)q32cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q32avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q33cnt = appfeed.Where(q => q.Q3 == 3).Count();
            float q33avg = ((float)q33cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q33avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q34cnt = appfeed.Where(q => q.Q3 == 4).Count();
            float q34avg = ((float)q34cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q34avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q35cnt = appfeed.Where(q => q.Q3 == 5).Count();
            float q35avg = ((float)q35cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q35avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q36cnt = appfeed.Where(q => q.Q3 == 6).Count();
            float q36avg = ((float)q36cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q36avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q37cnt = appfeed.Where(q => q.Q3 == 7).Count();
            float q37avg = ((float)q37cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q37avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q38cnt = appfeed.Where(q => q.Q3 == 8).Count();
            float q38avg = ((float)q38cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q38avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q39cnt = appfeed.Where(q => q.Q3 == 9).Count();
            float q39avg = ((float)q39cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q39avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q310cnt = appfeed.Where(q => q.Q3 == 10).Count();
            float q310avg = ((float)q310cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q310avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();







            //criteria 4
            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("4.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Completion of syllabus on time", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q41cnt = appfeed.Where(q => q.Q4 == 1).Count();
            float q41avg = (float)q41cnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q41avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q42cnt = appfeed.Where(q => q.Q4 == 2).Count();
            float q42avg = ((float)q42cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q42avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q43cnt = appfeed.Where(q => q.Q4 == 3).Count();
            float q43avg = ((float)q43cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q43avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q44cnt = appfeed.Where(q => q.Q4 == 4).Count();
            float q44avg = ((float)q44cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q44avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q45cnt = appfeed.Where(q => q.Q4 == 5).Count();
            float q45avg = ((float)q45cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q45avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q46cnt = appfeed.Where(q => q.Q4 == 6).Count();
            float q46avg = ((float)q46cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q46avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q47cnt = appfeed.Where(q => q.Q4 == 7).Count();
            float q47avg = ((float)q47cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q47avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q48cnt = appfeed.Where(q => q.Q4 == 8).Count();
            float q48avg = ((float)q48cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q48avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q49cnt = appfeed.Where(q => q.Q4 == 9).Count();
            float q49avg = ((float)q49cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q49avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q410cnt = appfeed.Where(q => q.Q4 == 10).Count();
            float q410avg = ((float)q410cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q410avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();







            //criteria 5
            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("5.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Competency to handle to the subject", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q51cnt = appfeed.Where(q => q.Q5 == 1).Count();
            float q51avg = (float)q51cnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q51avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q52cnt = appfeed.Where(q => q.Q5 == 2).Count();
            float q52avg = ((float)q52cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q52avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q53cnt = appfeed.Where(q => q.Q5 == 3).Count();
            float q53avg = ((float)q53cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q53avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q54cnt = appfeed.Where(q => q.Q5 == 4).Count();
            float q54avg = ((float)q54cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q54avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q55cnt = appfeed.Where(q => q.Q5 == 5).Count();
            float q55avg = ((float)q55cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q55avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q56cnt = appfeed.Where(q => q.Q5 == 6).Count();
            float q56avg = ((float)q56cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q56avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q57cnt = appfeed.Where(q => q.Q5 == 7).Count();
            float q57avg = ((float)q57cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q57avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q58cnt = appfeed.Where(q => q.Q5 == 8).Count();
            float q58avg = ((float)q58cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q58avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q59cnt = appfeed.Where(q => q.Q5 == 9).Count();
            float q59avg = ((float)q59cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q59avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q510cnt = appfeed.Where(q => q.Q5 == 10).Count();
            float q510avg = ((float)q510cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q510avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();





            //criteria 6
            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("6.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Presentation skills like Voice, clarity and language", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q61cnt = appfeed.Where(q => q.Q6 == 1).Count();
            float q61avg = (float)q61cnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q61avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q62cnt = appfeed.Where(q => q.Q6 == 2).Count();
            float q62avg = ((float)q62cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q62avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q63cnt = appfeed.Where(q => q.Q6 == 3).Count();
            float q63avg = ((float)q63cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q63avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q64cnt = appfeed.Where(q => q.Q6 == 4).Count();
            float q64avg = ((float)q64cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q64avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q65cnt = appfeed.Where(q => q.Q6 == 5).Count();
            float q65avg = ((float)q65cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q65avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q66cnt = appfeed.Where(q => q.Q6 == 6).Count();
            float q66avg = ((float)q66cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q66avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q67cnt = appfeed.Where(q => q.Q6 == 7).Count();
            float q67avg = ((float)q67cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q67avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q68cnt = appfeed.Where(q => q.Q6 == 8).Count();
            float q68avg = ((float)q68cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q68avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q69cnt = appfeed.Where(q => q.Q6 == 9).Count();
            float q69avg = ((float)q69cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q69avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q610cnt = appfeed.Where(q => q.Q6 == 10).Count();
            float q610avg = ((float)q610cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q610avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();






            //criteria 7
            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("7.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Methodology used to import the knowledge", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q71cnt = appfeed.Where(q => q.Q7 == 1).Count();
            float q71avg = (float)q71cnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q71avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q72cnt = appfeed.Where(q => q.Q7 == 2).Count();
            float q72avg = ((float)q72cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q72avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q73cnt = appfeed.Where(q => q.Q7 == 3).Count();
            float q73avg = ((float)q73cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q73avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q74cnt = appfeed.Where(q => q.Q7 == 4).Count();
            float q74avg = ((float)q74cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q74avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q75cnt = appfeed.Where(q => q.Q7 == 5).Count();
            float q75avg = ((float)q75cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q75avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q76cnt = appfeed.Where(q => q.Q7 == 6).Count();
            float q76avg = ((float)q76cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q76avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q77cnt = appfeed.Where(q => q.Q7 == 7).Count();
            float q77avg = ((float)q77cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q77avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q78cnt = appfeed.Where(q => q.Q7 == 8).Count();
            float q78avg = ((float)q78cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q78avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q79cnt = appfeed.Where(q => q.Q7 == 9).Count();
            float q79avg = ((float)q79cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q79avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q710cnt = appfeed.Where(q => q.Q7 == 10).Count();
            float q710avg = ((float)q710cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q710avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();







            //criteria 8
            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("8.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Interaction with the students", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q81cnt = appfeed.Where(q => q.Q8 == 1).Count();
            float q81avg = (float)q81cnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q81avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q82cnt = appfeed.Where(q => q.Q8 == 2).Count();
            float q82avg = ((float)q82cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q82avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q83cnt = appfeed.Where(q => q.Q8 == 3).Count();
            float q83avg = ((float)q83cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q83avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q84cnt = appfeed.Where(q => q.Q8 == 4).Count();
            float q84avg = ((float)q84cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q84avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q85cnt = appfeed.Where(q => q.Q8 == 5).Count();
            float q85avg = ((float)q85cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q85avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q86cnt = appfeed.Where(q => q.Q8 == 6).Count();
            float q86avg = ((float)q86cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q86avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q87cnt = appfeed.Where(q => q.Q8 == 7).Count();
            float q87avg = ((float)q87cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q87avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q88cnt = appfeed.Where(q => q.Q8 == 8).Count();
            float q88avg = ((float)q88cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q88avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q89cnt = appfeed.Where(q => q.Q8 == 9).Count();
            float q89avg = ((float)q89cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q89avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q810cnt = appfeed.Where(q => q.Q8 == 10).Count();
            float q810avg = ((float)q810cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q810avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();







            //criteria 9
            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("9.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Accessibility", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q91cnt = appfeed.Where(q => q.Q9 == 1).Count();
            float q91avg = (float)q91cnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q91avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q92cnt = appfeed.Where(q => q.Q9 == 2).Count();
            float q92avg = ((float)q92cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q92avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q93cnt = appfeed.Where(q => q.Q9 == 3).Count();
            float q93avg = ((float)q93cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q93avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q94cnt = appfeed.Where(q => q.Q9 == 4).Count();
            float q94avg = ((float)q94cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q94avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q95cnt = appfeed.Where(q => q.Q9 == 5).Count();
            float q95avg = ((float)q95cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q95avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q96cnt = appfeed.Where(q => q.Q9 == 6).Count();
            float q96avg = ((float)q96cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q96avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q97cnt = appfeed.Where(q => q.Q9 == 7).Count();
            float q97avg = ((float)q97cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q97avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q98cnt = appfeed.Where(q => q.Q9 == 8).Count();
            float q98avg = ((float)q98cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q98avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q99cnt = appfeed.Where(q => q.Q9 == 9).Count();
            float q99avg = ((float)q99cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q99avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q910cnt = appfeed.Where(q => q.Q9 == 10).Count();
            float q910avg = ((float)q910cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q910avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();







            //criteria 10
            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("10", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Role as a Mentor", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q101cnt = appfeed.Where(q => q.Q10 == 1).Count();
            float q101avg = (float)q101cnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q101avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q102cnt = appfeed.Where(q => q.Q10 == 2).Count();
            float q102avg = ((float)q102cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q102avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q103cnt = appfeed.Where(q => q.Q10 == 3).Count();
            float q103avg = ((float)q103cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q103avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q104cnt = appfeed.Where(q => q.Q10 == 4).Count();
            float q104avg = ((float)q104cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q104avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q105cnt = appfeed.Where(q => q.Q10 == 5).Count();
            float q105avg = ((float)q105cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q105avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q106cnt = appfeed.Where(q => q.Q10 == 6).Count();
            float q106avg = ((float)q106cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q106avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q107cnt = appfeed.Where(q => q.Q10 == 7).Count();
            float q107avg = ((float)q107cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q107avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q108cnt = appfeed.Where(q => q.Q10 == 8).Count();
            float q108avg = ((float)q108cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q108avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q109cnt = appfeed.Where(q => q.Q10 == 9).Count();
            float q109avg = ((float)q109cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q109avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q1010cnt = appfeed.Where(q => q.Q10 == 10).Count();
            float q1010avg = ((float)q1010cnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q1010avg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();

            _document.Add(_ptable);

            _document.NewPage();

                      
        }

        public void ReportBody(CurriculumReportByCrse curriculumReportByCrse)
        {
            List<CurriculumFeedbacks> currfeed = new List<CurriculumFeedbacks>();
            List<Subjects> subj = new List<Subjects>();

            List<CurriculumFeedbacks> cfbyyear = new List<CurriculumFeedbacks>();

            cfbyyear = db.CurriculumFeedback.Where(a => a.EYear == curriculumReportByCrse.Year).ToList();

            if (curriculumReportByCrse.Crse != "All")
            {
                subj = db.Subject.Where(d => d.Branch == curriculumReportByCrse.Crse).ToList();
            }
            else
            {
                subj = db.Subject.ToList();
            }



            List<string> Bname = new List<string>();


            foreach (var item in subj)
            {
                Bname.Add(item.Subject);
            }

            foreach (var name in Bname)
            {
                
                currfeed = cfbyyear.Where(s => s.Subject == name).ToList();

                int totalcount = currfeed.Count();

                this.ReportHeader();

                Font phfs = new Font();
                phfs = FontFactory.GetFont("Times", 16f, 1);

                Paragraph phas = new Paragraph("Curriculum Feedback", phfs);
                phas.Alignment = Element.ALIGN_CENTER;
                phas.SpacingAfter = 10f;
                _document.Add(phas);

                PdfPTable _ptable = new PdfPTable(7);
                PdfPCell _pcell;
                _ptable.WidthPercentage = 100;
                _ptable.HorizontalAlignment = Element.ALIGN_LEFT;
                _ptable.SetWidths(new float[] { 15f, 90f, 30f, 30f, 30f, 30f, 30f });

                Font ln = new Font();
                ln = FontFactory.GetFont("Times", 16f, 1);

                Paragraph ph = new Paragraph("Subject Name: " + name, ln);
                ph.Alignment = Element.ALIGN_LEFT;
                ph.SpacingAfter = 10f;
                _document.Add(ph);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Sl. No.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Criteria \\ Rating", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Excellent", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Very Good", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Good", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Average", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Poor", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();

                //criteria 1
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("1.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Is the design of the syllabus and the sequencing of units in the syllabus coherent? ", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q1acnt = currfeed.Where(q => q.Q1 == "A").Count();
                float q1aavg = (float)q1acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q1aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q1bcnt = currfeed.Where(q => q.Q1 == "B").Count();
                float q1bavg = ((float)q1bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q1bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q1ccnt = currfeed.Where(q => q.Q1 == "C").Count();
                float q1cavg = ((float)q1ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q1cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q1dcnt = currfeed.Where(q => q.Q1 == "D").Count();
                float q1davg = ((float)q1dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q1davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q1ecnt = currfeed.Where(q => q.Q1 == "E").Count();
                float q1eavg = ((float)q1ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q1eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();

                
                //criteria 2

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("2.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Are the objectives of the syllabus met?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q2acnt = currfeed.Where(q => q.Q2 == "A").Count();
                float q2aavg = (float)q2acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q2aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q2bcnt = currfeed.Where(q => q.Q2 == "B").Count();
                float q2bavg = ((float)q2bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q2bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q2ccnt = currfeed.Where(q => q.Q2 == "C").Count();
                float q2cavg = ((float)q2ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q2cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q2dcnt = currfeed.Where(q => q.Q2 == "D").Count();
                float q2davg = ((float)q2dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q2davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q2ecnt = currfeed.Where(q => q.Q2 == "E").Count();
                float q2eavg = ((float)q2ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q2eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();
                

                //criteria 3
                
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("3.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Do you think the syllabus is challenging?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q3acnt = currfeed.Where(q => q.Q3 == "A").Count();
                float q3aavg = (float)q3acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q3aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q3bcnt = currfeed.Where(q => q.Q3 == "B").Count();
                float q3bavg = ((float)q3bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q3bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q3ccnt = currfeed.Where(q => q.Q3 == "C").Count();
                float q3cavg = ((float)q3ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q3cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q3dcnt = currfeed.Where(q => q.Q3 == "D").Count();
                float q3davg = ((float)q3dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q3davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
 
                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q3ecnt = currfeed.Where(q => q.Q3 == "E").Count();
                float q3eavg = ((float)q3ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q3eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();



                //criteria 4

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("4.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Is the number of hours allotted to the syllabus adequate?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q4acnt = currfeed.Where(q => q.Q4 == "A").Count();
                float q4aavg = (float)q4acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q4aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q4bcnt = currfeed.Where(q => q.Q4 == "B").Count();
                float q4bavg = ((float)q4bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q4bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q4ccnt = currfeed.Where(q => q.Q4 == "C").Count();
                float q4cavg = ((float)q4ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q4cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q4dcnt = currfeed.Where(q => q.Q4 == "D").Count();
                float q4davg = ((float)q4dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q4davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q4ecnt = currfeed.Where(q => q.Q4 == "E").Count();
                float q4eavg = ((float)q4ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q4eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();



                //criteria 5

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("5.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Were the given support material inspiring? ", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q5acnt = currfeed.Where(q => q.Q5 == "A").Count();
                float q5aavg = (float)q5acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q5aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q5bcnt = currfeed.Where(q => q.Q5 == "B").Count();
                float q5bavg = ((float)q5bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q5bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q5ccnt = currfeed.Where(q => q.Q5 == "C").Count();
                float q5cavg = ((float)q5ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q5cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q5dcnt = currfeed.Where(q => q.Q5 == "D").Count();
                float q5davg = ((float)q5dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q5davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q5ecnt = currfeed.Where(q => q.Q5 == "E").Count();
                float q5eavg = ((float)q5ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q5eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();



                //criteria 6

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("6.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Do the theoretical concepts help to relate with real life situations?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q6acnt = currfeed.Where(q => q.Q6 == "A").Count();
                float q6aavg = (float)q6acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q6aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q6bcnt = currfeed.Where(q => q.Q6 == "B").Count();
                float q6bavg = ((float)q6bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q6bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q6ccnt = currfeed.Where(q => q.Q6 == "C").Count();
                float q6cavg = ((float)q6ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q6cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q6dcnt = currfeed.Where(q => q.Q6 == "D").Count();
                float q6davg = ((float)q6dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q6davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q6ecnt = currfeed.Where(q => q.Q6 == "E").Count();
                float q6eavg = ((float)q6ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q6eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();



                //criteria 7

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("7.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Were the assignments designed to help you understand the syllabus better?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q7acnt = currfeed.Where(q => q.Q7 == "A").Count();
                float q7aavg = (float)q7acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q7aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q7bcnt = currfeed.Where(q => q.Q7 == "B").Count();
                float q7bavg = ((float)q7bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q7bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q7ccnt = currfeed.Where(q => q.Q7 == "C").Count();
                float q7cavg = ((float)q7ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q7cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q7dcnt = currfeed.Where(q => q.Q7 == "D").Count();
                float q7davg = ((float)q7dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q7davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q7ecnt = currfeed.Where(q => q.Q7 == "E").Count();
                float q7eavg = ((float)q7ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q7eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();



                //criteria 8

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("8.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Were the assignments challenging your understanding? ", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q8acnt = currfeed.Where(q => q.Q8 == "A").Count();
                float q8aavg = (float)q8acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q8aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q8bcnt = currfeed.Where(q => q.Q8 == "B").Count();
                float q8bavg = ((float)q8bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q8bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q8ccnt = currfeed.Where(q => q.Q8 == "C").Count();
                float q8cavg = ((float)q8ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q8cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q8dcnt = currfeed.Where(q => q.Q8 == "D").Count();
                float q8davg = ((float)q8dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q8davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q8ecnt = currfeed.Where(q => q.Q8 == "E").Count();
                float q8eavg = ((float)q8ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q8eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();



                //criteria 9

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("9.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Do the Tests challenge your comprehension of the topic discussed?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q9acnt = currfeed.Where(q => q.Q9 == "A").Count();
                float q9aavg = (float)q9acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q9aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q9bcnt = currfeed.Where(q => q.Q9 == "B").Count();
                float q9bavg = ((float)q9bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q9bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q9ccnt = currfeed.Where(q => q.Q9 == "C").Count();
                float q9cavg = ((float)q9ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q9cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q9dcnt = currfeed.Where(q => q.Q9 == "D").Count();
                float q9davg = ((float)q9dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q9davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q9ecnt = currfeed.Where(q => q.Q9 == "E").Count();
                float q9eavg = ((float)q9ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q9eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();


                //criteria 10

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("10.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Is the laboratory well equipped for the practical work?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q10acnt = currfeed.Where(q => q.Q10 == "A").Count();
                float q10aavg = (float)q10acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q10aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q10bcnt = currfeed.Where(q => q.Q10 == "B").Count();
                float q10bavg = ((float)q10bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q10bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q10ccnt = currfeed.Where(q => q.Q10 == "C").Count();
                float q10cavg = ((float)q10ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q10cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q10dcnt = currfeed.Where(q => q.Q10 == "D").Count();
                float q10davg = ((float)q10dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q10davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q10ecnt = currfeed.Where(q => q.Q10 == "E").Count();
                float q10eavg = ((float)q10ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q10eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();




                //criteria 11

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("11.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Does the Library have reading materials related to the syllabus?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q11acnt = currfeed.Where(q => q.Q11 == "A").Count();
                float q11aavg = (float)q11acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q11aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q11bcnt = currfeed.Where(q => q.Q11 == "B").Count();
                float q11bavg = ((float)q11bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q11bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q11ccnt = currfeed.Where(q => q.Q11 == "C").Count();
                float q11cavg = ((float)q11ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q11cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q11dcnt = currfeed.Where(q => q.Q11 == "D").Count();
                float q11davg = ((float)q11dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q11davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q11ecnt = currfeed.Where(q => q.Q11 == "E").Count();
                float q11eavg = ((float)q11ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q11eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();


                //criteria 12

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("12.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Were the internal tests held in a fair manner?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q12acnt = currfeed.Where(q => q.Q12 == "A").Count();
                float q12aavg = (float)q12acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q12aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q12bcnt = currfeed.Where(q => q.Q12 == "B").Count();
                float q12bavg = ((float)q12bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q12bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q12ccnt = currfeed.Where(q => q.Q12 == "C").Count();
                float q12cavg = ((float)q12ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q12cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q12dcnt = currfeed.Where(q => q.Q12 == "D").Count();
                float q12davg = ((float)q12dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q12davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q12ecnt = currfeed.Where(q => q.Q12 == "E").Count();
                float q12eavg = ((float)q12ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q12eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();


                //criteria 13

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("13.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Did you participate actively in the class? ", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q13acnt = currfeed.Where(q => q.Q13 == "A").Count();
                float q13aavg = (float)q13acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q13aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q13bcnt = currfeed.Where(q => q.Q13 == "B").Count();
                float q13bavg = ((float)q13bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q13bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q13ccnt = currfeed.Where(q => q.Q13 == "C").Count();
                float q13cavg = ((float)q13ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q13cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q13dcnt = currfeed.Where(q => q.Q13 == "D").Count();
                float q13davg = ((float)q13dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q13davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q13ecnt = currfeed.Where(q => q.Q13 == "E").Count();
                float q13eavg = ((float)q13ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q13eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();


                //criteria 14

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("14.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Are the classrooms learner-centric?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q14acnt = currfeed.Where(q => q.Q14 == "A").Count();
                float q14aavg = (float)q14acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q14aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q14bcnt = currfeed.Where(q => q.Q14 == "B").Count();
                float q14bavg = ((float)q14bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q14bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q14ccnt = currfeed.Where(q => q.Q14 == "C").Count();
                float q14cavg = ((float)q14ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q14cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q14dcnt = currfeed.Where(q => q.Q14 == "D").Count();
                float q14davg = ((float)q14dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q14davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q14ecnt = currfeed.Where(q => q.Q14 == "E").Count();
                float q14eavg = ((float)q14ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q14eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();


                //criteria 15

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("15.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Are you satisfied with your progress in the discipline of your interest?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q15acnt = currfeed.Where(q => q.Q15 == "A").Count();
                float q15aavg = (float)q15acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q15aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q15bcnt = currfeed.Where(q => q.Q15 == "B").Count();
                float q15bavg = ((float)q15bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q15bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q15ccnt = currfeed.Where(q => q.Q15 == "C").Count();
                float q15cavg = ((float)q15ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q15cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q15dcnt = currfeed.Where(q => q.Q15 == "D").Count();
                float q15davg = ((float)q15dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q15davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q15ecnt = currfeed.Where(q => q.Q15 == "E").Count();
                float q15eavg = ((float)q15ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q15eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();


                //criteria 16

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("16.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Is the syllabus designed to help you to take up higher education?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q16acnt = currfeed.Where(q => q.Q16 == "A").Count();
                float q16aavg = (float)q16acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q16aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q16bcnt = currfeed.Where(q => q.Q16 == "B").Count();
                float q16bavg = ((float)q16bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q16bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q16ccnt = currfeed.Where(q => q.Q16 == "C").Count();
                float q16cavg = ((float)q16ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q16cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q16dcnt = currfeed.Where(q => q.Q16 == "D").Count();
                float q16davg = ((float)q16dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q16davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q16ecnt = currfeed.Where(q => q.Q16 == "E").Count();
                float q16eavg = ((float)q16ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q16eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();


                //criteria 17

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("17.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Does the syllabus motivates you take up research?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q17acnt = currfeed.Where(q => q.Q17 == "A").Count();
                float q17aavg = (float)q17acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q17aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q17bcnt = currfeed.Where(q => q.Q17 == "B").Count();
                float q17bavg = ((float)q17bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q17bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q17ccnt = currfeed.Where(q => q.Q17 == "C").Count();
                float q17cavg = ((float)q17ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q17cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q17dcnt = currfeed.Where(q => q.Q17 == "D").Count();
                float q17davg = ((float)q17dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q17davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q17ecnt = currfeed.Where(q => q.Q17 == "E").Count();
                float q17eavg = ((float)q17ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q17eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();


                //criteria 18

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("18.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Does the syllabus equip you with employability skills?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q18acnt = currfeed.Where(q => q.Q18 == "A").Count();
                float q18aavg = (float)q18acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q18aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q18bcnt = currfeed.Where(q => q.Q18 == "B").Count();
                float q18bavg = ((float)q18bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q18bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q18ccnt = currfeed.Where(q => q.Q18 == "C").Count();
                float q18cavg = ((float)q18ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q18cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q18dcnt = currfeed.Where(q => q.Q18 == "D").Count();
                float q18davg = ((float)q18dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q18davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q18ecnt = currfeed.Where(q => q.Q18 == "E").Count();
                float q18eavg = ((float)q18ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q18eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();


                //criteria 19

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("19.", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 12f, 1);
                _pcell = new PdfPCell(new Phrase("Is the syllabus relevant in the global context?", _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pcell.Padding = 8f;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q19acnt = currfeed.Where(q => q.Q19 == "A").Count();
                float q19aavg = (float)q19acnt / (float)totalcount;
                _pcell = new PdfPCell(new Phrase(q19aavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q19bcnt = currfeed.Where(q => q.Q19 == "B").Count();
                float q19bavg = ((float)q19bcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q19bavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q19ccnt = currfeed.Where(q => q.Q19 == "C").Count();
                float q19cavg = ((float)q19ccnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q19cavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q19dcnt = currfeed.Where(q => q.Q19 == "D").Count();
                float q19davg = ((float)q19dcnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q19davg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);

                _fontStyle = FontFactory.GetFont("Times", 11f, 1);
                int q19ecnt = currfeed.Where(q => q.Q19 == "E").Count();
                float q19eavg = ((float)q19ecnt / (float)totalcount);
                _pcell = new PdfPCell(new Phrase(q19eavg.ToString("0.0%"), _fontStyle));
                _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _ptable.AddCell(_pcell);
                _ptable.CompleteRow();



                _document.Add(_ptable);

                _document.NewPage();



            }

        }

        public void ReportBody(CurriculumReportBySubj curriculumReportBySubj)
        {
            List<CurriculumFeedbacks> currfeed = new List<CurriculumFeedbacks>();
            

            List<CurriculumFeedbacks> cfbyyear = new List<CurriculumFeedbacks>();

            cfbyyear = db.CurriculumFeedback.Where(a => a.EYear == curriculumReportBySubj.Year).ToList();

            currfeed = cfbyyear.Where(s => s.Subject == curriculumReportBySubj.Subject).ToList();

            int totalcount = currfeed.Count();

            this.ReportHeader();

            Font phfs = new Font();
            phfs = FontFactory.GetFont("Times", 16f, 1);

            Paragraph phas = new Paragraph("Curriculum Feedback", phfs);
            phas.Alignment = Element.ALIGN_CENTER;
            phas.SpacingAfter = 10f;
            _document.Add(phas);

            PdfPTable _ptable = new PdfPTable(7);
            PdfPCell _pcell;
            _ptable.WidthPercentage = 100;
            _ptable.HorizontalAlignment = Element.ALIGN_LEFT;
            _ptable.SetWidths(new float[] { 15f, 90f, 30f, 30f, 30f, 30f, 30f });

            Font ln = new Font();
            ln = FontFactory.GetFont("Times", 16f, 1);

            Paragraph ph = new Paragraph("Subject Name: " + curriculumReportBySubj.Subject, ln);
            ph.Alignment = Element.ALIGN_LEFT;
            ph.SpacingAfter = 10f;
            _document.Add(ph);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Sl. No.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Criteria \\ Rating", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Excellent", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Very Good", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Good", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Average", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Poor", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();

            //criteria 1
            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("1.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Is the design of the syllabus and the sequencing of units in the syllabus coherent? ", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q1acnt = currfeed.Where(q => q.Q1 == "A").Count();
            float q1aavg = (float)q1acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q1aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q1bcnt = currfeed.Where(q => q.Q1 == "B").Count();
            float q1bavg = ((float)q1bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q1bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q1ccnt = currfeed.Where(q => q.Q1 == "C").Count();
            float q1cavg = ((float)q1ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q1cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q1dcnt = currfeed.Where(q => q.Q1 == "D").Count();
            float q1davg = ((float)q1dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q1davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q1ecnt = currfeed.Where(q => q.Q1 == "E").Count();
            float q1eavg = ((float)q1ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q1eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();


            //criteria 2

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("2.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Are the objectives of the syllabus met?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q2acnt = currfeed.Where(q => q.Q2 == "A").Count();
            float q2aavg = (float)q2acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q2aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q2bcnt = currfeed.Where(q => q.Q2 == "B").Count();
            float q2bavg = ((float)q2bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q2bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q2ccnt = currfeed.Where(q => q.Q2 == "C").Count();
            float q2cavg = ((float)q2ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q2cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q2dcnt = currfeed.Where(q => q.Q2 == "D").Count();
            float q2davg = ((float)q2dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q2davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q2ecnt = currfeed.Where(q => q.Q2 == "E").Count();
            float q2eavg = ((float)q2ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q2eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();


            //criteria 3

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("3.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Do you think the syllabus is challenging?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q3acnt = currfeed.Where(q => q.Q3 == "A").Count();
            float q3aavg = (float)q3acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q3aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q3bcnt = currfeed.Where(q => q.Q3 == "B").Count();
            float q3bavg = ((float)q3bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q3bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q3ccnt = currfeed.Where(q => q.Q3 == "C").Count();
            float q3cavg = ((float)q3ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q3cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q3dcnt = currfeed.Where(q => q.Q3 == "D").Count();
            float q3davg = ((float)q3dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q3davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q3ecnt = currfeed.Where(q => q.Q3 == "E").Count();
            float q3eavg = ((float)q3ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q3eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();



            //criteria 4

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("4.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Is the number of hours allotted to the syllabus adequate?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q4acnt = currfeed.Where(q => q.Q4 == "A").Count();
            float q4aavg = (float)q4acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q4aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q4bcnt = currfeed.Where(q => q.Q4 == "B").Count();
            float q4bavg = ((float)q4bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q4bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q4ccnt = currfeed.Where(q => q.Q4 == "C").Count();
            float q4cavg = ((float)q4ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q4cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q4dcnt = currfeed.Where(q => q.Q4 == "D").Count();
            float q4davg = ((float)q4dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q4davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q4ecnt = currfeed.Where(q => q.Q4 == "E").Count();
            float q4eavg = ((float)q4ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q4eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();



            //criteria 5

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("5.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Were the given support material inspiring? ", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q5acnt = currfeed.Where(q => q.Q5 == "A").Count();
            float q5aavg = (float)q5acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q5aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q5bcnt = currfeed.Where(q => q.Q5 == "B").Count();
            float q5bavg = ((float)q5bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q5bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q5ccnt = currfeed.Where(q => q.Q5 == "C").Count();
            float q5cavg = ((float)q5ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q5cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q5dcnt = currfeed.Where(q => q.Q5 == "D").Count();
            float q5davg = ((float)q5dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q5davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q5ecnt = currfeed.Where(q => q.Q5 == "E").Count();
            float q5eavg = ((float)q5ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q5eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();



            //criteria 6

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("6.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Do the theoretical concepts help to relate with real life situations?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q6acnt = currfeed.Where(q => q.Q6 == "A").Count();
            float q6aavg = (float)q6acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q6aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q6bcnt = currfeed.Where(q => q.Q6 == "B").Count();
            float q6bavg = ((float)q6bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q6bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q6ccnt = currfeed.Where(q => q.Q6 == "C").Count();
            float q6cavg = ((float)q6ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q6cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q6dcnt = currfeed.Where(q => q.Q6 == "D").Count();
            float q6davg = ((float)q6dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q6davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q6ecnt = currfeed.Where(q => q.Q6 == "E").Count();
            float q6eavg = ((float)q6ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q6eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();



            //criteria 7

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("7.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Were the assignments designed to help you understand the syllabus better?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q7acnt = currfeed.Where(q => q.Q7 == "A").Count();
            float q7aavg = (float)q7acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q7aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q7bcnt = currfeed.Where(q => q.Q7 == "B").Count();
            float q7bavg = ((float)q7bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q7bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q7ccnt = currfeed.Where(q => q.Q7 == "C").Count();
            float q7cavg = ((float)q7ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q7cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q7dcnt = currfeed.Where(q => q.Q7 == "D").Count();
            float q7davg = ((float)q7dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q7davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q7ecnt = currfeed.Where(q => q.Q7 == "E").Count();
            float q7eavg = ((float)q7ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q7eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();



            //criteria 8

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("8.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Were the assignments challenging your understanding? ", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q8acnt = currfeed.Where(q => q.Q8 == "A").Count();
            float q8aavg = (float)q8acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q8aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q8bcnt = currfeed.Where(q => q.Q8 == "B").Count();
            float q8bavg = ((float)q8bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q8bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q8ccnt = currfeed.Where(q => q.Q8 == "C").Count();
            float q8cavg = ((float)q8ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q8cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q8dcnt = currfeed.Where(q => q.Q8 == "D").Count();
            float q8davg = ((float)q8dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q8davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q8ecnt = currfeed.Where(q => q.Q8 == "E").Count();
            float q8eavg = ((float)q8ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q8eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();



            //criteria 9

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("9.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Do the Tests challenge your comprehension of the topic discussed?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q9acnt = currfeed.Where(q => q.Q9 == "A").Count();
            float q9aavg = (float)q9acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q9aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q9bcnt = currfeed.Where(q => q.Q9 == "B").Count();
            float q9bavg = ((float)q9bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q9bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q9ccnt = currfeed.Where(q => q.Q9 == "C").Count();
            float q9cavg = ((float)q9ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q9cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q9dcnt = currfeed.Where(q => q.Q9 == "D").Count();
            float q9davg = ((float)q9dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q9davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q9ecnt = currfeed.Where(q => q.Q9 == "E").Count();
            float q9eavg = ((float)q9ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q9eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();


            //criteria 10

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("10.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Is the laboratory well equipped for the practical work?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q10acnt = currfeed.Where(q => q.Q10 == "A").Count();
            float q10aavg = (float)q10acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q10aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q10bcnt = currfeed.Where(q => q.Q10 == "B").Count();
            float q10bavg = ((float)q10bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q10bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q10ccnt = currfeed.Where(q => q.Q10 == "C").Count();
            float q10cavg = ((float)q10ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q10cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q10dcnt = currfeed.Where(q => q.Q10 == "D").Count();
            float q10davg = ((float)q10dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q10davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q10ecnt = currfeed.Where(q => q.Q10 == "E").Count();
            float q10eavg = ((float)q10ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q10eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();




            //criteria 11

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("11.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Does the Library have reading materials related to the syllabus?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q11acnt = currfeed.Where(q => q.Q11 == "A").Count();
            float q11aavg = (float)q11acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q11aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q11bcnt = currfeed.Where(q => q.Q11 == "B").Count();
            float q11bavg = ((float)q11bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q11bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q11ccnt = currfeed.Where(q => q.Q11 == "C").Count();
            float q11cavg = ((float)q11ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q11cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q11dcnt = currfeed.Where(q => q.Q11 == "D").Count();
            float q11davg = ((float)q11dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q11davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q11ecnt = currfeed.Where(q => q.Q11 == "E").Count();
            float q11eavg = ((float)q11ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q11eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();


            //criteria 12

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("12.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Were the internal tests held in a fair manner?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q12acnt = currfeed.Where(q => q.Q12 == "A").Count();
            float q12aavg = (float)q12acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q12aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q12bcnt = currfeed.Where(q => q.Q12 == "B").Count();
            float q12bavg = ((float)q12bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q12bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q12ccnt = currfeed.Where(q => q.Q12 == "C").Count();
            float q12cavg = ((float)q12ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q12cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q12dcnt = currfeed.Where(q => q.Q12 == "D").Count();
            float q12davg = ((float)q12dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q12davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q12ecnt = currfeed.Where(q => q.Q12 == "E").Count();
            float q12eavg = ((float)q12ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q12eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();


            //criteria 13

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("13.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Did you participate actively in the class? ", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q13acnt = currfeed.Where(q => q.Q13 == "A").Count();
            float q13aavg = (float)q13acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q13aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q13bcnt = currfeed.Where(q => q.Q13 == "B").Count();
            float q13bavg = ((float)q13bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q13bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q13ccnt = currfeed.Where(q => q.Q13 == "C").Count();
            float q13cavg = ((float)q13ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q13cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q13dcnt = currfeed.Where(q => q.Q13 == "D").Count();
            float q13davg = ((float)q13dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q13davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q13ecnt = currfeed.Where(q => q.Q13 == "E").Count();
            float q13eavg = ((float)q13ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q13eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();


            //criteria 14

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("14.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Are the classrooms learner-centric?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q14acnt = currfeed.Where(q => q.Q14 == "A").Count();
            float q14aavg = (float)q14acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q14aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q14bcnt = currfeed.Where(q => q.Q14 == "B").Count();
            float q14bavg = ((float)q14bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q14bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q14ccnt = currfeed.Where(q => q.Q14 == "C").Count();
            float q14cavg = ((float)q14ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q14cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q14dcnt = currfeed.Where(q => q.Q14 == "D").Count();
            float q14davg = ((float)q14dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q14davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q14ecnt = currfeed.Where(q => q.Q14 == "E").Count();
            float q14eavg = ((float)q14ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q14eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();


            //criteria 15

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("15.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Are you satisfied with your progress in the discipline of your interest?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q15acnt = currfeed.Where(q => q.Q15 == "A").Count();
            float q15aavg = (float)q15acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q15aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q15bcnt = currfeed.Where(q => q.Q15 == "B").Count();
            float q15bavg = ((float)q15bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q15bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q15ccnt = currfeed.Where(q => q.Q15 == "C").Count();
            float q15cavg = ((float)q15ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q15cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q15dcnt = currfeed.Where(q => q.Q15 == "D").Count();
            float q15davg = ((float)q15dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q15davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q15ecnt = currfeed.Where(q => q.Q15 == "E").Count();
            float q15eavg = ((float)q15ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q15eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();


            //criteria 16

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("16.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Is the syllabus designed to help you to take up higher education?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q16acnt = currfeed.Where(q => q.Q16 == "A").Count();
            float q16aavg = (float)q16acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q16aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q16bcnt = currfeed.Where(q => q.Q16 == "B").Count();
            float q16bavg = ((float)q16bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q16bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q16ccnt = currfeed.Where(q => q.Q16 == "C").Count();
            float q16cavg = ((float)q16ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q16cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q16dcnt = currfeed.Where(q => q.Q16 == "D").Count();
            float q16davg = ((float)q16dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q16davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q16ecnt = currfeed.Where(q => q.Q16 == "E").Count();
            float q16eavg = ((float)q16ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q16eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();


            //criteria 17

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("17.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Does the syllabus motivates you take up research?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q17acnt = currfeed.Where(q => q.Q17 == "A").Count();
            float q17aavg = (float)q17acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q17aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q17bcnt = currfeed.Where(q => q.Q17 == "B").Count();
            float q17bavg = ((float)q17bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q17bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q17ccnt = currfeed.Where(q => q.Q17 == "C").Count();
            float q17cavg = ((float)q17ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q17cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q17dcnt = currfeed.Where(q => q.Q17 == "D").Count();
            float q17davg = ((float)q17dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q17davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q17ecnt = currfeed.Where(q => q.Q17 == "E").Count();
            float q17eavg = ((float)q17ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q17eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();


            //criteria 18

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("18.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Does the syllabus equip you with employability skills?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q18acnt = currfeed.Where(q => q.Q18 == "A").Count();
            float q18aavg = (float)q18acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q18aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q18bcnt = currfeed.Where(q => q.Q18 == "B").Count();
            float q18bavg = ((float)q18bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q18bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q18ccnt = currfeed.Where(q => q.Q18 == "C").Count();
            float q18cavg = ((float)q18ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q18cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q18dcnt = currfeed.Where(q => q.Q18 == "D").Count();
            float q18davg = ((float)q18dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q18davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q18ecnt = currfeed.Where(q => q.Q18 == "E").Count();
            float q18eavg = ((float)q18ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q18eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();


            //criteria 19

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("19.", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 12f, 1);
            _pcell = new PdfPCell(new Phrase("Is the syllabus relevant in the global context?", _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pcell.Padding = 8f;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q19acnt = currfeed.Where(q => q.Q19 == "A").Count();
            float q19aavg = (float)q19acnt / (float)totalcount;
            _pcell = new PdfPCell(new Phrase(q19aavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q19bcnt = currfeed.Where(q => q.Q19 == "B").Count();
            float q19bavg = ((float)q19bcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q19bavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q19ccnt = currfeed.Where(q => q.Q19 == "C").Count();
            float q19cavg = ((float)q19ccnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q19cavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q19dcnt = currfeed.Where(q => q.Q19 == "D").Count();
            float q19davg = ((float)q19dcnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q19davg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);

            _fontStyle = FontFactory.GetFont("Times", 11f, 1);
            int q19ecnt = currfeed.Where(q => q.Q19 == "E").Count();
            float q19eavg = ((float)q19ecnt / (float)totalcount);
            _pcell = new PdfPCell(new Phrase(q19eavg.ToString("0.0%"), _fontStyle));
            _pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _ptable.AddCell(_pcell);
            _ptable.CompleteRow();



            _document.Add(_ptable);

            _document.NewPage();

        }
        


        
        
    }
}