using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class CountryData
    {
        public IEnumerable<CountryEntity> Read(bool isActive)
        {
            var List = new List<CountryEntity>();
            using (var context = new NCBPEntities())
            {
                foreach (var item in context.Countries.Where(x => x.IsActive == isActive))
                {
                    List.Add(new CountryEntity
                    {
                        Id = item.Id,
                        Name = item.Name,
                        IsActive = true,
                    });
                }
            }
            return List;
        }

        public void Create(CountryEntity Country)
        {
            using (var context = new NCBPEntities())
            {
                Country info = new Country
                {
                    Name = Country.Name,
                    IsActive = true
                };
                context.Countries.Add(info);
                context.SaveChanges();
            }
        }

        public void Update(CountryEntity Country)
        {
            using (var context = new NCBPEntities())
            {
                var info = context.Countries.Single(x => x.Id == Country.Id);
                info.Name = Country.Name;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var country = context.Countries.Single(x => x.Id == id);
                country.IsActive = false; ;
                context.SaveChanges();
            }
        }

        public void UndoDelete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var country = context.Countries.Single(x => x.Id == id);
                country.IsActive = true;
                context.SaveChanges();
            }
        }

    }
}
