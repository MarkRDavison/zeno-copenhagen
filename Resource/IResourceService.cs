namespace zeno_copenhagen.Resource;

public interface IResourceService
{
    SpriteBatch CreateSpriteBatch();
    void AddTexture2D(string name, string path);
    void AddTexture2D(string name, Texture2D texture);
    Texture2D? GetTexture2D(string name);

    void AddSpriteFont(string name, string path);
    void AddSpriteFont(string name, SpriteFont font);
    SpriteFont? GetSpriteFont(string name);
}
