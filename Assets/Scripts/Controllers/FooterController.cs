using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;

namespace Tactics.Controllers
{
    public class FooterController : MonoBehaviour
    {
        public UIEvent showDangerZone;
        public UIEvent hideDangerZone;
        public UIEvent showArrangeTiles;
        public UIEvent hideArrangeTiles;
        public ButtonEvent showActiveButton;
        public ButtonEvent showDefaultButton;
        public ButtonEvent showButton;
        public ButtonEvent hideButton;
        public ButtonEvent pressButton;
        public ButtonEvent unpressButton;
        public Footer footer;

        public void Start()
        {
            // HideFightButton();
            PressFightButton();
        }

        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        public void OnClickFooterButton(FooterEventData footerEvent)
        {
            string buttonType = footerEvent.button.type;
            Button button = footerEvent.button;

            switch (buttonType)
            {
                case ButtonTypes.Settings:
                    ClickSettingsButton(button);
                    break;

                case ButtonTypes.Danger:
                    ClickDangerButton(button);
                    break;

                case ButtonTypes.Arrange:
                    ClickArrangeButton(button);
                    break;

                case ButtonTypes.EndTurn:
                    ClickEndTurnButton(button);
                    break;

                case ButtonTypes.Fight:
                    ClickFightButton(button);
                    break;

                default:
                    return;

            }
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/

        private void ClickSettingsButton(Button button)
        {
            // TO DO
        }

        private void ClickDangerButton(Button button)
        {
            if (!button.active)
            {
                RaiseShowDangerZoneUIEvent();
                RaiseShowActiveButtonEvent(button);
            }
            else
            {
                RaiseHideDangerZoneUIEvent();
                RaiseShowDefaultButtonEvent(button);
            }
        }

        private void ClickArrangeButton(Button button)
        {
            RaiseShowArrangeTilesUIEvent();
            RaisePressArrangeButtonEvent();
            RaiseUnpressFightButtonEvent();
        }

        private void ClickEndTurnButton(Button button)
        {
            // TO DO
        }

        private void ClickFightButton(Button button)
        {
            RaiseHideArrangeTilesUIEvent();
            RaisePressFightButtonEvent();
            RaiseUnpressArrangeButtonEvent();
        }

        private void HideFightButton()
        {
            RaiseHideFightButtonEvent();
        }

        private void HideEndTurnButton()
        {
            RaiseHideEndTurnButtonEvent();
        }

        private void PressFightButton()
        {
            RaisePressFightButtonEvent();
        }

        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/
        private void RaiseShowDangerZoneUIEvent()
        {
            UIEventData uiEventData = new UIEventData();

            this.showDangerZone.Raise(uiEventData);
        }

        private void RaiseHideDangerZoneUIEvent()
        {
            UIEventData uiEventData = new UIEventData();

            this.hideDangerZone.Raise(uiEventData);
        }

        private void RaiseShowActiveButtonEvent(Button button)
        {
            ButtonEventData buttonEventData = new ButtonEventData();
            buttonEventData.button = button;

            this.showActiveButton.Raise(buttonEventData);
        }

        private void RaiseShowDefaultButtonEvent(Button button)
        {
            ButtonEventData buttonEventData = new ButtonEventData();
            buttonEventData.button = button;

            this.showDefaultButton.Raise(buttonEventData);
        }

        private void RaiseShowArrangeTilesUIEvent()
        {
            UIEventData uiEventData = new UIEventData();

            this.showArrangeTiles.Raise(uiEventData);
        }

        private void RaiseHideArrangeTilesUIEvent()
        {
            UIEventData uiEventData = new UIEventData();

            this.hideArrangeTiles.Raise(uiEventData);
        }

        private void RaiseShowArrangeButtonEvent()
        {
            ButtonEventData buttonEventData = new ButtonEventData();
            buttonEventData.buttonType = ButtonTypes.Arrange;

            this.showButton.Raise(buttonEventData);
        }

        private void RaiseHideArrangeButtonEvent()
        {
            ButtonEventData buttonEventData = new ButtonEventData();
            buttonEventData.buttonType = ButtonTypes.Arrange;

            this.hideButton.Raise(buttonEventData);
        }

        private void RaiseShowFightButtonEvent()
        {
            ButtonEventData buttonEventData = new ButtonEventData();
            buttonEventData.buttonType = ButtonTypes.Fight;

            this.showButton.Raise(buttonEventData);
        }

        private void RaiseHideFightButtonEvent()
        {
            ButtonEventData buttonEventData = new ButtonEventData();
            buttonEventData.buttonType = ButtonTypes.Fight;

            this.hideButton.Raise(buttonEventData);
        }

        private void RaiseHideEndTurnButtonEvent()
        {
            ButtonEventData buttonEventData = new ButtonEventData();
            buttonEventData.buttonType = ButtonTypes.EndTurn;

            this.hideButton.Raise(buttonEventData);
        }

        private void RaisePressFightButtonEvent()
        {
            ButtonEventData buttonEventData = new ButtonEventData();
            buttonEventData.buttonType = ButtonTypes.Fight;

            this.pressButton.Raise(buttonEventData);
        }

        private void RaiseUnpressFightButtonEvent()
        {
            ButtonEventData buttonEventData = new ButtonEventData();
            buttonEventData.buttonType = ButtonTypes.Fight;

            this.unpressButton.Raise(buttonEventData);
        }

        private void RaisePressArrangeButtonEvent()
        {
            ButtonEventData buttonEventData = new ButtonEventData();
            buttonEventData.buttonType = ButtonTypes.Arrange;

            this.pressButton.Raise(buttonEventData);
        }

        private void RaiseUnpressArrangeButtonEvent()
        {
            ButtonEventData buttonEventData = new ButtonEventData();
            buttonEventData.buttonType = ButtonTypes.Arrange;

            this.unpressButton.Raise(buttonEventData);
        }
    }
}
