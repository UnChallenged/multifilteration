using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mytestproject.Models
{
    public partial class MainDataTable
    {
        //public MainDataTable()
        //{
        //    CompanyName = new HashSet<Company>();
        //}
        public string CompanyName { get; set; }
       // public string CompanyName { get; set; }
        public virtual IEnumerable<Country> CountryName { get; set; }
        public virtual IEnumerable<City> CityName { get; set; }
        public virtual IEnumerable<User> User { get; set; }
    }
}
