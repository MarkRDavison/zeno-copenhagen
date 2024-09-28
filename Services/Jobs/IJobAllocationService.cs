namespace zeno_copenhagen.Services.Jobs;

public interface IJobAllocationService
{
    void AllocateJobs();
    void AllocateJobs(int number);
    bool CanWorkerPerformJob(Worker worker, Job job);
}
