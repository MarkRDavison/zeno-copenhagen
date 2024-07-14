namespace zeno_copenhagen.Commands.PlaceBuilding;

public sealed class PlaceBuildingCommandHandler : IGameCommandHandler<PlaceBuildingCommand>
{
    private readonly IGameData _gameData;
    private readonly IPrototypeService<BuildingPrototype, Building> _buildingPrototypeService;
    private readonly IBuildingPlacementService _buildingPlacementService;

    public PlaceBuildingCommandHandler(
        IGameData gameData,
        IPrototypeService<BuildingPrototype, Building> buildingPrototypeService,
        IBuildingPlacementService buildingPlacementService)
    {
        _gameData = gameData;
        _buildingPrototypeService = buildingPrototypeService;
        _buildingPlacementService = buildingPlacementService;
    }

    public bool Handle(PlaceBuildingCommand command)
    {
        if (!_buildingPlacementService.CanPlaceBuilding(command.PrototypeId, command.Position))
        {
            return false;
        }

        var building = _buildingPrototypeService.CreateEntity(command.PrototypeId);

        building.Position = command.Position;

        _gameData.Building.Buildings.Add(building);

        return true;
    }
}
