﻿namespace CustomerService.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

    }
}
