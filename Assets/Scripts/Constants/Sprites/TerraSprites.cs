using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Constants
{
    public class TerraSprites : BaseSprites
    {
        public Sprite NeutralTerra;
        public Sprite SwampTerra;
        public Sprite DesertTerra;
        public Sprite ForestTerra;
        public Sprite VolcanicTerra;
        public Sprite OceanicTerra;
        public Sprite IndustrialTerra;

        protected override Dictionary<string, Sprite> GetSpriteMap()
        {
            return new Dictionary<string, Sprite>{
                { TerraTypes.Neutral, this.NeutralTerra },
                { TerraTypes.Swamp, this.SwampTerra },
                { TerraTypes.Desert, this.DesertTerra  },
                { TerraTypes.Forest, this.ForestTerra },
                { TerraTypes.Volcanic, this.VolcanicTerra },
                { TerraTypes.Oceanic, this.OceanicTerra },
                { TerraTypes.Industrial, this.IndustrialTerra }
            };
        }

        protected override string GetSpriteType()
        {
            return "Terra";
        }
    }

}
