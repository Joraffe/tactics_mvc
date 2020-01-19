using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models {

    public class PathOverlay : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        public Sprite arrowLeft;
        public Sprite arrowRight;
        public Sprite arrowUp;
        public Sprite arrowDown;

        public Sprite startLeft;
        public Sprite startRight;
        public Sprite startUp;
        public Sprite startDown;

        public Sprite midDownLeft;
        public Sprite midDownRight;
        public Sprite midUpLeft;
        public Sprite midUpRight;
        public Sprite midHorizontal;
        public Sprite midVertical;

        private Dictionary<string, Sprite> pathOverlayMap;

        public void Start()
        {
            pathOverlayMap = new Dictionary<string, Sprite>{
                { "arrow_left", arrowLeft },
                { "arrow_right", arrowRight },
                { "arrow_up", arrowUp },
                { "arrow_down", arrowDown },

                { "start_left", startLeft },
                { "start_right", startRight },
                { "start_up", startUp },
                { "start_down", startDown },

                { "mid_down_left", midDownLeft },
                { "mid_down_right", midDownRight },
                { "mid_up_left", midUpLeft },
                { "mid_up_right", midUpRight },
                { "mid_horizontal", midHorizontal },
                { "mid_vertical", midVertical }
            };
        }

        public void SetSprite(string spriteKey)
        {
            this.spriteRenderer.sprite = this.pathOverlayMap[spriteKey];
        }

        public void ClearSprite()
        {
            this.spriteRenderer.sprite = null;
        }
    }
}

