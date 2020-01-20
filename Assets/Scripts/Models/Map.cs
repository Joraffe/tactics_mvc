﻿using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class Map : MonoBehaviour
    {
        public static int xMinSize = 0;
        public static int xMaxSize = 9;
        public static int yMinSize = 0;
        public static int yMaxSize = 6;
        public Tile[,] tiles = new Tile[xMaxSize,yMaxSize];
        public Character currentSelectedCharacter;
        public Forma currentSelectedForma;
        public List<Tile> terraformingTiles = new List<Tile>();
        public List<Tile> playerStartTiles = new List<Tile>();
        public List<Tile> enemyStartTiles = new List<Tile>();
        public SpriteRenderer spriteRenderer;

        /*-------------------------------------------------
        *                     Setters
        --------------------------------------------------*/
        public void SetCurrentSelectedCharacter(Character character)
        {
            this.currentSelectedCharacter = character;
        }

        public void ClearCurrentSelectedCharacter()
        {
            this.currentSelectedCharacter = null;
        }

        public void AddTerraformingTile(Tile tile)
        {
            this.terraformingTiles.Add(tile);
        }

        public void ClearTerraformingTiles()
        {
            this.terraformingTiles.Clear();
        }

        public void SetCurrentSelectedForma(Forma forma)
        {
            this.currentSelectedForma = forma;
        }

        public void ClearCurrentSelectedForma()
        {
            this.currentSelectedForma = null;
        }

        /*-------------------------------------------------
        *                     Getters
        --------------------------------------------------*/
        public Character GetCurrentSelectedCharacter()
        {
            return this.currentSelectedCharacter;
        }

        public List<Tile> GetAdjacentTilesForTile(Tile tile, int adjacencyRange)
        {
            List<Tile> adjacentTiles = new List<Tile>();

            // Left Tile: [(x - 1), y]
            if (isLeftAdjacentTileInMapForTile(tile, adjacencyRange)) {
                adjacentTiles.Add(GetLeftAdjacentTileForTile(tile, adjacencyRange));
            }

            // Right Tile [(x + 1), y]
            if (isRightAdjacentTileInMapForTile(tile, adjacencyRange)) {
                adjacentTiles.Add(GetRightAdjacentTileForTile(tile, adjacencyRange));
            }

            // Top Tile  [x, (y + 1)]
            if (isUpAdjacentTileInMapForTile(tile, adjacencyRange)) {
                adjacentTiles.Add(GetUpAdjacentTileForTile(tile, adjacencyRange));
            }

            // Bottom Tile [x, (y - 1)]
            if (isDownAdjacentTileInMapForTile(tile, adjacencyRange)) {
                adjacentTiles.Add(GetDownAdjacentTileForTile(tile, adjacencyRange));
            }

            return adjacentTiles;
        }

        public Tile GetLeftAdjacentTileForTile(Tile tile, int adjacencyRange)
        {
            return this.tiles[tile.XPosition - adjacencyRange, tile.YPosition];
        }

        public Tile GetRightAdjacentTileForTile(Tile tile, int adjacencyRange)
        {
            return this.tiles[tile.XPosition + adjacencyRange, tile.YPosition];
        }

        public Tile GetUpAdjacentTileForTile(Tile tile, int adjacencyRange)
        {
            return this.tiles[tile.XPosition, tile.YPosition + adjacencyRange];
        }

        public Tile GetDownAdjacentTileForTile(Tile tile, int adjacencyRange)
        {
            return this.tiles[tile.XPosition, tile.YPosition - adjacencyRange];
        }

        public List<Tile> GetPlayerStartTiles()
        {
            return this.playerStartTiles;
        }

        public string GetDangerOverlayImageForTile(Tile tile)
        {
            string right = "0";
            string left = "0";
            string up = "0";
            string down = "0";

            if (isRightAdjacentTileInMapForTile(tile, 1))
            {
                right = GetRightAdjacentTileForTile(tile, 1).isShowingDanger ? "1" : "0";
            }

            if (isLeftAdjacentTileInMapForTile(tile, 1))
            {
                left = GetLeftAdjacentTileForTile(tile, 1).isShowingDanger ? "1" : "0";
            }

            if (isUpAdjacentTileInMapForTile(tile, 1))
            {
                up = GetUpAdjacentTileForTile(tile, 1).isShowingDanger ? "1" : "0";
            }

            if (isDownAdjacentTileInMapForTile(tile, 1))
            {
                down = GetDownAdjacentTileForTile(tile, 1).isShowingDanger ? "1" : "0";
            }

            return $"{right}{left}{up}{down}";
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        public List<Tile> FilterMovementAdjacentTilesForCharacter(List<Tile> adjacentTiles, Character character)
        {
            List<Tile> movementAdjacentTiles = new List<Tile>();

            foreach (Tile adjacentTile in adjacentTiles)
            {
                if (!adjacentTile.isOccupiedByOpposition(character) && adjacentTile.isMoveableTerrain(character))
                {
                    movementAdjacentTiles.Add(adjacentTile);
                }
            }

            return movementAdjacentTiles;
        }

        public List<Tile> FilterAlliesAdjacentTilesForCharacter(List<Tile> adjacentTiles, Character character)
        {
            List<Tile> alliesAdjacentTiles = new List<Tile>();

            foreach (Tile adjacentTile in adjacentTiles)
            {
                if (!adjacentTile.isOccupiedByAlly(character))
                {
                    alliesAdjacentTiles.Add(adjacentTile);
                }
            }

            return alliesAdjacentTiles;
        }

        public bool isLeftAdjacentTileInMapForTile(Tile tile, int adjacencyRange)
        {
            return tile.XPosition - adjacencyRange >= xMinSize;
        }

        public bool isRightAdjacentTileInMapForTile(Tile tile, int adjacencyRange)
        {
            return tile.XPosition + adjacencyRange < xMaxSize;
        }

        public bool isUpAdjacentTileInMapForTile(Tile tile, int adjacencyRange)
        {
            return tile.YPosition + adjacencyRange < yMaxSize;
        }

        public bool isDownAdjacentTileInMapForTile(Tile tile, int adjacencyRange)
        {
            return tile.YPosition - adjacencyRange >= yMinSize;
        }

        public bool isCoordinatesWithinMap(int XPosition, int YPosition)
        {
            bool withinXBounds = (xMinSize <= XPosition) && (XPosition <= xMaxSize);
            bool withinYBounds = (yMinSize <= YPosition) && (YPosition <= yMaxSize);

            return withinXBounds && withinYBounds;
        }
    }
}
