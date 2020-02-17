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

        private Dictionary<string, int> teamScoreMap = new Dictionary<string, int>();

        /*-------------------------------------------------
        *                     Getters
        --------------------------------------------------*/
        public Team GetActiveTeam()
        {
            return this.activeTeam;
        }

        public Dictionary<string, int> GetTeamScoreMap()
        {
            return this.teamScoreMap;
        }

        /*-------------------------------------------------
        *                     Setters
        --------------------------------------------------*/
        public void SetActiveTeam(Team team)
        {
            this.activeTeam = team;
        }

        public void SetMap(Map map)
        {
            this.map = map;
        }

        public void InitTeamScore(string teamName)
        {
            this.teamScoreMap.Add(teamName, 0);
        }

        public void AddTeamScore(string teamName, int score)
        {
            this.teamScoreMap[teamName] += score;
        }

    }
}

