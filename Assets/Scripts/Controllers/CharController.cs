using System.Collections;
using System.Collections.Generic;
using Tactics.Algorithms;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class CharController : MonoBehaviour
    {
        public MapEvent clickCharacter;
        public MapEvent occupyTileOnMap;
        public MapEvent unoccupyTileOnMap;

        public Character character;

        public void Update()
        {
            if (this.character.isMoving)
            {
                ResolveMovementForCharacter(this.character);
            }
        }

        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        public void OnMouseDown()
        {
            if (!this.character.HasActed())
            {
                ClickCharacter(this.character);
            }
        }

        public void OnPreviewCharacterMovement(CharacterEventData characterEventData)
        {
            if (this.character == characterEventData.character)
            {
                characterEventData.character.SetPreviewCharacterMovementState(
                    characterEventData.tilePath,
                    characterEventData.targetTile
                );
            }
        }

        public void OnPreviewCharacterCombat(CharacterEventData characterEventData)
        {
            if (this.character == characterEventData.character)
            {
                Character previewCharacter = characterEventData.character;
                previewCharacter.SetSelectedCombatTile(characterEventData.targetTile);
            }
        }

        public void OnClearCharacterCombat(CharacterEventData characterEventData)
        {
            if (this.character == characterEventData.character)
            {
                Character characterToClear = characterEventData.character;
                characterToClear.ResetSelectedCombatTile();
            }
        }

        public void OnUndoCharacterMovement(CharacterEventData characterEventData)
        {
            if (this.character == characterEventData.character)
            {
                Character characterToUndo = characterEventData.character;
                characterToUndo.SetUndoCharacterMovementState();
            }
        }

        public void OnCompleteCharacterAction(CharacterEventData characterEventData)
        {
            if (this.character == characterEventData.character)
            {
                Character completedCharacter = characterEventData.character;
                completedCharacter.SetActed();
                completedCharacter.SetOriginTileToCurrentTile();
                completedCharacter.ResetPreviousTile();
                completedCharacter.ResetPreviewState();
                completedCharacter.SetSpriteMaterialGreyscale();
            }
        }


        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void ClickCharacter(Character character)
        {
            RaiseClickCharacterMapEvent(character);
        }

        private void ResolveMovementForCharacter(Character character)
        {

            Vector3 targetPathTilePosition = character.pathTargetTile.GetTransformPosition();

            Vector3 targetTilePosition = character.targetTile.GetTransformPosition();

            // First move to the current targetPathTile
            character.MoveTowards(targetPathTilePosition);

            bool hasReachedTargetPathPosition = character.HasReachedTargetPosition(targetPathTilePosition);
            bool hasReachedTargetPositon = character.HasReachedTargetPosition(targetTilePosition);

            // Check if we've reached the current target path but not the end goal
            if (hasReachedTargetPathPosition && !hasReachedTargetPositon)
            {
                // Get the next target tile from the tilePath
                character.EnqueueNextPathTargetTile();
            }
            else if (hasReachedTargetPositon)
            {
                // We've reached the overall target tile
                character.FinishMovementToTile();
            }
        }


        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/
        private void RaiseClickCharacterMapEvent(Character character)
        {
            MapEventData clickCharacterData = new MapEventData();
            clickCharacterData.character = character;

            this.clickCharacter.Raise(clickCharacterData);
        }

        private void RaiseOccupyTileMapEvent(Character character, Tile tile)
        {
            MapEventData mapEventData = new MapEventData();
            mapEventData.character = character;
            mapEventData.tile = tile;

            this.occupyTileOnMap.Raise(mapEventData);
        }

        private void RaiseUnoccupyTileMapEvent(Tile tile)
        {
            MapEventData mapEventData = new MapEventData();
            mapEventData.tile = tile;

            this.unoccupyTileOnMap.Raise(mapEventData);
        }

    }

}
