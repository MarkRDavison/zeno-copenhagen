namespace zeno_copenhagen.Services.Workers;

public sealed class WorkerMovementService : IWorkerMovementService
{
    private readonly IGameData _gameData;

    public WorkerMovementService(IGameData gameData)
    {
        _gameData = gameData;
    }

    public void Update(TimeSpan delta)
    {
        foreach (var worker in _gameData.Worker.Workers)
        {
            UpdateWorker(worker, delta);
        }
    }

    public void UpdateWorker(Worker worker, TimeSpan delta)
    {
        // TODO: Strategy pattern?
        switch (worker.State)
        {
            case WorkerState.Idle:
                UpdateIdleWorker(worker, delta);
                break;
            case WorkerState.MovingToTarget:
                UpdateMovingToTargetWorker(worker, delta);
                break;
            case WorkerState.WorkingJob:
                UpdateWorkingJobWorker(worker, delta);
                break;
            default:
                break;
        }
    }

    private void UpdateWorkingJobWorker(Worker worker, TimeSpan delta)
    {

    }

    private (bool ReachedTarget, TimeSpan RemainingDelta) MoveTowardsTarget(float speed, TimeSpan delta, Worker worker, Vector2 target)
    {
        var distanceToTarget = (target - worker.Position).Length();
        var maxMovement = (float)delta.TotalSeconds * speed;

        if (distanceToTarget <= maxMovement)
        {
            worker.Position = target;

            var remainingDelta = delta - (distanceToTarget / maxMovement * delta);

            return (true, remainingDelta);
        }

        worker.Position += Vector2.Normalize(target - worker.Position) * maxMovement;

        return (false, TimeSpan.Zero);
    }

    private void UpdateMovingToTargetWorker(Worker worker, TimeSpan delta)
    {
        var targetPosition = worker.Target;
        if (worker.Position.Y != worker.Target.Y)
        {
            if (worker.Position.X == 0)
            {
                // On elevator, go to destination floor
                targetPosition = new Vector2(0, targetPosition.Y);
            }
            else
            {
                // Go to elevator on current floor
                targetPosition = new Vector2(0, worker.Position.Y);
            }
        }

        var (reachedTarget, remainingDelta) = MoveTowardsTarget(worker.Speed, delta, worker, targetPosition);

        if (reachedTarget && worker.Position == worker.Target)
        {
            worker.State = WorkerState.WorkingJob;
        }

        if (remainingDelta > TimeSpan.Zero)
        {
            UpdateWorker(worker, remainingDelta);
        }
    }

    private void UpdateIdleWorker(Worker worker, TimeSpan delta)
    {
        if (worker.AllocatedJobId is not null &&
            _gameData.Job.GetJobById(worker.AllocatedJobId) is { } job)
        {
            worker.State = WorkerState.MovingToTarget;
            worker.Target = job.TileCoords + new Vector2(job.Offset.X, 0);

            UpdateWorker(worker, delta);
            return;
        }
    }
}
