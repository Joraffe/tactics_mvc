using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class TileController : MonoBehaviour
    {
        public MapEvent selectTileOnMap;
        public MapEvent clearTilesOnMap;
        public VoidEvent resetTilesOnMap;

        public UIEvent showTerraUI;
        public UIEvent hideTerraUI;

        public Tile tile;
        public Collider2D[] mouseOverlapColliders;

        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        public void Start()
        {
            this.mouseOverlapColliders = new Collider2D[2];   
        }
        public void OnMouseDown()
        {
            if (this.tile.active && !this.tile.occupant) {
                SelectTileOnMap(this.tile);
            }
            else if (!this.tile.occupant) {
                ResetTilesOnMap();
            }
        }

        public void HandleMouseHover()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int numColliders = Physics2D.OverlapPointNonAlloc(mousePosition, this.mouseOverlapColliders);
            if (numColliders == 0)
            {
                HideTerraUI();
                return;
            }

            foreach (Collider2D collider in mouseOverlapColliders)
            {
                GameObject colliderGameObject = collider.gameObject;
                if (colliderGameObject.tag == "Tile")
                {
                    Terra colliderTerra = colliderGameObject.GetComponent<Tile>().terra;
                    TerraformOverlay terraformOverlay = colliderGameObject.GetComponent<Tile>().terraformOverlay;
                    ShowTerraUI(colliderTerra, terraformOverlay);
                    break;
                }
            }
        }

        public void Update()
        {
            HandleMouseHover();
        }

        public void OnSetMovementTile(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile movementTile = tileEventData.tile;
                movementTile.SetActiveMovement();
            }
        }

        public void OnSetCombatTile(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile combatTile = tileEventData.tile;
                combatTile.SetActiveCombat();
            }
        }

        public void OnSetArrangementTile(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile startTile = tileEventData.tile;
                startTile.SetActiveArrangement();
            }
        }

        public void OnClearArrangementTile(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile startTile = tileEventData.tile;
                startTile.ClearActiveArrangement();
            }
        }

        public void OnSetTerraformActiveState(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                tileEventData.tile.SetActiveTerraform();
            }
        }

        public void OnClearActiveState(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                tileEventData.tile.ClearActiveState();
            }
        }

        public void OnCleartile(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToClear = tileEventData.tile;
                tileToClear.ClearTile();
            }
        }

        public void OnOccupyTile(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToOccupy = tileEventData.tile;
                Character occupant = tileEventData.character;
                tileToOccupy.SetOccupant(occupant);
            }
        }

        public void OnUnoccupyTile(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToUnoccupy = tileEventData.tile;
                tileToUnoccupy.ClearOccupant();
            }
        }

        public void OnPreviewPathOverlay(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToPreview = tileEventData.tile;
                tileToPreview.SetPathOverlayImage(tileEventData.pathOverlayImage);
            }
        }

        public void OnClearPathOverlay(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToClear = tileEventData.tile;
                tileToClear.ClearPathOverlayImage();
            }
        }

        public void OnPreviewSelectOverlay(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToPreview = tileEventData.tile;
                tileToPreview.SetSelectOverlayImage(tileEventData.selectOverlayType);
            }
        }

        public void OnClearSelectOverlay(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToClear = tileEventData.tile;
                tileToClear.ClearSelectOverlayImage();
            }
        }

        public void OnShowDangerOverlay(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToShow = tileEventData.tile;
                tileToShow.SetDangerOverlayImage(tileEventData.dangerOverlayImage);
            }
        }

        public void OnClearDangerOverlay(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToClear = tileEventData.tile;
                tileToClear.ClearDangerOverlayImage();
            }
        }

        public void OnShowActionOverlay(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToShow = tileEventData.tile;
                tileToShow.SetActionOverlayImage(tileEventData.actionOverlayImage);
            }
        }

        public void OnClearActionOverlay(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToClear = tileEventData.tile;
                tileToClear.ClearActionOverlayImage();
            }
        }

        public void OnShowTerraformOverlay(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToShow = tileEventData.tile;
                tileToShow.SetTerraformOverlayImage(tileEventData.terraformOverlayImage);
            }
        }

        public void OnClearTerraformOverlay(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                Tile tileToClear = tileEventData.tile;
                tileToClear.ClearTerraformOverlayImage();
            }
        }


        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void SelectTileOnMap(Tile tile)
        {
            RaiseSelectTileMapEvent(tile);
        }

        private void ClearTilesOnMap()
        {
            RaiseClearTilesMapEvent();
        }

        private void ResetTilesOnMap()
        {
            RaiseResetTilesMapEvent();
        }

        private void ShowTerraUI(Terra terra, TerraformOverlay terraformOverlay)
        {
            RaiseShowTerraUIEvent(terra, terraformOverlay);
        }

        private void HideTerraUI()
        {
            RaiseHideTerraUIEvent();
        }


        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/
        private void RaiseSelectTileMapEvent(Tile tile)
        {
            MapEventData mapEventData = new MapEventData();
            mapEventData.tile = tile;

            selectTileOnMap.Raise(mapEventData);
        }

        private void RaiseClearTilesMapEvent()
        {
            MapEventData mapEventData = new MapEventData();

            clearTilesOnMap.Raise(mapEventData);
        }

        private void RaiseResetTilesMapEvent()
        {
            resetTilesOnMap.Raise();
        }

        private void RaiseShowTerraUIEvent(Terra terra, TerraformOverlay terraformOverlay)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.terra = terra;
            uiEventData.terraformOverlay = terraformOverlay;

            this.showTerraUI.Raise(uiEventData);
        }

        private void RaiseHideTerraUIEvent()
        {
            UIEventData uIEventData = new UIEventData();

            this.hideTerraUI.Raise(uIEventData);
        }

    }

}
