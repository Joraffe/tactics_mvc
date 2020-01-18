using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class GameUIController : MonoBehaviour
    {
        public UIEvent updateCharacterUI;
        public GameUI gameUI;



        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        public void OnShowCharacterUI(UIEventData uiEventData)
        {
            ShowCharacterUI();
            UpdateCharacterUI(uiEventData.character);
        }

        public void OnHideCharacterUI(UIEventData uiEventData)
        {
            HideCharacterUI();
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void ShowCharacterUI()
        {
            if (!this.gameUI.characterUIGameObject.activeSelf)
            {
                this.gameUI.characterUIGameObject.SetActive(true);
            }
        }

        private void HideCharacterUI()
        {
            if (this.gameUI.characterUIGameObject.activeSelf)
            {
                this.gameUI.characterUIGameObject.SetActive(false);
            }
        }

        private void UpdateCharacterUI(Character character)
        {
            if (this.gameUI.GetCharacterUI().character != character)
            {
                RaiseUpdateCharacterUIEvent(character);
            }
        }

        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/

        private void RaiseUpdateCharacterUIEvent(Character character)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.character = character;

            this.updateCharacterUI.Raise(uiEventData);
        }
    }
}

