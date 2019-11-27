using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<DasboardOrderStatusCountModel> DasboardOrderStatusCount { get; set; }
        public IEnumerable<DasboardOrderStatusChargeModel> DasboardOrderStatusCharges { get; set; }
        public IEnumerable<DasboardUsersCount> DasboardUsersRegistered { get; set; }
        public IEnumerable<DasboardBestsellersViewModel> DasboardBestsellerByRevenue { get; set; }
        public IEnumerable<DasboardBestsellersViewModel> DasboardBestsellerByQuantity { get; set; }
        public IEnumerable<DasboardBestsellersViewModel> DasboardGoodsByExpensive { get; set; }
        public IEnumerable<DasboardBestsellersViewModel> DasboardGoodsByCheapness { get; set; }
        public IEnumerable<DasboardBestsellersViewModel> DasboardProductExistence { get; set; }
        public IEnumerable<DasboardBestsellersViewModel> DasboardProductsUnsale { get; set; }
        public IEnumerable<DasboardBestsellersViewModel> DasboardInRoleCount { get; set; }
    }
}