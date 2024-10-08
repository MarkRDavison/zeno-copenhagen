﻿namespace zeno_copenhagen.Infrastructure;

public sealed class GameCommand<TCommand>
    where TCommand : class, IGameCommand
{
    public GameCommand(TCommand command)
    {
        Command = command;
    }

    public TCommand Command { get; }
}
