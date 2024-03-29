namespace zeno_copenhagen.Resource;

public interface ISpriteSheetService
{
    void RegisterSprite(string name, Vector2 coords, Vector2 size);
    SpriteInfo GetSpriteInfo(string name);
}
