using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Algorithms
{
    public class BFS
    {
        public static List<Tile> GetAllMovementTilesForCharacter(Character character, Map map)
        {
            List<Tile> movementTiles = new List<Tile>();
            Dictionary<string, int> visitedTilesMoveCountMap = new Dictionary<string, int>();
            Queue<Tile> tileQueue = new Queue<Tile>();
            int movementAdjacencyRange = 1;
            Tile startTile = character.currentTile;

            visitedTilesMoveCountMap.Add(startTile.GetCoordinates(), 0);
            movementTiles.Add(startTile);
            tileQueue.Enqueue(character.currentTile);

            while (tileQueue.Count > 0)
            {
                Tile tile = tileQueue.Dequeue();
                int tileMoveCount = visitedTilesMoveCountMap[tile.GetCoordinates()];

                if (tileMoveCount < character.movementRange)
                {
                    List<Tile> adjacentTiles = map.GetAdjacentTilesForTile(tile, movementAdjacencyRange);
                    List<Tile> adjacentMovementTiles = map.FilterMovementAdjacentTilesForCharacter(adjacentTiles, character);

                    foreach (Tile adjacentMovementTile in adjacentMovementTiles)
                    {
                        bool hasVisitedTile = visitedTilesMoveCountMap.ContainsKey(adjacentMovementTile.GetCoordinates());
                        if (!hasVisitedTile)
                        {
                            int moveCountForTile = tileMoveCount + 1;
                            visitedTilesMoveCountMap.Add(adjacentMovementTile.GetCoordinates(), moveCountForTile);

                            bool withinMoveRange = moveCountForTile <= character.movementRange;
                            if (withinMoveRange)
                            {
                                movementTiles.Add(adjacentMovementTile);
                            }
                            tileQueue.Enqueue(adjacentMovementTile);
                        }
                    }
                }
            }

            return movementTiles;
        }

        public static int GetDistanceFromAToB(Tile startTile, Tile endTile, Character character, Map map)
        {
            int distance = 0;
            Path tilePath = AStar.GetTilePathFromAToBForCharacter(startTile, endTile, map, character);
            
            PathNode current = tilePath.GetTail();
            if (current != null)
            {
                while (current.next != null)
                {
                    distance += current.next.tile.moveCost;
                    current = current.next;
                }
            }
            return distance;
        }

        public static List<Tile> FilterMovementTilesOutsideOfCharacterMovementRange(List<Tile> movmentTiles, Character character, Map map)
        {
            List<Tile> validMovementTiles = new List<Tile>();

            foreach (Tile movementTile in movmentTiles)
            {
                int tileDistance = BFS.GetDistanceFromAToB(character.originTile, movementTile, character, map);
                if (tileDistance <= character.movementRange)
                {
                    validMovementTiles.Add(movementTile);
                }
            }

            return validMovementTiles;
        }

        public static HashSet<Tile> GetCombatTilesForCharacterMovementTiles(Character character, List<Tile> movementTiles, Map map)
        {
            Dictionary<string, Tile> combatTilesMap = new Dictionary<string, Tile>();
            HashSet<Tile> combatTiles = new HashSet<Tile>();
            int combatAdjacencyRange = character.attackRange;

            foreach (Tile movementTile in movementTiles)
            {
                List<Tile> adjacentCombatTiles = map.GetAdjacentTilesForTile(movementTile, combatAdjacencyRange);

                foreach (Tile adjacentCombatTile in adjacentCombatTiles)
                {
                    if (!combatTiles.Contains(adjacentCombatTile))
                    {
                        combatTiles.Add(adjacentCombatTile);
                    }

                    if (!adjacentCombatTile.associatedMovementTiles.Contains(movementTile))
                    {
                        adjacentCombatTile.associatedMovementTiles.Add(movementTile);
                    }
                }
            }

            return combatTiles;
        }

    }

}

