namespace zeno_copenhagen.Services.Buildings;

public interface IBuildingPlacementService
{
    bool CanPlaceBuilding(Guid prototypeId, Vector2 position);
}
