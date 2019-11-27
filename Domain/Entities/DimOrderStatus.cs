using System.Collections.Generic;


namespace Domain.Entities
{
    public class DimOrderStatus
    {
        public int DimOrderStatusID { get; set; }
        public string OrderStatusDesc { get; set; }
        public int Sequence { get; set; }

        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }  

    }
 }
