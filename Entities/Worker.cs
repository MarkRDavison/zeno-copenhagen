namespace zeno_copenhagen.Entities;
public enum WorkerState
{
    Idle = 0,
    WorkingJob,
    MovingToTarget,
}

public sealed class Worker : IEntity
{
    public Guid Id { get; set; }
    public Guid PrototypeId { get; set; }
    public Vector2 Position { get; set; }
    public Guid? AllocatedJobId { get; set; }
    public WorkerState State { get; set; }
    public Vector2 Target { get; set; }
    public float Speed { get; set; }
}
