using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using TMPro;
using UnityEngine;


namespace Tactics.Models
{
    public class TerraformUI : MonoBehaviour
    {
        // 1.0 Terraform UI
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

        public void AddAfterTerraNumber(Dictionary<string, int> postTerraformTerraCountMap)
        {
            foreach (KeyValuePair<string, int> terraCount in postTerraformTerraCountMap)
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
