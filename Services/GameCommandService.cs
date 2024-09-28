namespace zeno_copenhagen.Services;

public sealed class GameCommandService : IGameCommandService
{
    private readonly IServiceProvider _services;

    public GameCommandService(IServiceProvider services)
    {
        _services = services;
    }

    public bool Execute<TCommand>(GameCommand<TCommand> command)
        where TCommand : class, IGameCommand
    {
        var handler = _services.GetRequiredService<IGameCommandHandler<TCommand>>();

        return handler.Handle(command.Command);
    }

    public bool CanExecute<TCommand>(GameCommand<TCommand> command)
        where TCommand : class, IGameCommand
    {
        var handler = _services.GetRequiredService<IGameCommandHandler<TCommand>>();

        return handler.CanHandle(command.Command);
    }
}
