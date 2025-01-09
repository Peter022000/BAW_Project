using BAW_Project_API.Dtos;
using BAW_Project_API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BAW_Project_API.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(int id);
        public Task<List<User>> GetUserByLogin(String name);
        public Task AddUser(User user);
        public Task UpdateUser(User userDB, UserDto userDto);
        public Task DeleteUser(User user);
    }
}
