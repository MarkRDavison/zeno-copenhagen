namespace zeno_copenhagen.Services.Resources;

public interface IGameResourceService
{
    void SetResource(string name, int amount);
    void SetResource(Guid id, int amount);

    int GetResource(string name);
    int GetResource(Guid id);

    void ReduceResource(string name, int amount);
    void ReduceResource(Guid id, int amount);

    void AddResource(string name, int amount);
    void AddResource(Guid id, int amount);
}
