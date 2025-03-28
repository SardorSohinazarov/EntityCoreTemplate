using Microsoft.AspNetCore.Mvc;
using Services.Books;
using Common.Paginations.Models;
using Common;
using EntityCoreTemplate.Application.DataTransferObjects.Books;
using EntityCoreTemplate.Domain.Entities;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpPost]
        public async Task<Result<BookViewModel>> AddAsync(BookCreationDto bookCreationDto)
        {
            return Result<BookViewModel>.Success(await _booksService.AddAsync(bookCreationDto));
        }

        [HttpGet]
        public async Task<Result<List<BookViewModel>>> GetAllAsync()
        {
            return Result<List<BookViewModel>>.Success(await _booksService.GetAllAsync());
        }

        [HttpPost("filter")]
        public async Task<Result<List<BookViewModel>>> FilterAsync(PaginationOptions filter)
        {
            return Result<List<BookViewModel>>.Success(await _booksService.FilterAsync(filter));
        }

        [HttpGet("{id}")]
        public async Task<Result<BookViewModel>> GetByIdAsync(long id)
        {
            return Result<BookViewModel>.Success(await _booksService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<Result<BookViewModel>> UpdateAsync(long id, BookModificationDto bookModificationDto)
        {
            return Result<BookViewModel>.Success(await _booksService.UpdateAsync(id, bookModificationDto));
        }

        [HttpDelete("{id}")]
        public async Task<Result<BookViewModel>> DeleteAsync(long id)
        {
            return Result<BookViewModel>.Success(await _booksService.DeleteAsync(id));
        }
    }
}