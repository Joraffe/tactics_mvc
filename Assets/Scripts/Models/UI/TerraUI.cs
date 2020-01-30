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
        public string currentTerraType;

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

        public void SetTerraType(string terraType)
        {
            this.currentTerraType = terraType;
        }

        public void SetTerra(Terra terra)
        {
            SetTerraType(terra.type);
            SetImageSprite(terra.GetCurrentSprite());
            SetName(terra.type);
        }
    }
}
