using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("borrowing")]
    public class BorrowingsController : ControllerBase
    {
        private readonly IBorrowingsService _borrowingsService;

        public BorrowingsController(IBorrowingsService borrowingsService)
        {
            _borrowingsService = borrowingsService;
        }

        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<Borrowing>>> Search([FromBody] SearchBorrowingDto request)
        {
            var result = await _borrowingsService.SearchAsync(request, HttpContext.RequestAborted);
            
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Create([FromBody] CreateBorrowingDto request)
        {
            await _borrowingsService.CreateAsync(request, HttpContext.RequestAborted);
            
            return Ok();
        }
    }
}