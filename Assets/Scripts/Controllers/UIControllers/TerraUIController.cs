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
            UpdateTerra(uiEventData.terra);
        }


        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void UpdateTerra(Terra terra)
        {
            this.terraUI.SetTerra(terra);
            this.terraUI.SetImageSprite(terra.sprite);
            this.terraUI.SetName(terra.type);
        }

        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/
    }
}
