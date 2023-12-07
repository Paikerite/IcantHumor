using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IcantHumor.Data;
using IcantHumor.Models;

namespace IcantHumor.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactedUserController : ControllerBase
    {
        private readonly IHDbContext _context;

        public ReactedUserController(IHDbContext context)
        {
            _context = context;
        }

        // GET: api/ReactedUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReactedUserViewModel>>> GetReactedUsers()
        {
          if (_context.ReactedUsers == null)
          {
              return NotFound();
          }
            return Ok(await _context.ReactedUsers.ToListAsync());
        }

        // GET: api/ReactedUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReactedUserViewModel>> GetReactedUserViewModel(Guid id)
        {
          if (_context.ReactedUsers == null)
          {
              return NotFound();
          }
            var reactedUserViewModel = await _context.ReactedUsers.FindAsync(id);

            if (reactedUserViewModel == null)
            {
                return NotFound();
            }

            return Ok(reactedUserViewModel);
        }

        // PUT: api/ReactedUser/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ReactedUserViewModel>> PutReactedUserViewModel(Guid id, ReactedUserViewModel reactedUserViewModel)
        {
            if (id != reactedUserViewModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(reactedUserViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReactedUserViewModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(reactedUserViewModel);
        }

        // POST: api/ReactedUser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReactedUserViewModel>> PostReactedUserViewModel(ReactedUserViewModel reactedUserViewModel)
        {
          if (_context.ReactedUsers == null)
          {
              return Problem("Entity set 'IHDbContext.ReactedUsers'  is null.");
          }
            _context.ReactedUsers.Add(reactedUserViewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReactedUserViewModel", new { id = reactedUserViewModel.Id }, reactedUserViewModel);
        }

        // DELETE: api/ReactedUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReactedUserViewModel>> DeleteReactedUserViewModel(Guid id)
        {
            if (_context.ReactedUsers == null)
            {
                return NotFound();
            }
            var reactedUserViewModel = await _context.ReactedUsers.FindAsync(id);
            if (reactedUserViewModel == null)
            {
                return NotFound();
            }

            _context.ReactedUsers.Remove(reactedUserViewModel);
            await _context.SaveChangesAsync();

            return Ok(reactedUserViewModel);
        }

        private bool ReactedUserViewModelExists(Guid id)
        {
            return (_context.ReactedUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
