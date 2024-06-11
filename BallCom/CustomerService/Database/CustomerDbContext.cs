using CustomerService.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Database
{
    public class CustomerDbContext : DbContext
    {

        public DbSet<Customer>? Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "S Doe", Email = "customer1@avans.nl", Address = "AvansStraat", City="Breda",State="Nederland", Zip="1234AB"},
                new Customer { Id = 2, Name = "J Doe", Email = "customer2@avans.nl", Address = "AvansStraat", City="Breda",State="Nederland", Zip="1234AB"},
                new Customer { Id = 3, Name = "Q Doe", Email = "customer3@avans.nl", Address = "AvansStraat", City="Breda",State="Nederland", Zip="1234AB"}
            };

            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().HasData(customers);
        }
    }
}
