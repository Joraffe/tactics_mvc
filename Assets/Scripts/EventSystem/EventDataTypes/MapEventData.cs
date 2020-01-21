using System.Collections;
using System.Collections.Generic;
using Tactics.Models;


namespace Tactics.Events
{

    public class MapEventData
    {
        public Tile tile;

        public Character character;

        public Team team;

        public Forma forma;
        public List<FormaTile> formaTiles;
    }
}
