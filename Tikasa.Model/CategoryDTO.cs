using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tikasa.Model
{
   public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        public List<CategoryDTO> Childs { get; set; }
    }
}
