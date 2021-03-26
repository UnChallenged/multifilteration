using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mytestproject.Models;

namespace Mytestproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly mytestdbContext _context;

        public CountriesController(mytestdbContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }
        [HttpGet]
        [Route("maintable")]
        public async Task<ActionResult<IEnumerable<MainDataTable>>> GetMainDataTable(string country, string company, string user)
        {
            var separatedcountry = new string[0];
            var separatedcompany = new string[0];
            var separateduser = new string[0];

            List<int> parsedcountry = new List<int>();
            List<int> parsedcompany = new List<int>();
            List<int> parseduser = new List<int>();

            if (country != "" && country != null)
            {
                separatedcountry = country.Split(new char[] { ',' });
                parsedcountry = separatedcountry.Select(s => int.Parse(s)).ToList();
            }
            if (company != "" && company != null)
            {
                separatedcompany = company.Split(new char[] { ',' });
                parsedcompany = separatedcompany.Select(s => int.Parse(s)).ToList();
            }
            if (user != "" && user != null)
            {
                separateduser = user.Split(new char[] { ',' });
                parseduser = separateduser.Select(s => int.Parse(s)).ToList();
            }

            if (parsedcountry.Any() && parsedcompany.Any() && parseduser.Any())
            {
                return await (from comp in _context.Companies

                              where parsedcompany.Contains(comp.CompanyId)
                              select new MainDataTable
                              {
                                  CompanyName = comp.Name,
                                  CountryName = from con in _context.Countries
                                                where con.Id == comp.CountryID
                                                && parsedcountry.Contains(con.Id)
                                                select new Country
                                                {
                                                    CountryName = con.CountryName
                                                },
                                  CityName = from cit in _context.Cities
                                             where cit.Id == comp.CityID
                                             select new City
                                             {
                                                 City1 = cit.City1
                                             },
                                  User = from usr in _context.Users
                                         where usr.CompanyId == comp.CompanyId
                                         && parseduser.Contains(usr.UserId)
                                         select new User
                                         {
                                             Name = usr.Name
                                         }

                              }).ToListAsync();

            }
            else if (parsedcompany.Any() && !parsedcountry.Any() && !parseduser.Any())
            {
                return await (from comp in _context.Companies

                              where parsedcompany.Contains(comp.CompanyId)
                              select new MainDataTable
                              {
                                  CompanyName = comp.Name,
                                  CountryName = from con in _context.Countries
                                                where con.Id == comp.CountryID
                                                select new Country
                                                {
                                                    CountryName = con.CountryName
                                                },
                                  CityName = from cit in _context.Cities
                                             where cit.Id == comp.CityID
                                             select new City
                                             {
                                                 City1 = cit.City1
                                             },
                                  User = from usr in _context.Users
                                         where usr.CompanyId == comp.CompanyId
                                         select new User
                                         {
                                             Name = usr.Name
                                         }

                              }).ToListAsync();
            }
            else if (!parsedcompany.Any() && parsedcountry.Any() && !parseduser.Any())
            {
                return await (from comp in _context.Companies
                              where parsedcountry.Contains(comp.CountryID)

                              select new MainDataTable
                              {
                                  CompanyName = comp.Name,
                                  CountryName = from con in _context.Countries
                                                where con.Id == comp.CountryID
                                                && parsedcountry.Contains(con.Id)
                                                select new Country
                                                {
                                                    CountryName = con.CountryName
                                                },
                                  CityName = from cit in _context.Cities
                                             where cit.Id == comp.CityID
                                             select new City
                                             {
                                                 City1 = cit.City1
                                             },
                                  User = from usr in _context.Users
                                         where usr.CompanyId == comp.CompanyId
                                         select new User
                                         {
                                             Name = usr.Name
                                         }

                              }).ToListAsync();
            }
            else if (!parsedcompany.Any() && !parsedcountry.Any() && parseduser.Any())
            {
                return await (from comp in _context.Companies
                              
                              join u in _context.Users
                              on comp.CompanyId equals u.CompanyId

                              where parseduser.Contains(u.UserId) &&
                              comp.CompanyId == u.CompanyId

                              select new MainDataTable
                              {
                                  CompanyName = comp.Name,
                                  CountryName = from con in _context.Countries
                                                where con.Id == comp.CountryID

                                                select new Country
                                                {
                                                    CountryName = con.CountryName
                                                },
                                  CityName = from cit in _context.Cities
                                             where cit.Id == comp.CityID
                                             select new City
                                             {
                                                 City1 = cit.City1
                                             },
                                  User = from usr in _context.Users
                                         where
                                         parseduser.Contains(usr.UserId) &&
                                         comp.CompanyId == usr.CompanyId

                                         select new User
                                         {
                                             Name = usr.Name
                                         }

                              }).ToListAsync();
            }
            else if (parsedcompany.Any() && parsedcountry.Any())
            {
                return await (from comp in _context.Companies

                              where parsedcompany.Contains(comp.CompanyId)
                              select new MainDataTable
                              {
                                  CompanyName = comp.Name,
                                  CountryName = from con in _context.Countries
                                                where con.Id == comp.CountryID
                                                && parsedcountry.Contains(con.Id)
                                                select new Country
                                                {
                                                    CountryName = con.CountryName
                                                },
                                  CityName = from cit in _context.Cities
                                             where cit.Id == comp.CityID
                                             select new City
                                             {
                                                 City1 = cit.City1
                                             },
                                  User = from usr in _context.Users
                                         where usr.CompanyId == comp.CompanyId

                                         select new User
                                         {
                                             Name = usr.Name
                                         }

                              }).ToListAsync();
            }
            else if (parsedcompany.Any() && parseduser.Any())
            {
                return await (from comp in _context.Companies

                              where parsedcompany.Contains(comp.CompanyId)
                              select new MainDataTable
                              {
                                  CompanyName = comp.Name,
                                  CountryName = from con in _context.Countries
                                                where con.Id == comp.CountryID
                                                select new Country
                                                {
                                                    CountryName = con.CountryName
                                                },
                                  CityName = from cit in _context.Cities
                                             where cit.Id == comp.CityID
                                             select new City
                                             {
                                                 City1 = cit.City1
                                             },
                                  User = from usr in _context.Users
                                         where usr.CompanyId == comp.CompanyId
                                         && parseduser.Contains(usr.UserId)
                                         select new User
                                         {
                                             Name = usr.Name
                                         }

                              }).ToListAsync();
            }
            else if (parsedcountry.Any() && parseduser.Any())
            {
                return await (from comp in _context.Companies

                              where parsedcountry.Contains(comp.CountryID)
                              select new MainDataTable
                              {
                                  CompanyName = comp.Name,
                                  CountryName = from con in _context.Countries
                                                where con.Id == comp.CountryID
                                                && parsedcountry.Contains(con.Id)
                                                select new Country
                                                {
                                                    CountryName = con.CountryName
                                                },
                                  CityName = from cit in _context.Cities
                                             where cit.Id == comp.CityID
                                             select new City
                                             {
                                                 City1 = cit.City1
                                             },
                                  User = from usr in _context.Users
                                         where usr.CompanyId == comp.CompanyId
                                         && parseduser.Contains(usr.UserId)
                                         select new User
                                         {
                                             Name = usr.Name
                                         }

                              }).ToListAsync();
            }
            else
            {
                return await (from comp in _context.Companies

                                  // where parsedcompany.Contains(comp.CompanyId)
                              select new MainDataTable
                              {
                                  CompanyName = comp.Name,
                                  CountryName = from con in _context.Countries
                                                where con.Id == comp.CountryID
                                                // && parsedcountry.Contains(con.Id)
                                                select new Country
                                                {
                                                    CountryName = con.CountryName
                                                },
                                  CityName = from cit in _context.Cities
                                             where cit.Id == comp.CityID
                                             select new City
                                             {
                                                 City1 = cit.City1
                                             },
                                  User = from usr in _context.Users
                                         where usr.CompanyId == comp.CompanyId
                                         // && parseduser.Contains(usr.UserId)
                                         select new User
                                         {
                                             Name = usr.Name
                                         }

                              }).ToListAsync();
            }
        }
        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return country;
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
