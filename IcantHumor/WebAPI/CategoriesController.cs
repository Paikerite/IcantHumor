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
    public class CategoriesController : ControllerBase
    {
        private readonly IHDbContext _context;

        public CategoriesController(IHDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetCategories()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            return Ok(await _context.Categories.ToListAsync());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryViewModel>> GetCategoryViewModel(Guid id)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            var categoryViewModel = await _context.Categories.FindAsync(id);

            if (categoryViewModel == null)
            {
                return NotFound();
            }

            return Ok(categoryViewModel);
        }

        //GET: api/Categories/VideoGame
        [HttpGet("GetCategoryByName/{name}")]
        public async Task<ActionResult<CategoryViewModel>> GetCategoryViewModelByName(string name)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var categoryViewModel = await _context.Categories.FirstOrDefaultAsync(x => x.Name == name);

            if (categoryViewModel == null)
            {
                return NotFound();
            }

            return Ok(categoryViewModel);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryViewModel>> PutCategoryViewModel(Guid id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoryViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryViewModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(categoryViewModel);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> PostCategoryViewModel(CategoryViewModel categoryViewModel)
        {
          if (_context.Categories == null)
          {
              return Problem("Entity set 'IHDbContext.Categories'  is null.");
          }
            _context.Categories.Add(categoryViewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryViewModel", new { id = categoryViewModel.Id }, categoryViewModel);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryViewModel>> DeleteCategoryViewModel(Guid id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var categoryViewModel = await _context.Categories.FindAsync(id);
            if (categoryViewModel == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryViewModel);
            await _context.SaveChangesAsync();

            return Ok(categoryViewModel);
        }

        private bool CategoryViewModelExists(Guid id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
