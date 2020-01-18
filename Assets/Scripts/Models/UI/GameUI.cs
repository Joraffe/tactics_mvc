using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class GameUI : MonoBehaviour
    {
        public GameObject characterUIGameObject;
        public GameObject terrainUIGameObject;

        public CharacterUI GetCharacterUI()
        {
            return this.characterUIGameObject.GetComponent<CharacterUI>();
        }
    }

}
