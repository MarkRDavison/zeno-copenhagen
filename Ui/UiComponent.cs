namespace zeno_copenhagen.Ui;

public abstract class UiComponent
{
    public abstract void Update(TimeSpan delta);
    public abstract void Draw(SpriteBatch spriteBatch);

    public Vector2 Position { get; set; }
    public Color Color { get; set; } = Color.White;
}
