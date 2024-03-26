namespace zeno_copenhagen.Views;

public interface IView
{
    void Update(TimeSpan delta);
    void Draw(TimeSpan delta);
}
