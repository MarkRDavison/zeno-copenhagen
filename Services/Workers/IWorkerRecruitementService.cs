namespace zeno_copenhagen.Services.Workers;

public interface IWorkerRecruitementService
{
    void ReduceWorkerRequirement(Guid workerPrototypeId, int amount);
    void IncreaseWorkerRequirement(Guid workerPrototypeId, int amount);
    int GetWorkerRequirement(Guid workerPrototypeId);

    HashSet<Guid> GetRequiredWorkers();
}
