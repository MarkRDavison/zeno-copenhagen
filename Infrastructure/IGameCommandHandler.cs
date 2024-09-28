namespace zeno_copenhagen.Infrastructure;

public interface IGameCommandHandler<TCommand>
    where TCommand : class, IGameCommand
{
    bool Handle(TCommand command);
    bool CanHandle(TCommand command) => true;
}
