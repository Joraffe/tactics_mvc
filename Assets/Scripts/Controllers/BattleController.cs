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
        public Battle battle;


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
        *              Trigger Helpers
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
            mapEventData.team = this.battle.GetEnemyTeam();

            this.showPlayerDangerZone.Raise(mapEventData);
        }

        private void RaiseHideDangerZoneMapEvent()
        {
            MapEventData mapEventData = new MapEventData();

            this.hidePlayerDangerZone.Raise(mapEventData);
        }
    }
}
