namespace zeno_copenhagen.Services.Workers;

public interface IWorkerCreationService
{
    bool CreateWorker(Guid prototypeId, Vector2 position);
}
