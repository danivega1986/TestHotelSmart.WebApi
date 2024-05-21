using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DataAccess.Repositories.Contractos
{
    internal interface ICustomerRepository
    {
        List<Customer> GetCustomersByDocument(List<string> documenNumbers);
        Customer? GetCustomerByDocument(string documenNumber);
        void InsertTransactList(List<Customer> customers);
    }
}
