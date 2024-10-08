﻿namespace zeno_copenhagen.Views;

public interface IView
{
    void Initialise();
    void Update(TimeSpan delta);
    void Draw(TimeSpan delta, Matrix camera);
}
