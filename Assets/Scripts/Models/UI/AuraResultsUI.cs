using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class AuraResultsUI : MonoBehaviour
    {
        public GameObject playerAuraInfoGameObject;
        public GameObject enemyAuraInfoGameObject;


        public void UpdateAuraInfo(Dictionary<string, int> teamAuraScoreMap, Dictionary<Tile, Dictionary<string, int>> auraCountMap, Dictionary<Tile, Dictionary<string, int>> postTerraformAuraCountMap)
        {
            Dictionary<string, int> auraDeltaMap = GetAuraDeltaMap(
                teamAuraScoreMap,
                auraCountMap,
                postTerraformAuraCountMap
            );

            string playerTeamName = this.GetPlayerAuraInfoTeamName();
            string enemyTeamName = this.GetEnemeyAuraInfoTeamName();
            int currentPlayerAuraScore = teamAuraScoreMap[playerTeamName];
            int currentEnemyAuraScore = teamAuraScoreMap[enemyTeamName];

            foreach (KeyValuePair<string, int> teamAuraDelta in auraDeltaMap)
            {
                string teamName = teamAuraDelta.Key;
                int auraDelta = teamAuraDelta.Value;

                if (teamName == playerTeamName)
                {
                    AuraInfoUI playerAuraInfo = this.GetPlayerAuraInfoUI();
                    AuraNumbersUI playerAuraNumbers = playerAuraInfo.GetAuraNumbersUI();
                    playerAuraNumbers.SetAuraTotal(currentPlayerAuraScore + auraDelta);
                    playerAuraNumbers.SetAuraDelta(auraDelta);
                }
                else
                {
                    AuraInfoUI enemtAuraInfo = this.GetEnemyAuraInfoUI();
                    AuraNumbersUI enemyAuraNumbers = enemtAuraInfo.GetAuraNumbersUI();
                    enemyAuraNumbers.SetAuraTotal(currentEnemyAuraScore + auraDelta);
                    enemyAuraNumbers.SetAuraDelta(auraDelta);
                }
            }
        }

        private Dictionary<string, int> GetAuraDeltaMap(Dictionary<string, int> teamAuraScoreMap, Dictionary<Tile, Dictionary<string, int>> auraCountMap, Dictionary<Tile, Dictionary<string, int>> postTerraformAuraCountMap)
        {
            Dictionary<string, int> auraDeltaMap = new Dictionary<string, int>();

            foreach (KeyValuePair<string, int> teamScore in teamAuraScoreMap)
            {
                auraDeltaMap.Add(teamScore.Key, 0);
            }

            foreach (KeyValuePair<Tile, Dictionary<string, int>> auraCount in auraCountMap)
            {
                Tile tile = auraCount.Key;
                Dictionary<string, int> currentAuraValues = auraCount.Value;
                Dictionary<string, int> postAuraValues = postTerraformAuraCountMap[tile];

                foreach (KeyValuePair<string, int> teamAuraCount in currentAuraValues)
                {
                    string teamName = teamAuraCount.Key;
                    int currentTeamAuraValueForTile = teamAuraCount.Value;
                    int postTeamAuraValueForTile = postAuraValues[teamName];
                    int teamAuraDeltaForTile = postTeamAuraValueForTile - currentTeamAuraValueForTile;

                    auraDeltaMap[teamName] += teamAuraDeltaForTile;
                }
            }

            return auraDeltaMap;
        }

        private AuraInfoUI GetPlayerAuraInfoUI()
        {
            return this.playerAuraInfoGameObject.GetComponent<AuraInfoUI>();
        }

        private AuraInfoUI GetEnemyAuraInfoUI()
        {
            return this.enemyAuraInfoGameObject.GetComponent<AuraInfoUI>();
        }

        private string GetPlayerAuraInfoTeamName()
        {
            return this.GetPlayerAuraInfoUI().GetTeamName();
        }

        private string GetEnemeyAuraInfoTeamName()
        {
            return this.GetEnemyAuraInfoUI().GetTeamName();
        }
    }

}
