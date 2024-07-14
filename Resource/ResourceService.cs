namespace zeno_copenhagen.Resource;

public sealed class ResourceService : IResourceService, IDisposable
{
    private readonly GraphicsDeviceManager _graphicsDeviceManager;
    private readonly ContentManager _contentManager;
    private readonly IDictionary<string, IDictionary<string, object>> _resources;
    private bool disposedValue;

    public ResourceService(
        GraphicsDeviceManager graphicsDeviceManager,
        ContentManager contentManager)
    {
        _graphicsDeviceManager = graphicsDeviceManager;
        _contentManager = contentManager;
        _resources = new Dictionary<string, IDictionary<string, object>>
        {
            { nameof(Texture2D), new Dictionary<string, object>() },
            { nameof(SpriteFont), new Dictionary<string, object>() }
        };
    }

    public SpriteBatch CreateSpriteBatch()
    {
        return new SpriteBatch(_graphicsDeviceManager.GraphicsDevice);
    }

    public void AddTexture2D(string name, string path)
    {
        AddTexture2D(name, Texture2D.FromFile(_graphicsDeviceManager.GraphicsDevice, path));
    }

    public void AddTexture2D(string name, Texture2D texture)
    {
        var textures = _resources[nameof(Texture2D)];
        if (textures.ContainsKey(name))
        {
            textures[name] = texture;
        }
        else
        {
            textures.Add(name, texture);
        }
    }

    public Texture2D? GetTexture2D(string name)
    {
        var textures = _resources[nameof(Texture2D)];

        if (textures.TryGetValue(name, out var resource) && resource is Texture2D texture)
        {
            return texture;
        }

        return null;
    }

    public void AddSpriteFont(string name, string path)
    {
        AddSpriteFont(name, _contentManager.Load<SpriteFont>(path));
    }

    public void AddSpriteFont(string name, SpriteFont font)
    {
        var fonts = _resources[nameof(SpriteFont)];
        if (fonts.ContainsKey(name))
        {
            fonts[name] = font;
        }
        else
        {
            fonts.Add(name, font);
        }
    }

    public SpriteFont? GetSpriteFont(string name)
    {
        var fonts = _resources[nameof(SpriteFont)];

        if (fonts.TryGetValue(name, out var resource) && resource is SpriteFont font)
        {
            return font;
        }

        return null;
    }

    public void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach (var (_, resources) in _resources)
                {
                    foreach (var (_, res) in resources)
                    {
                        if (res is IDisposable disRes)
                        {
                            disRes.Dispose();
                        }
                    }
                }

                _resources.Clear();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
