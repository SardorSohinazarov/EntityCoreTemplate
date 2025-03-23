using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Common.Paginations.Models;
using Common.Paginations.Extensions;
using EntityCoreTemplate.Application.DataTransferObjects.Books;
using EntityCoreTemplate.Domain.Entities;

namespace Services.Books
{
    public interface IBooksService
    {
        Task<BookViewModel> AddAsync(BookCreationDto entity);
        Task<List<BookViewModel>> GetAllAsync();
        Task<List<BookViewModel>> FilterAsync(PaginationOptions filter);
        Task<BookViewModel> GetByIdAsync(long id);
        Task<BookViewModel> UpdateAsync(long id, BookModificationDto entity);
        Task<BookViewModel> DeleteAsync(long id);
    }
}