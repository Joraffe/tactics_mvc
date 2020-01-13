using UnityEngine;


namespace Tactics.Events
{
    [CreateAssetMenu(fileName = "New Map Event", menuName = "Game Events/Map Event")]
    public class MapEvent : BaseGameEvent<MapEventData> {}
}
