using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Constants
{

    public class SelectOverlayTypes
    {
        public const string Movement = "movement";
        public const string Combat = "combat";
        public const string Terraform = "terraform";
    }
    public class SelectOverlaySprites : BaseSprites
    {
        public Sprite SelectedMovement;
        public Sprite SelectedCombat;
        public Sprite SelectedTerraform;

        protected override Dictionary<string, Sprite> GetSpriteMap()
        {
            return new Dictionary<string, Sprite>{
                { SelectOverlayTypes.Movement, SelectedMovement },
                { SelectOverlayTypes.Combat, SelectedCombat },
                { SelectOverlayTypes.Terraform, SelectedTerraform }
            };
        }

        protected override string GetSpriteType()
        {
            return "Select Overlay";
        }
    }
}