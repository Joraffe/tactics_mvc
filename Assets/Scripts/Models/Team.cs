using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class Team : MonoBehaviour
    {
        public string faction = "";
        public List<Character> members = new List<Character>();

        public List<Character> availableToAct = new List<Character>();
        public List<Character> alreadyActed = new List<Character>();

        public void AddCharacterToAlreadyActed(Character character)
        {
            this.alreadyActed.Add(character);
        }

        public void RemoveCharacterFromAvailableToAct(Character character)
        {
            this.availableToAct.Remove(character);
        }
    }
}
