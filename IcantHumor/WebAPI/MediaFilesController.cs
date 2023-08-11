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
    public class MediaFilesController : ControllerBase
    {
        private readonly IHDbContext _context;

        public MediaFilesController(IHDbContext context)
        {
            _context = context;
        }

        // GET: api/MediaFiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaViewModel>>> GetMediaFiles()
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }
            return Ok(await _context.MediaFiles.ToListAsync());
        }

        // GET: api/MediaFiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MediaViewModel>> GetMediaViewModel(Guid id)
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }
            var mediaViewModel = await _context.MediaFiles.Include(c=>c.Categories)
                                                          .FirstOrDefaultAsync(m=>m.Id == id);

            if (mediaViewModel == null)
            {
                return NotFound();
            }

            return Ok(mediaViewModel);
        }

        // GET: api/MediaFiles/GetMediaFilesByCategories/5
        [HttpGet("GetMediaFilesByCategories/{id}")]
        public async Task<ActionResult<IEnumerable<MediaViewModel>>> GetMediaFilesByCategories(Guid id)
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }
            var mediaFiles = await _context.MediaFiles
                .Include(c=>c.Categories)
                .Where(m=>m.Categories.Any(ct=>ct.Id == id))
                .ToListAsync();

            return Ok(mediaFiles);
        }

        // PATCH: api/MediaFiles/PatchCategoryInPost
        [HttpPatch("PatchCategoryInPost/{idPost}")]
        public async Task<ActionResult<MediaViewModel>> PatchCategoryInPost(Guid idPost, IEnumerable<Guid> categoriesIds)
        {
            if (categoriesIds == null)
            {
                return BadRequest();
            }

            try
            {
                var post = await _context.MediaFiles
                    .Include(c => c.Categories)
                    .FirstOrDefaultAsync(u => u.Id == idPost);

                if (post == null)
                {
                    return NotFound();
                }

                var categories = await _context.Categories.Where(x => categoriesIds.Any(y => y == x.Id)).ToListAsync();

                if (categories == null)
                {
                    return NotFound();
                }

                post.Categories = categories;
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMediaViewModel), new { id = post.Id }, post);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving data from the database: {e}");
            }
        }

        // PUT: api/MediaFiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<MediaViewModel>> PutMediaViewModel(Guid id, MediaViewModel mediaViewModel)
        {
            if (id != mediaViewModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(mediaViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaViewModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(mediaViewModel);
        }

        // POST: api/MediaFiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MediaViewModel>> PostMediaViewModel(MediaViewModel mediaViewModel)
        {
            if (_context.MediaFiles == null)
            {
                return Problem("Entity set 'IHDbContext.MediaFiles'  is null.");
            }
            _context.MediaFiles.Add(mediaViewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMediaViewModel", new { id = mediaViewModel.Id }, mediaViewModel);
        }

        // DELETE: api/MediaFiles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MediaViewModel>> DeleteMediaViewModel(Guid id)
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }
            var mediaViewModel = await _context.MediaFiles.FindAsync(id);
            if (mediaViewModel == null)
            {
                return NotFound();
            }

            _context.MediaFiles.Remove(mediaViewModel);
            await _context.SaveChangesAsync();

            return Ok(mediaViewModel);
        }

        private bool MediaViewModelExists(Guid id)
        {
            return (_context.MediaFiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
