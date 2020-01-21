﻿using System.Collections;
using System.Collections.Generic;
using Tactics.Models;


namespace Tactics.Events
{
    public class TileEventData
    {
        public Tile tile;
        public Character character;
        public string activeState;
        public string previewTerraformType;
        public string overlayImageKey;
        public string overlayType;
        public string terraType;
    }
}
