using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class TeamTypes
    {
        public const string Player = "Player";
        public const string Enemey = "Enemy";
    }


    public class Team : MonoBehaviour
    {
        public string teamName = "";
        public int totalAura = 0;
        private List<Character> members = new List<Character>();
        public string teamType;

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

        public bool HaveAllMembersActed()
        {
            foreach (Character member in this.members)
            {
                if (!member.acted)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
