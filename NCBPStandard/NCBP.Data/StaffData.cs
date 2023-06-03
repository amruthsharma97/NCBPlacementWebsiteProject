using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class StaffData
    {
        public IEnumerable<StaffEntity> Read(bool IsActive)
        {
            List<StaffEntity> staffList = new List<StaffEntity>();
            using (var context = new NCBPEntities())
            {
                foreach (var item in context.Staffs.Where(x => x.IsActive == IsActive))
                {
                    staffList.Add(new StaffEntity
                    {
                        Id = item.Id,
                        FirstName = item.FullName.Trim(),
                        RollNo = item.RollNo,
                        Qualification = item.Qualification,
                        Designation = item.Designation,
                        Experiance = item.Experiance,
                        Department = item.Department,
                        Email = item.Email.Trim(),
                        Phone = item.Phone,
                        Address = item.Address,
                    });
                }
            }
            return staffList;
        }

        public int Read(bool IsActive,string Email)
        {
            Staff staff = new Staff();
            using (var context = new NCBPEntities())
            {
                staff = context.Staffs.SingleOrDefault(x => x.IsActive == IsActive && x.Email == Email);   
            }
            return staff.Id;
        }

        public StaffEntity Read(bool IsActive, int staffId)
        {
            StaffEntity staffEntity = new StaffEntity();
            using (var context = new NCBPEntities())
            {
                var staff = context.Staffs.SingleOrDefault(x => x.IsActive == IsActive && x.Id == staffId);
                if (staff != null)
                {
                    staffEntity = new StaffEntity
                    {
                        Id = staff.Id,
                        FirstName = staff.FullName,
                        RollNo = staff.RollNo,
                        Qualification = staff.Qualification,
                        Designation = staff.Designation,
                        Experiance = staff.Experiance,
                        DateOfBirth = staff.DateOfBirth ?? new DateTime(),
                        DateOfJoin = staff.DateOfJoin ?? new DateTime(),
                        Department = staff.Department,
                        Email = staff.Email,
                        Phone = staff.Phone,
                        Address = staff.Address,
                        IsActive = true,
                        CreatedBy = staff.CreatedBy,
                        CreatedDate = DateTime.Now
                    };
                }
            }
            return staffEntity;
        }

        public void Create(StaffEntity staff)
        {
            using (var context = new NCBPEntities())
            {
                Staff staffInfo = new Staff
                {
                    FullName = staff.FirstName,
                    RollNo = staff.RollNo,
                    Qualification = staff.Qualification,
                    Designation = staff.Designation,
                    Experiance = staff.Experiance,
                    DateOfBirth = staff.DateOfBirth,
                    DateOfJoin = staff.DateOfJoin,
                    Department = staff.Department,
                    Email = staff.Email,
                    Phone = staff.Phone,
                    Address = staff.Address,
                    IsActive = true,
                    CreatedBy = staff.CreatedBy,
                    CreatedDate = DateTime.Now
                };
                context.Staffs.Add(staffInfo);
                context.SaveChanges();
            }
        }

        public void Update(StaffEntity staff)
        {
            using (var context = new NCBPEntities())
            {
                var staffInfo = context.Staffs.Single(x => x.Id == staff.Id);
                staffInfo.FullName = staff.FirstName;
                //staffInfo.RollNo = staff.RollNo;
                //staffInfo.Qualification = staff.Qualification;
                //staffInfo.Designation = staff.Designation;
                //staffInfo.Experiance = staff.Experiance;
                //staffInfo.DateOfBirth = staff.DateOfBirth;
                //staffInfo.DateOfJoin = staff.DateOfJoin;
                staffInfo.Department = staff.Department;
                staffInfo.Email = staff.Email;
                staffInfo.Phone = staff.Phone;
                //staffInfo.Address = staff.Address;
                staffInfo.UpdatedDate = DateTime.Now;
                staffInfo.UpdatedBy = staff.UpdatedBy;
                context.SaveChanges();
            }
        }

        public void Delete(int id, string person)
        {
            using (var context = new NCBPEntities())
            {
                var staffInfo = context.Staffs.Single(x => x.Id == id);
                staffInfo.IsActive = false;
                staffInfo.UpdatedBy = person;
                staffInfo.UpdatedDate = DateTime.Now;
                context.SaveChanges();
            }
        }

        public void UndoDelete(int id, string person)
        {
            using (var context = new NCBPEntities())
            {
                var staffInfo = context.Staffs.Single(x => x.Id == id);
                staffInfo.IsActive = true;
                staffInfo.UpdatedBy = person;
                staffInfo.UpdatedDate = DateTime.Now;
                context.SaveChanges();
            }
        }
    }
}
