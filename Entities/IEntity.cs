namespace zeno_copenhagen.Entities;

public interface IEntity
{
    public Guid Id { get; set; }
    public Guid PrototypeId { get; set; }
}
