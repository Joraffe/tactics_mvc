using System;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{

    public class TileInteractType
    {   
        public static string Movement = "movement";
        public static string Combat = "combat";
        public static string Arrangement = "arrangement";
    }

    public class TileTerrainType
    {
        public static string Normal = "Tile";
        public static string Rock = "Rock";
        public static string Tree = "Tree";
        public static string Water = "Water";

    }

    public class Tile : MonoBehaviour
    {
        public string terrainType;

        public int moveCost;

        public int XPosition;  // X position on the map
        public int YPosition;  // Y position on the map

        public Character occupant;  // What is currently occupying this tile
        public PathOverlay pathOverlay;
        public SelectOverlay selectOverlay;

        public DangerOverlay dangerOverlay;
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

            if (this.terrainType == TileTerrainType.Rock)
            {
                return false;
            }

            // This is a "normal" tile
            return true;
        }

        public bool isCombatableTerrain(Character character)
        {
            if (this.terrainType == TileTerrainType.Rock)
            {
                return false;
            }

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
            this.ChangeTileColor(Color.blue);
            this.active = true;
            this.activeState = TileInteractType.Movement;
        }

        public void SetActiveCombat()
        {
            this.ChangeTileColor(Color.red);
            this.active = true;
            this.activeState = TileInteractType.Combat;
        }

        public void SetActiveArrangement()
        {
            this.ChangeTileColor(Color.green);
            this.active = true;
            this.activeState = TileInteractType.Arrangement;
        }

        public void ClearActiveArrangement()
        {
            this.ChangeTileColor(Color.white);
            this.active = false;
            this.activeState = "";
        }

        public void ClearTile()
        {
            this.ChangeTileColor(Color.white);
            this.ClearPathOverlayImage();
            this.ClearSelectOverlayImage();
            this.ClearAssociatedMovementTiles();
            this.active = false;
            this.activeState = "";
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

    }
}
