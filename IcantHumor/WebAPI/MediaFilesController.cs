using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IcantHumor.Data;
using IcantHumor.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            return Ok(await _context.MediaFiles
                .Include(c => c.Categories)
                .Include(r => r.WhoReacted)
                .ToListAsync());
            //return Ok(await _context.MediaFiles.ToListAsync());
        }

        // GET: api/MediaFiles/GetCountMediaFiles
        [HttpGet("GetCountMediaFiles")]
        public async Task<ActionResult<int>> GetCountMediaFiles()
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }

            var mediaFiles = await _context.MediaFiles.ToListAsync();

            return Ok(mediaFiles.Count);
        }

        // GET: api/MediaFiles/GetCountMediaFilesIncludeCategories/568&595&999
        [HttpGet("GetCountMediaFilesIncludeCategories/{categoriesStr}")]
        public async Task<ActionResult<int>> GetCountMediaFilesIncludeCategories(string categoriesStr)
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }

            var categoryGuids = categoriesStr.Split('&')
                    .Select(s => Guid.Parse(s))
                    .ToList();

            var mediaFiles = await _context.MediaFiles
                    .Include(c => c.Categories)
                    .Where(m => m.Categories.Any(c => categoryGuids.Contains(c.Id)))
                    .ToListAsync();

            return Ok(mediaFiles.Count);
        }

        // GET: api/MediaFiles/GetCountMediaFilesBySearch/egg
        [HttpGet("GetCountMediaFilesBySearch/{SearchText}")]
        public async Task<ActionResult<int>> GetCountMediaFilesBySearch(string SearchText)
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }

            var mediaFiles = await _context.MediaFiles
                    .Include(c => c.Categories)
                    .Where(m => m.Title.ToUpper().Contains(SearchText.ToUpper()) ||
                    m.Categories.Any(c => c.Name.ToUpper().Contains(SearchText.ToUpper())))
                    .ToListAsync();

            return Ok(mediaFiles.Count);
        }

        // GET: api/MediaFiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MediaViewModel>> GetMediaViewModel(Guid id)
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }
            var mediaViewModel = await _context.MediaFiles.Include(c => c.Categories).ThenInclude(p => p.Posts)
                                                          .Include(r => r.WhoReacted)
                                                          .FirstOrDefaultAsync(m => m.Id == id);

            if (mediaViewModel == null)
            {
                return NotFound();
            }

            return Ok(mediaViewModel);
        }

        //GET: api/MediaFiles/GetMediaPerPage/2/8
        [HttpGet("GetMediaPerPage/{page}/{itemsPerPage}")]
        public async Task<ActionResult<IEnumerable<MediaViewModel>>> GetMediaPerPage(int page, int itemsPerPage)
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }
            if (page == 0 || page == 1)
            {
                return Ok(await _context.MediaFiles
                        .OrderByDescending(c => c.DateUpload)
                        .AsSingleQuery()
                        .Include(c => c.Categories)
                        .Include(r => r.WhoReacted)
                        .Take(itemsPerPage)
                        .ToListAsync());

            }
            else
            {
                int index = page * itemsPerPage - itemsPerPage;
                return Ok(await _context.MediaFiles
                        .OrderByDescending(c => c.DateUpload)
                        .AsSingleQuery()
                        .Include(c => c.Categories)
                        .Include(r => r.WhoReacted)
                        .Skip(index).Take(itemsPerPage)
                        .ToListAsync());
            }
        }

        //GET: api/MediaFiles/GetCategorizedMediaPerPage/2/8/546&5395&384
        [HttpGet("GetCategorizedMediaPerPage/{page}/{itemsPerPage}/{categoriesStr}")]
        public async Task<ActionResult<IEnumerable<MediaViewModel>>> GetCategorizedMediaPerPage(int page, int itemsPerPage, string categoriesStr)
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }

            var categoryGuids = categoriesStr.Split('&')
                                .Select(s => Guid.Parse(s))
                                .ToList();

            if (page == 0 || page == 1)
            {
                return Ok(await _context.MediaFiles
                    .OrderByDescending(c => c.DateUpload)
                        .AsSingleQuery()
                        .Include(c => c.Categories)
                        .Include(r => r.WhoReacted)
                        .Where(m => m.Categories.Any(c => categoryGuids.Contains(c.Id)))
                        .Take(itemsPerPage)
                        .ToListAsync());

            }
            else
            {
                int index = page * itemsPerPage - itemsPerPage;
                return Ok(await _context.MediaFiles
                    .OrderByDescending(c => c.DateUpload)
                        .AsSingleQuery()
                        .Include(c => c.Categories)
                        .Include(r => r.WhoReacted)
                        .Where(m => m.Categories.Any(c => categoryGuids.Contains(c.Id)))
                        .Skip(index).Take(itemsPerPage)
                        .ToListAsync());
            }
        }

        //GET: api/MediaFiles/GetMediaFilesByCategories/
        [HttpGet("GetMediaFilesByCategories/{categoriesStr}")]
        public async Task<ActionResult<IEnumerable<MediaViewModel>>> GetMediaFilesByCategories(string categoriesStr)
        {

            if (_context.MediaFiles == null)
            {
                return NotFound();
            }

            var categoryGuids = categoriesStr.Split('&')
                                            .Select(s => Guid.Parse(s))
                                            .ToList();

            return Ok(await _context.MediaFiles
                .OrderByDescending(c => c.DateUpload)
                .Include(r => r.WhoReacted)
                .Include(c => c.Categories)
                .Where(m => m.Categories.Any(c => categoryGuids.Contains(c.Id)))
                .ToListAsync());
        }

        //GET: api/MediaFiles/GetMediaFilesByName/egg
        [HttpGet("GetMediaFilesByName/{SearchText}")]
        public async Task<ActionResult<IEnumerable<MediaViewModel>>> GetMediaFilesByName(string SearchText)
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                return CreatedAtAction(nameof(GetMediaFiles), SearchText);
            }

            var posts = await _context.MediaFiles
                .OrderByDescending(c => c.DateUpload)
                .Include(r => r.WhoReacted)
                .Include(c => c.Categories)
                .Where(m => m.Title.Trim().ToUpper().Contains(SearchText.Trim().ToUpper()) ||
                m.Categories.Any(c => c.Name.Trim().ToUpper().Contains(SearchText.Trim().ToUpper())))
                .ToListAsync();

            return Ok(posts);
        }

        //GET: api/MediaFiles/GetMediaFilesByNameByPages/egg/2/8
        [HttpGet("GetMediaFilesByNameByPages/{SearchText}/{page}/{itemsPerPage}")]
        public async Task<ActionResult<IEnumerable<MediaViewModel>>> GetMediaFilesByNameByPages(string SearchText, int page, int itemsPerPage)
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                return CreatedAtAction(nameof(GetMediaPerPage), page, itemsPerPage);
            }

            if (page == 0 || page == 1)
            {
                return(await _context.MediaFiles
                    .OrderByDescending(c => c.DateUpload)
                    .AsSingleQuery()
                    .Include(r => r.WhoReacted)
                    .Include(c => c.Categories)
                    .Where(m => m.Title.Trim().ToUpper().Contains(SearchText.Trim().ToUpper()) ||
                    m.Categories.Any(c => c.Name.Trim().ToUpper().Contains(SearchText.Trim().ToUpper())))
                    .Take(itemsPerPage)
                    .ToListAsync());
            }
            else
            {
                int index = page * itemsPerPage - itemsPerPage;
                return Ok(await _context.MediaFiles
                    .OrderByDescending(c => c.DateUpload)
                    .AsSingleQuery()
                    .Include(r => r.WhoReacted)
                    .Include(c => c.Categories)
                    .Where(m => m.Title.Trim().ToUpper().Contains(SearchText.Trim().ToUpper()) ||
                    m.Categories.Any(c => c.Name.Trim().ToUpper().Contains(SearchText.Trim().ToUpper())))
                    .Skip(index).Take(itemsPerPage)
                    .ToListAsync());
            }
        }

        // GET: api/MediaFiles/GetMediaFilesByCategory/5
        [HttpGet("GetMediaFilesByCategory/{id}")]
        public async Task<ActionResult<IEnumerable<MediaViewModel>>> GetMediaFilesByCategory(Guid id)
        {
            if (_context.MediaFiles == null)
            {
                return NotFound();
            }
            var mediaFiles = await _context.MediaFiles
                .OrderByDescending(c => c.DateUpload)
                .Include(c => c.Categories)
                .Where(m => m.Categories.Any(ct => ct.Id == id))
                .ToListAsync();

            return Ok(mediaFiles);
        }

        // PATCH: api/MediaFiles/PatchCategoryInPost/5
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

        // PATCH: api/MediaFiles/MakeReactionInPost/7
        [HttpPatch("MakeReactionInPost/{idPost}")]
        public async Task<ActionResult<MediaViewModel>> MakeReactionInPost(Guid idPost, ReactedUserViewModel reactedUser)
        {
            if (reactedUser == null)
            {
                return BadRequest();
            }

            try
            {
                var post = await _context.MediaFiles
                    .Include(c => c.WhoReacted)
                    .FirstOrDefaultAsync(u => u.Id == idPost);

                if (post == null)
                {
                    return NotFound();
                }

                post.WhoReacted.Add(reactedUser);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMediaViewModel), new { id = post.Id }, post);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving data from the database: {e}");
            }
        }

        // PATCH: api/MediaFiles/UnMakeReactionInPost/7
        [HttpPatch("UnMakeReactionInPost/{idPost}")]
        public async Task<ActionResult<MediaViewModel>> UnMakeReactionInPost(Guid idPost, Guid reactedUserId)
        {
            if (reactedUserId == null)
            {
                return BadRequest();
            }

            try
            {
                var post = await _context.MediaFiles
                    .Include(c => c.WhoReacted)
                    .FirstOrDefaultAsync(u => u.Id == idPost);

                if (post == null)
                {
                    return NotFound();
                }

                var reac = post.WhoReacted.FirstOrDefault(i => i.Id == reactedUserId);
                if (reac != null)
                {
                    post.WhoReacted.Remove(reac);
                }
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
            //_context.Update(mediaViewModel);

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
            var mediaViewModel = await _context.MediaFiles.Include(c => c.Categories).ThenInclude(p => p.Posts)
                                                          .Include(r => r.WhoReacted)
                                                          .FirstOrDefaultAsync(m => m.Id == id);
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
