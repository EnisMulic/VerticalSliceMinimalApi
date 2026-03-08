namespace Application.Domain.Common;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }

    void Delete()
    {
        IsDeleted = true;
    }
}