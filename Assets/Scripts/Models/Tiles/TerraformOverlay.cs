using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class TerraformOverlay : BaseOverlay
    {
        public Sprite swampTerraform;

        protected override Dictionary<string, Sprite> GetOverlaySpriteMap()
        {
            return new Dictionary<string, Sprite>{
                { TerraTypes.Swamp, swampTerraform }
            };
        }

        public override string GetOverlayType()
        {
            return TileOverlayTypes.Terraform;
        }
    }
}
