namespace zeno_copenhagen.Ignition;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection Initialise(this IServiceCollection services, GraphicsDeviceManager graphicsDeviceManager, ContentManager content)
    {
        services.AddSingleton<IGameData, GameData>();
        services.AddSingleton<Application>();
        services.AddSingleton<GraphicsDeviceManager>(graphicsDeviceManager);
        services.AddSingleton<ContentManager>(content);
        services.AddSingleton<IResourceService, ResourceService>();
        services.AddSingleton<IGameCommandService, GameCommandService>();
        services.AddSingleton<ISpriteSheetService, SpriteSheetService>();

        services.AddTransient<GameScene>();
        services.AddTransient<TerrainView>();
        services.AddTransient<ShuttleView>();

        services.AddTransient<IGameCommandHandler<DigTileCommand>, DigTileCommandHandler>();
        services.AddTransient<IGameCommandHandler<DigShaftCommand>, DigShaftCommandHandler>();
        services.AddTransient<IGameCommandHandler<CreateShuttleCommand>, CreateShuttleCommandHandler>();

        services.AddSingleton<IPrototypeService<ShuttlePrototype, Shuttle>, ShuttlePrototypeService>();

        services.AddSingleton<IShuttleScheduleService, ShuttleScheduleService>();

        return services;
    }
}
