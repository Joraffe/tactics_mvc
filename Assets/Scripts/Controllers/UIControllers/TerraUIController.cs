using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;

namespace Tactics.Controllers
{
    public class TerraUIController : MonoBehaviour
    {
        public TerraUI terraUI;

        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        public void OnUpdateTerra(UIEventData uiEventData)
        {
            UpdateTerra(uiEventData.terra, uiEventData.terraformOverlay);
        }


        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void UpdateTerra(Terra terra, TerraformOverlay terraformOverlay)
        {
            if (terraformOverlay.currentTerraType != "")
            {
                this.terraUI.SetTerraType(terraformOverlay.currentTerraType);
                this.terraUI.SetImageSprite(terraformOverlay.GetCurrentSprite());
                this.terraUI.SetName(terraformOverlay.currentTerraType);
            }
            else
            {
                this.terraUI.SetTerraType(terra.type);
                this.terraUI.SetImageSprite(terra.sprite);
                this.terraUI.SetName(terra.type);
            }

        }

        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/
    }
}
