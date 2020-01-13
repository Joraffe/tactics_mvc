using UnityEngine;


namespace Tactics.Models
{

    public class ModalTypes
    {
        public const string Confirmation = "confirmation";
        public const string Menu = "menu";
    }
    public class Modal : MonoBehaviour
    {
        public string type;
    }
}
