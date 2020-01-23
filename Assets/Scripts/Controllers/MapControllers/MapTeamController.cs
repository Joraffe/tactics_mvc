using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using Tactics.Views;
using UnityEngine;


namespace Tactics.Controllers
{
    public class MapTeamController : MonoBehaviour
    {
        public MapTeamView mapTeamView;

        /*-------------------------------------------------
        *              Event Handlers
        --------------------------------------------------*/
        public void OnSetActiveTeam(MapEventData mapEventData)
        {
            this.SetActiveTeam(mapEventData.team);
        }

        /*-------------------------------------------------
        *                 Helpers
        --------------------------------------------------*/
        private void SetActiveTeam(Team team)
        {
            this.mapTeamView.SetActiveTeam(team);
        }

        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
    }
}


