using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Constants
{
    public class BaseSprites : MonoBehaviour
    {
        public Dictionary<string, Sprite> SpriteMap;

        public void Awake()
        {
            this.SpriteMap = GetSpriteMap();
        }

        protected virtual Dictionary<string, Sprite> GetSpriteMap()
        {
            throw new NotImplementedException();
        }

        public Sprite GetSprite(string spriteKey)
        {
            if (!HasSprite(spriteKey))
            {
                throw new Exception($"{GetSpriteType()} sprite singleton does not contain sprite: {spriteKey}");
            }

            return this.SpriteMap[spriteKey];
        }

        public bool HasSprite(string spriteKey)
        {
            return this.SpriteMap.ContainsKey(spriteKey);
        }

        protected virtual string GetSpriteType()
        {
            throw new NotImplementedException();
        }
    }
}

