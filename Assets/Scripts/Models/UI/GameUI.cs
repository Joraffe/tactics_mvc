using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class GameUI : MonoBehaviour
    {
        public GameObject characterUIGameObject;
        public GameObject terraUIGameObject;

        public CharacterUI GetCharacterUI()
        {
            return this.characterUIGameObject.GetComponent<CharacterUI>();
        }

        public TerraUI GetTerraUI()
        {
            return this.terraUIGameObject.GetComponent<TerraUI>();
        }
    }

}
