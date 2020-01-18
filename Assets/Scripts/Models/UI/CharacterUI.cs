using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Tactics.Models
{
    public class CharacterUI : MonoBehaviour
    {
        public GameObject portraitGameObject;
        public GameObject detailsGameObject;
        public Character character;

        public void SetPortraitSprite(Sprite sprite)
        {
            Image portraitImage = portraitGameObject.GetComponent<Image>();
            portraitImage.sprite = sprite;
        }

        public void SetDetailsName(string name)
        {
            TextMeshProUGUI detailsName = detailsGameObject.GetComponent<TextMeshProUGUI>();
            detailsName.text = name;
        }

        public void SetCharacter(Character character)
        {
            this.character = character;
        }
    }
}

