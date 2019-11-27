using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
    public class EFOrderSummaryRepository : IOrderSummaryRepository
    {
         private EFDbContext context;

        public EFOrderSummaryRepository(EFDbContext context)
        {
            this.context = context;
        }

        public OrdersSummary CreateOrderSummary(ShippingDatails shippingDatails, int userID)
        {
            OrdersSummary orderSummary = new OrdersSummary();
            {
                orderSummary.UserID = userID;
                orderSummary.UserName = shippingDatails.ShippingName;
                orderSummary.Email = shippingDatails.ShippingEmail;
                orderSummary.UserAddress = shippingDatails.ShippingAddress;
                orderSummary.Phone = shippingDatails.ShippingPhone;
                orderSummary.TransactionDate = DateTime.Now;
                orderSummary.OrderNumber = NewOrderNumber();
                orderSummary.ShippingType = "sdasd";
                orderSummary.TotalValue = 0;
                orderSummary.IsActive = true;
                context.OrdersSummaries.Add(orderSummary);
                context.SaveChanges();
                return orderSummary;
            }
        }



        //public async Task<OrdersSummary> CreateOrderSummaryAsync(ShippingDatails shippingDatails, int userID)
        //{
        //    OrdersSummary orderSummary = new OrdersSummary();
        //    {
        //        orderSummary.UserID = userID;
        //        orderSummary.UserName = shippingDatails.ShippingName;
        //        orderSummary.Email = shippingDatails.ShippingEmail;
        //        orderSummary.UserAddress = shippingDatails.ShippingAddress;
        //        orderSummary.Phone = shippingDatails.ShippingPhone;
        //        orderSummary.TransactionDate = DateTime.Now;
        //        orderSummary.OrderNumber = NewOrderNumber();
        //        orderSummary.ShippingType = "sdasd";
        //        orderSummary.TotalValue = 0;
        //        orderSummary.IsActive = true;
        //        context.OrdersSummaries.Add(orderSummary);
        //        await context.SaveChangesAsync().ConfigureAwait(false);
        //        return orderSummary;
        //    }
        //}



        public void DeleteOrderSummary(OrdersSummary os)
        {
            context.OrdersSummaries.Remove(os);
            context.SaveChanges();
        }

       public IQueryable<OrdersSummary> OrdersSummaryInfo
        {
            get { return context.OrdersSummaries; }
            set { }
        }

        //действия по созданию, удалению заказов
        public void SaveOrder(OrdersSummary orderSummary)
        {
            if (orderSummary.OrderSummaryID == 0)
            {
                context.OrdersSummaries.Add(orderSummary);
            }
            else
            {
                context.Entry(orderSummary).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

/*
        public void DeleteOrder(OrdersSummary orderSummary)
        {
            context.OrdersSummaries.Remove(orderSummary);
            context.SaveChanges();
        }
        */
        //создает номер заказа следующий за максимальным
        public int NewOrderNumber()
        {
            try
            {
                int z = OrdersSummary.Max(x => x.OrderNumber);
                return z + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public void RefreshTotalValue(OrdersSummary os)
        {
            decimal totalValue = 0;
            //os.TotalValue=totalValue;
            foreach (var p in context.OrdersDetails.Where(x=>x.OrderSummaryID==os.OrderSummaryID))
            {
                totalValue+=(decimal)p.Price*(decimal)p.Quantity;
            }
            os.TotalValue=totalValue;
            context.SaveChanges();
        }

        public async Task<IEnumerable<OrdersSummary>> GetOrderSummaryListAsync()
        {
            return await context.OrdersSummaries.ToListAsync().ConfigureAwait(false);
        }

        /*   public async Task<List<OrdersSummary>> OrdersSummaryList()
        {
                return await context.OrdersSummaries.ToListAsync();
        }*/

        public IQueryable<OrdersSummary> OrdersSummary
        {
            //get { return context.OrdersSummaries; }
            get { return context.OrdersSummaries; }
        }



        public async Task<OrdersSummary> GetOrderSummaryByIDAsync(int orderSummaryID)
        {
            return await context.OrdersSummaries.FirstOrDefaultAsync(x => x.OrderSummaryID == orderSummaryID);
        }


        

    }
}
