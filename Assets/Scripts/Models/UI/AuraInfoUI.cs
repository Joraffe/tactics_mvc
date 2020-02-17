using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class AuraInfoUI : MonoBehaviour
    {
        public string teamName;
        public string teamType;

        public GameObject auraNumbersGameObject;

        public void SetTeamName(string teamName)
        {
            this.teamName = teamName;
        }

        public string GetTeamType()
        {
            return this.teamType;
        }

        public string GetTeamName()
        {
            return this.teamName;
        }

        public AuraNumbersUI GetAuraNumbersUI()
        {
            return this.auraNumbersGameObject.GetComponent<AuraNumbersUI>();
        }
    }
}


