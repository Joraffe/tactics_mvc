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
        public TerraformOverlay terraformOverlay;
        public Dictionary<string, int> terraCountMap;
        public Dictionary<string, int> postTerraformTerraCountMap;
    }
}
