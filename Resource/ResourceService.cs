namespace zeno_copenhagen.Resource;

public class ResourceService : IResourceService
{
    private readonly GraphicsDevice _graphicsDevice;
    private readonly IDictionary<string, IDictionary<string, IDisposable>> _resources;

    public ResourceService(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;
        _resources = new Dictionary<string, IDictionary<string, IDisposable>>();
        _resources.Add(nameof(Texture2D), new Dictionary<string, IDisposable>());
    }

    public void AddTexture2D(string name, string path)
    {
        AddTexture2D(name, Texture2D.FromFile(_graphicsDevice, path));
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
}
