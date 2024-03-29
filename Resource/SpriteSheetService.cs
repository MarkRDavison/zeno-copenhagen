namespace zeno_copenhagen.Resource;

public class SpriteSheetService : ISpriteSheetService
{
    private readonly IDictionary<string, SpriteInfo> _spriteInfo;

    public SpriteSheetService()
    {
        _spriteInfo = new Dictionary<string, SpriteInfo>();
    }

    public SpriteInfo GetSpriteInfo(string name)
    {
        if (_spriteInfo.TryGetValue(name, out var spriteInfo))
        {
            return spriteInfo;
        }

        return new SpriteInfo(false, Vector2.Zero, Vector2.Zero);
    }

    public void RegisterSprite(string name, Vector2 coords, Vector2 size)
    {
        _spriteInfo.Add(name, new SpriteInfo(true, coords, size));
    }
}
