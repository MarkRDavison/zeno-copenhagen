﻿
namespace zeno_copenhagen.Services.Jobs;

public sealed class JobAllocationService : IJobAllocationService
{
    private readonly IGameData _gameData;
    private readonly IPrototypeService<WorkerPrototype, Worker> _workerPrototypeService;

    public JobAllocationService(
        IGameData gameData,
        IPrototypeService<WorkerPrototype, Worker> workerPrototypeService)
    {
        _gameData = gameData;
        _workerPrototypeService = workerPrototypeService;
    }

    public void AllocateJobs() => AllocateJobs(-1);

    public void AllocateJobs(int number)
    {
        if (number < 0)
        {
            number = int.MaxValue;
        }

        foreach (var job in _gameData.Job.Jobs.Where(_ => _.AllocatedWorkerId is null))
        {
            foreach (var worker in _gameData.Worker.Workers.Where(_ => _.AllocatedJobId is null))
            {
                if (CanWorkerPerformJob(worker, job) && CanWorkerReachJob(worker, job))
                {
                    worker.AllocatedJobId = job.Id;
                    job.AllocatedWorkerId = worker.Id;
                    number--;

                    if (number <= 0)
                    {
                        return;
                    }

                    break;
                }
            }
        }
    }

    private bool CanWorkerReachJob(Worker worker, Job job)
    {
        var workingPosition = job.TileCoords + job.Offset;

        return _gameData.Terrain.CanReachPosition(workingPosition);
    }

    public bool CanWorkerPerformJob(Worker worker, Job job)
    {
        var workerPrototype = _workerPrototypeService.GetPrototype(worker.PrototypeId);

        return workerPrototype.Jobs.Any(_ => StringHash.Hash(_) == job.PrototypeId);
    }
}
