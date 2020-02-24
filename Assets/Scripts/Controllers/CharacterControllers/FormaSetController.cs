using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class FormaSetController : MonoBehaviour
    {
        public FormaSet formaSet;

        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        public void OnSetPreviewableForma(CharacterEventData characterEventData)
        {
            
            if (this.formaSet.character == characterEventData.character)
            {
                foreach (KeyValuePair<KeyCode, Forma> forma in this.formaSet.formaKeyBindingMap)
                {
                    forma.Value.SetPreviewAble();
                }
            }
        }

        public void OnClearPreviewableForma(CharacterEventData characterEventData)
        {
            foreach (KeyValuePair<KeyCode, Forma> forma in this.formaSet.formaKeyBindingMap)
            {
                forma.Value.ResetPreviewable();
            }
        }


        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/


        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
    }
}
