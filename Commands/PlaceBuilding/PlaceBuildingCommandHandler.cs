namespace zeno_copenhagen.Commands.PlaceBuilding;

public sealed class PlaceBuildingCommandHandler : IGameCommandHandler<PlaceBuildingCommand>
{
    private readonly IGameData _gameData;
    private readonly IBuildingPlacementService _buildingPlacementService;

    public PlaceBuildingCommandHandler(
        IGameData gameData,
        IBuildingPlacementService buildingPlacementService)
    {
        _gameData = gameData;
        _buildingPlacementService = buildingPlacementService;
    }

    public bool Handle(PlaceBuildingCommand command)
    {
        return _buildingPlacementService.PlacePrototype(command.PrototypeId, command.Position, command.ClearJobReservations);
    }
}
