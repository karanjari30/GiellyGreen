using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface ICompanyProfile
    {
        dynamic GetProfile();
        dynamic PostProfile(dynamic model, int? id);
    }
}
