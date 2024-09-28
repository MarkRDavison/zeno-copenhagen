namespace zeno_copenhagen.Prototypes;

public sealed class JobPrototypeService : PrototypeService<JobPrototype, Job>
{
    public override Job CreateEntity(JobPrototype prototype)
    {
        return new Job
        {
            Id = Guid.NewGuid(),
            PrototypeId = prototype.Id
        };
    }
}
