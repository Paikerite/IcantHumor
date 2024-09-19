using IcantHumor.Data;
using IcantHumor.Models;
using IcantHumor.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IcantHumor.Services
{
    public class UsersService : IUsersService
    {
        private readonly IHDbContext _context;
        public UsersService(IHDbContext context)
        {
            _context = context;
        }

        public async Task<UserViewModel?> DeleteUser(Guid id)
        {
            if (_context.Users == null)
            {
                return default;
            }
            var userViewModel = await _context.Users.FindAsync(id);
            if (userViewModel == null)
            {
                return default;
            }

            _context.Users.Remove(userViewModel);
            await _context.SaveChangesAsync();

            return userViewModel;
        }

        public async Task<UserViewModel?> GetUser(Guid id, bool FullInfo = false)
        {
            if (_context.Users == null)
            {
                return default;
            }

            UserViewModel? userViewModel;
            if (FullInfo)
            {
                userViewModel = await _context.Users.Include(u=>u.FullUserInfo)
                                                    .Include(d=>d.Favourites)
                                                    .FirstOrDefaultAsync(d=>d.Id == id);
            }
            else
            {
                userViewModel = await _context.Users.FindAsync(id);
            }

            if (userViewModel == null)
            {
                return default;
            }

            return userViewModel;
        }

        public async Task<UserViewModel?> ForbidUserAccess(Guid guid)
        {
            if (_context.Users == null)
            {
                return default;
            }

            var user = await _context.Users.FindAsync(guid);
            if (user != null)
            {
                user.Role = Models.Enums.Roles.Forbidden;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(guid))
                {
                    return default;
                }
                else
                {
                    throw;
                }
            }

            return user;
        }

        public async Task<UserViewModel?> GetUserByName(string name, bool FullInfo = false)
        {
            if (_context.Users == null)
            {
                return default;
            }

            UserViewModel? userViewModel;
            if (FullInfo)
            {
                userViewModel = await _context.Users.Include(u => u.FullUserInfo)
                                                    .Include(u=>u.Favourites)
                                                    .FirstOrDefaultAsync(u=>u.UserName == name);
            }
            else
            {
                userViewModel = await _context.Users.FirstOrDefaultAsync(u => u.UserName == name);
            }

            if (userViewModel == null)
            {
                return default;
            }

            return userViewModel;
        }

        public async Task<IEnumerable<UserViewModel>?> GetUsers()
        {
            if (_context.Users == null)
            {
                return default;
            }
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> IsExistNameUserInDB(string UserName)
        {
            if (_context.Users == null) return false;

            return await _context.Users.AnyAsync(u=>u.UserName == UserName);
        }

        public async Task<bool> IsExistEmailUserInDB(string email)
        {
            if (_context.Users == null) return false; 

            return await _context.FullInfoUsers.AnyAsync(u=>u.UserEmail == email);
        }

        public async Task<UserViewModel?> PostUser(UserViewModel userViewModel)
        {
            if (_context.Users == null)
            {
                return default;
            }
            _context.Users.Add(userViewModel);
            await _context.SaveChangesAsync();

            return await GetUser(userViewModel.Id);
        }

        public async Task<UserViewModel?> PutUser(Guid id, UserViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                return default;
            }

            //_context.Entry(userViewModel).State = EntityState.Modified;

            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                //user.UserEmail = userViewModel.UserEmail;
                user.UserName = userViewModel.UserName;
                //user.Password = userViewModel.Password;
                //user.ConfirmEmail = userViewModel.ConfirmEmail;
                user.ProfilePicture = userViewModel.ProfilePicture;
                user.Role = userViewModel.Role;
                user.FullUserInfo = userViewModel.FullUserInfo;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return default;
                }
                else
                {
                    throw;
                }
            }

            return userViewModel;
        }

        private bool UsersExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<UserViewModel?> GetUserByEmail(string email, bool FullInfo = false)
        {
            if (_context.Users == null)
            {
                return default;
            }
            UserViewModel? userViewModel;

            if (FullInfo)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                userViewModel = await _context.Users
                                      .Include(a=>a.FullUserInfo)
                                      .Include(u => u.Favourites)
                                      .FirstOrDefaultAsync(u => u.FullUserInfo.UserEmail == email);

            }
            else
            {
                userViewModel = await _context.Users.FirstOrDefaultAsync(u => u.FullUserInfo.UserEmail == email);
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (userViewModel == null)
            {
                return default;
            }

            return userViewModel;
        }
    }
}
