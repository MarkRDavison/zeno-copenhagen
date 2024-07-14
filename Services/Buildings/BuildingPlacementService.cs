namespace zeno_copenhagen.Services.Buildings;

public sealed class BuildingPlacementService : IBuildingPlacementService
{
    private readonly IGameData _gameData;
    private readonly IPrototypeService<BuildingPrototype, Building> _buildingPrototypeService;
    public BuildingPlacementService(
        IGameData gameData,
        IPrototypeService<BuildingPrototype, Building> buildingPrototypeService)
    {
        _gameData = gameData;
        _buildingPrototypeService = buildingPrototypeService;
    }

    public bool CanPlaceBuilding(Guid prototypeId, Vector2 position)
    {
        if (!_buildingPrototypeService.IsPrototypeRegistered(prototypeId))
        {
            return false;
        }

        var prototype = _buildingPrototypeService.GetPrototype(prototypeId);

        for (var y = 0; y < prototype.Size.Y; ++y)
        {
            for (var x = 0; x < prototype.Size.X; ++x)
            {
                var tile = _gameData.Terrain.GetTile((int)(y + position.Y), (int)(x + position.X));

                if (tile is null || !tile.DugOut)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
