using System.Collections;
using System.Collections.Generic;
using Tactics.Constants;
using UnityEngine;

namespace Tactics.Models
{
        public class MoveOverlay : BaseOverlay
        {

            protected override BaseSprites GetSpriteConstants()
            {
                return SpritesConstants.Instance.moveOverlaySprites;
            }

            public override string GetOverlayType()
            {
                return TileOverlayTypes.Move;
            }
        }
}
