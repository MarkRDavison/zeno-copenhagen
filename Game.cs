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
            _services.GetRequiredService<IPrototypeService<BuildingPrototype, Building>>());

        IntializeMap(gameCommandService);
    }

    private void IntializeMap(IGameCommandService gameCommandService)
    {
        gameCommandService.Execute<CreateShuttleCommand>(new(new("Shuttle_Basic")));
        gameCommandService.Execute<PlaceBuildingCommand>(new(new(new Vector2(1, 0), "Building_Bunk")));
        gameCommandService.Execute<PlaceBuildingCommand>(new(new(new Vector2(3, 0), "Building_Hut")));
    }

    private void SeedData(
        IGameCommandService gameCommandService,
        IGameData data,
        IPrototypeService<ShuttlePrototype, Shuttle> shuttlePrototypeService,
        IPrototypeService<BuildingPrototype, Building> buildingPrototypeService)
    {
        const int MAX_WIDTH = 5;
        const int MAX_HEIGHT = 4;
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
                    Name = "BuildinBuilding_Hutg_Bunk",
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
                    Size = new Vector2(3, 1)
                });
        }
    }

    protected override void LoadContent()
    {
        _application.LoadContent(_graphics, Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        var kstate = Keyboard.GetState();

        _application.Update(gameTime.ElapsedGameTime);

        base.Update(gameTime);
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
