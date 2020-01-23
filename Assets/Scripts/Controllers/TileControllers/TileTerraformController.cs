﻿using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using Tactics.Views;
using UnityEngine;


namespace Tactics.Controllers
{
    public class TileTerraformController : MonoBehaviour
    {
        public TileTerraformView tileTerraformView;

        /*-------------------------------------------------
        *              Event Handlers
        --------------------------------------------------*/
        public void OnPreviewTerraform(TileEventData tileEventData)
        {
            if (this.tileTerraformView.tile == tileEventData.tile)
            {
                this.PreviewTerrform(
                    tileEventData.previewTerraformType,
                    tileEventData.previewAuraAmount
                );
            }
        }

        public void OnClearTerraformPreview(TileEventData tileEventData)
        {
            if (this.tileTerraformView.tile == tileEventData.tile)
            {
                this.ClearTerraformPreview();
            }
        }

        public void OnTerraformTile(TileEventData tileEventData)
        {
            if (this.tileTerraformView.tile == tileEventData.tile)
            {
                this.TerraformTile(tileEventData.terraType);
            }
        }


        /*-------------------------------------------------
        *                 Helpers
        --------------------------------------------------*/
        private void PreviewTerrform(string terraType, int auraAmount)
        {
            this.tileTerraformView.SetPreviewTerraformTerraType(terraType);
            this.tileTerraformView.SetPreviewTerraformAuraAmount(auraAmount);
        }

        private void ClearTerraformPreview()
        {
            this.tileTerraformView.SetPreviewTerraformTerraType("");
            this.tileTerraformView.SetPreviewTerraformAuraAmount(0);
        }

        private void TerraformTile(string terraType)
        {
            this.tileTerraformView.tile.SetTerraType(terraType);
            this.tileTerraformView.tile.SetSprite(
                this.tileTerraformView.tile.GetTerra().GetCurrentSprite()
            );
        }


        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
    }

}

