namespace zeno_copenhagen.Entities.Data;

public sealed class JobData
{
    public List<Job> Jobs { get; } = [];

    public Job? GetJobById(Guid? id) => Jobs.FirstOrDefault(_ => _.Id == id);
}
