using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class PreviousExamData
    {
        public IEnumerable<PreviousExamEntity> Read(Guid StudentId)
        {
            IEnumerable<PreviousExamEntity> previousExams;
            using(var context =new NCBPEntities())
            {
                previousExams = ParseToEntity(context.PreviousExamInfoes.Where(x => x.StudentId == StudentId)).OrderByDescending(s=>s.PreviousCourseName).ToList();
            }
            return previousExams;
        }

        public void Create(PreviousExamEntity previousExam)
        {
            using(var context=new NCBPEntities())
            {
                PreviousExamInfo examInfo = new PreviousExamInfo()
                {
                    StudentId = previousExam.StudentId,
                    PreviousCourseName = previousExam.PreviousCourseName,
                    PreviousInstitution = previousExam.PreviousInstitution,
                    MonthAndYearOfPassing = previousExam.MonthAndYearOfPassing,
                    Result = previousExam.Result,
                    StateId = previousExam.StateId,
                    CountryId = previousExam.CountryId
                };
                context.PreviousExamInfoes.Add(examInfo);
                context.SaveChanges();
            }
        }

        public void Update(PreviousExamEntity previousExam)
        {
            using(var context = new NCBPEntities())
            {
                var info = context.PreviousExamInfoes.Single(x => x.Id == previousExam.Id);
                info.StudentId = previousExam.StudentId;
                info.PreviousCourseName = previousExam.PreviousCourseName;
                info.PreviousInstitution = previousExam.PreviousInstitution;
                info.MonthAndYearOfPassing = previousExam.MonthAndYearOfPassing;
                info.Result = previousExam.Result;
                info.StateId = previousExam.StateId;
                info.CountryId = previousExam.CountryId;
                context.SaveChanges();
            }
        }

        public IEnumerable<PreviousExamEntity> ParseToEntity(IQueryable<PreviousExamInfo> previousExams)
        {
            var list = new List<PreviousExamEntity>();
            foreach(var item in previousExams)
            {
                list.Add(new PreviousExamEntity
                {
                    Id = item.Id,
                    StudentId=item.StudentId,
                    PreviousCourseName = item.PreviousCourseName,
                    PreviousInstitution =item.PreviousInstitution,
                    MonthAndYearOfPassing=item.MonthAndYearOfPassing,
                    Result=item.Result,
                    StateId=item.StateId,
                    CountryId=item.CountryId
                });
            }
            return list;
        }
    }
}
