namespace zeno_copenhagen.Views;

public sealed class UiView : BaseView
{
    private SpriteBatch _spriteBatch;

    public UiView(
        IResourceService resourceService,
        ISpriteSheetService spriteSheetService
    ) : base(
        resourceService,
        spriteSheetService)
    {
        _spriteBatch = _resourceService.CreateSpriteBatch();
    }

    public override void Update(TimeSpan delta)
    {
    }

    public override void Draw(TimeSpan delta, Matrix camera)
    {
        if (_resourceService.GetSpriteFont(ResourceConstants.DebugFontName) is { } font)
        {
            var text = "MonoGame Font Test";
            _spriteBatch.Begin(transformMatrix: camera);
            _spriteBatch.DrawString(
                font,
                text,
                new Vector2(128, 128),
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
