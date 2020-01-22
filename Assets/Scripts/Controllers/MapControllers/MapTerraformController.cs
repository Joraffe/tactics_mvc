using System.Collections;
using System.Collections.Generic;
using Tactics.Constants;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class MapTerraformController : MonoBehaviour
    {
        public TileEvent setActiveState;
        public TileEvent clearActiveState;
        public TileEvent setPreviewTerraformType;
        public TileEvent clearPreviewTerraformType;
        public TileEvent showOverlay;
        public TileEvent clearOverlay;
        public TileEvent terraformTile;

        public UIEvent showTerraformUI;
        public UIEvent hideTerraformUI;
        public Map map;

        public void Update()
        {
            if (this.map.currentSelectedCharacter &&
                this.map.currentSelectedForma &&
                !this.map.isPreviewingTerraform)
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
            string formaDirection = "";

            if (mousePosition.x > selectedCharPosition.x + 0.5 &&
                mousePosition.y < selectedCharPosition.y + 0.5 &&
                mousePosition.y > selectedCharPosition.y - 0.5)
            {
                formaDirection = "right";
                formaTiles = this.map.currentSelectedForma.GetRightFormaTiles();
            }
            else if (mousePosition.x < selectedCharPosition.x - 0.5 &&
                     mousePosition.y < selectedCharPosition.y + 0.5 &&
                     mousePosition.y > selectedCharPosition.y - 0.5)
            {
                formaDirection = "left";
                formaTiles = this.map.currentSelectedForma.GetLeftFormaTiles();
            }
            else if (mousePosition.y > selectedCharPosition.y + 0.5 &&
                     mousePosition.x < selectedCharPosition.x + 0.5 &&
                     mousePosition.x > selectedCharPosition.x - 0.5)
            {
                formaDirection = "up";
                formaTiles = this.map.currentSelectedForma.GetUpFormaTiles();
            }
            else if (mousePosition.y < selectedCharPosition.y - 0.5 &&
                     mousePosition.x < selectedCharPosition.x + 0.5 &&
                     mousePosition.x > selectedCharPosition.x - 0.5)
            {
                formaDirection = "down";
                formaTiles = this.map.currentSelectedForma.GetDownFormaTiles();
            }

            if (formaTiles.Count > 0 && formaDirection != this.map.currentFormaDirection)
            {
                ClearPreviousTerraformAOE();
                ShowTerraformAOE(formaTiles);
                this.map.currentFormaDirection = formaDirection;
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
            ResetMapForma();
        }

        public void OnPreviewTerraform(MapEventData mapEventData)
        {
            PreviewTerraform(mapEventData.terraformingTiles, mapEventData.terraCountMap);
        }

        public void OnCommitTerraform(MapEventData mapEventData)
        {
            CommitTerraform(mapEventData.terraformingTiles, mapEventData.terraCountMap);
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void ShowTerraformAOE(List<FormaTile> formaTiles)
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
                    RaiseSetPreviewTerraformTypeTileEvent(terraformTile, formaTile.terraType);
                }
            }
        }

        private void ClearPreviousTerraformAOE()
        {
            foreach (Tile terraformTile in this.map.terraformingTiles)
            {
                RaiseClearActiveStateTileEvent(terraformTile);
                RaiseClearOverlayTileEvent(terraformTile, TileOverlayTypes.Terraform);
                RaiseClearOverlayTileEvent(terraformTile, TileOverlayTypes.Select);
                RaiseClearPreviewTerraformTypeTileEvent(terraformTile);
            }
            this.map.ClearTerraformingTiles();
        }

        private void PreviewTerraform(List<Tile> terraformingTiles, Dictionary<string, int> terraCountMap)
        {
            this.map.SetIsPreviewingTerraform();
            foreach (Tile tile in terraformingTiles)
            {
                RaiseShowOverlayTileEvent(
                    tile,
                    SelectOverlayTypes.Terraform,
                    TileOverlayTypes.Select
                );
            }
            Dictionary<string, int> postTerraformTerraCountMap = this.map.GetPostTerraformTerraCountMap(
                terraformingTiles,
                terraCountMap
            );
            RaiseShowTerraformUI(terraCountMap, postTerraformTerraCountMap);
        }

        private void CommitTerraform(List<Tile> terraformingTiles, Dictionary<string, int> terraCountMap)
        {
            this.map.UpdateTerraCountMap(terraformingTiles);
            foreach (Tile terraformingTile in terraformingTiles)
            {
                RaiseTerraformTileEvent(terraformingTile, terraformingTile.previewTerraformType);
            }
            ResetMapForma();
        }

        private void SelectMapForma(Forma forma)
        {
            this.map.SetCurrentSelectedForma(forma);
        }

        private void ResetMapForma()
        {
            ClearPreviousTerraformAOE();
            this.map.ClearCurrentSelectedForma();
            this.map.ResetIsPreviewingTerraform();
            this.map.currentFormaDirection = "";
            RaiseHideTerraformUI();
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

        private void RaiseSetPreviewTerraformTypeTileEvent(Tile tile, string previewTerraformType)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;
            tileEventData.previewTerraformType = previewTerraformType;

            this.setPreviewTerraformType.Raise(tileEventData);
        }

        private void RaiseClearPreviewTerraformTypeTileEvent(Tile tile)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;

            this.clearPreviewTerraformType.Raise(tileEventData);
        }

        private void RaiseShowTerraformUI(Dictionary<string, int> terraCountMap, Dictionary<string, int> postTerraformTerraCountMap)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.terraCountMap = terraCountMap;
            uiEventData.postTerraformTerraCountMap = postTerraformTerraCountMap;

            this.showTerraformUI.Raise(uiEventData);
        }

        private void RaiseHideTerraformUI()
        {
            UIEventData uiEventData = new UIEventData();

            this.hideTerraformUI.Raise(uiEventData);
        }

        private void RaiseTerraformTileEvent(Tile tile, string terraType)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;
            tileEventData.terraType = terraType;

            this.terraformTile.Raise(tileEventData);
        }
    }
}
