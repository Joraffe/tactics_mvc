using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tactics.Models
{
    public class SelectOverlayTypes
    {
        public const string Movement = "movement";
        public const string Combat = "combat";
    }

    public class SelectOverlay : BaseOverlay
    {
        // Sprite List
        public Sprite selectedMovement;
        public Sprite selectedCombat;

        protected override Dictionary<string, Sprite> GetOverlaySpriteMap()
        {
            return new Dictionary<string, Sprite>{
                { SelectOverlayTypes.Movement, selectedMovement },
                { SelectOverlayTypes.Combat, selectedCombat }
            };
        }

        public override string GetOverlayType()
        {
            return TileOverlayTypes.Select;
        }
    }

}

