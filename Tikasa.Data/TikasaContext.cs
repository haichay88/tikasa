using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tikasa.Data
{
    public partial class tikasaEntities : DbContext
    {
        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
