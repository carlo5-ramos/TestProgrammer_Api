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
    public class PositionsApiController : ControllerBase
    {
        private readonly Contexto _context;

        public PositionsApiController(Contexto context)
        {
            _context = context;
        }

        // GET: api/PositionsApi
        [HttpGet]
        public IEnumerable<Positions> GetPositions()
        {
            return _context.Positions;
        }

        // GET: api/PositionsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPositions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var positions = await _context.Positions.FindAsync(id);

            if (positions == null)
            {
                return NotFound();
            }

            return Ok(positions);
        }

        // PUT: api/PositionsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositions([FromRoute] int id, [FromBody] Positions positions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != positions.PositionID)
            {
                return BadRequest();
            }

            _context.Entry(positions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionsExists(id))
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

        // POST: api/PositionsApi
        [HttpPost]
        public async Task<IActionResult> PostPositions([FromBody] Positions positions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Positions.Add(positions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPositions", new { id = positions.PositionID }, positions);
        }

        // DELETE: api/PositionsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var positions = await _context.Positions.FindAsync(id);
            if (positions == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(positions);
            await _context.SaveChangesAsync();

            return Ok(positions);
        }

        private bool PositionsExists(int id)
        {
            return _context.Positions.Any(e => e.PositionID == id);
        }
    }
}