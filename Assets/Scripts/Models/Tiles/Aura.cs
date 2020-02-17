
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class Aura
    {
        public string teamName;
        public int amount;
        public int previewAmount;

        public Aura(string teamName)
        {
            this.teamName = teamName;
            this.amount = 0;
            this.previewAmount = 0;
        }

        public void AddAmount(int amount)
        {
            this.amount += amount;
        }

        public void SubtractAmount(int amount)
        {
            if (this.amount - amount < 0)
            {
                throw new Exception($"Unable to subtract aura amount {amount} from {this.teamName}'s aura because that would reduce it to a negative number");
            }
            this.amount -= amount;
        }

        public void SetPreviewAmount(int previewAmount)
        {
            this.previewAmount = previewAmount;
        }

        public void ResetPreviewAmount()
        {
            this.previewAmount = 0;
        }
    }
}
