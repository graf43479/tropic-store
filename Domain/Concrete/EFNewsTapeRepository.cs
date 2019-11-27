using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
    public class EFNewsTapeRepository : INewsTapeRepository
    {
        private EFDbContext context;

        public EFNewsTapeRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IQueryable<NewsTape> NewsTapes {
            get { return context.NewsTapes; } 
        }

        public void SaveNewsTape(NewsTape newsTape)
        {
            newsTape.UpdateDate = DateTime.Now;

            if (newsTape.NewsID == 0)
            {
                context.NewsTapes.Add(newsTape);
            }
            else
            {
                context.Entry(newsTape).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void DeleteNewsTape(NewsTape newsTape)
        {
            context.NewsTapes.Remove(newsTape);
            context.SaveChanges();
        }

        //public async Task<IEnumerable<NewsTape>> GetNewsTapeListAsync()
        //{
        //    return await context.NewsTapes.ToListAsync().ConfigureAwait(false);
        //}



        public void SaveNewsTape2(NewsTape newsTape)
        {
            newsTape.UpdateDate = DateTime.Now;

            if (newsTape.NewsID == 0)
            {
                context.NewsTapes.Add(newsTape);
            }
            else
            {
                context.Entry(newsTape).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

       
    }
}
