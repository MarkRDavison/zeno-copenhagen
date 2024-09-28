namespace zeno_copenhagen.Services.Resources;

public class GameResourceService : IGameResourceService
{
    private readonly IDictionary<Guid, int> _resources;

    public GameResourceService()
    {
        _resources = new Dictionary<Guid, int>();
    }

    public void AddResource(string name, int amount) => AddResource(StringHash.Hash(name), amount);

    public void AddResource(Guid id, int amount)
    {
        SetResource(id, GetResource(id) + amount);
    }

    public int GetResource(string name) => GetResource(StringHash.Hash(name));

    public int GetResource(Guid id)
    {
        if (_resources.ContainsKey(id))
        {
            return _resources[id];
        }

        return 0;
    }

    public void ReduceResource(string name, int amount) => ReduceResource(StringHash.Hash(name), amount);

    public void ReduceResource(Guid id, int amount)
    {
        SetResource(id, GetResource(id) - amount);
    }

    public void SetResource(string name, int amount) => SetResource(StringHash.Hash(name), amount);

    public void SetResource(Guid id, int amount)
    {
        _resources[id] = amount;
    }
}
