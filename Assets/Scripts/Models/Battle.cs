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
        public Map map;

        public Team playerTeam;

        public Team enemyTeam;

        public string activeTeam;

        public string phase;

        public void Start()
        {
            this.SetPrepPhase();
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

        /*-------------------------------------------------
        *                     Getters
        --------------------------------------------------*/
        public Team GetEnemyTeam()
        {
            return this.enemyTeam;
        }
    }
}
