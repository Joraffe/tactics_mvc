using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class TerraResultsUIController : MonoBehaviour
    {
        public TerraResultsUI terraResultsUI;


        /*-------------------------------------------------
        *               Event Handlers
        --------------------------------------------------*/
        public void OnUpdateTerraResultsUI(UIEventData uiEventData)
        {
            this.UpdateTerraResultsUI(
                uiEventData.terraCountMap,
                uiEventData.postTerraformTerraCountMap
            );
        }

        /*-------------------------------------------------
        *                 Helpers
        --------------------------------------------------*/
        private void UpdateTerraResultsUI(Dictionary<string, int> terraCountMap, Dictionary<string, int> postTerraformTerraCountMap)
        {
            this.terraResultsUI.DestroyTerraInfo();
            this.terraResultsUI.AddTerraInfo(terraCountMap, postTerraformTerraCountMap);
        }

        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/

    }
}

