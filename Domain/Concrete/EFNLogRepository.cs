using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFNLogRepository : INLogRepository
    {
        private EFDbContext context;

        public EFNLogRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IQueryable<NLog_Error> NLogErrors
        {
            get { return context.NLogErrors; } 
        }

        public void DeleteLogData(IEnumerable<NLog_Error> nLogError)
        {

            if (nLogError.Count()>3000)
            {
                context.NLogErrors.RemoveRange(nLogError);
                context.SaveChanges();
            }
            else
            {
           foreach (var logError in nLogError)
            {
                
               context.NLogErrors.Remove(logError);    
}
           context.SaveChanges();
        }
        }

        //public async Task<IEnumerable<NLog_Error>> GetNlogListAsync()
        //{
        //    return await context.NLogErrors.ToListAsync().ConfigureAwait(false);
        //}
    }
}
