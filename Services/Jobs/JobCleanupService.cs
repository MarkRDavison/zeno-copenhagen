namespace zeno_copenhagen.Services.Jobs;

public class JobCleanupService : IJobCleanupService
{
    private readonly IGameData _gameData;

    public JobCleanupService(IGameData gameData)
    {
        _gameData = gameData;
    }

    public void Cleanup()
    {
        _gameData.Job.Jobs.RemoveAll(_ => _.Complete);
    }
}
