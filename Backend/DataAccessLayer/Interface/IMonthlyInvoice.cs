using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IMonthlyInvoice
    {
        dynamic GetMonthlyInvoices(int month, int year);
        dynamic PostMonthlyInvoice(dynamic model);
        dynamic PatchApproveSupplier(List<int> id, int month, int year);
    }
}
