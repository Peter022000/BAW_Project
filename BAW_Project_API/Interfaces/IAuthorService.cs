using BAW_Project_API.Dtos;
using BAW_Project_API.Models;

namespace BAW_Project_API.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author?> GetAuthorById(int id);
        Task<List<Author>> GetAuthorByName(string name);
        Task AddAuthor(Author author);
        Task UpdateAuthor(Author authorDB, AuthorDto authorDto);
        Task DeleteAuthor(Author author);
    }
}
