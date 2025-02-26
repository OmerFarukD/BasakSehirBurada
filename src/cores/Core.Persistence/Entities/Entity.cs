namespace Core.Persistence.Entities;

public abstract class Entity<TId>
{

    public TId Id { get; set; } = default(TId);
    public DateTime CreatedTime { get; set; }

    public DateTime? UpdateTime { get; set; }

}
