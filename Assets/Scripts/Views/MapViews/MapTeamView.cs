using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Views
{
    public class MapTeamView : MonoBehaviour
    {
        /*-------------------------------------------------
        *                     Models
        --------------------------------------------------*/
        public Map map;
        public List<Team> teams;

        /*-------------------------------------------------
        *               Relationship Variables
        --------------------------------------------------*/
        private Team activeTeam;  // whose turn is it to act on the map

        /*-------------------------------------------------
        *                     Getters
        --------------------------------------------------*/
        public Team GetActiveTeam()
        {
            return this.activeTeam;
        }

        /*-------------------------------------------------
        *                     Setters
        --------------------------------------------------*/
        public void SetActiveTeam(Team team)
        {
            this.activeTeam = team;
        }
    }
}

