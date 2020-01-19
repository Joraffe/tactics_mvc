using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tactics.Models
{
    public class TerraUI : MonoBehaviour
    {
        public GameObject imageGameObject;
        public GameObject nameGameObject;
        public Terra terra;

        public void SetImageSprite(Sprite sprite)
        {
            Image image = imageGameObject.GetComponent<Image>();
            image.sprite = sprite;
        }

        public void SetName(string name)
        {
            TextMeshProUGUI tmpName = nameGameObject.GetComponent<TextMeshProUGUI>();
            tmpName.text = name;
        }

        public void SetTerra(Terra terra)
        {
            this.terra = terra;
        }
    }
}


