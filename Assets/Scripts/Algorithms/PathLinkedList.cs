using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using UnityEngine;



namespace Tactics.Algorithms
{

    public class PathNode {

        private static string ArrowPath = "arrow";
        private static string StartPath = "start";
        private static string MidPath = "mid";
        private Dictionary<string, string> ArrowPathDirections = new Dictionary<string, string>{
            // coordinates are (prevXDiff, prevYDiff)
            { "(0, -1)",  "down" },
            { "(0, 1)", "up" },
            { "(-1, 0)", "left" },
            { "(1, 0)", "right" }
        };

        private Dictionary<string, string> StartPathDirections = new Dictionary<string, string>{
            // coordinates are (nextXDiff, nextYDiff)
            { "(0, -1)",  "up" },
            { "(0, 1)", "down" },
            { "(-1, 0)", "right" },
            { "(1, 0)", "left" }
        };

        private Dictionary<string, string> MidPathDirections = new Dictionary<string, string>{
            // coordinates are [(prevXDiff, prevYDiff), (nextXDiff, nextYDiff)]
            { "[(0, 1), (0, -1)]", "vertical" },
            { "[(0, -1), (0, 1)]", "vertical" },
            { "[(1, 0), (-1, 0)]", "horizontal" },
            { "[(-1, 0), (1, 0)]", "horizontal" },

            { "[(0, 1), (-1, 0)]", "down_right" },
            { "[(-1, 0), (0, 1)]", "down_right" },
            { "[(0, 1), (1, 0)]", "down_left" },
            { "[(1, 0), (0, 1)]", "down_left" },
            { "[(0, -1), (-1, 0)]", "up_right" },
            { "[(-1, 0), (0, -1)]", "up_right" },
            { "[(1, 0), (0, -1)]", "up_left" },
            { "[(0, -1), (1, 0)]", "up_left" }
        };
        public PathNode prev;
        public PathNode next;
        public Tile tile;

        public string GetImage()
        {
            string imageType = GetImageType();
            string imageDirection = GetImageDirection(imageType);

            return $"{imageType}_{imageDirection}";
        }

        private string GetImageType()
        {
            if (next == null) {
                return ArrowPath;
            } else if (prev == null)
            {
                return StartPath;
            } else
            {
                return MidPath;
            }
        }

        private string GetImageDirection(string imageType)
        {
            if (imageType == ArrowPath)
            {
                return GetArrowImageDirection();
            } else if (imageType == StartPath)
            {
                return GetStartImageDirection();
            } else
            {
                return GetMidPathImageDirection();
            }
        }

        private string GetArrowImageDirection()
        {
            string pathDirectionKey = $"({GetXDiffPrev()}, {GetYDiffPrev()})";
            return ArrowPathDirections[pathDirectionKey];
        }

        private string GetStartImageDirection()
        {
            string pathDirectionKey = $"({GetXDiffNext()}, {GetYDiffNext()})";
            return StartPathDirections[pathDirectionKey];
        }

        private string GetMidPathImageDirection()
        {
            string prevPathPart = $"({GetXDiffPrev()}, {GetYDiffPrev()})";
            string nextPathPart = $"({GetXDiffNext()}, {GetYDiffNext()})";
            string pathDirectionKey = $"[{prevPathPart}, {nextPathPart}]";
            return MidPathDirections[pathDirectionKey];
        }

        private int GetXDiffNext()
        {
            return this.tile.XPosition - next.tile.XPosition;
        }

        private int GetXDiffPrev()
        {
            return this.tile.XPosition - prev.tile.XPosition;
        }

        private int GetYDiffNext()
        {
            return this.tile.YPosition - next.tile.YPosition;
        }

        private int GetYDiffPrev()
        {
            return this.tile.YPosition - prev.tile.YPosition;
        }
    }

    public class Path {
        private PathNode head;
        private PathNode tail;

        public bool HasTiles()
        {
            return this.head != null;
        }

        public void AddToTail(Tile tile)
        {
            PathNode newNode = new PathNode();
            newNode.tile = tile;

            // brand new linked list
            if (this.tail == null)
            {
                this.head = this.tail = newNode;
            } else
            {
                PathNode oldTail = this.tail;
                oldTail.prev = newNode;
                newNode.next = oldTail;
                this.tail = newNode;
            }
        }

        public void AddToHead(Tile tile)
        {
            PathNode newNode = new PathNode();
            newNode.tile = tile;

            // branch new linked list
            if (this.head == null)
            {
                this.head = this.tail = newNode;
            } else
            {
                PathNode oldHead = this.head;
                oldHead.next = newNode;
                newNode.prev = oldHead;
                this.head = newNode;
            }
        }

        public Tile RemoveFromTail()
        {
            if (this.head == this.tail)
            {
                PathNode oldTail = this.tail;
                this.head = this.tail = null;
                return oldTail.tile;
            } else {
                PathNode oldTail = this.tail;
                PathNode newTail = oldTail.next;

                newTail.prev = null;
                this.tail = newTail;
                oldTail.next = null;

                return oldTail.tile;
            }
        }

        public Tile RemoveFromHead()
        {
            PathNode oldHead = this.head;
            PathNode newHead = oldHead.prev;

            newHead.next = null;
            this.head = newHead;
            oldHead.prev = null;

            return oldHead.tile;
        }

        public PathNode GetTail()
        {
            return this.tail;
        }

        public int Count()
        {
            int count = 0;
            PathNode current = this.tail;
            while (current != null)
            {
                count++;
                current = current.next;
            }

            return count;
        }
    }

}

