using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProgrammer.Api.Data.Entities;

namespace TestProgrammer.Api.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesApiController : ControllerBase
    {
        private readonly Contexto _context;

        public ProfilesApiController(Contexto context)
        {
            _context = context;
        }

        // GET: api/ProfilesApi
        [HttpGet]
        public IEnumerable<Profiles> GetProfiles()
        {
            return _context.Profiles;
        }

        // GET: api/ProfilesApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfiles([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profiles = await _context.Profiles.FindAsync(id);

            if (profiles == null)
            {
                return NotFound();
            }

            return Ok(profiles);
        }

        // PUT: api/ProfilesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfiles([FromRoute] int id, [FromBody] Profiles profiles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profiles.ProfileID)
            {
                return BadRequest();
            }

            _context.Entry(profiles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfilesExists(id))
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

        // POST: api/ProfilesApi
        [HttpPost]
        public async Task<IActionResult> PostProfiles([FromBody] Profiles profiles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Profiles.Add(profiles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfiles", new { id = profiles.ProfileID }, profiles);
        }

        // DELETE: api/ProfilesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfiles([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profiles = await _context.Profiles.FindAsync(id);
            if (profiles == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profiles);
            await _context.SaveChangesAsync();

            return Ok(profiles);
        }

        private bool ProfilesExists(int id)
        {
            return _context.Profiles.Any(e => e.ProfileID == id);
        }
    }
}