using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Tactics.Models
{
    public class AuraNumbersUI : MonoBehaviour
    {
        public GameObject auraTotalGameObject;
        public GameObject auraDeltaGameObject;

        public void SetAuraTotal(int total)
        {
            TextMeshProUGUI auraTotalTMP = auraTotalGameObject.GetComponent<TextMeshProUGUI>();
            auraTotalTMP.text = $"{total}";
        }

        public void SetAuraDelta(int delta)
        {
            TextMeshProUGUI auraDeltaTMP = auraDeltaGameObject.GetComponent<TextMeshProUGUI>();

            if (delta == 0)
            {
                auraDeltaTMP.text = "";
            }
            else
            {
                string deltaSymbol = delta > 0 ? "+" : "";
                auraDeltaTMP.text = $"({deltaSymbol}{delta})";
            }
        }
    }
}
