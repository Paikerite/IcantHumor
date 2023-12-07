﻿using IcantHumor.Models;

namespace IcantHumor.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task<UserViewModel> GetUser(Guid id, bool FullInfo = false);
        Task<UserViewModel> GetUserByName(string name);
        Task<UserViewModel> PutUser(Guid id, UserViewModel userViewModel);
        Task<UserViewModel> PostUser(UserViewModel userViewModel);
        Task<UserViewModel> DeleteUser(Guid id);
        Task<bool> IsExistEmailUserInDB(string email);
        Task<bool> IsExistNameUserInDB(string UserName);
    }
}
