using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IDimOrderStatusRepository
    {
        IQueryable<DimOrderStatus> DimOrderStatuses { get; }

        void SaveDimOrderStatus(DimOrderStatus dimOrderStatus);

        void DeleteDimOrderStatus(DimOrderStatus dimOrderStatus);

        void DimOrderStatusSequence(int dimOrderStatusId, string actionType);

        void UpdateDimOrderStatusSequence();

       // Task<IEnumerable<DimOrderStatus>> GetDimOrderStatusListAsync();
    }
}
