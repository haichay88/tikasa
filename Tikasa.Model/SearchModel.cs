using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tikasa.Model
{
   public class SearchModel
    {
        public SearchModel() {
            this.Page = new PagingModel();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Keyword { get; set; }
        public PagingModel Page { get; set; }

    }
    public class PagingModel
    {
        public PagingModel()
        {
            this.currentPage = 1;
            this.pageSize = 10;
        }
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int total { get; set; }
    }
}
