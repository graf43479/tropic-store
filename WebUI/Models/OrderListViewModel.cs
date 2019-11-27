
using System.Collections.Generic;

using Domain.Entities;

namespace WebUI.Models
{
    public class OrderListViewModel
    {
        public IEnumerable<OrdersSummary> OrdersSummaries  { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}

