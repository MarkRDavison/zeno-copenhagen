using zeno_copenhagen.Commands.AddResource;

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
        services.AddSingleton<ISpriteSheetService, SpriteSheetService>();
        services.AddSingleton<IInputActionManager, InputActionManager>();
        services.AddSingleton<IInputManager, InputManager>();
        services.AddSingleton<IGameCamera, GameCamera>();

        services.AddTransient<GameScene>();
        services.AddTransient<TerrainView>();
        services.AddTransient<ShuttleView>();
        services.AddTransient<BuildingView>();
        services.AddTransient<JobView>();
        services.AddTransient<WorkerView>();
        services.AddTransient<UiView>();

        services.AddSingleton<IGameCommandService, GameCommandService>();

        services.AddTransient<IGameCommandHandler<DigTileCommand>, DigTileCommandHandler>();
        services.AddTransient<IGameCommandHandler<DigShaftCommand>, DigShaftCommandHandler>();
        services.AddTransient<IGameCommandHandler<CreateShuttleCommand>, CreateShuttleCommandHandler>();
        services.AddTransient<IGameCommandHandler<PlaceBuildingCommand>, PlaceBuildingCommandHandler>();
        services.AddTransient<IGameCommandHandler<AddResourceCommand>, AddResourceCommandHandler>();

        services.AddSingleton<IPrototypeService<ShuttlePrototype, Shuttle>, ShuttlePrototypeService>();
        services.AddSingleton<IPrototypeService<BuildingPrototype, Building>, BuildingPrototypeService>();
        services.AddSingleton<IPrototypeService<JobPrototype, Job>, JobPrototypeService>();
        services.AddSingleton<IPrototypeService<WorkerPrototype, Worker>, WorkerPrototypeService>();

        services.AddSingleton<IGameResourceService, GameResourceService>();

        services.AddSingleton<IShuttleScheduleService, ShuttleScheduleService>();
        services.AddSingleton<IWorkerRecruitementService, WorkerRecruitementService>();
        services.AddSingleton<IBuildingPlacementService, BuildingPlacementService>();
        services.AddSingleton<IJobCreationService, JobCreationService>();
        services.AddSingleton<IJobCleanupService, JobCleanupService>();
        services.AddSingleton<IWorkerCreationService, WorkerCreationService>();
        services.AddSingleton<IJobAllocationService, JobAllocationService>();
        services.AddSingleton<IWorkerMovementService, WorkerMovementService>();

        return services;
    }
}
