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

        public Sprite neutralTerra;
        public Sprite swampTerra;
        public Sprite desertTerra;
        public Sprite forestTerra;
        public Sprite volcanicTerra;
        public Sprite oceanicTerra;
        public Sprite industrialTerra;
        public Dictionary<string, Sprite> terraSpriteMap;

        public void Awake()
        {
            this.terraSpriteMap = new Dictionary<string, Sprite>{
                { TerraTypes.Neutral, neutralTerra },
                { TerraTypes.Swamp, swampTerra },
                { TerraTypes.Desert, desertTerra  },
                { TerraTypes.Forest, forestTerra },
                { TerraTypes.Volcanic, volcanicTerra },
                { TerraTypes.Oceanic, oceanicTerra },
                { TerraTypes.Industrial, industrialTerra }
            };
        }

        public void SetImageSprite(string spriteKey)
        {
            Image image = imageGameObject.GetComponent<Image>();
            image.sprite = this.terraSpriteMap[spriteKey];
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

        public void SetTerra(string terraType)
        {
            SetTerraType(terraType);
            SetImageSprite(terraType);
            SetName(terraType);
        }
    }
}


