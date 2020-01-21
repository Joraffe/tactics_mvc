using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using UnityEngine;


namespace Tactics.SetUp
{
    public class TerraSetUp: MonoBehaviour
    {
        public void SetDesertTerra(Terra terra)
        {
            terra.SetTerraType(TerraTypes.Desert);
        }

        public void SetSwampTerra(Terra terra)
        {
            terra.SetTerraType(TerraTypes.Swamp);
        }

        public void SetForestTerra(Terra terra)
        {
            terra.SetTerraType(TerraTypes.Forest);
        }

        public void SetVolcanicTerra(Terra terra)
        {
            terra.SetTerraType(TerraTypes.Volcanic);
        }

        public void SetOceanicTerra(Terra terra)
        {
            terra.SetTerraType(TerraTypes.Oceanic);
        }

        public void SetIndustrialTerra(Terra terra)
        {
            terra.SetTerraType(TerraTypes.Industrial);
        }

        public void SetNeutralTerra(Terra terra)
        {
            terra.SetTerraType(TerraTypes.Neutral);
        }

        public void SetUpDevTerra(int yPosition, Terra terra)
        {
            if (yPosition == 0)
            {
                this.SetDesertTerra(terra);
            }
            else if (yPosition == 1)
            {
                this.SetSwampTerra(terra);
            }
            else if (yPosition == 2)
            {
                this.SetForestTerra(terra);
            }
            else if (yPosition == 3)
            {
                this.SetVolcanicTerra(terra);
            }
            else if (yPosition == 4)
            {
                this.SetOceanicTerra(terra);
            }
            else if (yPosition == 5)
            {
                this.SetIndustrialTerra(terra);
            }
        }
    }

}

