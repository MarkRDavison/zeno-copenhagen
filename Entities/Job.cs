namespace zeno_copenhagen.Entities;

public sealed class Job : IEntity
{
    public Guid Id { get; set; }
    public Guid PrototypeId { get; set; }
    public Vector2 TileCoords { get; set; }
    public Vector2 Offset { get; set; }
    public Guid? AllocatedWorkerId { get; set; }
    public float WorkRemaining { get; set; }
    public bool Complete { get; set; }
}
