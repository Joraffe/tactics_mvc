using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class TileTerraformController : MonoBehaviour
    {
        public Tile tile;

        /*-------------------------------------------------
        *              Event Handlers
        --------------------------------------------------*/
        public void OnSetPreviewTerraformType(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                SetPreviewTerraformTypeForTile(
                    tileEventData.tile,
                    tileEventData.previewTerraformType
                );
            }
        }

        public void OnClearPreviewTerraformType(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                ClearPreviewTerraformTypeForTile(tileEventData.tile);
            }
        }

        public void OnTerraformTile(TileEventData tileEventData)
        {
            if (this.tile == tileEventData.tile)
            {
                TerraformTile(tileEventData.tile, tileEventData.terraType);
            }
        }


        /*-------------------------------------------------
        *                 Helpers
        --------------------------------------------------*/
        private void SetPreviewTerraformTypeForTile(Tile tile, string previewTerraformType)
        {
            tile.SetPreviewTerraformType(previewTerraformType);
        }

        private void ClearPreviewTerraformTypeForTile(Tile tile)
        {
            tile.ClearPreviewTerraformType();
        }

        private void TerraformTile(Tile tile, string terraType)
        {
            tile.SetTerraType(terraType);
            tile.SetSprite(tile.GetTerra().GetCurrentSprite());
        }


        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
    }

}

