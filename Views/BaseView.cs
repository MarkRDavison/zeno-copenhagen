namespace zeno_copenhagen.Views;

public abstract class BaseView : IView
{
    public abstract void Draw(TimeSpan delta);
    public abstract void Update(TimeSpan delta);
}
