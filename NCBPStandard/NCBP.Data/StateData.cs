using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class StateData
    {
        public IEnumerable<StateEntity> Read(bool isActive)
        {
            var List = new List<StateEntity>();
            using (var context = new NCBPEntities())
            {
                foreach (var item in context.States.Where(x => x.IsActive == isActive))
                {
                    List.Add(new StateEntity
                    {
                        Id = item.Id,
                        Name = item.Name,
                        CountryId = item.CountryId,
                        IsActive = true,
                    });
                }
            }
            return List;
        }

        public void Create(StateEntity state)
        {
            using (var context = new NCBPEntities())
            {
                State info = new State
                {
                    Name = state.Name,
                    CountryId = state.CountryId,
                    IsActive = true
                };
                context.States.Add(info);
                context.SaveChanges();
            }
        }

        public void Update(StateEntity state)
        {
            using (var context = new NCBPEntities())
            {
                var info = context.States.Single(x => x.Id == state.Id);
                info.Name = state.Name;
                info.CountryId = state.CountryId;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var state = context.States.Single(x => x.Id == id);
                state.IsActive = false; ;
                context.SaveChanges();
            }
        }

        public void UndoDelete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var state = context.States.Single(x => x.Id == id);
                state.IsActive = true;
                context.SaveChanges();
            }
        }


    }
}
