namespace zeno_copenhagen.Services.Jobs;

public sealed class JobCreationService : IJobCreationService
{
    private readonly IGameData _gameData;
    private readonly IPrototypeService<JobPrototype, Job> _jobPrototypeService;

    public JobCreationService(
        IGameData gameData,
        IPrototypeService<JobPrototype, Job> jobPrototypeService)
    {
        _gameData = gameData;
        _jobPrototypeService = jobPrototypeService;
    }


    public bool CreateJob(Guid prototypeId, Vector2 offset, Vector2 coords)
    {
        if (!_jobPrototypeService.IsPrototypeRegistered(prototypeId))
        {
            return false;
        }

        var tile = _gameData.Terrain.GetTile((int)coords.Y, (int)coords.X);
        if (tile is null || tile.JobReserved)
        {
            return false;
        }

        tile.JobReserved = true;

        var job = _jobPrototypeService.CreateEntity(prototypeId);
        job.TileCoords = coords;
        job.Offset = offset;

        _gameData.Job.Jobs.Add(job);

        return true;
    }
}
