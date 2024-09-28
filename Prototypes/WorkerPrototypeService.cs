namespace zeno_copenhagen.Prototypes;

public sealed class WorkerPrototypeService : PrototypeService<WorkerPrototype, Worker>
{
    public override Worker CreateEntity(WorkerPrototype prototype)
    {
        return new Worker
        {
            Id = Guid.NewGuid(),
            PrototypeId = prototype.Id,
            Speed = prototype.Speed
        };
    }
}