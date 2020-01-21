
using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class BaseOverlay : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public string currentSpriteKey;
        protected Dictionary<string, Sprite> overlaySpriteMap;
        public void Awake()
        {
            this.overlaySpriteMap = GetOverlaySpriteMap();
        }

        public void SetSprite(string spriteKey)
        {
            if (!this.overlaySpriteMap.ContainsKey(spriteKey))
            {
                throw new Exception($"Missing spriteKey: {spriteKey} in {GetOverlayType()} overlay");
            }
            else
            {
                this.currentSpriteKey = spriteKey;
                this.spriteRenderer.sprite = this.overlaySpriteMap[spriteKey];
            }
        }

        public void ClearSprite()
        {
            this.currentSpriteKey = null;
            this.spriteRenderer.sprite = null;
        }

        protected virtual Dictionary<string, Sprite> GetOverlaySpriteMap()
        {
            throw new NotImplementedException();
        }

        public virtual string GetOverlayType()
        {
            throw new NotImplementedException();
        }

        public Sprite GetCurrentSprite()
        {
            return this.spriteRenderer.sprite;
        }
    }

}

