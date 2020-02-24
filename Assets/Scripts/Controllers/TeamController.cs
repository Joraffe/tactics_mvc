using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class TeamController : MonoBehaviour
    {
        public CharacterEvent completeCharacterAction;
        public BattleEvent completeTeamTurn;
        public Team team;

        /*-------------------------------------------------
        *             Event Handlers
        --------------------------------------------------*/
        public void OnCompleteTeamMemberAction(TeamEventData teamEventData)
        {
            if (this.team == teamEventData.team)
            {
                this.CompleteTeamMemberAction(teamEventData.character);
                if (this.team.HaveAllMembersActed())
                {
                    this.CompleteTeamTurn(this.team);
                }
            }
        }


        /*-------------------------------------------------
        *                Helpers
        --------------------------------------------------*/
        private void CompleteTeamMemberAction(Character character)
        {
            this.RaiseCompleteCharacterActionCharacterEvent(character);
        }

        private void CompleteTeamTurn(Team team)
        {
            this.RaiseCompleteTeamTurnBattleEvent(this.team);
        }


        /*-------------------------------------------------
        *              Event Triggers
        --------------------------------------------------*/
        private void RaiseCompleteCharacterActionCharacterEvent(Character character)
        {
            CharacterEventData characterEventData = new CharacterEventData();
            characterEventData.character = character;

            this.completeCharacterAction.Raise(characterEventData);
        }

        private void RaiseCompleteTeamTurnBattleEvent(Team team)
        {
            BattleEventData battleEventData = new BattleEventData();
            battleEventData.team = team;

            this.completeTeamTurn.Raise(battleEventData);
        }
    }
}
