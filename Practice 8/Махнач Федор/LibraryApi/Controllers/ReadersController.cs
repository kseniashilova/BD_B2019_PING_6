using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApi.Controllers.Responses;
using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Services;
using LibraryApi.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("reader")]
    public class ReadersController : ControllerBase
    {
        private readonly IReadersService _readersService;

        public ReadersController(IReadersService readersService)
        {
            _readersService = readersService;
        }

        [HttpGet("{reader_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Reader>> Get([FromRoute(Name = "reader_id")] long readerId)
        {
            var reader = await _readersService.GetAsync(readerId, HttpContext.RequestAborted);

            return Ok(reader);
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<Book>>> GetAll()
        {
            var readers = await _readersService.GetAllAsync(HttpContext.RequestAborted);

            return Ok(readers);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateReaderResponse>> Create([FromBody] CreateReaderDto request)
        {
            var newReaderId = await _readersService.CreateAsync(request, HttpContext.RequestAborted);

            var response = new CreateReaderResponse
            {
                Id = newReaderId
            };

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] Reader readerToUpdate)
        {
            await _readersService.UpdateAsync(readerToUpdate, HttpContext.RequestAborted);

            return Ok();
        }

        [HttpDelete("{reader_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute(Name = "reader_id")] long readerId)
        {
            await _readersService.DeleteAsync(readerId, HttpContext.RequestAborted);

            return Ok();
        }
    }
}