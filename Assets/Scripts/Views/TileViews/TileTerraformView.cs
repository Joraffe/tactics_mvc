using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Views
{
    public class TileTerraformView : MonoBehaviour
    {
        /*-------------------------------------------------
        *                     Models
        --------------------------------------------------*/
        public Tile tile;
        public AuraMap auraMap;
        public Terra terra;


        /*-------------------------------------------------
        *               Relationship Variables
        --------------------------------------------------*/
        public string previewTerraformTerraType;
        public int previewTerraformAuraAmount;
        public string previewTerraformTeamName;


        /*-------------------------------------------------
        *                     Getters
        --------------------------------------------------*/
        public string GetPreviewTerraformTerraType()
        {
            return this.previewTerraformTerraType;
        }

        public string GetPreviewTerraformTeamName()
        {
            return this.previewTerraformTeamName;
        }

        public int GetPreviewTerraformAuraAmount()
        {
            return this.previewTerraformAuraAmount;
        }


        /*-------------------------------------------------
        *                     Setters
        --------------------------------------------------*/
        public void SetPreviewTerraformTerraType(string terraType)
        {
            this.previewTerraformTerraType = terraType;
        }

        public void SetPreviewTerraformTeamName(string teamName)
        {
            this.previewTerraformTeamName = teamName;
        }

        public void SetPreviewTerraformAuraAmount(int auraAmount)
        {
            this.previewTerraformAuraAmount = auraAmount;
        }
    }
}
