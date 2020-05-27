using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registry.Models
{
    public class userModel
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
    }
}