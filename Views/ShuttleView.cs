namespace zeno_copenhagen.Views;

public sealed class ShuttleView : BaseView
{
    private SpriteBatch _spriteBatch;
    private readonly IGameData _gameData;
    private readonly IPrototypeService<ShuttlePrototype, Shuttle> _shuttlePrototypeService;

    public ShuttleView(
        IGameData gameData,
        IResourceService resourceService,
        ISpriteSheetService spriteSheetService,
        IPrototypeService<ShuttlePrototype, Shuttle> shuttlePrototypeService) :
    base(
        resourceService,
        spriteSheetService)
    {
        _gameData = gameData;
        _spriteBatch = _resourceService.CreateSpriteBatch();
        _shuttlePrototypeService = shuttlePrototypeService;
    }

    public override void Draw(TimeSpan delta, Matrix camera)
    {
        _spriteBatch.Begin(transformMatrix: camera);

        foreach (var shuttle in _gameData.Shuttle.Shuttles)
        {
            if (!_shuttlePrototypeService.IsPrototypeRegistered(shuttle.PrototypeId))
            {
                continue;
            }

            var prototype = _shuttlePrototypeService.GetPrototype(shuttle.PrototypeId);
            var spriteInfo = _spriteSheetService.GetSpriteInfo(prototype.TextureName);

            if (!spriteInfo.Valid)
            {
                continue;
            }

            var destination = new Rectangle(
                (int)(-spriteInfo.Size.X / 2.0f) * ResourceConstants.CellSize + (int)shuttle.Position.X,
                (int)(-spriteInfo.Size.Y) * ResourceConstants.CellSize + (int)shuttle.Position.Y,
                (int)spriteInfo.Size.X * ResourceConstants.CellSize,
                (int)spriteInfo.Size.Y * ResourceConstants.CellSize);

            var source = new Rectangle(
                (int)spriteInfo.Coordinates.X * ResourceConstants.CellSize,
                (int)spriteInfo.Coordinates.Y * ResourceConstants.CellSize,
                (int)spriteInfo.Size.X * ResourceConstants.CellSize,
                (int)spriteInfo.Size.Y * ResourceConstants.CellSize);

            _spriteBatch.Draw(_spritesheetTexture, destination, source, Color.White);
        }

        _spriteBatch.End();
    }

    public override void Update(TimeSpan delta)
    {

    }
}
