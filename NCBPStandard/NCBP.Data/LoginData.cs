using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class LoginData
    {
        public IEnumerable<LoginEntity> Read(bool IsActive)
        {
            var loginlist = new List<LoginEntity>();
            using (var context = new NCBPEntities())
            {
                foreach (var item in context.Users.Where(x => x.IsActive == IsActive))
                {
                    loginlist.Add(new LoginEntity
                    {
                        Id = item.Id,
                        Name = item.Name,
                        UserName = item.UserName.ToUpper().Trim(),
                        Passcode = item.Passcode,
                        StaffId = item.StaffId,
                        IsActive = item.IsActive,
                        IsFirstLogin = item.IsFirstLogin,
                        IsAdmin = item.IsAdmin,
                    });
                }
            }
            return loginlist;
        }

        public LoginEntity Read(bool IsActive, string UserName, string EncryptedPassword)
        {
            LoginEntity LoginEntity = new LoginEntity();
            using (var context = new NCBPEntities())
            {
                var item = context.Users.FirstOrDefault(x => x.IsActive == IsActive && x.UserName.ToUpper().Trim() == UserName.ToUpper().Trim() && x.Passcode.Trim() == EncryptedPassword.Trim());
                if (item != null)
                {
                    LoginEntity = new LoginEntity
                    {
                        Id = item.Id,
                        Name = item.Name,
                        UserName = item.UserName.ToUpper().Trim(),
                        Passcode = item.Passcode,
                        StaffId = item.StaffId,
                        IsActive = item.IsActive,
                        IsFirstLogin = item.IsFirstLogin,
                        IsAdmin = item.IsAdmin,
                        CreatedDate = DateTime.Now
                    };
                }
            }
            return LoginEntity;
        }

        public LoginEntity Read(bool IsActive, int LoginId)
        {
            LoginEntity LoginEntity = new LoginEntity();
            using (var context = new NCBPEntities())
            {
                var item = context.Users.SingleOrDefault(x => x.Id == LoginId);
                if (item != null)
                {
                    LoginEntity = new LoginEntity
                    {
                        Id = item.Id,
                        Name = item.Name,
                        UserName = item.UserName.ToUpper().Trim(),
                        Passcode = item.Passcode,
                        StaffId = item.StaffId,
                        IsActive = item.IsActive,
                        IsFirstLogin = item.IsFirstLogin,
                        IsAdmin = item.IsAdmin,
                        CreatedDate = DateTime.Now
                    };
                }
            }
            return LoginEntity;
        }

        public void Create(LoginEntity login)
        {
            using (var context = new NCBPEntities())
            {
                User logininfo = new User
                {
                    Name = login.Name,
                    UserName = login.UserName.Trim(),
                    Passcode = login.Passcode,
                    StaffId = login.StaffId,
                    IsAdmin = login.IsAdmin,
                    IsFirstLogin = true,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = login.CreatedBy,
                };
                context.Users.Add(logininfo);
                context.SaveChanges();
            }
        }

        public void Update(LoginEntity login)
        {
            using (var context = new NCBPEntities())
            {
                var dbSet = context.Users;
                var logininfo = dbSet.SingleOrDefault(x => x.Id == login.Id);
                if (logininfo == null)
                {
                    logininfo = dbSet.SingleOrDefault(x => x.StaffId == login.StaffId);
                }
                //logininfo.Name = login.Name;
                logininfo.UserName = login.UserName.Trim();
                logininfo.Passcode = login.Passcode;
                logininfo.IsAdmin = login.IsAdmin;
                logininfo.UpdatedDate = DateTime.Now;
                logininfo.UpdatedBy = login.UpdatedBy;
                context.SaveChanges();
            }
        }

        public void Delete(int id, string person)
        {
            using (var context = new NCBPEntities())
            {
                var logininfo = context.Users.Single(x => x.Id == id);
                logininfo.IsActive = false;
                logininfo.UpdatedDate = DateTime.Now;
                logininfo.UpdatedBy = person;
                context.SaveChanges();
            }
        }
    }
}
