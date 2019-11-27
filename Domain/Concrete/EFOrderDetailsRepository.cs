using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
    public class EFOrderDetailsRepository : IOrderDetailsRepository
    {
        private EFDbContext context;

        public EFOrderDetailsRepository(EFDbContext context)
        {
            this.context = context;
        }

        
        public void CreateOrderDetails(OrderDetails orderDetails)
        {
            context.OrdersDetails.Add(orderDetails);
            context.SaveChanges();
        }

        public void DeleteOrderDetail(OrderDetails ord)
        {
            context.OrdersDetails.Remove(ord);
            context.SaveChanges();
        }

        public void SaveOrderDetails(OrderDetails orderDetails)
        {
            if (orderDetails.OrderDetailsID == 0)
            {
                context.OrdersDetails.Add(orderDetails);
            }
            else
            {
                context.Entry(orderDetails).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        //public async Task<IEnumerable<OrderDetails>> GetOrderDetailsListAsync()
        //{
        //    return await context.OrdersDetails.ToListAsync().ConfigureAwait(false);
        //}

        public IQueryable<OrderDetails> OrdersDetails
        {

            get
            {
                context.Products.Include("Category").Select(x=>x.Category.Name);
                return context.OrdersDetails.Include("Product").Include("OrdersSummary"); //.Include("Category"); }
            }
        }
        
        
    }
}

