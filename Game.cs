namespace zeno_copenhagen;

public class Game : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private Application _application = default!;
    private IServiceProvider _services = default!;

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

        _application.SetScene(_services.GetRequiredService<GameScene>());

        SeedData(
            _services.GetRequiredService<IGameCommandService>(),
            _services.GetRequiredService<IGameData>(),
            _services.GetRequiredService<IPrototypeService<ShuttlePrototype, Shuttle>>());
    }

    private void SeedData(
        IGameCommandService gameCommandService,
        IGameData data,
        IPrototypeService<ShuttlePrototype, Shuttle> shuttlePrototypeService)
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

        gameCommandService.Execute<CreateShuttleCommand>(new(new("Shuttle_Basic")));
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
}
