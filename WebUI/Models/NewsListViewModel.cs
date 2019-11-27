
using System.Collections.Generic;
using Domain.Entities;

namespace WebUI.Models
{
    public class NewsListViewModel
    {
        public IEnumerable<NewsTape> NewsTapes { get; set; }
        public PagingInfo PagingInfo { get; set; }
        //public string CurrentCategory { get; set; }
    }
}