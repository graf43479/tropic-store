using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
    public class EFOrderStatusRepository : IOrderStatusRepository
    {
        private EFDbContext context;

        public EFOrderStatusRepository(EFDbContext context)
        {
            this.context = context;
        }
        
        public IQueryable<OrderStatus> OrderStatuses {
            get { return context.OrderStatuses; }
        }



        public void SaveOrderStatus(OrderStatus orderStatus)
        {
            if (orderStatus.OrderStatusID == 0)
            {
                context.OrderStatuses.Add(orderStatus);
            }
            else
            {
                context.Entry(orderStatus).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

    


        // public void DeleteOrderStatus(OrderStatus orderStatus)
        public void DeleteOrderStatus(int orderStatusId)
        {
            OrderStatus orderStatus = context.OrderStatuses.FirstOrDefault(x => x.OrderStatusID == orderStatusId);
            context.OrderStatuses.Remove(orderStatus);
            context.SaveChanges();
        }

        public void DeleteAllOrderStatuses(IEnumerable<OrderStatus> oStatus)
        {
            foreach (var tmp in oStatus)
            {
                context.Entry(tmp).State=EntityState.Deleted;
            }
            context.SaveChanges();
        }

        //public async Task<IEnumerable<OrderStatus>> GetOrderStatusListAsync()
        //{
        //    return await context.OrderStatuses.ToListAsync().ConfigureAwait(false);
        //}
    }
}
