using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registry.Models
{
    public class ChargeModel
    {
        public int id { get; set; }
        public string number { get; set; }
        public string destination { get; set; }
        public int page { get; set; }
        public System.DateTime date { get; set; }
    }
}