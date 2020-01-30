using System.Collections;
using System.Collections.Generic;
using Tactics.Models;


namespace Tactics.Events
{
    public class EventData
    {
        Dictionary<string, int> terraCountMap;
        Dictionary<string, int> postTerraformTerraCountMap;
        Dictionary<Tile, Dictionary<string, int>> auraCountMap;
        Dictionary<Tile, Dictionary<string, int>> postTerraformAuraCountMap;
    }
}
