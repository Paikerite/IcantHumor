using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IcantHumor.Data;
using IcantHumor.Models;

namespace IcantHumor.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly IHDbContext _context;

        public FavouritesController(IHDbContext context)
        {
            _context = context;
        }

        // GET: api/Favourites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavouriteViewModel>>> GetFavourites()
        {
            return await _context.Favourites.ToListAsync();
        }

        // GET: api/Favourites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavouriteViewModel>> GetFavouriteViewModel(Guid id)
        {
            var favouriteViewModel = await _context.Favourites.FindAsync(id);

            if (favouriteViewModel == null)
            {
                return NotFound();
            }

            return favouriteViewModel;
        }

        [HttpGet("GetCountFavouritesByUser/{idUser}/{countFav}")]
        public async Task<ActionResult<IEnumerable<FavouriteViewModel>>> GetCountFavouritesByUser(Guid idUser, int countFav)
        {
            var favouriteViewModel = await _context.Favourites
                .Include(b=>b.FavMedia)
                .Where(a=> a.IdReactedUser == idUser)
                .Take(countFav)
                .ToListAsync();

            if (favouriteViewModel == null)
            {
                return NotFound();
            }

            return favouriteViewModel;
        }

        [HttpGet("GetFavouritesByUser/{idUser}")]
        public async Task<ActionResult<IEnumerable<FavouriteViewModel>>> GetFavouritesByUser(Guid idUser)
        {
            var favouriteViewModel = await _context.Favourites
                .Include(b => b.FavMedia)
                .Where(a => a.IdReactedUser == idUser)
                .ToListAsync();

            if (favouriteViewModel == null)
            {
                return NotFound();
            }

            return favouriteViewModel;
        }

        // PUT: api/Favourites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<FavouriteViewModel>> PutFavouriteViewModel(Guid id, FavouriteViewModel favouriteViewModel)
        {
            if (id != favouriteViewModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(favouriteViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavouriteViewModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(favouriteViewModel);
        }

        // POST: api/Favourites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavouriteViewModel>> PostFavouriteViewModel(FavouriteViewModel favouriteViewModel)
        {
            _context.Favourites.Add(favouriteViewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavouriteViewModel", new { id = favouriteViewModel.Id }, favouriteViewModel);
        }

        // DELETE: api/Favourites/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FavouriteViewModel>> DeleteFavouriteViewModel(Guid id)
        {
            var favouriteViewModel = await _context.Favourites.FindAsync(id);
            if (favouriteViewModel == null)
            {
                return NotFound();
            }

            _context.Favourites.Remove(favouriteViewModel);
            await _context.SaveChangesAsync();

            return Ok(favouriteViewModel);
        }

        private bool FavouriteViewModelExists(Guid id)
        {
            return _context.Favourites.Any(e => e.Id == id);
        }
    }
}
