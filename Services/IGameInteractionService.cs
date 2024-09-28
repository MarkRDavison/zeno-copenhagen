namespace zeno_copenhagen.Services;

public interface IGameInteractionService
{
    void Update(TimeSpan delta);

    bool IsMouseOverDrill();

    bool CanDrillLevel();
}
