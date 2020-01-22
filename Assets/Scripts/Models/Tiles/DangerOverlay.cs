using System.Collections;
using System.Collections.Generic;
using Tactics.Constants;
using UnityEngine;

namespace Tactics.Models
{
    public class DangerOverlay : BaseOverlay
    {
        protected override BaseSprites GetSpriteConstants()
        {
            return SpritesConstants.Instance.dangerOverlaySprites;
        }

        public override string GetOverlayType()
        {
            return TileOverlayTypes.Danger;
        }
    }
}
