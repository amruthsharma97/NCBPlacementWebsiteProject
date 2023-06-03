using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class StudentProfileData
    {
        public IEnumerable<StudentProfileEntity> Read()
        {
            IEnumerable<StudentProfileEntity> studentProfiles;
            using(var context=new NCBPEntities())
            {
                studentProfiles = ParseToEntity(context.StudentProfiles).OrderBy(s => s.Status).ThenBy(s=>s.FName).ToList();
            }
            return studentProfiles;
        }

        public IEnumerable<StudentProfileEntity> Read(int CourseId)
        {
            IEnumerable<StudentProfileEntity> studentProfiles;
            using (var context = new NCBPEntities())
            {
                studentProfiles = ParseToEntity(context.StudentProfiles.Where(x=>x.CourseId==CourseId)).OrderBy(s => s.Status).ThenBy(s => s.FName).ToList();
            }
            return studentProfiles;
        }

        public StudentProfileEntity Read(string EmailId,string EncryptedPasscode)
        {
            StudentProfileEntity studentProfile = new StudentProfileEntity();
            using (var context = new NCBPEntities())
            {
                var info = context.StudentProfiles.FirstOrDefault(x => x.EmailId.ToLower().Trim()==EmailId.ToLower().Trim()&&x.Passcode.Trim()==EncryptedPasscode.Trim());
                if (info != null)
                {
                    studentProfile = new StudentProfileEntity
                    {
                        Id = info.Id,
                        Aadnum = info.Aadnum,
                        URNo = info.URNo,
                        FName = info.FName,
                        MName = info.MName,
                        LName = info.LName,
                        DOB = info.DOB,
                        MNumber = info.MNumber,
                        EmailId = info.EmailId.ToLower().Trim(),
                        EnroledYear = info.EnroledYear,
                        ExpectedYOP = info.ExpectedYOP,
                        AddressLine1 = info.AddrLine1,
                        AddressLine2 = info.AddrLine2,
                        PostalCode = info.PostalCode,
                        Status = info.Status,
                        CourseId = info.CourseId,
                        CityId = info.CityId,
                        StateId = info.StateId,
                        CountryId = info.CountryId
                    };
                }
            }
            return studentProfile;
        }

        public Guid Read(string Email)
        {
            StudentProfile student = new StudentProfile();
            using (var context = new NCBPEntities())
            {
                student = context.StudentProfiles.SingleOrDefault(x => x.EmailId == Email);
            }
            return student.Id;
        }

        public StudentProfileEntity Read(Guid StudentId)
        {
            StudentProfileEntity studentProfile = new StudentProfileEntity();
            using(var context=new NCBPEntities())
            {
                var info = context.StudentProfiles.SingleOrDefault(x => x.Id == StudentId);
                if (info != null)
                {
                    studentProfile = new StudentProfileEntity
                    {
                        Id=info.Id,
                        Aadnum = info.Aadnum,
                        URNo = info.URNo,
                        FName = info.FName,
                        MName = info.MName,
                        LName = info.LName,
                        DOB = info.DOB,
                        MNumber = info.MNumber,
                        EmailId = info.EmailId.ToLower().Trim(),
                        EnroledYear = info.EnroledYear,
                        ExpectedYOP = info.ExpectedYOP,
                        AddressLine1 = info.AddrLine1,
                        AddressLine2 = info.AddrLine2,
                        PostalCode = info.PostalCode,
                        Status = info.Status,
                        CourseId = info.CourseId,
                        CityId = info.CityId,
                        StateId = info.StateId,
                        CountryId = info.CountryId
                    };
                }
            }
            return studentProfile;
        }

        public void Create(StudentProfileEntity studentProfile)
        {
            using(var context=new NCBPEntities())
            {
                StudentProfile student = new StudentProfile()
                {
                    Aadnum = studentProfile.Aadnum,
                    URNo = studentProfile.URNo,
                    FName = studentProfile.FName,
                    MName = studentProfile.MName,
                    LName = studentProfile.LName,
                    DOB = studentProfile.DOB,
                    MNumber = studentProfile.MNumber,
                    EmailId = studentProfile.EmailId.ToLower().Trim(),
                    EnroledYear = studentProfile.EnroledYear,
                    ExpectedYOP = studentProfile.ExpectedYOP,
                    AddrLine1 = studentProfile.AddressLine1,
                    AddrLine2 = studentProfile.AddressLine2,
                    PostalCode = studentProfile.PostalCode,
                    Status = studentProfile.Status,
                    CourseId = studentProfile.CourseId,
                    CityId = studentProfile.CityId,
                    StateId = studentProfile.StateId,
                    CountryId = studentProfile.CountryId,
                    CreatedDate=studentProfile.CreatedDate
                };
            }
        }

        public void Update(StudentProfileEntity studentProfile)
        {
            using (var context = new NCBPEntities())
            {
                var studentProfileInfo = context.StudentProfiles.Single(x => x.Id == studentProfile.Id);
                studentProfileInfo.Aadnum = studentProfile.Aadnum;
                studentProfileInfo.URNo = studentProfile.URNo;
                studentProfileInfo.FName = studentProfile.FName;
                studentProfileInfo.MName = studentProfile.MName;
                studentProfileInfo.LName = studentProfile.LName;
                studentProfileInfo.DOB = studentProfile.DOB;
                studentProfileInfo.MNumber = studentProfile.MNumber;
                studentProfileInfo.AddrLine1 = studentProfile.AddressLine1;
                studentProfileInfo.AddrLine2 = studentProfile.AddressLine2;
                studentProfileInfo.PostalCode = studentProfile.PostalCode;
                studentProfileInfo.Status = studentProfile.Status;
                studentProfileInfo.CourseId = studentProfile.CourseId;
                studentProfileInfo.CityId = studentProfile.CityId;
                studentProfileInfo.StateId = studentProfile.StateId;
                studentProfileInfo.CountryId = studentProfile.CountryId;
                context.SaveChanges();
            }
        }

        public void Approve(StudentProfileEntity studentProfile)
        {
            using (var context = new NCBPEntities())
            {
                var studentProfileInfo = context.StudentProfiles.Single(x => x.Id == studentProfile.Id);
                studentProfileInfo.Aadnum = studentProfile.Aadnum;
                studentProfileInfo.URNo = studentProfile.URNo;
                studentProfileInfo.FName = studentProfile.FName;
                studentProfileInfo.MName = studentProfile.MName;
                studentProfileInfo.LName = studentProfile.LName;
                studentProfileInfo.DOB = studentProfile.DOB;
                studentProfileInfo.MNumber = studentProfile.MNumber;
                studentProfileInfo.AddrLine1 = studentProfile.AddressLine1;
                studentProfileInfo.AddrLine2 = studentProfile.AddressLine2;
                studentProfileInfo.PostalCode = studentProfile.PostalCode;
                studentProfileInfo.Status = studentProfile.Status;
                studentProfileInfo.CourseId = studentProfile.CourseId;
                studentProfileInfo.CityId = studentProfile.CityId;
                studentProfileInfo.StateId = studentProfile.StateId;
                studentProfileInfo.CountryId = studentProfile.CountryId;
                studentProfileInfo.ApprovedBy = studentProfile.ApprovedBy;
                studentProfileInfo.ApprovedDate = studentProfile.ApprovedDate;
                context.SaveChanges();
            }
        }

        public IEnumerable<StudentProfileEntity> ParseToEntity(IQueryable<StudentProfile> students)
        {
            var studentProfileList = new List<StudentProfileEntity>();
            foreach(var item in students)
            {
                studentProfileList.Add(new StudentProfileEntity
                {
                    Id=item.Id,
                    Aadnum=item.Aadnum,
                    URNo=item.URNo,
                    FName=item.FName,
                    MName=item.MName,
                    LName=item.LName,
                    DOB=item.DOB,
                    MNumber=item.MNumber,
                    EmailId=item.EmailId,
                    EnroledYear=item.EnroledYear,
                    ExpectedYOP=item.ExpectedYOP,
                    AddressLine1=item.AddrLine1,
                    AddressLine2=item.AddrLine2,
                    PostalCode=item.PostalCode,
                    Status=item.Status,
                    CourseId=item.CourseId,
                    CityId=item.CityId,
                    StateId=item.StateId,
                    CountryId=item.CountryId
                });
            }
            return studentProfileList;
        }
    }
}
