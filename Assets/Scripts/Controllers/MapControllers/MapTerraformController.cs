using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class MapTerraformController : MonoBehaviour
    {
        public TileEvent setActiveState;
        public TileEvent clearActiveState;
        public TileEvent showOverlay;
        public TileEvent clearOverlay;
        public Map map;

        public void Update()
        {
            if (this.map.currentSelectedCharacter && this.map.currentSelectedForma)
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
            if (this.map.currentSelectedCharacter && !this.map.currentSelectedForma)
            {
                SelectMapForma(mapEventData.forma);
            }
        }

        public void OnResetForma(MapEventData mapEventData)
        {
            if (this.map.currentSelectedCharacter && this.map.currentSelectedForma)
            {
                ResetMapForma();
            }

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

                if (this.map.isCoordinatesWithinMap(terraformTileXPos, terraformTileYPos))
                {
                    Tile terraformTile = this.map.tiles[terraformTileXPos, terraformTileYPos];
                    this.map.AddTerraformingTile(terraformTile);
                    RaiseShowOverlayTileEvent(terraformTile, formaTile.terraType, TileOverlayTypes.Terraform);
                    RaiseSetActiveStateTileEvent(terraformTile, TileInteractType.Terraform);
                }
            }
        }

        private void ClearPreviousTerraform()
        {
            foreach (Tile terraformTile in this.map.terraformingTiles)
            {
                RaiseClearActiveStateTileEvent(terraformTile);
                RaiseClearOverlayTileEvent(terraformTile, TileOverlayTypes.Terraform);
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
        *                 Event Triggers
        --------------------------------------------------*/
        private void RaiseShowOverlayTileEvent(Tile tile, string overlayImageKey, string overlayType)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;
            tileEventData.overlayImageKey = overlayImageKey;
            tileEventData.overlayType = overlayType;

            this.showOverlay.Raise(tileEventData);
        }

        private void RaiseClearOverlayTileEvent(Tile tile, string overlayType)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;
            tileEventData.overlayType = overlayType;

            this.clearOverlay.Raise(tileEventData);
        }

        private void RaiseSetActiveStateTileEvent(Tile tile, string activeState)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;
            tileEventData.activeState = activeState;

            this.setActiveState.Raise(tileEventData);
        }

        private void RaiseClearActiveStateTileEvent(Tile tile)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;

            this.clearActiveState.Raise(tileEventData);
        }
    }
}
