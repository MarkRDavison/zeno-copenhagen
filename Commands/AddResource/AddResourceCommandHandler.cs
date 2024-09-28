namespace zeno_copenhagen.Commands.AddResource;

public class AddResourceCommandHandler : IGameCommandHandler<AddResourceCommand>
{
    private readonly IGameResourceService _gameResourceService;

    public AddResourceCommandHandler(IGameResourceService gameResourceService)
    {
        _gameResourceService = gameResourceService;
    }

    public bool Handle(AddResourceCommand command)
    {
        _gameResourceService.AddResource(command.Name, command.Amount);

        return true;
    }
}
