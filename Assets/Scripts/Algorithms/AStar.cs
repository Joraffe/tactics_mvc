using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tactics.Models;
using UnityEngine;

namespace Tactics.Algorithms
{

    public class Location
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Location Parent;
    }

    public class AStar {
        public static Path GetTilePathFromAToBForCharacter(Tile startTile, Tile endTile, Map map, Character character)
        {
            Path tilePath = new Path();

            Location startLocation = new Location{X = startTile.XPosition, Y = startTile.YPosition};
            Location endLocation = new Location{X = endTile.XPosition, Y = endTile.YPosition};
            Stack<Location> locationStack = AStar.GetPathStackFromAToB(startLocation, endLocation, map, character);

            while (locationStack.Count > 0) {
                Location location = locationStack.Pop();
                tilePath.AddToHead(map.tiles[location.X, location.Y]);
            }

            return tilePath;
        }

        public static Stack<Location> GetPathStackFromAToB(Location start, Location end, Map map, Character character)
        {
            Location current = null;
            List<Location> openList = new List<Location>();
            List<Location> closedList = new List<Location>();
            int gScore = 0;

            openList.Add(start);

            while (openList.Count > 0)
            {
                // Get the Location with the lowest F score
                int lowest = openList.Min(location => location.F);
                current = openList.First(location => location.F == lowest);

                // Add this Location to the closed list
                closedList.Add(current);

                // Remove this Location from the open list
                openList.Remove(current);

                // If we've added the desitination to the closed list, we've found a path
                if (closedList.FirstOrDefault(l => l.X == end.X && l.Y == end.Y) != null)
                {
                    break;
                }

                Tile currentTile = map.tiles[current.X, current.Y];
                List<Tile> adjacentTiles = map.GetAdjacentTilesForTile(currentTile, 1);
                List<Tile> adjacentWalkableTiles = map.FilterMovementAdjacentTilesForCharacter(adjacentTiles, character);
                List<Location> adjacentLocations = AStar.MapTilesToLocations(adjacentWalkableTiles);
                gScore = AStar.ComputeGScore(gScore, currentTile);

                foreach(Location adjacentLocation in adjacentLocations)
                {
                    // If this adjacent location is already in the closed list, ignore it
                    if (closedList.FirstOrDefault(l => l.X == adjacentLocation.X && l.Y == adjacentLocation.Y) != null)
                    {
                        continue;
                    }

                    // If this adjacent tile is not in the open list
                    if (openList.FirstOrDefault(l => l.X == adjacentLocation.X && l.Y == adjacentLocation.Y) == null)
                    {
                        // compute its score and set the parent
                        adjacentLocation.G = AStar.ComputeGScore(gScore, map.tiles[adjacentLocation.X, adjacentLocation.Y]);
                        adjacentLocation.H = AStar.ComputeHScore(adjacentLocation.X, adjacentLocation.Y, end.X, end.Y);
                        adjacentLocation.F = adjacentLocation.G + adjacentLocation.H;
                        adjacentLocation.Parent = current;

                        // and add it to the open list
                        openList.Insert(0, adjacentLocation);
                    }
                    else
                    {
                        // Test if using the current G score makes the adjacent location's F score
                        // lower; if yes then update the parent because it means it's a better path
                        if (gScore + adjacentLocation.H < adjacentLocation.F)
                        {
                            adjacentLocation.G = AStar.ComputeGScore(gScore, map.tiles[adjacentLocation.X, adjacentLocation.Y]);
                            adjacentLocation.F = adjacentLocation.G + adjacentLocation.H;
                            adjacentLocation.Parent = current;
                        }
                    }
                }
            }

            Stack<Location> path = new Stack<Location>();

            while (current != null)
            {
                path.Push(current);
                current = current.Parent;
            }

            return path;
        }

        public static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }

        public static int ComputeGScore(int gScoreSoFar, Tile tile)
        {
            return gScoreSoFar + tile.moveCost;
        }

        public static List<Location> MapTilesToLocations(List<Tile> tiles)
        {
            List<Location> locations = new List<Location>();
            foreach (Tile tile in tiles)
            {
                Location location = new Location();
                location.X = tile.XPosition;
                location.Y = tile.YPosition;
                locations.Add(location);
            }

            return locations;
        }

    }
}


