using BanklyDemo.Core.Common.Models;
using System;

namespace BanklyDemo.Core.Users.Models
{
    public class UserEntity: BaseEntity
    {
        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedTimeUtc { get; set; }

        public LoginProfile LoginProfile { get; set; }
    }

}
