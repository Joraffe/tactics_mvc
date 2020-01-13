using System.Collections;
using System.Collections.Generic;
using Tactics.Models;


namespace Tactics.Events
{

    public class TileEventData
    {
        public Tile tile;
        public Character character;

        public string pathOverlayImage;
        public string selectOverlayType;
        public string dangerOverlayImage;
    }
}
