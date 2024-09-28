using System.Linq;

namespace zeno_copenhagen.Services.Workers;

public sealed class WorkerRecruitementService : IWorkerRecruitementService
{
    private readonly Dictionary<Guid, int> _workerRequirements = [];

    public int GetWorkerRequirement(Guid workerPrototypeId)
    {
        if (_workerRequirements.TryGetValue(workerPrototypeId, out var requirement))
        {
            return requirement;
        }

        return 0;
    }

    public void IncreaseWorkerRequirement(Guid workerPrototypeId, int amount)
    {
        if (_workerRequirements.ContainsKey(workerPrototypeId))
        {
            _workerRequirements[workerPrototypeId] += amount;
        }
        else
        {
            _workerRequirements.Add(workerPrototypeId, amount);
        }
    }

    public void ReduceWorkerRequirement(Guid workerPrototypeId, int amount)
    {
        if (_workerRequirements.ContainsKey(workerPrototypeId))
        {
            _workerRequirements[workerPrototypeId] -= amount;
        }
    }

    public HashSet<Guid> GetRequiredWorkers()
    {
        return _workerRequirements.Where(_ => _.Value > 0).Select(_ => _.Key).ToHashSet();
    }
}
