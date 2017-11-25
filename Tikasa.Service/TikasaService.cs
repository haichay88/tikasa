using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tikasa.Service
{
   public interface ITikasaService:IUserService, IWebsiteService
    {
    }

    public partial class TikasaService: ITikasaService
    {

    }
}
