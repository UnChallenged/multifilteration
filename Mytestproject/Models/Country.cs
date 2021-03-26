using System;
using System.Collections.Generic;

#nullable disable

namespace Mytestproject.Models
{
    public partial class Country
    {
        public int Id { get; set; }
        public string RegionCode { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}
