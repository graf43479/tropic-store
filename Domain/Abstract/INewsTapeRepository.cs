using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
   public interface INewsTapeRepository
    {
        IQueryable<NewsTape> NewsTapes { get; }

        void SaveNewsTape(NewsTape newsTape);

        void DeleteNewsTape(NewsTape newsTape);

     //   Task<IEnumerable<NewsTape>> GetNewsTapeListAsync();

    }
}
