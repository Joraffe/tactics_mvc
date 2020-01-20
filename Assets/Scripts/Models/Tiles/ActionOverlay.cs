using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tactics.Models
{
    public class ActionOverlay : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Sprite movementSprite;
        public Sprite combatSprite;
        public Sprite arrangeSprite;

        private Dictionary<string, Sprite> actionOverlayMap;

        public void Awake()
        {
            this.actionOverlayMap = new Dictionary<string, Sprite>{
                { "movement", movementSprite },
                { "combat", combatSprite },
                { "arrange", arrangeSprite },
            };
        }

        public void SetSprite(string spriteKey)
        {
            this.spriteRenderer.sprite = this.actionOverlayMap[spriteKey];
        }
        public void ClearSprite()
        {
            this.spriteRenderer.sprite = null;
        }
    }

}

