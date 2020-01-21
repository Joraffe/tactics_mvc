using System.Collections;
using System.Collections.Generic;
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
        public Sprite NeutralTerra;
        public Sprite SwampTerra;
        public Sprite DesertTerra;
        public Sprite ForestTerra;
        public Sprite VolcanicTerra;
        public Sprite OceanicTerra;
        public Sprite IndustrialTerra;
        public Dictionary<string, Sprite> terraSpriteMap;
        public string type;

        public void Awake()
        {
            this.terraSpriteMap = new Dictionary<string, Sprite>{
                { TerraTypes.Neutral, NeutralTerra },
                { TerraTypes.Swamp, SwampTerra },
                { TerraTypes.Desert, DesertTerra  },
                { TerraTypes.Forest, ForestTerra },
                { TerraTypes.Volcanic, VolcanicTerra },
                { TerraTypes.Oceanic, OceanicTerra },
                { TerraTypes.Industrial, IndustrialTerra }
            };
        }

        public Sprite GetCurrentSprite()
        {
            return this.terraSpriteMap[this.type];
        }

        public void SetTerraType(string terraType)
        {
            this.type = terraType;
        }
    }

}
