namespace zeno_copenhagen.Views;

public sealed class UiView : BaseView
{
    private readonly IGameData _gameData;
    private readonly IInputActionManager _inputActionManager;
    private readonly IGameCamera _camera;
    private SpriteBatch _spriteBatch;

    public UiView(
        IGameData gameData,
        IResourceService resourceService,
        ISpriteSheetService spriteSheetService,
        IInputActionManager inputActionManager,
        IGameCamera camera
    ) : base(
        resourceService,
        spriteSheetService)
    {
        _gameData = gameData;
        _inputActionManager = inputActionManager;
        _camera = camera;
        _spriteBatch = _resourceService.CreateSpriteBatch();
    }

    public override void Update(TimeSpan delta)
    {
        if (_inputActionManager.IsActionInvoked("LCLICK"))
        {
            if (_inputActionManager.GetTilePosition(_camera) is { } coords)
            {
                Debug.WriteLine($"Tile: {coords.X}, {coords.Y}");
            }
            else
            {
                Debug.WriteLine("No tile");
            }
        }
    }

    public override void Draw(TimeSpan delta, Matrix camera)
    {
        if (_resourceService.GetSpriteFont(ResourceConstants.DebugFontName) is { } font)
        {
            _spriteBatch.Begin(transformMatrix: camera);
            _spriteBatch.DrawString(
                font,
                $"Jobs: {_gameData.Job.Jobs.Count}",
                new Vector2(4, 0),
                Color.Black,
                0,
                new Vector2(),
                1.0f,
                SpriteEffects.None,
                0.5f);
            _spriteBatch.DrawString(
                font,
                $"Buildings: {_gameData.Building.Buildings.Count}",
                new Vector2(4, 20),
                Color.Black,
                0,
                new Vector2(),
                1.0f,
                SpriteEffects.None,
                0.5f);
            _spriteBatch.DrawString(
                font,
                $"Workers: {_gameData.Worker.Workers.Count}",
                new Vector2(4, 40),
                Color.Black,
                0,
                new Vector2(),
                1.0f,
                SpriteEffects.None,
                0.5f);
            _spriteBatch.End();
        }
    }
}
