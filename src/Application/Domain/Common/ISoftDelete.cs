namespace Application.Domain.Common;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }

    void Delete()
    {
        IsDeleted = true;
    }
}