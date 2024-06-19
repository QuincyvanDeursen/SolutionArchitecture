using CustomerService.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Database
{
    public class CustomerDbContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Customer> customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), FirstName = "Sybrand", LastName="Bos",CompanyName="ICTutor" , PhoneNumber="0612345678", Address = "Avansstraat 123, 1234AB Breda"},
                new Customer { Id = Guid.NewGuid(), FirstName = "Tristan", LastName="Goossens",CompanyName="Willy's" ,PhoneNumber="0612345678", Address = "Avansstraat 123, 1234AB Breda"},
                new Customer { Id = Guid.NewGuid(), FirstName = "Thimo", LastName="Luijsterburg",CompanyName="Dropshipper" ,PhoneNumber="0612345678", Address = "Avansstraat 123, 1234AB Breda"},
            };

            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().HasData(customers);
        }
    }
}
