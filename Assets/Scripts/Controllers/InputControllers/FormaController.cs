using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class FormaController : MonoBehaviour
    {
        public MapEvent selectForma;
        public MapEvent resetForma;
        public Forma forma;

        public void Update()
        {
            HandleFormaCastableInput();
            HandleFormaCancelInput();
        }

        // Start is called before the first frame update
        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        private void HandleFormaCastableInput()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                SelectFormaForMap(this.forma);
            }
        }

        private void HandleFormaCancelInput()
        {
            if (Input.GetKey(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                ResetFormaForMap();
            }
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void SelectFormaForMap(Forma forma)
        {
            RaiseSelectFormaMapEvent(forma);
        }

        private void ResetFormaForMap()
        {
            RaiseResetFormaMapEvent();
        }


        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/
        private void RaiseSelectFormaMapEvent(Forma forma)
        {
            MapEventData mapEventData = new MapEventData();
            mapEventData.forma = forma;

            this.selectForma.Raise(mapEventData);
        }

        private void RaiseResetFormaMapEvent()
        {
            MapEventData mapEventData = new MapEventData();
            
            this.resetForma.Raise(mapEventData);
        }
    }

}
