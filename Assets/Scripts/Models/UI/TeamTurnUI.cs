using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Tactics.Models
{
    public class TeamTurnUI : MonoBehaviour
    {
        public GameObject teamTurnTextGameObject;
        public RectTransform teamTurnUIRectTransform;
        public string currentPosition = "left";

        public void SetTeamTurnText(Team team)
        {
            TextMeshProUGUI teamTurnTMP = teamTurnTextGameObject.GetComponent<TextMeshProUGUI>();
            teamTurnTMP.text = $"{team.teamType} Phase";
            teamTurnTMP.color = this.GetTeamTextColor(team);
        }

        public Color GetTeamTextColor(Team team)
        {
            if (team.teamType == TeamTypes.Player)
            {
                return Color.blue;
            }

            if (team.teamType == TeamTypes.Enemey)
            {
                return Color.red;
            }

            return Color.black;
        }

        public void MoveToCenter()
        {
            LeanTween.moveX(this.teamTurnUIRectTransform, 0, 0.5f);
        }

        public void MoveToLeft()
        {
            LeanTween.moveX(this.teamTurnUIRectTransform, -1500f, 0.5f);
        }

        public void MoveToRight()
        {
            LeanTween.moveX(this.teamTurnUIRectTransform, 1500f, 0.5f);
        }

        public void SetCurrentPosition(string position)
        {
            this.currentPosition = position;
        }

        public IEnumerator MoveLeftToRight()
        {
            this.MoveToCenter();
            yield return new WaitForSeconds(1f);
            this.MoveToRight();
        }

        public IEnumerator MoveRightToLeft()
        {
            this.MoveToCenter();
            yield return new WaitForSeconds(1f);
            this.MoveToLeft();
        }

    }
}
