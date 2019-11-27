using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface INLogRepository
    {
        IQueryable<NLog_Error> NLogErrors { get; }

        //void SaveNewsTape(NewsTape newsTape);

        void DeleteLogData(IEnumerable<NLog_Error> nLogError);

       // Task<IEnumerable<NLog_Error>> GetNlogListAsync();
    }
}
