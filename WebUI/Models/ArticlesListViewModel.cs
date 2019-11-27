using System.Collections.Generic;
using Domain.Entities;

namespace WebUI.Models
{
    public class ArticlesListViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}