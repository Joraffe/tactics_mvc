using Tactics.Algorithms;
using Tactics.Models;


namespace Tactics.Events
{
    public class CharacterEventData
    {
        public Character character;
        public Tile targetTile;

        public Path tilePath;
    }
}
