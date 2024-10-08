﻿namespace zeno_copenhagen.Entities.Data;

public sealed class GameData : IGameData
{
    public TerrainData Terrain { get; } = new();
    public ShuttleData Shuttle { get; } = new();
    public BuildingData Building { get; } = new();
    public JobData Job { get; } = new();
    public WorkerData Worker { get; } = new();
}
