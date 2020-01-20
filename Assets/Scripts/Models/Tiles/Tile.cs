using System;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{

    public class TileInteractType
    {   
        public const string Movement = "movement";
        public const string Combat = "combat";
        public const string Arrangement = "arrangement";
        public const string Terraform = "terraform";
    }

    public class TileOverlayTypes
    {
        public const string Path = "path";
        public const string Select = "select";
        public const string Move = "move";
        public const string Arrange = "arrange";
        public const string Terraform = "terraform";
        public const string Danger = "danger";
        public const string All = "all";
    }

    public class Tile : MonoBehaviour
    {
        public string terrainType;

        public int moveCost = 1;

        public int XPosition;  // X position on the map
        public int YPosition;  // Y position on the map

        public Character occupant;  // What is currently occupying this tile
        public PathOverlay pathOverlay;
        public SelectOverlay selectOverlay;
        public DangerOverlay dangerOverlay;
        public MoveOverlay moveOverlay;
        public TerraformOverlay terraformOverlay;

        public Terra terra;  // Stores What type of tile this is

        public bool active;  // If the tile has some kind of "active" state; i.e.
                             // If the player can select it to move, or selct to target
        public string activeState;  // What type of active state is it (move/combat)

        public bool isShowingDanger = false;

        public HashSet<Tile> associatedMovementTiles = new HashSet<Tile>();  // if active state is a combat tile

        public SpriteRenderer spriteRenderer;
        public Transform tileTransform;

        /*
         * Things you can do with tiles
         *
         * With Character Unselected:
         *   - Select an empty map tile (nothing happens)
         *   - Select a player character on a map tile
         *     - Displays character movement + attack + assist options
         *     - Also display character info at top
         *   - Select an enemy character on a map tile
         *     - Displays enemy attack range
         *     - Also displays enemy character info at top
         *
         * With Character Selected:
         *   - Move to an empty tile within move range
         *   - Attack an enemy within combat range
         *   - Assist an ally within move range + 1
         */

        public string GetCoordinates()
        {
            return $"({this.XPosition}, {this.YPosition})";
        }

        public bool isOccupiedByOpposition(Character character) {
            return this.occupant != null && this.occupant.isOpposition(character);
        }

        public bool isOccupiedByAlly(Character character)
        {
            return (
                this.occupant != null
                && this.occupant.isAlly(character)
                && this.occupant.name != character.name
            );
        }

        public bool isMoveableTerrain(Character character)
        {
            // Note: we pass in character to account for character move type later

            // if (this.terra.type == TerraTypes.Oceanic)
            // {
            //     return false;
            // }

            // This is a "normal" tile
            return true;
        }

        public bool isCombatableTerrain(Character character)
        {
            // if (this.terra.type == TerraTypes.Oceanic)
            // {
            //     return false;
            // }

            return true;
        }

        public bool isWithinMoveDistance(Character character)
        {
            int xDiff = character.originTile.XPosition - this.XPosition;
            int yDiff = character.originTile.YPosition - this.YPosition;

            return (Math.Abs(xDiff) + Math.Abs(yDiff)) <= character.movementRange;
        }

        public bool isMoveableForCharacter(Character character)
        {
            return (
                this.isMoveableTerrain(character)
                && this.isWithinMoveDistance(character)
                && !this.isOccupiedByOpposition(character)

            );
        }

        public bool isCombatableForCharacter(Character character)
        {
            return (
                this.isCombatableTerrain(character)
                || this.isOccupiedByOpposition(character)
            );
        }

        public void ChangeTileColor(Color color)
        {
            this.spriteRenderer.color = color;
        }

        public void SetActiveMovement()
        {
            this.active = true;
            this.activeState = TileInteractType.Movement;
        }

        public void SetActiveCombat()
        {
            this.active = true;
            this.activeState = TileInteractType.Combat;
        }

        public void SetActiveArrangement()
        {
            this.active = true;
            this.activeState = TileInteractType.Arrangement;
        }

        public void SetActiveTerraform()
        {
            this.active = true;
            this.activeState = TileInteractType.Terraform;
        }

        public void ClearActiveState()
        {
            this.active = false;
            this.activeState = "";
        }

        public void ClearActiveArrangement()
        {
            this.active = false;
            this.activeState = "";
        }

        public void ClearTile()
        {
            ClearOverlay(TileOverlayTypes.All);
            ClearAssociatedMovementTiles();
            ClearActiveState();
        }

        public Vector3 GetTransformPosition()
        {
            return this.tileTransform.position;
        }

        public void SetOccupant(Character character)
        {
            this.occupant = character;
        }

        public void ClearOccupant()
        {
            this.occupant = null;
        }

        public void ClearAssociatedMovementTiles()
        {
            this.associatedMovementTiles.Clear();
        }

        public void SetIsShowingDanger()
        {
            this.isShowingDanger = true;
        }

        public void SetIsNotShowingDanger()
        {
            this.isShowingDanger = false;
        }

        public void ShowOverlay(string overlayImageKey, string overlayType)
        {
            switch (overlayType)
            {
                case TileOverlayTypes.Path:
                    this.pathOverlay.SetSprite(overlayImageKey);
                    break;
                case TileOverlayTypes.Select:
                    this.selectOverlay.SetSprite(overlayImageKey);
                    break;
                case TileOverlayTypes.Danger:
                    this.dangerOverlay.SetSprite(overlayImageKey);
                    break;
                case TileOverlayTypes.Move:
                    this.moveOverlay.SetSprite(overlayImageKey);
                    break;
                case TileOverlayTypes.Arrange:
                    Debug.Log("revisit Arrange.ShowOverlay pls");
                    // SetActionOverlayImage(overlayImageKey);
                    break;
                case TileOverlayTypes.Terraform:
                    this.terraformOverlay.SetSprite(overlayImageKey);
                    break;

                default:
                    return;
            }

        }

        public void ClearOverlay(string overlayType)
        {
            switch (overlayType)
            {
                case TileOverlayTypes.Path:
                    this.pathOverlay.ClearSprite();
                    break;
                case TileOverlayTypes.Select:
                    this.selectOverlay.ClearSprite();
                    break;
                case TileOverlayTypes.Danger:
                    this.dangerOverlay.ClearSprite();
                    break;
                case TileOverlayTypes.Move:
                    this.moveOverlay.ClearSprite();
                    break;
                case TileOverlayTypes.Arrange:
                    Debug.Log("revisit Arrange.ClearOverlay pls");
                    // ClearActionOverlayImage();
                    break;
                case TileOverlayTypes.Terraform:
                    this.terraformOverlay.ClearSprite();
                    break;
                case TileOverlayTypes.All:
                    this.pathOverlay.ClearSprite();
                    this.selectOverlay.ClearSprite();
                    this.dangerOverlay.ClearSprite();
                    this.moveOverlay.ClearSprite();
                    this.terraformOverlay.ClearSprite();
                    break;
                default:
                    return;
            }
        }

    }
}
