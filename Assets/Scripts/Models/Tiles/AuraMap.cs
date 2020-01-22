using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class AuraMap : MonoBehaviour
    {
        public Dictionary<string, Aura> auras;
        public int currentTotalAura = 0;

        // making capacity a function so we can potentially overload it in the future
        public int GetCapacity()
        {
            return 10;
        }

        public Aura GetTeamAura(string teamName)
        {
            if (!this.auras.ContainsKey(teamName))
            {
                throw new Exception($"Missing team {teamName} in aura collection");
            }

            return this.auras[teamName];
        }

        // this is the simple case if we don't have
        // to worry about taking from other team auras
        public void AddToTeamAura(int auraAmount, string teamName)
        {
            int newTotalAura = this.currentTotalAura + auraAmount;
            int freeAuraAmount = this.GetCapacity() - this.currentTotalAura;
            int adjustedAuraAmount = newTotalAura <= this.GetCapacity() ? auraAmount : freeAuraAmount;

            this.currentTotalAura += adjustedAuraAmount;
            this.GetTeamAura(teamName).AddAmount(adjustedAuraAmount);
        }

        // This is the case when there's no more "free" aura capacity
        // and you target taking away from the opponent's aura
        public void ConvertTeamAura(int auraAmount, string toTeamName, string fromTeamName)
        {
            this.GetTeamAura(fromTeamName).SubtractAmount(auraAmount);
            this.GetTeamAura(toTeamName).AddAmount(auraAmount);
        }
    }
}
