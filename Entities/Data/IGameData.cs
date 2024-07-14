namespace zeno_copenhagen.Entities.Data;

public interface IGameData
{
    TerrainData Terrain { get; }
    ShuttleData Shuttle { get; }
    BuildingData Building { get; }
}
