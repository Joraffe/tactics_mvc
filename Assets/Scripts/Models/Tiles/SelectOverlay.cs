using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tactics.Models
{
    public class SelectOverlay : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Sprite selectedMovement;
        public Sprite selectedCombat;

        public void SetSelectedSprite(string selectOverlayType)
        {
            if (selectOverlayType == TileInteractType.Movement)
            {
                this.spriteRenderer.sprite = selectedMovement;
            }
            else if (selectOverlayType == TileInteractType.Combat)
            {
                this.spriteRenderer.sprite = selectedCombat;
            }

        }

        public void ClearSprite()
        {
            this.spriteRenderer.sprite = null;
        }

    }

}

