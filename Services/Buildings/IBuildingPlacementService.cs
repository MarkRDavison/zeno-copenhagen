namespace zeno_copenhagen.Services.Buildings;

public interface IBuildingPlacementService
{
    bool CanPlacePrototype(Guid prototypeId, Vector2 position, bool clearJobReservations);
    bool PlacePrototype(Guid prototypeId, Vector2 position, bool clearJobReservations);
}
