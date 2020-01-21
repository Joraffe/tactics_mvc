using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tactics.Algorithms;
using Tactics.Models;
using Tactics.Events;
using UnityEngine;


namespace Tactics.Controllers
{
    public class MapController : MonoBehaviour
    {

        public TileEvent setActiveState;
        public TileEvent clearActiveState;
        public TileEvent clearTile;
        public TileEvent occupyTile;
        public TileEvent unoccupyTile;
        public TileEvent showOverlay;
        public TileEvent clearOverlay;

        public CharacterEvent previewCharacterMovement;
        public CharacterEvent previewCharacterCombat;
        public CharacterEvent clearCharacterCombat;
        public CharacterEvent undoCharacterMovement;

        public UIEvent showCharacterUI;
        public UIEvent hideCharacterUI;

        public CameraEvent focusCamera;
        public CameraEvent resetCamera;

        public TeamEvent completeTeamMemberAction;

        // For communicating with the other map controllers
        public MapEvent resetForma;
        public MapEvent previewTerraform;
        public MapEvent commitTerraform;

        public Map map;

        /*-------------------------------------------------
        *                 Event Handlers
        --------------------------------------------------*/
        public void OnCharacterClicked(MapEventData mapEventData)
        {
            // If we have no currently selected character on the map, show the available actions
            if (!this.map.currentSelectedCharacter)
            {
                ClearAllTiles();
                ShowInteractiveTilesForCharacter(mapEventData.character);
                PreviewSelectOverlayForTile(mapEventData.character.currentTile, TileInteractType.Movement);
                ShowCharacterUI(mapEventData.character);
                this.map.SetCurrentSelectedCharacter(mapEventData.character);
                FocusCameraOnCharacter(mapEventData.character);
            }
            // if we click the same character and they're previewing and they don't have a forma selected...
            else if (this.map.currentSelectedCharacter == mapEventData.character &&
                     this.map.currentSelectedCharacter.isPreviewing &&
                     !this.map.currentSelectedForma)
            {
                CompleteCharacterTurn(mapEventData.character);
            }
            // if we click the same character and they're not previewing...
            else if (this.map.currentSelectedCharacter == mapEventData.character
                     && !this.map.currentSelectedCharacter.isPreviewing)
            {
                ResetMap();
            }
            // if we click an enemy character that's on an active combat tile...
            else if (this.map.currentSelectedCharacter.isOpposition(mapEventData.character)
                     && mapEventData.character.currentTile.activeState == TileInteractType.Combat)
            {
                PreviewSelectCombatTile(mapEventData.character.currentTile);
            }
            // if we click an enemy character that's not on an active combat tile...
            else if (this.map.currentSelectedCharacter.isOpposition(mapEventData.character)
                     && !(mapEventData.character.currentTile.activeState == TileInteractType.Combat))
            {
                // TO DO: Show the enemy portrait
            }
            // If we click an ally character...
            else if (this.map.currentSelectedCharacter.isAlly(mapEventData.character))
            {
                // TO DO: Handle assist skills
                ShowCharacterUI(mapEventData.character);
                // Otherwise, do nothing!!!
            }
        }

        public void OnOccupyTile(MapEventData mapEventData)
        {
            OccupyTile(mapEventData.character, mapEventData.tile);
        }

        public void OnUnoccupyTile(MapEventData mapEventData)
        {
            UnoccupyTile(mapEventData.tile);
        }

        public void OnClearTiles(MapEventData mapEventData)
        {
            ClearAllTiles();
        }

        public void OnResetTiles()
        {
            ResetMap();
        }

