using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class PhaseTypes
    {
        public static string Prep = "prep";
        public static string Engage = "engage";
    }

    public class Battle : MonoBehaviour
    {
        public string phase;

        /*-------------------------------------------------
        *                 Heirarchy
        --------------------------------------------------*/
        public GameObject mapGameObject;
        public GameObject teamsGameObject;

        public Teams GetTeams()
        {
            return this.teamsGameObject.GetComponent<Teams>();
        }

        public Team GetTeam(string teamName)
        {
            return this.GetTeams().teamMap[teamName];
        }

        public Map GetMap()
        {
            return this.mapGameObject.GetComponent<Map>();
        }

        /*-------------------------------------------------
        *                     Setters
        --------------------------------------------------*/
        public void SetPrepPhase()
        {
            this.phase = PhaseTypes.Prep;
        }

        public void SetEngagePhase()
        {
            this.phase = PhaseTypes.Engage;
        }
    }
}
