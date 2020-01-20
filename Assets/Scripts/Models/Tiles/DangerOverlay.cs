﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tactics.Models
{
    public class DangerOverlay : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        public Sprite dangerBox;
        public Sprite dangerRight;
        public Sprite dangerLeft;
        public Sprite dangerUp;
        public Sprite dangerDown;
        public Sprite dangerNoBorder;
        public Sprite dangerLeftRight;
        public Sprite dangerUpDown;
        public Sprite dangerLeftDown;
        public Sprite dangerLeftUp;
        public Sprite dangerRightUp;
        public Sprite dangerRightDown;
        public Sprite dangerLeftRightDown;
        public Sprite dangerLeftUpDown;
        public Sprite dangerLeftRightUp;
        public Sprite dangerRightUpDown;

        private Dictionary<string, Sprite> dangerOverlayMap;

        public void Awake()
        {
            this.dangerOverlayMap = new Dictionary<string, Sprite>{
                { "0000", dangerBox },
                { "1111", dangerNoBorder },

                { "0111", dangerRight },
                { "1011", dangerLeft },
                { "1101", dangerUp },
                { "1110", dangerDown },

                { "0011", dangerLeftRight },
                { "1100", dangerUpDown },

                { "1010", dangerLeftDown },
                { "1001", dangerLeftUp },
                { "0110", dangerRightDown },
                { "0101", dangerRightUp },

                { "0010", dangerLeftRightDown },
                { "1000", dangerLeftUpDown },
                { "0001", dangerLeftRightUp },
                { "0100", dangerRightUpDown }
            };
        }

        public void SetSprite(string spriteKey)
        {
            this.spriteRenderer.sprite = this.dangerOverlayMap[spriteKey];
        }

        public void ClearSprite()
        {
            this.spriteRenderer.sprite = null;
        }
    }
}
