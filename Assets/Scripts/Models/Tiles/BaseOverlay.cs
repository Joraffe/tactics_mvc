
using System; 
using System.Collections;
using System.Collections.Generic;
using Tactics.Constants;
using UnityEngine;


namespace Tactics.Models
{
    public class BaseOverlay : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public string currentSpriteKey;

        public void SetSprite(string spriteKey)
        {
            if (!HasSprite(spriteKey))
            {
                throw new Exception($"Missing spriteKey: {spriteKey} in {GetOverlayType()} overlay");
            }
            else
            {
                this.currentSpriteKey = spriteKey;
                this.spriteRenderer.sprite = GetSprite(spriteKey);
            }
        }

        public void ClearSprite()
        {
            this.currentSpriteKey = null;
            this.spriteRenderer.sprite = null;
        }

        public Sprite GetSprite(string spriteKey)
        {
            return GetSpriteConstants().GetSprite(spriteKey);
        }

        public bool HasSprite(string spriteKey)
        {
            return GetSpriteConstants().HasSprite(spriteKey);
        }

        protected virtual BaseSprites GetSpriteConstants()
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

        public string GetcurrentSpriteName()
        {
            return this.currentSpriteKey;
        }
    }

}

