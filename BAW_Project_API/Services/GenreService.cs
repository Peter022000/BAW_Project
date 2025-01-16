using BAW_Project_API.Data;
using BAW_Project_API.Dtos;
using BAW_Project_API.Interfaces;
using BAW_Project_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BAW_Project_API.Services
{
    public class GenreService : IGenreService
    {
        private readonly AppDbContext _context;

        public GenreService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre?> GetGenreById(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<List<Genre>> GetGenreByName(string name)
        {
            return await _context.Genres
                .Where(g => g.Name.Contains(name))
                .ToListAsync();
        }

        public async Task AddGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenre(Genre genreDB, GenreDto genreDto)
        {
            genreDB.Name = genreDto.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}
