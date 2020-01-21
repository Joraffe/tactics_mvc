using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using UnityEngine;


namespace Tactics.SetUp
{
    public class TerraSetUp: MonoBehaviour
    {
        public Sprite DesertSprite;
        public Sprite SwampSprite;
        public Sprite ForestSprite;
        public Sprite VolcanicSprite;
        public Sprite OceanicSprite;
        public Sprite IndustrialSprite;
        public Sprite NeutralSprite;

        public void SetDesertTerra(Terra terra)
        {
            terra.sprite = this.DesertSprite;
            terra.type = TerraTypes.Desert;
        }

        public void SetSwampTerra(Terra terra)
        {
            terra.sprite = this.SwampSprite;
            terra.type = TerraTypes.Swamp;
        }

        public void SetForestTerra(Terra terra)
        {
            terra.sprite = this.ForestSprite;
            terra.type = TerraTypes.Forest;
        }

        public void SetVolcanicTerra(Terra terra)
        {
            terra.sprite = this.VolcanicSprite;
            terra.type = TerraTypes.Volcanic;
        }

        public void SetOceanicTerra(Terra terra)
        {
            terra.sprite = this.OceanicSprite;
            terra.type = TerraTypes.Oceanic;
        }

        public void SetIndustrialTerra(Terra terra)
        {
            terra.sprite = this.IndustrialSprite;
            terra.type = TerraTypes.Industrial;
        }

        public void SetNeutralTerra(Terra terra)
        {
            terra.sprite = this.NeutralSprite;
            terra.type = TerraTypes.Neutral;
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

