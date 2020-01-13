using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{

    public class ButtonTypes {
        public const string Settings = "settings";
        public const string Danger = "danger";
        public const string Arrange = "arrange";
        public const string EndTurn = "endTurn";
        public const string Fight = "fight";
        public const string Confirmation = "confirmation";
        public const string Cancel = "cancel";
    }


    public class Button : MonoBehaviour
    {
        public string type;
        public bool active;
        public bool visible;
        public bool pressable;
        public Sprite defaultSprite;
        public Sprite activeSprite;
        public Material spritesDefault;
        public Material spritesGreyscale;

        public SpriteRenderer spriteRenderer;

        public void SetActiveSprite()
        {
            this.spriteRenderer.sprite = activeSprite;
        }

        public void SetDefaultSprite()
        {
            this.spriteRenderer.sprite = defaultSprite;
        }

        public void SetActive()
        {
            this.active = true;
        }

        public void SetInactive()
        {
            this.active = false;
        }

        public void SetVisible()
        {
            this.visible = true;
            this.spriteRenderer.enabled = true;
        }

        public void SetInvisible()
        {
            this.visible = false;
            this.spriteRenderer.enabled = false;
        }

        public void SetPressable()
        {
            this.pressable = true;
            this.spriteRenderer.material = this.spritesDefault;
            this.spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }

        public void SetUnpressable()
        {
            this.pressable = false;
            this.spriteRenderer.material = this.spritesGreyscale;
            this.spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }

    }
}
