using AutoMapper;
using BAW_Project_API.Dtos;
using BAW_Project_API.Interfaces;
using BAW_Project_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BAW_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genre>>> GetGenres()
        {
            var genres = await _genreService.GetAllGenres();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _genreService.GetGenreById(id);

            if (genre == null)
            {
                return NotFound("Genre not found");
            }

            return Ok(genre);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddGenre(GenreDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);

            var existingGenres = await _genreService.GetGenreByName(genreDto.Name);

            if (existingGenres.Any())
            {
                return BadRequest("Genre already exists");
            }

            await _genreService.AddGenre(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateGenre(int id, GenreDto genreDto)
        {
            var genreDB = await _genreService.GetGenreById(id);

            if (genreDB == null)
            {
                return NotFound("Genre does not exist");
            }

            await _genreService.UpdateGenre(genreDB, genreDto);

            return Ok("Genre updated");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var genre = await _genreService.GetGenreById(id);

            if (genre == null)
            {
                return NotFound("Genre does not exist");
            }

            await _genreService.DeleteGenre(genre);

            return Ok("Genre deleted");
        }
    }
}
