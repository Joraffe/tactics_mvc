using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class MapTerraformController : MonoBehaviour
    {
        public TileEvent showTerraformOverlay;
        public TileEvent clearTerraformOverlay;
        public Map map;

        public void Update()
        {
            if (this.map.currentSelectedForma)
            {
                HandleFormaDirection();
            }
        }

        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        private void HandleFormaDirection()
        {
            List<FormaTile> formaTiles = new List<FormaTile>();
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 selectedCharPosition = this.map.currentSelectedCharacter.transform.position;

            if (mousePosition.x > selectedCharPosition.x + 0.5 &&
                mousePosition.y < selectedCharPosition.y + 0.5 &&
                mousePosition.y > selectedCharPosition.y - 0.5)
            {
                formaTiles = this.map.currentSelectedForma.GetRightFormaTiles();
            }
            else if (mousePosition.x < selectedCharPosition.x - 0.5 &&
                     mousePosition.y < selectedCharPosition.y + 0.5 &&
                     mousePosition.y > selectedCharPosition.y - 0.5)
            {
                formaTiles = this.map.currentSelectedForma.GetLeftFormaTiles();
            }
            else if (mousePosition.y > selectedCharPosition.y + 0.5 &&
                     mousePosition.x < selectedCharPosition.x + 0.5 &&
                     mousePosition.x > selectedCharPosition.x - 0.5)
            {
                formaTiles = this.map.currentSelectedForma.GetUpFormaTiles();
            }
            else if (mousePosition.y < selectedCharPosition.y - 0.5 &&
                     mousePosition.x < selectedCharPosition.x + 0.5 &&
                     mousePosition.x > selectedCharPosition.x - 0.5)
            {
                formaTiles = this.map.currentSelectedForma.GetDownFormaTiles();
            }

            if (formaTiles.Count > 0)
            {
                ClearPreviousTerraform();
                PreviewTerraform(formaTiles);
            }

        }

        public void OnSelectForma(MapEventData mapEventData)
        {
            SelectMapForma(mapEventData.forma);
        }

        public void OnResetForma(MapEventData mapEventData)
        {
            ResetMapForma();
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void PreviewTerraform(List<FormaTile> formaTiles)
        {
            foreach (FormaTile formaTile in formaTiles)
            {
                Tile characterTile = this.map.GetCurrentSelectedCharacter().currentTile;
                int terraformTileXPos = characterTile.XPosition + formaTile.relativeX;
                int terraformTileYPos = characterTile.YPosition + formaTile.relativeY;

                Tile terraformTile = this.map.tiles[terraformTileXPos, terraformTileYPos];
                this.map.AddTerraformingTile(terraformTile);
                RaiseShowTerraformOverlayTileEvent(terraformTile, formaTile.terraType);
            }
        }

        private void ClearPreviousTerraform()
        {
            foreach (Tile terraformTile in this.map.terraformingTiles)
            {
                RaiseClearTerraformOverlayTileEvent(terraformTile);
            }
        }

        private void SelectMapForma(Forma forma)
        {
            this.map.SetCurrentSelectedForma(forma);
        }

        private void ResetMapForma()
        {
            ClearPreviousTerraform();
            this.map.ClearCurrentSelectedForma();
        }

        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/
        private void RaiseShowTerraformOverlayTileEvent(Tile tile, string terraformOverlayImage)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;
            tileEventData.terraformOverlayImage = terraformOverlayImage;

            this.showTerraformOverlay.Raise(tileEventData);
        }

        private void RaiseClearTerraformOverlayTileEvent(Tile tile)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;

            this.clearTerraformOverlay.Raise(tileEventData);
        }
    }
}
