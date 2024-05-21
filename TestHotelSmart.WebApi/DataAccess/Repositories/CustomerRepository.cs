using Microsoft.EntityFrameworkCore;
using TestHotelSmart.WebApi.DataAccess.Repositories.Contractos;
using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        #region Fields
        protected readonly TestHotelContext _context;
        #endregion

        #region Builders

        public CustomerRepository(TestHotelContext context)
        {
            _context = context;
        }
        #endregion

        public List<Customer> GetCustomersByDocument(List<string> documenNumbers)
        {
            return _context.Customer.Where(x => documenNumbers.Contains(x.DocumentNumber))
                .Include(x => x.IdDocumentTypeNavigation).Include(x => x.IdGenderNavigation).ToList();
        }

        public Customer? GetCustomerByDocument(string documenNumber)
        {
            return _context.Customer.FirstOrDefault(x => x.DocumentNumber.Contains(documenNumber));
        }

        public void InsertTransactList(List<Customer> customers)
        {
            _context.Customer.AddRange(customers);
        }
    }
}
