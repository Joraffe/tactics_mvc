using System.Collections;
using System.Collections.Generic;
using Tactics.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Tactics.Models
{
    public class TerraInfoUI : MonoBehaviour
    {
        public GameObject terraTotalGameObject;
        public GameObject terraDeltaGameObject;
        public GameObject terraIconGameObject;

        public void SetTerraTotal(int total)
        {
            TextMeshProUGUI terraTotalTMP = terraTotalGameObject.GetComponent<TextMeshProUGUI>();
            terraTotalTMP.text = $"{total}";
        }

        public void SetTerraDela(int delta)
        {
            TextMeshProUGUI terraDeltaTMP = terraDeltaGameObject.GetComponent<TextMeshProUGUI>();
            string deltaSymbol = delta > 0 ? "+" : "";
            terraDeltaTMP.text = $"({deltaSymbol}{delta})";
        }

        public void SetTerraIcon(string terraType)
        {
            Image image = this.terraIconGameObject.GetComponent<Image>();
            image.sprite = SpritesConstants.Instance.terraSprites.GetSprite(terraType);
        }
    }
}
