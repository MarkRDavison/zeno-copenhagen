namespace zeno_copenhagen.Ui;

public class UiButton : UiComponent
{
    protected readonly IResourceService _resourceService;
    protected readonly ISpriteSheetService _spriteSheetService;
    protected readonly IGameCamera _gameCamera;
    protected readonly IInputActionManager _inputActionManager;
    protected readonly GraphicsDeviceManager _graphicsDeviceManager;
    protected Texture2D _spritesheetTexture = default!;

    public UiButton(
        IResourceService resourceService,
        ISpriteSheetService spriteSheetService,
        IGameCamera gameCamera,
        IInputActionManager inputActionManager,
        GraphicsDeviceManager graphicsDeviceManager)
    {
        _resourceService = resourceService;
        _spriteSheetService = spriteSheetService;
        _gameCamera = gameCamera;
        _inputActionManager = inputActionManager;
        _graphicsDeviceManager = graphicsDeviceManager;

        _spritesheetTexture = _resourceService.GetTexture2D(ResourceConstants.SpriteSheetName) ?? throw new InvalidOperationException("Spritesheet texture is invalid");
    }

    public override void Update(TimeSpan delta)
    {
        if (!IsVisible())
        {
            return;
        }

        if (Disabled)
        {
            IsHovered = false;
            return;
        }

        var spriteInfo = _spriteSheetService.GetSpriteInfo("BUTTON");

        if (!spriteInfo.Valid)
        {
            return;
        }

        var mousePosition = _inputActionManager.GetMousePosition();

        var buttonPosition = GetPosition(spriteInfo);

        var buttonBounds = new Rectangle(
            (int)buttonPosition.X,
            (int)buttonPosition.Y,
            (int)spriteInfo.Size.X * ResourceConstants.CellSize,
            (int)spriteInfo.Size.Y * ResourceConstants.CellSize);

        IsHovered = buttonBounds.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y));

        if (IsHovered && _inputActionManager.IsActionInvoked("LCLICK"))
        {
            OnClick?.Invoke(string.IsNullOrEmpty(Id) ? Label : Id);
        }
    }

    private Vector2 GetPosition(SpriteInfo spriteInfo)
    {
        var position = Position;

        if (position.Y < 0)
        {
            position.Y += _graphicsDeviceManager.PreferredBackBufferHeight - (int)spriteInfo.Size.Y * ResourceConstants.CellSize;
        }
        if (position.X < 0)
        {
            position.X += _graphicsDeviceManager.PreferredBackBufferWidth - (int)spriteInfo.Size.X * ResourceConstants.CellSize;
        }

        return position;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsVisible())
        {
            return;
        }

        var spriteInfo = _spriteSheetService.GetSpriteInfo("BUTTON");
        if (!spriteInfo.Valid)
        {
            return;
        }

        var position = GetPosition(spriteInfo);

        var destination = new Rectangle(
            (int)position.X,
            (int)position.Y,
            (int)spriteInfo.Size.X * ResourceConstants.CellSize,
            (int)spriteInfo.Size.Y * ResourceConstants.CellSize);

        var source = new Rectangle(
            (int)spriteInfo.Coordinates.X * ResourceConstants.CellSize,
            (int)spriteInfo.Coordinates.Y * ResourceConstants.CellSize,
            (int)spriteInfo.Size.X * ResourceConstants.CellSize,
            (int)spriteInfo.Size.Y * ResourceConstants.CellSize);

        spriteBatch.Draw(_spritesheetTexture, destination, source, GetColor());

        if (!string.IsNullOrEmpty(Label) &&
            _resourceService.GetSpriteFont(ResourceConstants.DebugFontName) is { } font)
        {
            var bounds = font.MeasureString(Label);


            spriteBatch.DrawString(
                font,
                Label,
                new Vector2((int)position.X, (int)position.Y) +
                new Vector2((int)spriteInfo.Size.X * ResourceConstants.CellSize, (int)spriteInfo.Size.Y * ResourceConstants.CellSize) / 2 -
                bounds / 2,
                GetColor());
        }
    }

    private Color GetColor()
    {
        if (Disabled)
        {
            return Color.DarkGray;
        }

        return (IsHovered || IsForceActive()) ? HoverColor : Color;
    }

    // TODO: Configureable size, so can take [ then 0-many = then ]

    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public Color HoverColor { get; set; } = Color.Green;
    public bool IsHovered { get; private set; }
    public Func<bool> IsForceActive { get; set; } = () => false;
    public Func<bool> IsVisible { get; set; } = () => true;
    public bool Disabled { get; set; }
    public Action<string>? OnClick { get; set; }
}
