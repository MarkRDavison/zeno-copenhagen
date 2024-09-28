using zeno_copenhagen.Commands.AddResource;

namespace zeno_copenhagen;

public sealed class Game : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private Application _application = default!;
    private IServiceProvider _services = default!;

    bool _isFullscreen = false;
    bool _isBorderless = false;
    int _width = 0;
    int _height = 0;

    public Game()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        var serviceCollection = new ServiceCollection();
        serviceCollection.Initialise(_graphics, Content);
        _services = serviceCollection.BuildServiceProvider();
    }

    protected override void Initialize()
    {
        _application = _services.GetRequiredService<Application>();

        base.Initialize();
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }
        _application.SetScene(_services.GetRequiredService<GameScene>());

        var gameCommandService = _services.GetRequiredService<IGameCommandService>();

        SeedData(
            gameCommandService,
            _services.GetRequiredService<IGameData>(),
            _services.GetRequiredService<IPrototypeService<ShuttlePrototype, Shuttle>>(),
            _services.GetRequiredService<IPrototypeService<BuildingPrototype, Building>>(),
            _services.GetRequiredService<IPrototypeService<JobPrototype, Job>>(),
            _services.GetRequiredService<IPrototypeService<WorkerPrototype, Worker>>());

        IntializeMap(
            gameCommandService,
            _services.GetRequiredService<IGameResourceService>());
    }

    private static void IntializeMap(
        IGameCommandService gameCommandService,
        IGameResourceService gameResourceService)
    {
        const int MAX_WIDTH = 5;
        const int MAX_HEIGHT = 1;

        for (int y = 0; y < MAX_HEIGHT; ++y)
        {
            gameCommandService.Execute<DigShaftCommand>(new(new()));
        }

        for (int y = 0; y <= MAX_HEIGHT; ++y)
        {
            for (int x = 1; x <= MAX_WIDTH; ++x)
            {
                gameCommandService.Execute<DigTileCommand>(new(new() { Level = y, Column = -x }));
                gameCommandService.Execute<DigTileCommand>(new(new() { Level = y, Column = +x }));
            }
        }

        gameCommandService.Execute<CreateShuttleCommand>(new(new("Shuttle_Basic")));
        gameCommandService.Execute<PlaceBuildingCommand>(new(new(new Vector2(1, 0), "Building_Bunk")));
        gameCommandService.Execute<PlaceBuildingCommand>(new(new(new Vector2(3, 0), "Building_Hut")));
        gameCommandService.Execute<PlaceBuildingCommand>(new(new(new Vector2(1, 1), "Building_Miner")));

        gameResourceService.SetResource("Resource_Gold", 300);
    }

    private static void SeedData(
        IGameCommandService gameCommandService,
        IGameData data,
        IPrototypeService<ShuttlePrototype, Shuttle> shuttlePrototypeService,
        IPrototypeService<BuildingPrototype, Building> buildingPrototypeService,
        IPrototypeService<JobPrototype, Job> jobPrototypeService,
        IPrototypeService<WorkerPrototype, Worker> workerPrototypeService)
    {

        {   //  Shuttle prototypes
            var prototype = new ShuttlePrototype
            {
                Id = StringHash.Hash("Shuttle_Basic"),
                Name = "Shuttle_Basic",
                TextureName = "SHUTTLE_BASIC",
                Capacity = 25,
                LoadingTime = 5.0f,
                IdleTime = 25.0f,
                Speed = 4.0f * ResourceConstants.CellSize
            };

            shuttlePrototypeService.RegisterPrototype(prototype.Id, prototype);
        }

        {   // Job prototypes
            jobPrototypeService.RegisterPrototype(
                StringHash.Hash("Job_Mine"),
                new JobPrototype
                {
                    Id = StringHash.Hash("Job_Mine"),
                    Name = "Job_Mine",
                    Repeats = true,
                    Work = 5.0f,
                    OnWorkComplete = (s, j) => s.GetRequiredService<IGameCommandService>().Execute<AddResourceCommand>(new(new() // TODO: new(new( do not like
                    {
                        Name = "Resource_Gold",
                        Amount = 3 * int.Max(1, (int)j.TileCoords.Y)
                    }))
                });
        }

        {   // Building prototypes
            buildingPrototypeService.RegisterPrototype(
                StringHash.Hash("Building_Bunk"),
                new BuildingPrototype
                {
                    Id = StringHash.Hash("Building_Bunk"),
                    Name = "Building_Bunk",
                    TextureName = "BUILDING_BUNK",
                    Size = new Vector2(2, 1)
                });

            buildingPrototypeService.RegisterPrototype(
                StringHash.Hash("Building_Hut"),
                new BuildingPrototype
                {
                    Id = StringHash.Hash("Building_Hut"),
                    Name = "Building_Hut",
                    TextureName = "BUILDING_HUT",
                    Size = new Vector2(2, 1)
                });

            buildingPrototypeService.RegisterPrototype(
                StringHash.Hash("Building_Miner"),
                new BuildingPrototype
                {
                    Id = StringHash.Hash("Building_Miner"),
                    Name = "Building_Miner",
                    TextureName = "BUILDING_MINER",
                    Size = new Vector2(3, 1),
                    ProvidedJobs =
                    {
                        new ProvidedJob("Job_Mine", new Vector2(1.0f, 0.0f))
                    },
                    RequiredWorkers =
                    {
                        new RequiredWorker("Worker_Miner", 1)
                    }
                });
        }

        {   // Worker prototypes
            workerPrototypeService.RegisterPrototype(
                StringHash.Hash("Worker_Miner"),
                new WorkerPrototype
                {
                    Id = StringHash.Hash("Worker_Miner"),
                    Name = "Worker_Miner",
                    TextureName = "WORKER",
                    Speed = 2.0f,
                    Jobs =
                    {
                        "Job_Mine"
                    }
                });
        }
    }

    protected override void LoadContent()
    {
        _application.LoadContent(_graphics, Content);
    }

    protected override void Update(GameTime gameTime)
    {
        var inputManager = _services.GetRequiredService<IInputManager>();

        _application.Update(gameTime.ElapsedGameTime);

        base.Update(gameTime);

        inputManager.CacheInputs();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _application.Draw(gameTime.ElapsedGameTime);

        base.Draw(gameTime);
    }

    public void ToggleFullscreen()
    {
        bool oldIsFullscreen = _isFullscreen;

        if (_isBorderless)
        {
            _isBorderless = false;
        }
        else
        {
            _isFullscreen = !_isFullscreen;
        }

        ApplyFullscreenChange(oldIsFullscreen);
    }
    public void ToggleBorderless()
    {
        bool oldIsFullscreen = _isFullscreen;

        _isBorderless = !_isBorderless;
        _isFullscreen = _isBorderless;

        ApplyFullscreenChange(oldIsFullscreen);
    }

    private void ApplyFullscreenChange(bool oldIsFullscreen)
    {
        if (_isFullscreen)
        {
            if (oldIsFullscreen)
            {
                ApplyHardwareMode();
            }
            else
            {
                SetFullscreen();
            }
        }
        else
        {
            UnsetFullscreen();
        }
    }
    private void ApplyHardwareMode()
    {
        _graphics.HardwareModeSwitch = !_isBorderless;
        _graphics.ApplyChanges();
    }
    private void SetFullscreen()
    {
        _width = Window.ClientBounds.Width;
        _height = Window.ClientBounds.Height;

        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphics.HardwareModeSwitch = !_isBorderless;

        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
    }

    private void UnsetFullscreen()
    {
        _graphics.PreferredBackBufferWidth = _width;
        _graphics.PreferredBackBufferHeight = _height;
        _graphics.IsFullScreen = false;
        _graphics.ApplyChanges();
    }
}
