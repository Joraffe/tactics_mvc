using System.Collections.Generic;
using Tactics.Models;


namespace Tactics.Events
{
    public class UIEventData
    {
        public Footer footer;
        public Button button;
        public Character character;
        public Terra terra;
        public Team team;
        public TerraformOverlay terraformOverlay;
        public Dictionary<string, int> terraCountMap;
        public Dictionary<string, int> postTerraformTerraCountMap;
        public Dictionary<Tile, Dictionary<string, int>> auraCountMap;
        public Dictionary<Tile, Dictionary<string, int>> postTerraformAuraCountMap;
        public Dictionary<string, int> teamAuraScoreMap;
    }
}
