namespace zeno_copenhagen.Commands.PlaceBuilding;

public sealed class PlaceBuildingCommand : IGameCommand
{
    public PlaceBuildingCommand(
        Vector2 position,
        string prototypeName
    ) : this(
        position,
        StringHash.Hash(prototypeName))
    {
    }
    public PlaceBuildingCommand(Vector2 position, Guid prototypeId)
    {
        Position = position;
        PrototypeId = prototypeId;
    }

    public Vector2 Position { get; }
    public Guid PrototypeId { get; }
}
