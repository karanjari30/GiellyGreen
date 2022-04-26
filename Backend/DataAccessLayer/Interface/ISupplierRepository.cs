using DataAccessLayer.Model;

namespace DataAccessLayer.Interface
{
    public interface ISupplierRepository
    {
        dynamic GetSuppliers();
        Supplier PostSupplier(Supplier supplier);
        Supplier PutSupplier(Supplier supplier, int id);
        dynamic DeleteSupplier(int id);
    }
}
