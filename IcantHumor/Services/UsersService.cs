using IcantHumor.Data;
using IcantHumor.Models;
using IcantHumor.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<UserViewModel> DeleteUser(Guid id)
        {
            if (_context.Users == null)
            {
                return null;
            }
            var userViewModel = await _context.Users.FindAsync(id);
            if (userViewModel == null)
            {
                return null;
            }

            _context.Users.Remove(userViewModel);
            await _context.SaveChangesAsync();

            return userViewModel;
        }

        public async Task<UserViewModel> GetUser(Guid id, bool FullInfo = false)
        {
            if (_context.Users == null)
            {
                return null;
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
                return null;
            }

            return userViewModel;
        }

        public async Task<UserViewModel> GetUserByName(string name)
        {
            if (_context.Users == null)
            {
                return null;
            }
            var userViewModel = await _context.Users.FirstOrDefaultAsync(u=>u.UserName == name);
            if (userViewModel == null)
            {
                return null;
            }

            return userViewModel;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            if (_context.Users == null)
            {
                return null;
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

        public async Task<UserViewModel> PostUser(UserViewModel userViewModel)
        {
            if (_context.Users == null)
            {
                return null;
            }
            _context.Users.Add(userViewModel);
            await _context.SaveChangesAsync();

            return await GetUser(userViewModel.Id);
        }

        public async Task<UserViewModel> PutUser(Guid id, UserViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                return null;
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
                    return null;
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
    }
}
