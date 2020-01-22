using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Constants
{
    public class MoveTypes
    {
        public const string Move = "move";
    }

    public class MoveOverlaySprites : BaseSprites
    {
        public Sprite MoveSprite;

        protected override Dictionary<string, Sprite> GetSpriteMap()
        {
            return new Dictionary<string, Sprite>{
                { MoveTypes.Move, MoveSprite }
            };
        }

        protected override string GetSpriteType()
        {
            return "Move Overlay";
        }
    }
}

