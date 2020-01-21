using System.Collections;
using System.Collections.Generic;
using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class UIController : MonoBehaviour
    {
        public BattleEvent showBattleDangerZone;
        public BattleEvent hideBattleDangerZone;
        public BattleEvent showArrangeTiles;
        public BattleEvent hideArrangeTiles;
        public UI ui;

        /*-------------------------------------------------
        *                  Event Handlers
        --------------------------------------------------*/
        public void OnShowDangerZone(UIEventData uiEventData)
        {
            ShowBattleDangerZone();
        }

        public void OnHideDangerZone(UIEventData uiEventData)
        {
            HideBattleDangerZone();
        }

        public void OnShowArrangeTiles(UIEventData uiEventData)
        {
            ShowBattleArrangeTiles();
        }

        public void OnHideArrangeTiles(UIEventData uiEventData)
        {
            HideBattleArrangeTiles();
        }


        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void ShowBattleDangerZone()
        {
            RaiseShowDangerZoneBattleEvent();
        }

        private void HideBattleDangerZone()
        {
            RaiseHideDangerZoneBattleEvent();
        }

        private void ShowBattleArrangeTiles()
        {
            RaiseShowArrangeTilesBattleEvent();
        }

        private void HideBattleArrangeTiles()
        {
            RaiseHideArrangeTilesBattleEvent();
        }


        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
        private void RaiseShowDangerZoneBattleEvent()
        {
            BattleEventData battleEventData = new BattleEventData();

            this.showBattleDangerZone.Raise(battleEventData);
        }

        private void RaiseHideDangerZoneBattleEvent()
        {
            BattleEventData battleEventData = new BattleEventData();

            this.hideBattleDangerZone.Raise(battleEventData);
        }

        private void RaiseShowArrangeTilesBattleEvent()
        {
            BattleEventData battleEventData = new BattleEventData();

            this.showArrangeTiles.Raise(battleEventData);
        }

        private void RaiseHideArrangeTilesBattleEvent()
        {
            BattleEventData battleEventData = new BattleEventData();

            this.hideArrangeTiles.Raise(battleEventData);
        }
    }
}
