using System.Collections;
using System.Collections.Generic;
using Tactics.Constants;
using UnityEngine;


namespace Tactics.Models
{
    public class TerraformOverlay : BaseOverlay
    {
        protected override BaseSprites GetSpriteConstants()
        {
            return SpritesConstants.Instance.terraformOverlaySprites;
        }

        public override string GetOverlayType()
        {
            return TileOverlayTypes.Terraform;
        }
    }
}
