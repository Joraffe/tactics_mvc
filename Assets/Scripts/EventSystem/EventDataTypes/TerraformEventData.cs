using System.Collections;
using System.Collections.Generic;
using Tactics.Models;


namespace Tactics.Events
{
    public class TerraformEventData
    {
        public Dictionary<string, int> terraCountMap;
        public Dictionary<string, int> postTerraformTerraCountMap;
        public Dictionary<Tile, Dictionary<string, int>> auraCountMap;
        public Dictionary<Tile, Dictionary<string, int>> postTerraformAuraCountMap;
    }
}
