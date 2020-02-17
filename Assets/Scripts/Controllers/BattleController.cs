using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class BattleController : MonoBehaviour
    {
        public MapEvent showPlayerArrangementTiles;
        public MapEvent hidePlayerArragementTiles;
        public MapEvent showPlayerDangerZone;
        public MapEvent hidePlayerDangerZone;
        public MapEvent setActiveTeam;
        public MapEvent addTeamAura;

        public UIEvent setAuraInfoTeamName;
        public UIEvent hideTerraformUI;

        public CharacterEvent setUpCharacter;
        public TileEvent occupyTile;
        public Battle battle;

        /*-------------------------------------------------
        *                 Initialization
        --------------------------------------------------*/
        public void Start()
        {
            this.SetUpBattle();
        }

        private void SetUpBattle()
        {
            this.SetUpTeams();
        }

        private void SetUpTeams()
        {
            Map map = this.battle.GetMap();
            Team cakebin = this.battle.GetTeam("Cakebin");
            Team enemies = this.battle.GetTeam("Los Cattos");

            this.SetUpTeam(cakebin, map);
            this.SetUpTeam(enemies, map);
            this.RaiseSetActiveTeamMapEvent(cakebin);
            this.RaiseAddTeamAuraMapEvent(cakebin);
            this.RaiseAddTeamAuraMapEvent(enemies);
            this.RaiseSetAuraInfoTeamNameUIEvent(cakebin);
            this.RaiseSetAuraInfoTeamNameUIEvent(enemies);
            this.RaiseHideTerraformUI();
        }

        private void SetUpTeam(Team team, Map map)
        {
            foreach (Character member in team.GetMembers())
            {
                Tile startTile = map.GetTile(member.startXPosition, member.startYPosition);
                RaiseSetUpCharacterEvent(member, team, startTile);
                RaiseOccupyTileEvent(member, startTile);
            }
        }

        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        public void OnShowBattleDangerZone(BattleEventData battleEventData)
        {
            ShowMapDangerZoneForPlayer();
        }

        public void OnHideBattleDangerZone(BattleEventData battleEventData)
        {
            HideMapDangerZoneForPlayer();
        }

        public void OnShowBattleArrangeTiles(BattleEventData battleEventData)
        {
            ShowMapArrangeTiles();
        }

        public void OnHideBattleArrangeTiles(BattleEventData battleEventData)
        {
            HideMapArrangeTiles();
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/

        private void ShowMapArrangeTiles()
        {
            RaiseShowPlayerArrangeTilesMapEvent();
        }

        private void HideMapArrangeTiles()
        {
            RaiseHidePlayerArrangeTilesMapEvent();
        }

        private void ShowMapDangerZoneForPlayer()
        {
            RaiseShowDangerZoneMapEvent();
        }

        private void HideMapDangerZoneForPlayer()
        {
            RaiseHideDangerZoneMapEvent();
        }


        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
        private void RaiseShowPlayerArrangeTilesMapEvent()
        {
            MapEventData mapEventData = new MapEventData();

            this.showPlayerArrangementTiles.Raise(mapEventData);
        }

        private void RaiseHidePlayerArrangeTilesMapEvent()
        {
            MapEventData mapEventData = new MapEventData();

            this.hidePlayerArragementTiles.Raise(mapEventData);
        }

        private void RaiseShowDangerZoneMapEvent()
        {
            MapEventData mapEventData = new MapEventData();
            mapEventData.team = this.battle.GetTeam("Los Cattos");

            this.showPlayerDangerZone.Raise(mapEventData);
        }

        private void RaiseHideDangerZoneMapEvent()
        {
            MapEventData mapEventData = new MapEventData();

            this.hidePlayerDangerZone.Raise(mapEventData);
        }

        private void RaiseSetActiveTeamMapEvent(Team team)
        {
            MapEventData mapEventData = new MapEventData();
            mapEventData.team = team;

            this.setActiveTeam.Raise(mapEventData);
        }

        private void RaiseSetUpCharacterEvent(Character character, Team team, Tile tile)
        {
            CharacterEventData characterEventData = new CharacterEventData();
            characterEventData.character = character;
            characterEventData.team = team;
            characterEventData.tile = tile;

            this.setUpCharacter.Raise(characterEventData);
        }

        private void RaiseOccupyTileEvent(Character character, Tile tile)
        {
            TileEventData tileEventData = new TileEventData();
            tileEventData.tile = tile;
            tileEventData.character = character;

            this.occupyTile.Raise(tileEventData);
        }

        private void RaiseAddTeamAuraMapEvent(Team team)
        {
            MapEventData mapEventData = new MapEventData();
            mapEventData.team = team;

            this.addTeamAura.Raise(mapEventData);
        }

        private void RaiseSetAuraInfoTeamNameUIEvent(Team team)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.team = team;

            this.setAuraInfoTeamName.Raise(uiEventData);
        }

        private void RaiseHideTerraformUI()
        {
            UIEventData uiEventData = new UIEventData();

            this.hideTerraformUI.Raise(uiEventData);
        }
    }
}
