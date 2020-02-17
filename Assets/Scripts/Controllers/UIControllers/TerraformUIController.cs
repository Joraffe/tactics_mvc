using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class TerraformUIController : MonoBehaviour
    {
        public UIEvent updateTerraResults;
        public UIEvent updateAuraResults;
        public TerraformUI terraformUI;

        /*-------------------------------------------------
        *               Event Handlers
        --------------------------------------------------*/
        public void OnUpdateTerraform(UIEventData uiEventData)
        {
            this.UpdateTerraform(uiEventData);
        }


        /*-------------------------------------------------
        *                 Helpers
        --------------------------------------------------*/
    private void UpdateTerraform(UIEventData uiEventData)
        {
            this.RaiseUpdateTerraResultsUI(
                uiEventData.terraCountMap,
                uiEventData.postTerraformTerraCountMap
            );
            this.RaiseUpdateAuraResultsUI(
                uiEventData.teamAuraScoreMap,
                uiEventData.auraCountMap,
                uiEventData.postTerraformAuraCountMap
            );
        }

        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
        private void RaiseUpdateTerraResultsUI(Dictionary<string, int> terraCountMap, Dictionary<string, int> postTerraformTerraCountMap)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.terraCountMap = terraCountMap;
            uiEventData.postTerraformTerraCountMap = postTerraformTerraCountMap;

            this.updateTerraResults.Raise(uiEventData);
        }

        private void RaiseUpdateAuraResultsUI(Dictionary<string, int> teamAuraScoreMap, Dictionary<Tile, Dictionary<string, int>> auraCountMap, Dictionary<Tile, Dictionary<string, int>> postTerraformAuraCountMap)
        {
             UIEventData uIEventData = new UIEventData();
             uIEventData.teamAuraScoreMap = teamAuraScoreMap;
             uIEventData.auraCountMap = auraCountMap;
             uIEventData.postTerraformAuraCountMap = postTerraformAuraCountMap;

             this.updateAuraResults.Raise(uIEventData);
        }
    }
}
