namespace zeno_copenhagen.Prototypes;

public class ShuttlePrototypeService : PrototypeService<ShuttlePrototype, Shuttle>
{
    public override Shuttle CreateEntity(ShuttlePrototype prototype)
    {
        var shuttle = new Shuttle
        {
            Id = Guid.NewGuid(),
            PrototypeId = prototype.Id,
            State = ShuttleState.Idle,
            StartingPosition = new Vector2(-20.0f, -10.0f) * ResourceConstants.CellSize, // TODO: From prototype/constant
            SurfacePosition = new Vector2(-4.0f, +0.0f) * ResourceConstants.CellSize, // TODO: Optionally one landing pad per shuttle class?
            LeavingPosition = new Vector2(+20.0f, -10.0f) * ResourceConstants.CellSize// TODO: From prototype/constant
        };

        shuttle.Position = shuttle.StartingPosition;
        shuttle.Elapsed = prototype.IdleTime - 1.0f;
        return shuttle;
    }
}
