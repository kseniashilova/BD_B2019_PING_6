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
    [Route("book-copy")]
    public class BookCopyController : ControllerBase
    {
        private readonly IBookCopiesService _bookCopiesService;

        public BookCopyController(IBookCopiesService bookCopiesService)
        {
            _bookCopiesService = bookCopiesService;
        }

        [HttpGet("position")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookCopy>> Get(
            [FromQuery] string isbn,
            [FromQuery] int copyNumber)
        {
            var bookCopy = await _bookCopiesService.GetAsync(isbn, copyNumber, HttpContext.RequestAborted);

            return Ok(bookCopy);
        }

        [HttpGet("{isbn}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyCollection<BookCopy>>> GetAllByIsbn([FromRoute] string isbn)
        {
            var bookCopies = await _bookCopiesService.GetAllByIsbnAsync(isbn, HttpContext.RequestAborted);

            return Ok(bookCopies);
        }
        
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<BookCopy>>> GetAll()
        {
            var bookCopies = await _bookCopiesService.GetAllAsync(HttpContext.RequestAborted);

            return Ok(bookCopies);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Create([FromBody] BookCopy bookCopy)
        {
            await _bookCopiesService.CreateAsync(bookCopy, HttpContext.RequestAborted);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] BookCopy bookCopy)
        {
            await _bookCopiesService.UpdateAsync(bookCopy, HttpContext.RequestAborted);

            return Ok();
        }

        [HttpDelete("{isbn}/{copy_number}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(
            [FromRoute] string isbn,
            [FromRoute(Name = "copy_number")] int copyNumber)
        {
            await _bookCopiesService.DeleteAsync(isbn, copyNumber, HttpContext.RequestAborted);

            return Ok();
        }
    }
}