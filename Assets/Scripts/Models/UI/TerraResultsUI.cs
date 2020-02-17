using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace Tactics.Models
{
    public class TerraResultsUI : MonoBehaviour
    {
        public GameObject terraInfoSmallPrefab;
        public GameObject terraInfoLargePrefab;
        public GridLayoutGroup terraInfoGridLayoutGroup;

        public const float TerraInfoLargeCellSizeX = 247.5f;
        public const float TerraInfoLargeCellSizeY = 260f;
        public const float TerraInfoSmallCellSizeX = 165f;
        public const float TerraInfoSmallCellSizeY = 130f;

        public void DestroyTerraInfo()
        {
            foreach (Transform transform in this.gameObject.transform)
            {
                Destroy(transform.gameObject);
            }
        }

        public void AddTerraInfo(Dictionary<string, int> terraCountMap, Dictionary<string, int> postTerraformTerraCountMap)
        {
            bool useSmallPrefab = postTerraformTerraCountMap.Count > 2;
            GameObject terraInfoPrefab = useSmallPrefab ? this.terraInfoSmallPrefab : this.terraInfoLargePrefab;
            float cellSizeX = useSmallPrefab ? TerraResultsUI.TerraInfoSmallCellSizeX : TerraResultsUI.TerraInfoLargeCellSizeX;
            float cellSizeY = useSmallPrefab ? TerraResultsUI.TerraInfoSmallCellSizeY : TerraResultsUI.TerraInfoLargeCellSizeY;
            Dictionary<string, int> terraDeltaMap = this.GetTerraDeltaMap(terraCountMap, postTerraformTerraCountMap);

            Vector2 newCellSize = new Vector2(cellSizeX, cellSizeY);
            this.terraInfoGridLayoutGroup.cellSize = newCellSize;

            foreach (KeyValuePair<string, int> terraCount in postTerraformTerraCountMap)
            {
                GameObject terraInfoGameObject = Instantiate(terraInfoPrefab, Vector3.zero, Quaternion.identity);
                TerraInfoUI terraInfoUI = terraInfoGameObject.GetComponent<TerraInfoUI>();
                terraInfoUI.SetTerraTotal(terraCount.Value);
                terraInfoUI.SetTerraDela(terraDeltaMap[terraCount.Key]);
                terraInfoUI.SetTerraIcon(terraCount.Key);
                terraInfoGameObject.transform.SetParent(this.gameObject.transform, false);
            }
        }

        private Dictionary<string, int> GetTerraDeltaMap(Dictionary<string, int> terraCountMap, Dictionary<string, int> postTerraformTerraCountMap)
        {
            Dictionary<string, int> terraDeltaMap = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> terraCount in terraCountMap)
            {
                if (terraCount.Key != "total")
                {
                    string terraType = terraCount.Key;
                    int currentTerraCount = terraCount.Value;
                    int postTerraCount = postTerraformTerraCountMap[terraType];
                    int terraCountDelta = postTerraCount - currentTerraCount;
                    terraDeltaMap.Add(terraType, terraCountDelta);
                }
            }

            return terraDeltaMap;
        }
    }
}