        public void OnTileSelected(MapEventData mapEventData)
        {
            Tile selectedTile = mapEventData.tile;
            
            // If we click a tile and it has a "movement" active state,
            // we've displayed it as an option for a character to move;
            // we consider this previewing movement to that tile
            if (selectedTile.activeState == TileInteractType.Movement)
            {
                PreviewSelectMovementTile(selectedTile);
            }
            // If we click a tile and it has a "terraform" active state,
            // we've displayed it as an option for a character to terraform,
            // and we aren't already previewing a terraform...
            // we consider this previewing terraforming that tile
            else if (selectedTile.activeState == TileInteractType.Terraform &&
                     !this.map.isPreviewingTerraform)
            {
                PreviewTerraform(this.map.terraformingTiles, this.map.terraCountMap);
            }
            // If we click a tile and it has a "terraform" active state,
            // and we've displayed it as an option for a character to terraform,
            // and we already area previewing a terraform...
            // we consider this committing the terraform
            else if (selectedTile.activeState == TileInteractType.Terraform &&
                     this.map.isPreviewingTerraform)
            {
                CommitTerraform(this.map.terraformingTiles, this.map.terraCountMap);
                CompleteCharacterTurn(this.map.currentSelectedCharacter);
            }
        }

        public void OnShowPlayerArrangementTiles(MapEventData mapEventData)
        {
            List<Tile> playerStartTiles = this.map.GetPlayerStartTiles();

            foreach (Tile playerStartTile in playerStartTiles)
            {
                ShowPlayerArrangementTile(playerStartTile);
            }
        }

        public void OnHidePlayerArrangementTiles(MapEventData mapEventData)
        {
            List<Tile> playerStartTiles = this.map.GetPlayerStartTiles();

            foreach (Tile playerStartTile in playerStartTiles)
            {
                HidePlayerArrangementTile(playerStartTile);
            }
        }

        public void OnShowPlayerDangerZone(MapEventData mapEventData)
        {
            List<Character> enemies = mapEventData.team.members;

            ShowPlayerDangerZoneForEnemies(enemies);
        }

