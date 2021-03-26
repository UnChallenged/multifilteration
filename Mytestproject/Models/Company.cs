using System;
using System.Collections.Generic;

#nullable disable

namespace Mytestproject.Models
{
    public partial class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string EmailId { get; set; }
        public string Website { get; set; }
        public string HowComeToKnow { get; set; }
        public string Others { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public int CountryID { get; set; }
        public int CityID { get; set; }
    }
}
