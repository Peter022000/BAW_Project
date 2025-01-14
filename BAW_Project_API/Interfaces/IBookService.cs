using BAW_Project_API.Dtos;
using BAW_Project_API.Models;

namespace BAW_Project_API.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<Book?> GetBookById(int id);
        Task AddBook(Book book);
        Task UpdateBook(Book bookDB, BookDto bookDto);
        Task DeleteBook(Book book);
        Task<bool> LoanBook(int bookId, string userLogin);
        Task<bool> ReturnBook(int bookId, string userLogin);
    }
}
