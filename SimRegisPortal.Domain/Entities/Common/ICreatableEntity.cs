namespace SimRegisPortal.Domain.Entities.Common
{
    public interface ICreatableEntity
    {
        DateTime DateCreated { get; set; }
        int? AuthorId { get; set; }

        void SetAuthor(int? authorId)
        {
            DateCreated = DateTime.Now;
            AuthorId = authorId;
        }
    }
}
