using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IMailingRepository
    {
        IQueryable<Mailing> Mailings { get; }

        void SaveMailing(Mailing mailing);

        void DeleteMailing(Mailing mailing);

      //  Task<IEnumerable<Mailing>> GetMailingListAsync();
    }
}
