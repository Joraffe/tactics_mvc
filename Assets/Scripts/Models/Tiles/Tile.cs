﻿using System;
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
            this.ClearPathOverlayImage();
            this.ClearSelectOverlayImage();
            this.ClearAssociatedMovementTiles();
            this.ClearMoveOverlayImage();
            this.ClearTerraformOverlayImage();
            this.ClearActiveState();
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

        public void SetPathOverlayImage(string pathOverlayImageKey)
        {
            this.pathOverlay.SetSprite(pathOverlayImageKey);
        }

        public void ClearPathOverlayImage()
        {
            if (this.pathOverlay)
            {
                this.pathOverlay.ClearSprite();
            }
        }

        public void SetSelectOverlayImage(string selectOverlayType)
        {
            this.selectOverlay.SetSelectedSprite(selectOverlayType);
        }

        public void ClearSelectOverlayImage()
        {
            if (this.selectOverlay)
            {
                this.selectOverlay.ClearSprite();
            }
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

        public void SetDangerOverlayImage(string dangerOverlayImageKey)
        {
            this.dangerOverlay.SetSprite(dangerOverlayImageKey);
        }

        public void ClearDangerOverlayImage()
        {
            this.dangerOverlay.ClearSprite();
        }

        // public void SetActionOverlayImage(string actionOverlayImageKey)
        // {
        //     this.actionOverlay.SetSprite(actionOverlayImageKey);
        // }

        // public void ClearActionOverlayImage()
        // {
        //     this.actionOverlay.ClearSprite();
        // }
        public void SetMoveOverlayImage(string overlayImageKey)
        {
            this.moveOverlay.SetSprite(overlayImageKey);
        }

        public void ClearMoveOverlayImage()
        {
            this.moveOverlay.ClearSprite();
        }

        public void SetTerraformOverlayImage(string terraformOverlayImageKey)
        {
            this.terraformOverlay.SetSprite(terraformOverlayImageKey);
        }

        public void ClearTerraformOverlayImage()
        {
            this.terraformOverlay.ClearSprite();
        }

        public void ShowOverlay(string overlayImageKey, string overlayType)
        {
            switch (overlayType)
            {
                case TileOverlayTypes.Path:
                    SetPathOverlayImage(overlayImageKey);
                    break;
                case TileOverlayTypes.Select:
                    SetSelectOverlayImage(overlayImageKey);
                    break;
                case TileOverlayTypes.Danger:
                    SetDangerOverlayImage(overlayImageKey);
                    break;
                case TileOverlayTypes.Move:
                    SetMoveOverlayImage(overlayImageKey);
                    break;
                case TileOverlayTypes.Arrange:
                    Debug.Log("revisit Arrange.ShowOverlay pls");
                    // SetActionOverlayImage(overlayImageKey);
                    break;
                case TileOverlayTypes.Terraform:
                    SetTerraformOverlayImage(overlayImageKey);
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
                    ClearPathOverlayImage();
                    break;
                case TileOverlayTypes.Select:
                    ClearSelectOverlayImage();
                    break;
                case TileOverlayTypes.Danger:
                    ClearDangerOverlayImage();
                    break;
                case TileOverlayTypes.Move:
                    ClearMoveOverlayImage();
                    break;
                case TileOverlayTypes.Arrange:
                    Debug.Log("revisit Arrange.ClearOverlay pls");
                    // ClearActionOverlayImage();
                    break;
                case TileOverlayTypes.Terraform:
                    ClearTerraformOverlayImage();
                    break;

                default:
                    return;
            }
        }

    }
}
