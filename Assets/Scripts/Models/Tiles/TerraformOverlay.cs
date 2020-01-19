using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class TerraformOverlay : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Sprite swampTerraform;

        private Dictionary<string, Sprite> terraformOverlayMap;

        public void Start()
        {
            this.terraformOverlayMap = new Dictionary<string, Sprite>{
                { TerraTypes.Swamp, swampTerraform }
            };
        }

        public void SetSprite(string spriteKey)
        {
            this.spriteRenderer.sprite = this.terraformOverlayMap[spriteKey];
        }

        public void ClearSprite()
        {
            this.spriteRenderer.sprite = null;
        }
    }
}

