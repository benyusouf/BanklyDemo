using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanklyDemo.UIApi.Models
{
    public class TwilloCredentials
    {
        public string Phone { get; set; }

        public string Sid { get; set; }

        public string Auth { get; set; }
    }
}
