using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class Team : MonoBehaviour
    {
        public string teamName = "";
        private List<Character> members = new List<Character>();

        /*-------------------------------------------------
        *                 Heirarchy
        --------------------------------------------------*/
        public void Awake()
        {
            this.SetUpMembers();
        }

        public void SetUpMembers()
        {
            foreach (Transform characterTransform in this.transform)
            {
                
                GameObject characterGameObject = characterTransform.gameObject;
                Character character = characterGameObject.GetComponent<Character>();
                this.members.Add(character);
            }
        }

        public List<Character> GetMembers()
        {
            return this.members;
        }
    }
}
