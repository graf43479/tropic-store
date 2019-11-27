using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IOrderStatusRepository
    {
        IQueryable<OrderStatus> OrderStatuses { get; }

        void SaveOrderStatus(OrderStatus orderStatus);

        void DeleteOrderStatus(int orderStatusId);

        void DeleteAllOrderStatuses(IEnumerable<OrderStatus> oStatus);

       // Task<IEnumerable<OrderStatus>> GetOrderStatusListAsync();

    }
}
