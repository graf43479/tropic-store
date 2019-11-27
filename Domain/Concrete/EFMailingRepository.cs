using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFMailingRepository : IMailingRepository
    {
        private EFDbContext context;

        public EFMailingRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Mailing> Mailings {
            get { return context.Mailings; }
        }
        public void SaveMailing(Mailing mailing)
        {
            mailing.UpdateDate = DateTime.Now;

            if (mailing.MailingID == 0)
            {
                context.Mailings.Add(mailing);
            }
            else
            {
                context.Entry(mailing).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void DeleteMailing(Mailing mailing)
        {
            context.Mailings.Remove(mailing);
            context.SaveChanges();
        }

        //public async Task<IEnumerable<Mailing>> GetMailingListAsync()
        //{
        //    return await context.Mailings.ToListAsync().ConfigureAwait(false);
        //}
    }
}
