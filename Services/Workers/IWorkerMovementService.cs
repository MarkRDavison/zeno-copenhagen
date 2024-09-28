namespace zeno_copenhagen.Services.Workers;

public interface IWorkerMovementService
{
    void Update(TimeSpan delta);
    void UpdateWorker(Worker worker, TimeSpan delta);
}
