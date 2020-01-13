using Tactics.Models;
using Tactics.Events;
using UnityEngine;


namespace Tactics.Controllers
{
    public class ButtonController : MonoBehaviour
    {
        public FooterEvent clickButton;
        public Button button;


        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        public void OnMouseDown()
        {
            ClickButton(this.button);
        }

        public void OnShowActiveButton(ButtonEventData buttonEventData)
        {
            if (this.button == buttonEventData.button)
            {
                this.button.SetActiveSprite();
                this.button.SetActive();
            }
        }

        public void OnShowDefaultButton(ButtonEventData buttonEventData)
        {
            if (this.button == buttonEventData.button)
            {
                this.button.SetDefaultSprite();
                this.button.SetInactive();
            }
        }

        public void OnShowButton(ButtonEventData buttonEventData)
        {
            if (this.button.type == buttonEventData.buttonType)
            {
                this.button.SetVisible();
            }
        }

        public void OnHideButton(ButtonEventData buttonEventData)
        {
            if (this.button.type == buttonEventData.buttonType)
            {
                this.button.SetInvisible();
            }
        }

        public void OnPressButton(ButtonEventData buttonEventData)
        {
            if (this.button.type == buttonEventData.buttonType)
            {
                this.button.SetUnpressable();
            }
        }

        public void OnUnpressButton(ButtonEventData buttonEventData)
        {
            if (this.button.type == buttonEventData.buttonType)
            {
                this.button.SetPressable();
            }
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void ClickButton(Button button)
        {
            RaiseClickButtonFooterEvent(button);
        }

        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/
        private void RaiseClickButtonFooterEvent(Button button)
        {
            FooterEventData footerEventData = new FooterEventData();
            footerEventData.button = button;

            this.clickButton.Raise(footerEventData);
        }
    }
}
