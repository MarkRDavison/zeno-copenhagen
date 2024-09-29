namespace zeno_copenhagen.Services;

public enum UiState
{
    Idle = 0,
    Dig,
    Build,
    Tech
}

public interface IGameInteractionService
{
    void Update(TimeSpan delta);

    bool IsMouseOverDrill();

    bool CanDrillLevel();

    UiState State { get; set; }

    string ActiveBuilding { get; set; }
}
