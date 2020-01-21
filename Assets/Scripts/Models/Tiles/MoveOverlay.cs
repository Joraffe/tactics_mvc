using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tactics.Models
{
        public class MoveOverlay : BaseOverlay
        {
            public Sprite moveSprite;

            protected override Dictionary<string, Sprite> GetOverlaySpriteMap()
            {
                return new Dictionary<string, Sprite>{
                    { "move", moveSprite }
                };
            }

            public override string GetOverlayType()
            {
                return TileOverlayTypes.Move;
            }
        }
}
