namespace zeno_copenhagen.Services.Shuttles;

public sealed class ShuttleScheduleService : IShuttleScheduleService
{
    private readonly IGameData _gameData;
    private readonly IPrototypeService<ShuttlePrototype, Shuttle> _shuttlePrototypeService;
    private readonly IWorkerRecruitementService _workerRecruitementService;
    private readonly IWorkerCreationService _workerCreationService;

    public ShuttleScheduleService(
        IGameData gameData,
        IPrototypeService<ShuttlePrototype, Shuttle> shuttlePrototypeService,
        IWorkerRecruitementService workerRecruitementService,
        IWorkerCreationService workerCreationService)
    {
        _gameData = gameData;
        _shuttlePrototypeService = shuttlePrototypeService;
        _workerRecruitementService = workerRecruitementService;
        _workerCreationService = workerCreationService;
    }

    public void Update(TimeSpan delta)
    {
        foreach (var shuttle in _gameData.Shuttle.Shuttles)
        {
            UpdateShuttle(delta, shuttle, _shuttlePrototypeService.GetPrototype(shuttle.PrototypeId));
        }
    }

    internal void UpdateShuttle(TimeSpan delta, Shuttle shuttle, ShuttlePrototype prototype)
    {
        shuttle.Elapsed += delta.TotalSeconds;
        switch (shuttle.State)
        {
            case ShuttleState.Idle:
                HandleIdleShuttle(shuttle, prototype);
                break;
            case ShuttleState.TravellingToSurface:
                HandleTravellingToSurfaceShuttle(delta, shuttle, prototype);
                break;
            case ShuttleState.WaitingOnSurface:
                HandleWaitingOnSurfaceShuttle(shuttle, prototype);
                break;
            case ShuttleState.LeavingSurface:
                HandleLeavingSurfaceShuttle(delta, shuttle, prototype);
                break;
            case ShuttleState.Complete:
            default:
                HandleCompleteShuttle(shuttle);
                break;
        }
    }

    internal bool MoveShuttleTowardsLocation(TimeSpan delta, Shuttle shuttle, ShuttlePrototype prototype, Vector2 target, ShuttleState nextState)
    {
        var distanceToTarget = (shuttle.Position - target).Length();
        var maxMovement = (float)(prototype.Speed * delta.TotalSeconds);

        if (distanceToTarget <= maxMovement)
        {
            shuttle.State = nextState;
            shuttle.Position = target;
            shuttle.Elapsed = 0;
            return true;
        }

        var direction = target - shuttle.Position;
        direction.Normalize();

        shuttle.Position += direction * maxMovement;
        return false;
    }

    private void HandleIdleShuttle(Shuttle shuttle, ShuttlePrototype prototype)
    {
        if (shuttle.Elapsed >= prototype.IdleTime)
        {
            shuttle.State = ShuttleState.TravellingToSurface;
            shuttle.Elapsed = 0;
        }
    }

    private void HandleTravellingToSurfaceShuttle(TimeSpan delta, Shuttle shuttle, ShuttlePrototype prototype)
    {
        if (!MoveShuttleTowardsLocation(delta, shuttle, prototype, shuttle.SurfacePosition, ShuttleState.WaitingOnSurface))
        {
            return;
        }

        RecruitWorkers(shuttle);
        LoadCargo(shuttle);
    }

    private void LoadCargo(Shuttle shuttle)
    {
        Debug.WriteLine("TODO: ShuttleScheduleService.LoadCargo");
    }

    private void RecruitWorkers(Shuttle shuttle)
    {
        foreach (var requiredWorkerPrototypeId in _workerRecruitementService.GetRequiredWorkers())
        {
            var requiredAmount = _workerRecruitementService.GetWorkerRequirement(requiredWorkerPrototypeId);
            _workerRecruitementService.ReduceWorkerRequirement(requiredWorkerPrototypeId, requiredAmount);
            for (var i = 0; i < requiredAmount; ++i)
            {
                if (!_workerCreationService.CreateWorker(
                    requiredWorkerPrototypeId,
                    shuttle.SurfacePosition / ResourceConstants.CellSize - new Vector2(0, 1)))
                {
                    _workerRecruitementService.IncreaseWorkerRequirement(requiredWorkerPrototypeId, 1);
                }
            }
        }
    }

    private void HandleWaitingOnSurfaceShuttle(Shuttle shuttle, ShuttlePrototype prototype)
    {
        if (shuttle.Elapsed >= prototype.LoadingTime)
        {
            shuttle.State = ShuttleState.LeavingSurface;
            shuttle.Elapsed = 0;
        }
    }

    private void HandleLeavingSurfaceShuttle(TimeSpan delta, Shuttle shuttle, ShuttlePrototype prototype)
    {
        MoveShuttleTowardsLocation(delta, shuttle, prototype, shuttle.LeavingPosition, ShuttleState.Complete);
    }

    private void HandleCompleteShuttle(Shuttle shuttle)
    {
        shuttle.State = ShuttleState.Idle;
        shuttle.Elapsed = 0;
        shuttle.Position = shuttle.StartingPosition;
    }
}
