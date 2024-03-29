namespace zeno_copenhagen.Scenes;

public interface IScene
{
    void Initialise();
    void Update(TimeSpan delta);
    void Draw(TimeSpan delta, Matrix camera);
}