        public void OnHidePlayerDangerZone(MapEventData mapEventData)
        {
            ClearDangerOverlayForAllTiles();
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void ResetMap()
        {
            ClearAllTiles();
            ResetCurrentSelectedCharacter();
            HideCharacterUI();
            ResetCamera();
            ResetMapForma();
        }

        private void CompleteCharacterTurn(Character character)
        {
            this.map.ClearCurrentSelectedCharacter();
            ClearAllTiles();
            CompleteTeamMemberAction(character);
            HideCharacterUI();
        }
        private void ClearAllTiles()
        {
            foreach (Tile tile in this.map.tiles)
            {
                RaiseClearTileEvent(tile);
            }
        }

        private void PreviewSelectCombatTile(Tile selectedTile)
        {
            Character selectedCharacter = this.map.currentSelectedCharacter;

            // If we have selected a different comtbat tile before
            // we should clear out the older combat select overlay
            if (selectedCharacter.selectedCombatTile)
            {
                ClearPreviousSelectOverlay(selectedCharacter.selectedCombatTile);
            }

            PreviewCharacterCombat(selectedCharacter, selectedTile);
            PreviewSelectOverlayForTile(selectedTile, TileInteractType.Combat);

            // First check if we need to move (we don't if we're already on one
            // of the associatedmovement tiles of the selected combat tile)
            bool isCurrentlyOnAssociatedMovementTile = false;
            foreach (Tile associatedMovementTile in selectedTile.associatedMovementTiles)
            {
                if (associatedMovementTile == selectedCharacter.currentTile)
                {
                    isCurrentlyOnAssociatedMovementTile = true;
                }
            }

            // If we're not, then we preview movement to that tile
            if (!isCurrentlyOnAssociatedMovementTile)
            {
                // Also preview movement to a tile within range of the selected combat tile
                Tile associatedMovementTile = selectedTile.associatedMovementTiles.First();
                PreviewSelectMovementForCharacter(selectedCharacter, associatedMovementTile);
            }
        }

        private void PreviewSelectMovementTile(Tile selectedTile)
        {
            Character selectedCharacter = this.map.currentSelectedCharacter;

            // If we select the character originTile, then we are "unselecting" the character
            if (selectedTile == selectedCharacter.originTile)
            {
                ResetMap();
                return;
            }

            // if we previously selected a combat tile, we should
            // clear it for previewing just selecting a movement tile
            if (selectedCharacter.selectedCombatTile)
            {
                ClearPreviousSelectOverlay(selectedCharacter.selectedCombatTile);
                ClearCharacterCombat(selectedCharacter);
            }

            PreviewSelectMovementForCharacter(selectedCharacter, selectedTile);
        }

        private void PreviewSelectMovementForCharacter(Character selectedCharacter, Tile selectedTile)
        {
            Tile startTile = selectedCharacter.currentTile;
            Tile originTile = selectedCharacter.originTile;

            // If we were previewing a different movement tile before, we should
            // clean up the previous path overlay from the previous selected tile
            if (selectedCharacter.previousTile)
            {
                Path previousTilePath = AStar.GetTilePathFromAToBForCharacter(originTile, selectedCharacter.currentTile, this.map, selectedCharacter);
                ClearPreviousPathOverlay(previousTilePath);
                // If we are previewing a movement but then select the origin,
                // reset the character back to "first selected state";
                if (selectedTile == originTile)
                {
                    ResetCurrentSelectedCharacter();
                    this.map.SetCurrentSelectedCharacter(selectedCharacter);
                    PreviewSelectOverlayForTile(selectedTile, TileInteractType.Movement);
                    ClearPreviousSelectOverlay(selectedCharacter.currentTile);
                    return;
                }
            }

            Path currentTilePath = AStar.GetTilePathFromAToBForCharacter(startTile, selectedTile, this.map, selectedCharacter);
            Path originTilePath = AStar.GetTilePathFromAToBForCharacter(originTile, selectedTile, this.map, selectedCharacter);
            PreviewPathOverlayForOriginPath(originTilePath);
            PreviewCharacterMovement(selectedCharacter, currentTilePath, startTile, selectedTile);
            PreviewSelectOverlayForTile(selectedTile, TileInteractType.Movement);
            ClearPreviousSelectOverlay(selectedCharacter.currentTile);
        }

        private void ResetCurrentSelectedCharacter()
        {
            if (this.map.currentSelectedCharacter)
            {
                if (this.map.currentSelectedCharacter.isPreviewing)
                {
                    UndoCharacterMovement(this.map.currentSelectedCharacter);
                }
                this.map.ClearCurrentSelectedCharacter();
            }
        }

        private void UndoCharacterMovement(Character character)
        {
            this.map.currentSelectedCharacter = null;
            RaiseOccupyTileEvent(character, character.originTile);
            RaiseUnoccupyTileEvent(character.currentTile);
            RaiseUndoCharacterMovement(character);
        }

        private void ShowInteractiveTilesForCharacter(Character character)
        {
            List<Tile> allMovementTiles = BFS.GetAllMovementTilesForCharacter(character, this.map);
            List<Tile> validMovementTiles = BFS.FilterMovementTilesOutsideOfCharacterMovementRange(allMovementTiles, character, this.map);

            foreach (Tile movementTile in validMovementTiles)
            {
                if (movementTile.isMoveableForCharacter(character))
                {
                    RaiseSetActiveStateTileEvent(movementTile, TileInteractType.Movement);
                    RaiseShowOverlayTileEvent(movementTile, "move", TileOverlayTypes.Move);
                }
            }
        }

        private void OccupyTile(Character character, Tile tile)
        {
            RaiseOccupyTileEvent(character, tile);
        }

        private void UnoccupyTile(Tile tile)
        {
            RaiseUnoccupyTileEvent(tile);
        }

        private void PreviewPathOverlayForOriginPath(Path originTilePath)
        {
            PathNode current = originTilePath.GetTail();
            PathNode previous = null;
            PathNode next = current.next;

            bool reachedHead = false;
            while (!reachedHead)
            {
                Tile currentTile = current.tile;
                string pathOverlayImage = current.GetImage();
                RaiseShowOverlayTileEvent(currentTile, pathOverlayImage, TileOverlayTypes.Path);
                if (current.next == null) {
                    reachedHead = true;
                } else
                {
                    previous = current;
                    current = next;
                    next = current.next;
                }
            }
        }

        private void ClearPreviousPathOverlay(Path previousTilePath)
        {
            PathNode current = previousTilePath.GetTail();
            PathNode previous = null;
            PathNode next = current.next;

            bool reachedHead = false;
            while (!reachedHead)
            {
                Tile currentTile = current.tile;
                RaiseClearOverlayTileEvent(currentTile, TileOverlayTypes.Path);
                if (current.next == null) {
                    reachedHead = true;
                } else
                {
                    previous = current;
                    current = next;
                    next = current.next;
                }
            }
        }

        private void PreviewSelectOverlayForTile(Tile tile, string selectOverlayType)
        {
            RaiseShowOverlayTileEvent(tile, selectOverlayType, TileOverlayTypes.Select);
        }

        private void ClearPreviousSelectOverlay(Tile tile)
        {
            RaiseClearOverlayTileEvent(tile, TileOverlayTypes.Select);
        }

        private void PreviewCharacterMovement(Character selectedCharacter, Path currentTilePath, Tile startTile, Tile selectedTile)
        {
            RaisePreviewCharacterMovement(selectedCharacter, currentTilePath, selectedTile);
            RaiseOccupyTileEvent(selectedCharacter, selectedTile);
            RaiseUnoccupyTileEvent(startTile);
        }

        private void PreviewCharacterCombat(Character selectedCharacter, Tile selectedCombatTile)
        {
            RaisePreviewCombatCharacterEvent(selectedCharacter, selectedCombatTile);
        }

        private void ClearCharacterCombat(Character selectedCharacter)
        {
            RaiseClearCombatCharacterEvent(selectedCharacter);
        }

        private void CompleteTeamMemberAction(Character completedCharacter)
        {
            RaiseCompleteActionTeamEvent(completedCharacter);
        }

        private void ShowPlayerArrangementTile(Tile tile)
        {
            RaiseSetActiveStateTileEvent(tile, TileInteractType.Arrangement);
        }

        private void HidePlayerArrangementTile(Tile tile)
        {
            RaiseClearActiveStateTileEvent(tile);
        }

        private void ClearDangerOverlayForAllTiles()
        {
            foreach (Tile tile in this.map.tiles)
            {
                RaiseClearOverlayTileEvent(tile, TileOverlayTypes.Danger);
            }
        }

        private void ShowPlayerDangerZoneForEnemies(List<Character> enemies)
        {
            HashSet<Tile> allEnemyCombatTiles = new HashSet<Tile>();

            foreach (Character enemy in enemies)
            {
                List<Tile> allMovementTiles = BFS.GetAllMovementTilesForCharacter(enemy, this.map);
                List<Tile> validMovementTiles = BFS.FilterMovementTilesOutsideOfCharacterMovementRange(allMovementTiles, enemy, this.map);
                HashSet<Tile> combatTiles = BFS.GetCombatTilesForCharacterMovementTiles(enemy, validMovementTiles, this.map);

                foreach (Tile combatTile in combatTiles)
                {
                    if (!allEnemyCombatTiles.Contains(combatTile) && combatTile.isCombatableForCharacter(enemy))
                    {
                        allEnemyCombatTiles.Add(combatTile);
                        combatTile.SetIsShowingDanger();
                    }
                }
            }

            foreach (Tile enemyTile in allEnemyCombatTiles)
            {
                string dangerOverlayImage = this.map.GetDangerOverlayImageForTile(enemyTile);
                RaiseShowOverlayTileEvent(enemyTile, dangerOverlayImage, TileOverlayTypes.Danger);
            }
        }

        private void ShowCharacterUI(Character character)
        {
            RaiseShowCharacterUIEvent(character);
        }

        private void HideCharacterUI()
        {
            RaiseHideCharacterUIEvent();
        }

        private void FocusCameraOnCharacter(Character character)
        {
            RaiseFocusCameraEvent(character);
        }

        private void ResetCamera()
        {
            RaiseResetCameraEvent();
        }

        private void ResetMapForma()
        {
            RaiseResetFormaMapEvent();
        }

        private void PreviewTerraform(List<Tile> terraformingTiles, Dictionary<string, int> terraCountMap)
        {
            RaisePreviewTerraformMapEvent(terraformingTiles, terraCountMap);
        }

        private void CommitTerraform(List<Tile> terraformingTiles, Dictionary<string, int> terraCountMap)
        {
            RaiseCommitTerraformMapEvent(terraformingTiles, terraCountMap);
        }


        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
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

        private void RaiseClearTileEvent(Tile tile)
        {
            TileEventData clearTileData = new TileEventData();
            clearTileData.tile = tile;

            this.clearTile.Raise(clearTileData);
        }

        private void RaisePreviewCharacterMovement(Character character, Path tilePath, Tile targetTile)
        {
            CharacterEventData previewCharacterMovementData = new CharacterEventData();
            previewCharacterMovementData.character = character;
            previewCharacterMovementData.tilePath = tilePath;
            previewCharacterMovementData.targetTile = targetTile;

            this.previewCharacterMovement.Raise(previewCharacterMovementData);
        }

        private void RaisePreviewCombatCharacterEvent(Character character, Tile targetTile)
        {
            CharacterEventData previewCharacterCombatData = new CharacterEventData();
            previewCharacterCombatData.character = character;
            previewCharacterCombatData.targetTile = targetTile;

            this.previewCharacterCombat.Raise(previewCharacterCombatData);
        }

        private void RaiseClearCombatCharacterEvent(Character character)
        {
            CharacterEventData clearCharacterCombatData = new CharacterEventData();
            clearCharacterCombatData.character = character;

            this.clearCharacterCombat.Raise(clearCharacterCombatData);
        }

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

        private void RaiseUndoCharacterMovement(Character character)
        {
            CharacterEventData undoCharacterMovementData = new CharacterEventData();
            undoCharacterMovementData.character = character;

            this.undoCharacterMovement.Raise(undoCharacterMovementData);
        }

        private void RaiseOccupyTileEvent(Character character, Tile tile)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;
            tileEventData.character = character;

            this.occupyTile.Raise(tileEventData);
        }

