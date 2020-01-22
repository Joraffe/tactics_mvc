using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Constants
{
    public class TerraformOverlaySprites : BaseSprites
    {
        public Sprite swampTerraform;
        protected override Dictionary<string, Sprite> GetSpriteMap()
        {
            return new Dictionary<string, Sprite>{
                { TerraTypes.Swamp, swampTerraform }
            };
        }

        protected override string GetSpriteType()
        {
            return "Terraform Overlay";
        }
    }
}