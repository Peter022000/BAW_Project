using AutoMapper;
using BAW_Project_API.Dtos;
using BAW_Project_API.Interfaces;
using BAW_Project_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BAW_Project_API.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<ActionResult> AddBook(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);

            await _bookService.AddBook(book);

            return Ok(book);
        }

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
    }
}