        private void RaiseUnoccupyTileEvent(Tile tile)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;

            this.unoccupyTile.Raise(tileEventData);
        }

        private void RaiseCompleteActionTeamEvent(Character character)
        {
            TeamEventData teamEventData = new TeamEventData();
            teamEventData.character = character;
            teamEventData.team = character.team;

            this.completeTeamMemberAction.Raise(teamEventData);
        }
        private void RaiseShowCharacterUIEvent(Character character)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.character = character;

            this.showCharacterUI.Raise(uiEventData);
        }

        private void RaiseHideCharacterUIEvent()
        {
            UIEventData uiEventData = new UIEventData();

            this.hideCharacterUI.Raise(uiEventData);
        }

        private void RaiseFocusCameraEvent(Character character)
        {
            CameraEventData cameraEventData = new CameraEventData();
            cameraEventData.transform = character.transform;

            this.focusCamera.Raise(cameraEventData);
        }

        private void RaiseResetCameraEvent()
        {
            CameraEventData cameraEventData = new CameraEventData();

            this.resetCamera.Raise(cameraEventData);
        }

        private void RaiseResetFormaMapEvent()
        {
            MapEventData mapEventData = new MapEventData();

            this.resetForma.Raise(mapEventData);
        }

        private void RaisePreviewTerraformMapEvent(List<Tile> terraformingTiles, Dictionary<string, int> terraCountMap)
        {
            MapEventData mapEventData = new MapEventData();
            mapEventData.terraformingTiles = terraformingTiles;
            mapEventData.terraCountMap = terraCountMap;

            this.previewTerraform.Raise(mapEventData);
        }

        private void RaiseCommitTerraformMapEvent(List<Tile> terraformingTiles, Dictionary<string, int> terraCountMap)
        {
            MapEventData mapEventData = new MapEventData();
            mapEventData.terraformingTiles = terraformingTiles;
            mapEventData.terraCountMap = terraCountMap;

            this.commitTerraform.Raise(mapEventData);
        }
    }
}
