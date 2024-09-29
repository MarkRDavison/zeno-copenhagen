namespace zeno_copenhagen.Services.Jobs;

public interface IJobCreationService
{
    bool CreateJob(Guid prototypeId, Vector2 offset, Vector2 coords);
    bool CreateJob(Guid prototypeId, Guid relatedPrototypeId, Vector2 offset, Vector2 coords);
}
