using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class TeamTurnUIController : MonoBehaviour
    {
        public TeamTurnUI teamTurnUI;

        /*-------------------------------------------------
        *               Event Handlers
        --------------------------------------------------*/
        public void OnUpdateTeamTurnUI(UIEventData uiEventData)
        {
            this.UpdateTeamTurn(uiEventData.team);
        }

        /*-------------------------------------------------
        *                 Helpers
        --------------------------------------------------*/
        private void UpdateTeamTurn(Team team)
        {
            this.teamTurnUI.SetTeamTurnText(team);
        }


        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
    }
}
