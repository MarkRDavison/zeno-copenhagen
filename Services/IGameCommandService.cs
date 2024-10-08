﻿namespace zeno_copenhagen.Services;


public interface IGameCommandService
{
    bool Execute<TCommand>(GameCommand<TCommand> command)
        where TCommand : class, IGameCommand;

    bool CanExecute<TCommand>(GameCommand<TCommand> command)
        where TCommand : class, IGameCommand;
}
