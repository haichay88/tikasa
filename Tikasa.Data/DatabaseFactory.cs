using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tikasa.Data
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private tikasaEntities dataContext;
        public tikasaEntities Get()
        {
            return dataContext ?? (dataContext = new tikasaEntities());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
