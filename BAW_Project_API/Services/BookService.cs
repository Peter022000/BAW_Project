using BAW_Project_API.Data;
using BAW_Project_API.Dtos;
using BAW_Project_API.Interfaces;
using BAW_Project_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BAW_Project_API.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<bool> LoanBook(int bookId)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            var jwt = authorizationHeader?.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            var claims = token.Claims.Select(claim => (claim.Type, claim.Value)).ToList();

            var userLogin = claims[0].Value;

            var book = await _context.Books.FindAsync(bookId);
            if (book == null || book.IsLoaned)
            {
                return false;
            }

            book.IsLoaned = true;
            book.LoanedByUserLogin = userLogin;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> ReturnBook(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || !book.IsLoaned)
            {
                return false;
            }

            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            var jwt = authorizationHeader?.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            var claims = token.Claims.Select(claim => (claim.Type, claim.Value)).ToList();

            var userLogin = claims[0].Value;

            if (book.LoanedByUserLogin != userLogin)
            {
                return false;
            }

            book.IsLoaned = false;
            book.LoanedByUserLogin = string.Empty;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
