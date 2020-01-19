using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class TerraTypes
    {
        public const string Desert = "Desert";
        public const string Swamp = "Swamp";
        public const string Forest = "Forest";
        public const string Volcanic = "Volcanic";
        public const string Oceanic = "Oceanic";
        public const string Industrial = "Industrial";
    }

    public class Terra : MonoBehaviour
    {
        public Sprite sprite;
        public string type;
    }

}
