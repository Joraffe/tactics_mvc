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
        public UIEvent updateTerraformUI;
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
            UpdateTerraUI(uiEventData.terra);
        }

        public void OnHideTerraUI(UIEventData uiEventData)
        {
            HideTerraUI();
        }

        public void OnShowTerraformUI(UIEventData uiEventData)
        {
            ShowTerraformUI();
            UpdateTerraformUI(
                uiEventData.terraCountMap,
                uiEventData.postTerraformTerraCountMap
            );
        }

        public void OnHideTerraformUI(UIEventData uiEventData)
        {
            HideTerraformUI();
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

        private void UpdateTerraUI(Terra terra)
        {

            if (this.gameUI.GetTerraUI().currentTerraType != terra.type)
            {
                RaiseUpdateTerraUIEvent(terra);
            }
        }

        private void ShowTerraformUI()
        {
            if (!this.gameUI.terraformUIGameObject.activeSelf)
            {
                this.gameUI.terraformUIGameObject.SetActive(true);
            }
        }

        private void HideTerraformUI()
        {
            if (this.gameUI.terraformUIGameObject.activeSelf)
            {
                this.gameUI.terraformUIGameObject.SetActive(false);
            }
        }

        private void UpdateTerraformUI(Dictionary<string, int> terraCountMap, Dictionary<string, int> postTerraformTerraCountMap)
        {
            RaiseUpdateTerraformUIEvent(terraCountMap, postTerraformTerraCountMap);
        }

        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
        private void RaiseUpdateCharacterUIEvent(Character character)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.character = character;

            this.updateCharacterUI.Raise(uiEventData);
        }

        private void RaiseUpdateTerraUIEvent(Terra terra)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.terra = terra;

            this.updateTerraUI.Raise(uiEventData);
        }

        private void RaiseUpdateTerraformUIEvent(Dictionary<string, int> terraCountMap, Dictionary<string, int> postTerraformTerraCountMap)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.terraCountMap = terraCountMap;
            uiEventData.postTerraformTerraCountMap = postTerraformTerraCountMap;

            this.updateTerraformUI.Raise(uiEventData);
        }
    }
}

