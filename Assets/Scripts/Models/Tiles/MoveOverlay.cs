using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tactics.Models
{
        public class MoveOverlay : MonoBehaviour
        {
            public SpriteRenderer spriteRenderer;
            public Sprite moveSprite;

            public Dictionary<string, Sprite> moveOverlapMap;

            public void Awake()
            {
                this.moveOverlapMap = new Dictionary<string, Sprite>{
                    { "move", moveSprite }
                };
            }

            public void SetSprite(string spriteKey)
            {
                this.spriteRenderer.sprite = this.moveOverlapMap[spriteKey];
            }

            public void ClearSprite()
            {
                this.spriteRenderer.sprite = null;
            }
        }
}


