using UnityEngine;


namespace Tactics.Events
{
    [CreateAssetMenu(fileName = "New Camera Event", menuName = "Game Events/Camera Event")]
    public class CameraEvent : BaseGameEvent<CameraEventData> {}
}
