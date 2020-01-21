using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class TerraformUIController : MonoBehaviour
    {
        public TerraformUI terraformUI;

        /*-------------------------------------------------
        *               Event Handlers
        --------------------------------------------------*/
        public void OnUpdateTerraform(UIEventData uiEventData)
        {
            UpdateTerraform(
                uiEventData.terraCountMap,
                uiEventData.postTerraformTerraCountMap
            );
        }


        /*-------------------------------------------------
        *                 Helpers
        --------------------------------------------------*/
    private void UpdateTerraform(Dictionary<string, int> terraCountMap, Dictionary<string, int> postTerraformTerraCountMap)
        {
            this.terraformUI.DestroyBeforeTerraNumbers();
            this.terraformUI.AddBeforeTerraNumber(terraCountMap);

            this.terraformUI.DestroyAfterTerraNumbers();
            this.terraformUI.AddAfterTerraNumber(postTerraformTerraCountMap);
        }

        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
    }

}
