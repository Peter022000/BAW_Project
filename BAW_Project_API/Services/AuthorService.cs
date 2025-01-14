using BAW_Project_API.Data;
using BAW_Project_API.Dtos;
using BAW_Project_API.Interfaces;
using BAW_Project_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BAW_Project_API.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<List<Author>> GetAuthorByName(string name)
        {
            return await _context.Authors
                .Where(a => a.FirstName.Contains(name) || a.LastName.Contains(name))
                .ToListAsync();
        }

        public async Task AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthor(Author authorDB, AuthorDto authorDto)
        {
            authorDB.FirstName = authorDto.FirstName;
            authorDB.LastName = authorDto.LastName;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthor(Author author)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}
