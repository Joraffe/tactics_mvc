using System.Collections;
using System.Collections.Generic;
using Tactics.Constants;
using UnityEngine;


namespace Tactics.Models
{
    public class TerraTypes
    {
        public const string Neutral = "Neutral";
        public const string Desert = "Desert";
        public const string Swamp = "Swamp";
        public const string Forest = "Forest";
        public const string Volcanic = "Volcanic";
        public const string Oceanic = "Oceanic";
        public const string Industrial = "Industrial";
    }

    public class Terra : MonoBehaviour
    {
        public string type;

        public Sprite GetCurrentSprite()
        {
            return SpritesConstants.Instance.terraSprites.GetSprite(this.type);
        }

        public void SetTerraType(string terraType)
        {
            this.type = terraType;
        }
    }

}
