using System.Collections;
using System.Collections.Generic;
using Tactics.Constants;
using UnityEngine;


namespace Tactics.Models
{
    public class SelectOverlay : BaseOverlay
    {
        protected override BaseSprites GetSpriteConstants()
        {
            return SpritesConstants.Instance.selectOverlaySprites;
        }

        public override string GetOverlayType()
        {
            return TileOverlayTypes.Select;
        }
    }
}

