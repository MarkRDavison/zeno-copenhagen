namespace zeno_copenhagen.Entities;

public enum ShuttleState
{
    Idle = 0,
    TravellingToSurface,
    WaitingOnSurface,
    LeavingSurface,
    Complete
}

public sealed class Shuttle : IEntity
{
    public Guid Id { get; set; }
    public Guid PrototypeId { get; set; }
    public ShuttleState State { get; set; }
    public double Elapsed { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 StartingPosition { get; set; }
    public Vector2 SurfacePosition { get; set; }
    public Vector2 LeavingPosition { get; set; }
}
