using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class FormaSet : MonoBehaviour
    {
        public Character character;
        public Dictionary<KeyCode, Forma> formaKeyBindingMap = new Dictionary<KeyCode, Forma>();
    }
}
