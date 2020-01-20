using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class TerraformOverlay : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Sprite swampTerraform;
        public string currentTerraType;

        private Dictionary<string, Sprite> terraformOverlayMap;

        public void Awake()
        {
            this.terraformOverlayMap = new Dictionary<string, Sprite>{
                { TerraTypes.Swamp, swampTerraform }
            };
        }

        public void SetSprite(string spriteKey)
        {
            this.currentTerraType = spriteKey;
            this.spriteRenderer.sprite = this.terraformOverlayMap[spriteKey];
        }

        public void ClearSprite()
        {
            this.currentTerraType = "";
            this.spriteRenderer.sprite = null;
        }

        public Sprite GetCurrentSprite()
        {
            return this.spriteRenderer.sprite;
        }
    }
}

