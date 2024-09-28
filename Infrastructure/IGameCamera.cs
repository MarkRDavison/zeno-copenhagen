namespace zeno_copenhagen.Infrastructure;

public interface IGameCamera
{
    void Move(Vector2 offset);
    Vector2 Translation { get; }
    Vector2 GetTargetPixel();
}
