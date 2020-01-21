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
            if (terra.type != "" || terra.type != null)
            {
                this.terraUI.SetTerra(terra);
            }
        }

        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
    }
}
