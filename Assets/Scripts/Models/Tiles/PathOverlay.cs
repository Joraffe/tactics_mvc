using System.Collections;
using System.Collections.Generic;
using Tactics.Constants;
using UnityEngine;


namespace Tactics.Models {

    public class PathOverlay : BaseOverlay
    {
        protected override BaseSprites GetSpriteConstants()
        {
            return SpritesConstants.Instance.pathOverlaySprites;
        }

        public override string GetOverlayType()
        {
            return TileOverlayTypes.Path;
        }
    }
}

