using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class FormaController : MonoBehaviour
    {
        public MapEvent showFormaArea;
        public Forma forma;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        /*-------------------------------------------------
        *             Periodic Functionality
        --------------------------------------------------*/
        void Update()
        {
        // Only allow showing the forma if we have selected a character
            if (this.forma.castable)
            {
                HandleFormaCancelInput();
                HandleFormaDirectionInput();
            }
            else
            {
                HandleFormaCastableInput();
            }
        }
        


        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        private void HandleFormaDirectionInput()
        {
            List<FormaTile> formaTiles = new List<FormaTile>();
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                formaTiles = this.forma.GetUpFormaTiles();
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                formaTiles = this.forma.GetDownFormaTiles();
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                formaTiles = this.forma.GetLeftFormaTiles();
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                formaTiles = this.forma.GetRightFormaTiles();
            }

            if (formaTiles.Count != 0)
            {
                ShowFormaAreaOnMap(formaTiles);
            }
        }

        private void HandleFormaCastableInput()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                this.forma.SetCastable();
                // By default pick a direction; in the future this may be determined by character
                ShowFormaAreaOnMap(this.forma.GetUpFormaTiles());
            }
        }

        private void HandleFormaCancelInput()
        {
            if (Input.GetKey(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                this.forma.ResetCastable();
            }
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void ShowFormaAreaOnMap(List<FormaTile> formaTiles)
        {
            RaiseShowFormaAreaMapEvent(formaTiles);
        }


        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/
        private void RaiseShowFormaAreaMapEvent(List<FormaTile> formaTiles)
        {
            MapEventData mapEventData = new MapEventData();
            mapEventData.formaTiles = formaTiles;

            this.showFormaArea.Raise(mapEventData);
        }
    }

}
