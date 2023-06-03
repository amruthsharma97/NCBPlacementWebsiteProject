using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class SemesterData
    {
        public IEnumerable<SemesterEntity> Read(Guid StudentId)
        {
            IEnumerable<SemesterEntity> semesters;
            using (var context = new NCBPEntities())
            {
                semesters = ParseToEntity(context.Semesters.Where(x => x.StudentId == StudentId)).OrderBy(s=>s.SemesterNo).ToList();
            }
            return semesters;
        }

        public void Create(SemesterEntity semesterEntity)
        {
            using (var context = new NCBPEntities())
            {
                Semester semester=new Semester()
                {
                    StudentId = semesterEntity.StudentId,
                    SemesterNo = semesterEntity.SemesterNo,
                    Result = semesterEntity.Result
                };
                context.Semesters.Add(semester);
                context.SaveChanges();
            }
        }

        public void Update(SemesterEntity semesterEntity)
        {
            using (var context = new NCBPEntities())
            {
                var info = context.Semesters.Single(x => x.Id == semesterEntity.Id);
                info.StudentId = semesterEntity.StudentId;
                info.SemesterNo = semesterEntity.SemesterNo;
                info.Result = semesterEntity.Result;
                context.SaveChanges();
            }
        }

        public IEnumerable<SemesterEntity> ParseToEntity(IQueryable<Semester> semesters)
        {
            var list = new List<SemesterEntity>();
            foreach (var item in semesters)
            {
                list.Add(new SemesterEntity
                {
                    Id = item.Id,
                    StudentId = item.StudentId,
                    SemesterNo=item.SemesterNo,
                    Result=item.Result
                });
            }
            return list;
        }
    }
}
