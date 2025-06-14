using Microsoft.AspNetCore.Mvc;
using Services.Books;
using Common.Paginations.Models;
using Common;
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
        public async Task<Result<Book>> AddAsync(Book book)
        {
            return Result<Book>.Success(await _booksService.AddAsync(book));
        }

        [HttpGet]
        public async Task<Result<List<Book>>> GetAllAsync()
        {
            return Result<List<Book>>.Success(await _booksService.GetAllAsync());
        }

        //[HttpPost("filter")]
        //public async Task<Result<List<Book>>> FilterAsync(PaginationOptions filter)
        //{
        //    return Result<List<Book>>.Success(await _booksService.FilterAsync(filter));
        //}

        [HttpGet("{id}")]
        public async Task<Result<Book>> GetByIdAsync(long id)
        {
            return Result<Book>.Success(await _booksService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<Result<Book>> UpdateAsync(long id, Book book)
        {
            return Result<Book>.Success(await _booksService.UpdateAsync(id, book));
        }

        [HttpDelete("{id}")]
        public async Task<Result<Book>> DeleteAsync(long id)
        {
            return Result<Book>.Success(await _booksService.DeleteAsync(id));
        }
    }
}