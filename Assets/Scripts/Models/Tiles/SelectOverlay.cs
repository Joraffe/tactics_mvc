using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tactics.Models
{
    public class SelectOverlayTypes
    {
        public const string Movement = "movement";
        public const string Combat = "combat";
        public const string Terraform = "terraform";
    }

    public class SelectOverlay : BaseOverlay
    {
        // Sprite List
        public Sprite selectedMovement;
        public Sprite selectedCombat;
        public Sprite selectedTerraform;

        protected override Dictionary<string, Sprite> GetOverlaySpriteMap()
        {
            return new Dictionary<string, Sprite>{
                { SelectOverlayTypes.Movement, selectedMovement },
                { SelectOverlayTypes.Combat, selectedCombat },
                { SelectOverlayTypes.Terraform, selectedTerraform }
            };
        }

        public override string GetOverlayType()
        {
            return TileOverlayTypes.Select;
        }
    }

}

