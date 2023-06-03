using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using NCBPlacementWebsite.Models;

namespace NCBPlacementWebsite
{
    public class Ncbusers
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public string GetFullName(string uname)
        {

            var au = db.Users.Where(x => x.Email == uname).ToList().FirstOrDefault();


            return au.FullName;
        }
    }
}