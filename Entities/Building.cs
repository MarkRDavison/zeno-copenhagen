namespace zeno_copenhagen.Entities;

public sealed class Building : IEntity
{
    public Guid Id { get; set; }
    public Guid PrototypeId { get; set; }
    public Vector2 Position { get; set; }
}
