using System.Collections;
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
                    tileEventData.previewAuraAmount,
                    tileEventData.previewAuraTeam
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
                this.TerraformTile(
                    tileEventData.terraType,
                    tileEventData.teamName,
                    tileEventData.auraAmount
                );
            }
        }

        public void OnAddTeamAura(TileEventData tileEventData)
        {
            if (this.tileTerraformView.tile == tileEventData.tile)
            {
                this.AddTeamAura(tileEventData.team);
            }
        }


        /*-------------------------------------------------
        *                 Helpers
        --------------------------------------------------*/
        private void PreviewTerrform(string terraType, int auraAmount, string teamName)
        {
            this.tileTerraformView.SetPreviewTerraformTerraType(terraType);
            this.tileTerraformView.SetPreviewTerraformAuraAmount(auraAmount);
            this.tileTerraformView.SetPreviewTerraformTeamName(teamName);
        }

        private void ClearTerraformPreview()
        {
            this.tileTerraformView.SetPreviewTerraformTerraType("");
            this.tileTerraformView.SetPreviewTerraformAuraAmount(0);
        }

        private void TerraformTile(string terraType, string teamName, int auraAmount)
        {
            this.tileTerraformView.tile.SetTerraType(terraType);
            this.tileTerraformView.tile.SetSprite(
                this.tileTerraformView.tile.GetTerra().GetCurrentSprite()
            );
            this.tileTerraformView.tile.GetAuraMap().AddToTeamAura(teamName, auraAmount);
        }

        public void AddTeamAura(Team team)
        {
            this.tileTerraformView.tile.GetAuraMap().AddTeamToMap(team);
        }


        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
    }

}

