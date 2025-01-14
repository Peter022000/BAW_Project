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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorById(id);

            if (author == null)
            {
                return NotFound("Author not found");
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult> AddAuthor(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);

            var existingAuthors = await _authorService.GetAuthorByName(authorDto.FirstName);

            if (existingAuthors.Any(a =>
                a.FirstName.Equals(authorDto.FirstName, StringComparison.OrdinalIgnoreCase) &&
                a.LastName.Equals(authorDto.LastName, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("Author already exists");
            }

            await _authorService.AddAuthor(author);

            return Ok(author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, AuthorDto authorDto)
        {
            var authorDB = await _authorService.GetAuthorById(id);

            if (authorDB == null)
            {
                return NotFound("Author does not exist");
            }

            await _authorService.UpdateAuthor(authorDB, authorDto);

            return Ok("Author updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _authorService.GetAuthorById(id);

            if (author == null)
            {
                return NotFound("Author does not exist");
            }

            await _authorService.DeleteAuthor(author);

            return Ok("Author deleted");
        }
    }
}
