using UnityEngine;


namespace Tactics.Events
{
    [CreateAssetMenu(fileName = "New Battle Event", menuName = "Game Events/Battle Event")]
    public class BattleEvent : BaseGameEvent<BattleEventData> {}
}
