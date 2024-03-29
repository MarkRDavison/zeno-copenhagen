namespace zeno_copenhagen.Resource;

public interface IResourceService
{
    SpriteBatch CreateSpriteBatch();
    void AddTexture2D(string name, string path);
    void AddTexture2D(string name, Texture2D texture);
    Texture2D? GetTexture2D(string name);
}
