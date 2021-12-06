using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Services;
using LibraryApi.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("book")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("{isbn}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Book>> Get([FromRoute] string isbn)
        {
            var book = await _booksService.GetAsync(isbn, HttpContext.RequestAborted);

            return Ok(book);
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<Book>>> GetAll()
        {
            var books = await _booksService.GetAllAsync(HttpContext.RequestAborted);

            return Ok(books);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] Book newBook)
        {
            await _booksService.CreateAsync(newBook, HttpContext.RequestAborted);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] Book bookToUpdate)
        {
            await _booksService.UpdateAsync(bookToUpdate, HttpContext.RequestAborted);

            return Ok();
        }

        [HttpDelete("{isbn}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] string isbn)
        {
            await _booksService.DeleteAsync(isbn, HttpContext.RequestAborted);

            return Ok();
        }
    }
}