using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using Tactics.Views;
using UnityEngine;


namespace Tactics.Controllers
{
    public class MapTeamController : MonoBehaviour
    {
        public TileEvent addTeamAura;
        public MapTeamView mapTeamView;

        /*-------------------------------------------------
        *              Event Handlers
        --------------------------------------------------*/
        public void OnSetActiveTeam(MapEventData mapEventData)
        {
            this.SetActiveTeam(mapEventData.team);
        }

        public void OnAddTeamAura(MapEventData mapEventData)
        {
            this.AddTeamAuraToMap(mapEventData.team);
        }


        /*-------------------------------------------------
        *                 Helpers
        --------------------------------------------------*/
        private void SetActiveTeam(Team team)
        {
            this.mapTeamView.SetActiveTeam(team);
        }

        private void AddTeamAuraToMap(Team team)
        {
            foreach (Tile tile in this.mapTeamView.map.tiles)
            {
                this.RaiseAddTeamAuraTileEvent(tile, team);
            }

            this.mapTeamView.InitTeamScore(team.teamName);
        }


        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
        private void RaiseAddTeamAuraTileEvent(Tile tile, Team team)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;
            tileEventData.team = team;

            this.addTeamAura.Raise(tileEventData);
        }
    }
}


