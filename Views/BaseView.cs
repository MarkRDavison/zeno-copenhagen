namespace zeno_copenhagen.Views;

public abstract class BaseView : IView
{
    protected readonly IResourceService _resourceService;
    protected readonly ISpriteSheetService _spriteSheetService;
    protected Texture2D _spritesheetTexture = default!;

    protected BaseView(
        IResourceService resourceService,
        ISpriteSheetService spriteSheetService)
    {
        _resourceService = resourceService;
        _spriteSheetService = spriteSheetService;

    }

    public void Initialise()
    {
        _spritesheetTexture = _resourceService.GetTexture2D(ResourceConstants.SpriteSheetName) ?? throw new InvalidOperationException("Spritesheet texture is invalid");

        InitialiseView();
    }

    public virtual void InitialiseView()
    {

    }

    public abstract void Update(TimeSpan delta);
    public abstract void Draw(TimeSpan delta, Matrix camera);

    protected void DrawTileAlignedSpriteCell(SpriteBatch batch, string spriteName, Vector2 cellOrigin)
    {
        var spriteInfo = _spriteSheetService.GetSpriteInfo(spriteName);
        if (!spriteInfo.Valid)
        {
            return;
        }

        var destination = new Rectangle(
            (int)cellOrigin.X * ResourceConstants.CellSize,
            (int)cellOrigin.Y * ResourceConstants.CellSize,
            (int)spriteInfo.Size.X * ResourceConstants.CellSize,
            (int)spriteInfo.Size.Y * ResourceConstants.CellSize);

        var source = new Rectangle(
            (int)spriteInfo.Coordinates.X * ResourceConstants.CellSize,
            (int)spriteInfo.Coordinates.Y * ResourceConstants.CellSize,
            (int)spriteInfo.Size.X * ResourceConstants.CellSize,
            (int)spriteInfo.Size.Y * ResourceConstants.CellSize);

        batch.Draw(_spritesheetTexture, destination, source, Color.White);
    }
}
