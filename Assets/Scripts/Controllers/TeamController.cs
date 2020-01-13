using Tactics.Events;
using Tactics.Models;
using UnityEngine;


namespace Tactics.Controllers
{
    public class TeamController : MonoBehaviour
    {
        public CharacterEvent completeCharacterAction;
        public Team team;

        /*-------------------------------------------------
        *                 Event Handlers
        --------------------------------------------------*/
        public void OnCompleteTeamMemberAction(TeamEventData teamEventData)
        {
            if (this.team == teamEventData.team)
            {
                CompleteTeamMemberAction(teamEventData.character);
            }
        }


        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        private void CompleteTeamMemberAction(Character character)
        {
            RaiseCompleteCharacterActionCharacterEvent(character);
        }


        /*-------------------------------------------------
        *              Trigger Helpers
        --------------------------------------------------*/
        private void RaiseCompleteCharacterActionCharacterEvent(Character character)
        {
            CharacterEventData characterEventData = new CharacterEventData();
            characterEventData.character = character;

            this.completeCharacterAction.Raise(characterEventData);
        }
    }
}
