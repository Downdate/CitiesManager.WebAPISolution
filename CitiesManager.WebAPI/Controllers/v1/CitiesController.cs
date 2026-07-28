using Asp.Versioning;
using CitiesManager.WebAPI.DatabaseContext;
using CitiesManager.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CitiesManager.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/City
        /// <summary>
        /// to get a list of all cities (including CityID and CityName) from Cities table in the database
        /// </summary>
        /// <returns>city</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            return await _context.Cities.OrderBy(c => c.CityName).ToListAsync();
        }

        // GET: api/City/5
        [HttpGet("{cityid}")]
        public async Task<ActionResult<City>> GetCity(System.Guid cityid)
        {
            var city = await _context.Cities.FindAsync(cityid);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/City/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cityid}")]
        public async Task<IActionResult> PutCity(System.Guid? cityid, City city)
        {
            if (cityid != city.CityID)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(cityid))
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

        // POST: api/City
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { cityid = city.CityID }, city);
        }

        // DELETE: api/City/5
        [HttpDelete("{cityid}")]
        public async Task<IActionResult> DeleteCity(System.Guid? cityid)
        {
            var city = await _context.Cities.FindAsync(cityid);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(System.Guid? cityid)
        {
            return _context.Cities.Any(e => e.CityID == cityid);
        }
    }
}
