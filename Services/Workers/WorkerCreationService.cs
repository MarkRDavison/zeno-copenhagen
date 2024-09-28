namespace zeno_copenhagen.Services.Workers;

public sealed class WorkerCreationService : IWorkerCreationService
{
    private readonly IGameData _gameData;
    private readonly IPrototypeService<WorkerPrototype, Worker> _workerPrototypeService;

    public WorkerCreationService(
        IGameData gameData,
        IPrototypeService<WorkerPrototype, Worker> workerPrototypeService)
    {
        _gameData = gameData;
        _workerPrototypeService = workerPrototypeService;
    }

    public bool CreateWorker(Guid prototypeId, Vector2 position)
    {
        if (!_workerPrototypeService.IsPrototypeRegistered(prototypeId))
        {
            return false;
        }

        var worker = _workerPrototypeService.CreateEntity(prototypeId);
        worker.Position = position;

        _gameData.Worker.Workers.Add(worker);

        return true;
    }
}
