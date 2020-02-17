using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class AuraResultsUIController : MonoBehaviour
    {
        public AuraResultsUI auraResultsUI;

        /*-------------------------------------------------
        *               Event Handlers
        --------------------------------------------------*/
        public void OnUpdateAuraResultsUI(UIEventData uiEventData)
        {
            this.UpdateAuraResultsUI(
                uiEventData.teamAuraScoreMap,
                uiEventData.auraCountMap,
                uiEventData.postTerraformAuraCountMap
            );
        }

        /*-------------------------------------------------
        *                 Helpers
        --------------------------------------------------*/
        private void UpdateAuraResultsUI(Dictionary<string, int> teamAuraScoreMap, Dictionary<Tile, Dictionary<string, int>> auraCountMap, Dictionary<Tile, Dictionary<string, int>> postTerraformAuraCountMap)
        {
            this.auraResultsUI.UpdateAuraInfo(
                teamAuraScoreMap,
                auraCountMap,
                postTerraformAuraCountMap
            );
        }

        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
    }

}
