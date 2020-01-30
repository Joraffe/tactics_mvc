using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class AuraInfoUIController : MonoBehaviour
    {
        public AuraInfoUI auraInfoUI;

        /*-------------------------------------------------
        *               Event Handlers
        --------------------------------------------------*/
        public void OnSetAuraInfoTeamName(UIEventData uiEventData)
        {
            if (uiEventData.team.teamType == this.auraInfoUI.GetTeamType())
            {
                this.SetAuraInfoTeamName(uiEventData.team.teamName);
            }
        }


        /*-------------------------------------------------
        *                 Helpers
        --------------------------------------------------*/
        private void SetAuraInfoTeamName(string teamName)
        {
            this.auraInfoUI.SetTeamName(teamName);
        }

        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
    }

}


