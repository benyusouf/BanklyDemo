using System;

namespace BanklyDemo.Core.Users.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
