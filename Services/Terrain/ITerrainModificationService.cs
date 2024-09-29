namespace zeno_copenhagen.Services.Terrain;

public interface ITerrainModificationService
{
    bool EnsureTilesExistIncluding(Vector2 tilePosition);
}
