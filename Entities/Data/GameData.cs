﻿namespace zeno_copenhagen.Entities.Data;

public class GameData : IGameData
{
    public TerrainData Terrain { get; } = new();
    public ShuttleData Shuttle { get; } = new();
}
