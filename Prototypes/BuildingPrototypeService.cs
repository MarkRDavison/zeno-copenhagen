namespace zeno_copenhagen.Prototypes;

public sealed class BuildingPrototypeService : PrototypeService<BuildingPrototype, Building>
{
    public override Building CreateEntity(BuildingPrototype prototype)
    {
        var building = new Building
        {
            Id = Guid.NewGuid(),
            PrototypeId = prototype.Id
        };

        return building;
    }
}
