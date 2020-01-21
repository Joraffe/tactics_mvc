using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class CharacterUIController : MonoBehaviour
    {
        public CharacterUI characterUI;

        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        public void OnUpdateCharacter(UIEventData uiEventData)
        {
            UpdateCharacter(uiEventData.character);
        }


        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void UpdateCharacter(Character character)
        {
            this.characterUI.SetCharacter(character);
            this.characterUI.SetPortraitSprite(character.characterSprite);
            this.characterUI.SetDetailsName(character.name);
        }


        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
    }
}

