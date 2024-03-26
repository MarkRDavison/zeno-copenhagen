namespace zeno_copenhagen.Ignition;

public static class DependencyInjectionExtensions
{
    public static GameServiceContainer Initialise(this GameServiceContainer services)
    {
        services.AddService<IGameData>(new GameData());
        return services;
    }
}
