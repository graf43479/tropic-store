using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IDimShippingRepository
    {
        IQueryable<DimShipping> DimShipping { get; }

        void SaveDimShipping(DimShipping dimShipping);

        void DeleteDimShipping(DimShipping dimShipping);

      //  Task<IEnumerable<DimShipping>> GetDimShippingListAsync();
    }
}
