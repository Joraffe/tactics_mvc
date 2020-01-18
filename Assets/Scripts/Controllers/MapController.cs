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

        public TileEvent setMovementTile;
        public TileEvent setCombatTile;
        public TileEvent setArrangementTile;
        public TileEvent clearArrangementTile;
        public TileEvent clearTile;
        public TileEvent occupyTile;
        public TileEvent unoccupyTile;
        public TileEvent previewPathOverlay;
        public TileEvent clearPathOverlay;
        public TileEvent previewSelectOverlay;
        public TileEvent clearSelectOverlay;
        public TileEvent showDangerOverlay;
        public TileEvent clearDangerOverlay;

        public CharacterEvent previewCharacterMovement;
        public CharacterEvent previewCharacterCombat;
        public CharacterEvent clearCharacterCombat;
        public CharacterEvent undoCharacterMovement;

        public UIEvent showCharacterUI;
        public UIEvent hideCharacterUI;

        public TeamEvent completeTeamMemberAction;

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
                // TODO: change this into a proper CameraEvent
                CameraController.instance.followTransform = mapEventData.character.transform;
            }
            // if we click the same character and they're previewing...
            else if (this.map.currentSelectedCharacter == mapEventData.character
                     && this.map.currentSelectedCharacter.isPreviewing)
            {
                ClearAllTiles();
                this.map.ClearCurrentSelectedCharacter();
                CompleteTeamMemberAction(mapEventData.character);
            }
            // if we click the same character and they're not previewing...
            else if (this.map.currentSelectedCharacter == mapEventData.character
                     && !this.map.currentSelectedCharacter.isPreviewing)
            {
                ClearAllTiles();
                ResetCurrentSelectedCharacter();
                HideCharacterUI();
                // TODO: change this into a proper CameraEvent
                CameraController.instance.followTransform = null;
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
            ClearAllTiles();
            ResetCurrentSelectedCharacter();
            HideCharacterUI();
            // TODO: change this into a proper CameraEvent
            CameraController.instance.followTransform = null;
        }

        public void OnTileSelected(MapEventData mapEventData)
        {
            Tile selectedTile = mapEventData.tile;
            if (selectedTile.activeState == TileInteractType.Movement)
            {
                PreviewSelectMovementTile(selectedTile);
            }
            else if (selectedTile.activeState == TileInteractType.Combat)
            {
                PreviewSelectCombatTile(selectedTile);
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
                ClearAllTiles();
                ResetCurrentSelectedCharacter();
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
            HashSet<Tile> combatTiles = BFS.GetCombatTilesForCharacterMovementTiles(character, validMovementTiles, this.map);
            foreach (Tile combatTile in combatTiles)
            {
                if (combatTile.isCombatableForCharacter(character))
                {
                    RaiseCombatTileEvent(combatTile);
                }
            }

            foreach (Tile movementTile in validMovementTiles)
            {
                if (movementTile.isMoveableForCharacter(character))
                {
                    RaiseMovementTileEvent(movementTile);
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
                RaisePreviewPathOverlayTileEvent(currentTile, pathOverlayImage);
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
                RaiseClearPathOverlayTileEvent(currentTile);
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
            RaisePreviewSelectOverlayTileEvent(tile, selectOverlayType);
        }

        private void ClearPreviousSelectOverlay(Tile tile)
        {
            RaiseClearSelectOverlayTileEvent(tile);
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
            RaiseSetArrangementTileEvent(tile);
        }

        private void HidePlayerArrangementTile(Tile tile)
        {
            RaiseClearArrangementTileEvent(tile);
        }

        private void ClearDangerOverlayForAllTiles()
        {
            foreach (Tile tile in this.map.tiles)
            {
                RaiseClearDangerOverlayTileEvent(tile);
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
                RaiseShowDangerOverlayTileEvent(enemyTile, dangerOverlayImage);
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


        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/
        private void RaiseMovementTileEvent(Tile tile)
        {
            TileEventData setMovementTileData = new TileEventData();
            setMovementTileData.tile = tile;

            this.setMovementTile.Raise(setMovementTileData);
        }

        private void RaiseCombatTileEvent(Tile tile)
        {
            TileEventData setCombatTileData = new TileEventData();
            setCombatTileData.tile = tile;

            this.setCombatTile.Raise(setCombatTileData);
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

        private void RaisePreviewPathOverlayTileEvent(Tile tile, string pathOverlayImage)
        {
            TileEventData previewPathOverlayData = new TileEventData();
            previewPathOverlayData.tile = tile;
            previewPathOverlayData.pathOverlayImage = pathOverlayImage;

            this.previewPathOverlay.Raise(previewPathOverlayData);
        }

        private void RaiseClearPathOverlayTileEvent(Tile tile)
        {
            TileEventData clearPathOverlayData = new TileEventData();
            clearPathOverlayData.tile = tile;

            this.clearPathOverlay.Raise(clearPathOverlayData);
        }

        private void RaisePreviewSelectOverlayTileEvent(Tile tile, string selectOverlayType)
        {
            TileEventData previewSelectOverlayData = new TileEventData();
            previewSelectOverlayData.tile = tile;
            previewSelectOverlayData.selectOverlayType = selectOverlayType;

            this.previewSelectOverlay.Raise(previewSelectOverlayData);
        }

        private void RaiseClearSelectOverlayTileEvent(Tile tile)
        {
            TileEventData clearSelectOverlayData = new TileEventData();
            clearSelectOverlayData.tile = tile;

            this.clearSelectOverlay.Raise(clearSelectOverlayData);
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

        private void RaiseSetArrangementTileEvent(Tile tile)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;

            this.setArrangementTile.Raise(tileEventData);
        }

        private void RaiseClearArrangementTileEvent(Tile tile)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;

            this.clearArrangementTile.Raise(tileEventData);
        }

        private void RaiseShowDangerOverlayTileEvent(Tile tile, string dangerOverlayImage)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;
            tileEventData.dangerOverlayImage = dangerOverlayImage;

            this.showDangerOverlay.Raise(tileEventData);
        }

        private void RaiseClearDangerOverlayTileEvent(Tile tile)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;

            this.clearDangerOverlay.Raise(tileEventData);
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

    }
}
