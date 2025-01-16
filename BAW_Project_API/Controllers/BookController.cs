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
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            return Ok(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddBook(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);

            await _bookService.AddBook(book);

            return Ok(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, BookDto bookDto)
        {
            var bookDB = await _bookService.GetBookById(id);

            if (bookDB == null)
            {
                return NotFound("Book does not exist");
            }

            await _bookService.UpdateBook(bookDB, bookDto);

            return Ok("Book updated");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound("Book does not exist");
            }

            await _bookService.DeleteBook(book);

            return Ok("Book deleted");
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost("{id}/loan")]
        public async Task<ActionResult> LoanBook(int id)
        {
            var result = await _bookService.LoanBook(id);

            if (result)
            {
                return Ok("Book loaned successfully");
            }

            return BadRequest("Book is already loaned or does not exist");
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost("{id}/return")]
        public async Task<ActionResult> ReturnBook(int id)
        {
            var result = await _bookService.ReturnBook(id);

            if (result)
            {
                return Ok("Book returned successfully");
            }

            return BadRequest("Book is not loaned, or only the user who loaned it can return it");
        }
    }
}
