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
        public UIEvent updateTeamTurnUI;
        public UIEvent showTeamTurnUI;

        public CharacterEvent setUpCharacter;

        public CharacterEvent resetCharacter;
        public TileEvent occupyTile;
        public Battle battle;

        /*-------------------------------------------------
        *                 Heirarchy
        --------------------------------------------------*/
        public GameObject battleTeamsGameObject;

        public Teams GetTeams()
        {
            return this.battleTeamsGameObject.GetComponent<Teams>();
        }

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
            this.EnqueueNextTeam(enemies);

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
                this.RaiseSetUpCharacterEvent(member, team, startTile);
                this.RaiseOccupyTileEvent(member, startTile);
            }
        }

        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        public void OnShowBattleDangerZone(BattleEventData battleEventData)
        {
            this.ShowMapDangerZoneForPlayer();
        }

        public void OnHideBattleDangerZone(BattleEventData battleEventData)
        {
            this.HideMapDangerZoneForPlayer();
        }

        public void OnShowBattleArrangeTiles(BattleEventData battleEventData)
        {
            this.ShowMapArrangeTiles();
        }

        public void OnHideBattleArrangeTiles(BattleEventData battleEventData)
        {
            this.HideMapArrangeTiles();
        }

        public void OnCompleteTeamTurn(BattleEventData battleEventData)
        {
            Team nextTeam = this.GetNextTeam();
            this.ShowTeamTurnUI(nextTeam);
            this.RaiseSetActiveTeamMapEvent(nextTeam);
            this.ResetTeamMembers(battleEventData.team);
            this.EnqueueNextTeam(battleEventData.team);
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/

        private void ShowMapArrangeTiles()
        {
            this.RaiseShowPlayerArrangeTilesMapEvent();
        }

        private void HideMapArrangeTiles()
        {
            this.RaiseHidePlayerArrangeTilesMapEvent();
        }

        private void ShowMapDangerZoneForPlayer()
        {
            this.RaiseShowDangerZoneMapEvent();
        }

        private void HideMapDangerZoneForPlayer()
        {
            this.RaiseHideDangerZoneMapEvent();
        }

        private void ShowTeamTurnUI(Team team)
        {
            this.RaiseUpdateTeamTurnUIEvent(team);
            this.RaiseShowTeamTurnUIEvent(team);
        }

        private Team GetNextTeam()
        {
            return this.GetTeams().DequeueNextTeamTurn();
        }

        private void EnqueueNextTeam(Team team)
        {
            this.GetTeams().EnqueueNextTeamTurn(team);
        }

        private void ResetTeamMembers(Team team)
        {
            foreach (Character member in team.GetMembers())
            {
                this.RaiseResetTeamMemberCharacterEvent(member);
            }
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

        private void RaiseResetTeamMemberCharacterEvent(Character character)
        {
            CharacterEventData characterEventData = new CharacterEventData();
            characterEventData.character = character;

            this.resetCharacter.Raise(characterEventData);
        }

        private void RaiseUpdateTeamTurnUIEvent(Team team)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.team = team;

            this.updateTeamTurnUI.Raise(uiEventData);
        }

        private void RaiseShowTeamTurnUIEvent(Team team)
        {
            UIEventData uiEventData = new UIEventData();
            uiEventData.team = team;

            this.showTeamTurnUI.Raise(uiEventData);
        }
    }
}
