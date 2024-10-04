namespace zeno_copenhagen.Services.Buildings;

public sealed class BuildingPlacementService : IBuildingPlacementService
{
    private readonly IGameData _gameData;
    private readonly IPrototypeService<BuildingPrototype, Building> _buildingPrototypeService;
    private readonly IJobCreationService _jobCreationService;
    private readonly IWorkerRecruitementService _workerRecruitementService;

    public BuildingPlacementService(
        IGameData gameData,
        IPrototypeService<BuildingPrototype, Building> buildingPrototypeService,
        IJobCreationService jobCreationService,
        IWorkerRecruitementService workerRecruitementService)
    {
        _gameData = gameData;
        _buildingPrototypeService = buildingPrototypeService;
        _jobCreationService = jobCreationService;
        _workerRecruitementService = workerRecruitementService;
    }

    public bool CanPlacePrototype(Guid prototypeId, Vector2 position, bool clearJobReservations)
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

                if (tile is null || !tile.DugOut || tile.HasBuilding || (!clearJobReservations && tile.JobReserved))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool PlacePrototype(Guid prototypeId, Vector2 position, bool clearJobReservations)
    {
        if (!CanPlacePrototype(prototypeId, position, clearJobReservations))
        {
            return false;
        }

        var prototype = _buildingPrototypeService.GetPrototype(prototypeId);

        var building = _buildingPrototypeService.CreateEntity(prototypeId);

        building.Position = position;

        _gameData.Building.Buildings.Add(building);

        for (var y = 0; y < prototype.Size.Y; ++y)
        {
            for (var x = 0; x < prototype.Size.X; ++x)
            {
                var tile = _gameData.Terrain.GetTile((int)(y + position.Y), (int)(x + position.X));
                Debug.Assert(tile is not null);
                tile.HasBuilding = true;
                if (clearJobReservations)
                {
                    tile.JobReserved = false;
                }
            }
        }

        foreach (var (name, offset) in prototype.ProvidedJobs)
        {
            if (!_jobCreationService.CreateJob(StringHash.Hash(name), offset, position))
            {
                Debug.WriteLine("Failed to create {0} job when creating {1}", name, prototype.Name);
            }
        }

        foreach (var (name, amount) in prototype.RequiredWorkers)
        {
            _workerRecruitementService.IncreaseWorkerRequirement(StringHash.Hash(name), amount);
        }

        return true;
    }
}
