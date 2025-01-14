using BAW_Project_API.Dtos;
using BAW_Project_API.Models;

namespace BAW_Project_API.Interfaces
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAllGenres();
        Task<Genre> GetGenreById(int id);
        Task<List<Genre>> GetGenreByName(string name);
        Task AddGenre(Genre genre);
        Task UpdateGenre(Genre genreDB, GenreDto genreDto);
        Task DeleteGenre(Genre genre);
    }
}
