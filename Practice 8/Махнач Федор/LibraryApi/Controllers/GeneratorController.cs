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
    [Route("generate")]
    public class GeneratorController : ControllerBase
    {
        private readonly IGeneratorService _generatorService;

        public GeneratorController(IGeneratorService generatorService)
        {
            _generatorService = generatorService;
        }

        [HttpPost("books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<Book>>> GenerateBooks([FromQuery] int count)
        {
            var generatedBooks = await _generatorService.GenerateBooksAsync(count, HttpContext.RequestAborted);

            return Ok(generatedBooks);
        }

        [HttpPost("book-copies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<BookCopy>>> GenerateCopies([FromQuery] int count)
        {
            var generatedCopies = await _generatorService.GenerateCopiesAsync(count, HttpContext.RequestAborted);

            return Ok(generatedCopies);
        }

        [HttpPost("readers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<Reader>>> GenerateReaders([FromQuery] int count)
        {
            var generatedReaders = await _generatorService.GenerateReadersAsync(count, HttpContext.RequestAborted);

            return Ok(generatedReaders);
        }

        [HttpPost("borrowings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<Borrowing>>> GenerateBorrowings([FromQuery] int count)
        {
            var generatedBorrowings = await _generatorService.GenerateBorrowingsAsync(count, HttpContext.RequestAborted);

            return Ok(generatedBorrowings);
        }
    }
}