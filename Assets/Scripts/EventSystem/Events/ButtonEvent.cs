using UnityEngine;


namespace Tactics.Events
{
    [CreateAssetMenu(fileName = "New Button Event", menuName = "Game Events/Button Event")]
    public class ButtonEvent : BaseGameEvent<ButtonEventData> {}
}
