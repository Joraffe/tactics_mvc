using Tactics.Models;
using UnityEngine;


namespace Tactics.Events
{

    [CreateAssetMenu(fileName = "New Tile Event", menuName = "Game Events/Tile Event")]
    public class TileEvent : BaseGameEvent<TileEventData> { }

}
