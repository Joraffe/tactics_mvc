using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Models
{
    public class Teams : MonoBehaviour
    {
        public Dictionary<string, Team> teamMap;

        public Queue<Team> teamTurnQueue = new Queue<Team>();

        public void Awake()
        {
            this.teamMap = new Dictionary<string, Team>();

            foreach (Transform transform in this.transform)
            {
                GameObject teamGameObject = transform.gameObject;
                Team team = teamGameObject.GetComponent<Team>();
                teamMap.Add(team.teamName, team);
            }
        }

        public void EnqueueNextTeamTurn(Team team)
        {
            this.teamTurnQueue.Enqueue(team);
        }

        public Team DequeueNextTeamTurn()
        {
            return this.teamTurnQueue.Dequeue();
        }
    }
}

