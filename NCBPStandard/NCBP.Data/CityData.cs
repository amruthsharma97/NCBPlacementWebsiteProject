using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class CityData
    {
        public IEnumerable<CityEntity> Read(bool isActive)
        {
            var List = new List<CityEntity>();
            using (var context = new NCBPEntities())
            {
                foreach (var item in context.Cities.Where(x => x.IsActive == isActive))
                {
                    List.Add(new CityEntity
                    {
                        Id = item.Id,
                        Name = item.Name,
                        StateId = item.StateId,
                        IsActive = true,
                    });
                }
            }
            return List;
        }

        public void Create(CityEntity city)
        {
            using (var context = new NCBPEntities())
            {
                City info = new City
                {
                    Name = city.Name,
                    StateId = city.StateId,
                    IsActive = true
                };
                context.Cities.Add(info);
                context.SaveChanges();
            }
        }

        public void Update(CityEntity city)
        {
            using (var context = new NCBPEntities())
            {
                var info = context.Cities.Single(x => x.Id == city.Id);
                info.Name = city.Name;
                info.StateId = city.StateId;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var city = context.Cities.Single(x => x.Id == id);
                city.IsActive = false; ;
                context.SaveChanges();
            }
        }

        public void UndoDelete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var city = context.Cities.Single(x => x.Id == id);
                city.IsActive = true;
                context.SaveChanges();
            }
        }
    }
}
