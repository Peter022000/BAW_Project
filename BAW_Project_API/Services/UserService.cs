using BAW_Project_API.Data;
using BAW_Project_API.Dtos;
using BAW_Project_API.Entities;
using BAW_Project_API.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BAW_Project_API.Services
{
    public class UserService : IUserService
    {

        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            this._context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            
            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }
        
        public async Task<List<User>> GetUserByLogin(String name)
        {
            var users = await _context.Users.Where(x => x.Name == name).ToListAsync(); ;

            return users;
        }

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateUser(User userDB, UserDto userDto)
        {
            userDB.Name = userDto.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();   
        }

    }
}
