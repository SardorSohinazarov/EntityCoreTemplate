using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Common.Paginations.Models;
using Common.Paginations.Extensions;
using Common.ServiceAttribute;
using EntityCoreTemplate.Application.DataTransferObjects.Books;
using EntityCoreTemplate.Infrastructure;
using EntityCoreTemplate.Domain.Entities;

namespace Services.Books
{
    [ScopedService]
    public class BooksService : IBooksService
    {
        private readonly EntityCoreTemplateDbContext _entityCoreTemplateDbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        public BooksService(EntityCoreTemplateDbContext entityCoreTemplateDbContext, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _entityCoreTemplateDbContext = entityCoreTemplateDbContext;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<BookViewModel> AddAsync(BookCreationDto bookCreationDto)
        {
            var entity = _mapper.Map<Book>(bookCreationDto);
            var entry = await _entityCoreTemplateDbContext.Set<Book>().AddAsync(entity);
            await _entityCoreTemplateDbContext.SaveChangesAsync();
            return _mapper.Map<BookViewModel>(entry.Entity);
        }

        public async Task<List<BookViewModel>> GetAllAsync()
        {
            var entities = await _entityCoreTemplateDbContext.Set<Book>().ToListAsync();
            return _mapper.Map<List<BookViewModel>>(entities);
        }

        public async Task<List<BookViewModel>> FilterAsync(PaginationOptions filter)
        {
            var httpContext = _httpContext.HttpContext;
            var entities = await _entityCoreTemplateDbContext.Set<Book>().ApplyPagination(filter, httpContext).ToListAsync();
            return _mapper.Map<List<BookViewModel>>(entities);
        }

        public async Task<BookViewModel> GetByIdAsync(long id)
        {
            var entity = await _entityCoreTemplateDbContext.Set<Book>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new InvalidOperationException($"Book with Id {id} not found.");
            return _mapper.Map<BookViewModel>(entity);
        }

        public async Task<BookViewModel> UpdateAsync(long id, BookModificationDto bookModificationDto)
        {
            var entity = await _entityCoreTemplateDbContext.Set<Book>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new InvalidOperationException($"Book with {id} not found.");
            _mapper.Map(bookModificationDto, entity);
            var entry = _entityCoreTemplateDbContext.Set<Book>().Update(entity);
            await _entityCoreTemplateDbContext.SaveChangesAsync();
            return _mapper.Map<BookViewModel>(entry.Entity);
        }

        public async Task<BookViewModel> DeleteAsync(long id)
        {
            var entity = await _entityCoreTemplateDbContext.Set<Book>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new InvalidOperationException($"Book with {id} not found.");
            var entry = _entityCoreTemplateDbContext.Set<Book>().Remove(entity);
            await _entityCoreTemplateDbContext.SaveChangesAsync();
            return _mapper.Map<BookViewModel>(entry.Entity);
        }
    }

    /// <summary>
    /// AutoMapper mapping profile for Book entity.
    /// </summary>
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookViewModel>();
            CreateMap<BookCreationDto, Book>();
            CreateMap<BookModificationDto, Book>();
        }
    }
}