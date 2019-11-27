using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IOrderDetailsRepository
    {
        IQueryable<OrderDetails> OrdersDetails { get; }

        void CreateOrderDetails(OrderDetails orderDetails);

        void DeleteOrderDetail(OrderDetails ord);

        void SaveOrderDetails(OrderDetails orderDetails);

       // Task<IEnumerable<OrderDetails>> GetOrderDetailsListAsync();
      
    }
}
