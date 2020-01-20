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
        public UIEvent updateTerraUI;
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

        public void OnShowTerraUI(UIEventData uiEventData)
        {
            ShowTerraUI();
            UpdateTerraUI(uiEventData.terra, uiEventData.terraformOverlay);
        }

        public void OnHideTerraUI(UIEventData uiEventData)
        {
            HideTerraUI();
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

        private void ShowTerraUI()
        {
            if (!this.gameUI.terraUIGameObject.activeSelf)
            {
                this.gameUI.terraUIGameObject.SetActive(true);
            }
        }

        private void HideTerraUI()
        {
            if (this.gameUI.terraUIGameObject.activeSelf)
            {
                this.gameUI.terraUIGameObject.SetActive(false);
            }
        }

        private void UpdateTerraUI(Terra terra, TerraformOverlay terraformOverlay)
        {
            if (this.gameUI.GetTerraUI().currentTerraType != terra.type ||
                this.gameUI.GetTerraUI().currentTerraType != terraformOverlay.currentTerraType)
            {
                RaiseUpdateTerraUIEvent(terra, terraformOverlay);
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

        private void RaiseUpdateTerraUIEvent(Terra terra, TerraformOverlay terraformOverlay)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.terra = terra;
            uiEventData.terraformOverlay = terraformOverlay;

            this.updateTerraUI.Raise(uiEventData);
        }
    }
}

