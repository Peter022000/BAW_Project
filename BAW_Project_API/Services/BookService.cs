using BAW_Project_API.Data;
using BAW_Project_API.Dtos;
using BAW_Project_API.Interfaces;
using BAW_Project_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BAW_Project_API.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(Book bookDB, BookDto bookDto)
        {
            bookDB.Title = bookDto.Title;
            bookDB.AuthorId = bookDto.AuthorId;
            bookDB.GenreId = bookDto.GenreId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
