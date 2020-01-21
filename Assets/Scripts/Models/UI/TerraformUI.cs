using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using TMPro;
using UnityEngine;


namespace Tactics.Models
{
    public class TerraformUI : MonoBehaviour
    {
        public GameObject beforeNumbersUIGameObject;
        public GameObject afterNumbersUIGameObject;
        public GameObject terraNumberPrefab;

        public void AddBeforeTerraNumber(Dictionary<string, int> terraCountMap)
        {
            foreach (KeyValuePair<string, int> terraCount in terraCountMap)
            {
                if (terraCount.Key != "total")
                {
                    GameObject terraNumber = Instantiate(terraNumberPrefab, Vector3.zero, Quaternion.identity);
                    TextMeshProUGUI terraNumberTMP = terraNumber.GetComponent<TextMeshProUGUI>();
                    terraNumberTMP.text = $"{terraCount.Key} {terraCount.Value}";
                    terraNumber.transform.SetParent(this.beforeNumbersUIGameObject.transform);
                }
            }
        }

        public void DestroyBeforeTerraNumbers()
        {
            foreach (Transform transform in this.beforeNumbersUIGameObject.transform)
            {
                Destroy(transform.gameObject);
            }
        }

        public void AddAfterTerraNumber(Dictionary<string, int> terraCountMap, List<Tile> terraformTiles)
        {
            Dictionary<string, int> terraCountCopy = new Dictionary<string, int>(terraCountMap);
            
            foreach (Tile terraformTile in terraformTiles)
            {
                string currentTerraType = terraformTile.terra.type;
                string nextTerraType = terraformTile.previewTerraformType;

                terraCountCopy[currentTerraType] -= 1;
                terraCountCopy[nextTerraType] += 1;
            }

            foreach (KeyValuePair<string, int> terraCount in terraCountCopy)
            {
                if (terraCount.Key != "total")
                {
                    GameObject terraNumber = Instantiate(terraNumberPrefab, Vector3.zero, Quaternion.identity);
                    TextMeshProUGUI terraNumberTMP = terraNumber.GetComponent<TextMeshProUGUI>();
                    terraNumberTMP.text = $"{terraCount.Key} {terraCount.Value}";
                    terraNumber.transform.SetParent(this.afterNumbersUIGameObject.transform);
                }
            }
        }

        public void DestroyAfterTerraNumbers()
        {
            foreach (Transform transform in this.afterNumbersUIGameObject.transform)
            {
                Destroy(transform.gameObject);
            }
        }
    }

}
