namespace zeno_copenhagen.Infrastructure
{
    public class Application
    {
        private IScene? _scene;
        private readonly IResourceService _resourceService;
        private readonly ISpriteSheetService _spriteSheetService;
        private GraphicsDeviceManager _graphicsDeviceManager = default!;

        public Application(
            IResourceService resourceService,
            ISpriteSheetService spriteSheetService)
        {
            _resourceService = resourceService;
            _spriteSheetService = spriteSheetService;
        }

        public void LoadContent(GraphicsDeviceManager graphicsDeviceManager, ContentManager content)
        {
            _graphicsDeviceManager = graphicsDeviceManager;

            _resourceService.AddTexture2D(ResourceConstants.SpriteSheetName, content.Load<Texture2D>("Textures/tile_sprite_sheet"));

            _spriteSheetService.RegisterSprite("DIRT", new Vector2(0, 0), new Vector2(1, 1));
            _spriteSheetService.RegisterSprite("EMPTY", new Vector2(1, 0), new Vector2(1, 1));
            _spriteSheetService.RegisterSprite("LADDER", new Vector2(0, 1), new Vector2(1, 1));
            _spriteSheetService.RegisterSprite("LADDER_DRILL", new Vector2(0, 2), new Vector2(1, 1));
            _spriteSheetService.RegisterSprite("SHUTTLE_BASIC", new Vector2(0, 5), new Vector2(3, 2));
        }

        public void Update(TimeSpan delta)
        {
            _scene?.Update(delta);
        }

        public void Draw(TimeSpan delta)
        {
            // TODO: Bad, each view may have its own matrix

            _scene?.Draw(delta, Matrix.CreateTranslation(new Vector3(
                _graphicsDeviceManager.PreferredBackBufferWidth / 2 - 32, 64, 0)));
        }

        public void SetScene(IScene? scene)
        {
            _scene = scene;
            if (_scene != null)
            {
                _scene.Initialise();
            }
        }
    }
}
