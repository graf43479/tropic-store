using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IOrderSummaryRepository
    {
        IQueryable<OrdersSummary> OrdersSummaryInfo { get; set; }

        OrdersSummary CreateOrderSummary(ShippingDatails shippingDatails, int userID);

        void SaveOrder(OrdersSummary order);

      //  void DeleteOrder(OrdersSummary order);

        int NewOrderNumber();

        void DeleteOrderSummary(OrdersSummary os);

        void RefreshTotalValue(OrdersSummary os);

    //    Task<IEnumerable<OrdersSummary>> GetOrderSummaryListAsync();

     //   Task<OrdersSummary> GetOrderSummaryByIDAsync(int orderSummaryID);

      //  Task<OrdersSummary> CreateOrderSummaryAsync(ShippingDatails shippingDatails, int userID);

       
    }
}
