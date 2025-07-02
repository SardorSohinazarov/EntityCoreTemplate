using EntityCoreTemplate.Domain.Common;

namespace EntityCoreTemplate.Domain.Entities
{
    public class Book : Auditable, ISoftDeletable
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public BookType Type { get; set; }
        public bool IsDeleted { get; set; }
    }

    public enum BookType
    {
        Paperback,
        Ebook,
        Audiobook
    }
}
