using System.Collections;
using System.Collections.Generic;
using Tactics.Algorithms;
using UnityEngine;


namespace Tactics.Models
{
    public class Character : MonoBehaviour
    {
        public string characterName;
        public int movementRange = 2;
        public int attackRange = 1;

        public int startXPosition;
        public int startYPosition;


        // Movement related
        public bool isMoving;
        public bool isUndoing;
        public bool isPreviewing;
        public Path tilePath;
        public Tile pathTargetTile;
        public Tile targetTile;

        public Tile currentTile;
        public Tile previousTile;

        public Tile originTile;
        public Tile selectedCombatTile;

        // Team related
        public Team team;
        public bool acted;

        public Sprite characterSprite;
        public SpriteRenderer spriteRenderer;
        public Transform characterTransform;
        public Material greyscaleMaterial;
        public Material defaultMaterial;

        /*-------------------------------------------------
        *                 Heirarchy
        --------------------------------------------------*/
        public GameObject formaSetGameObject;

        public FormaSet GetCharacterFormaSet()
        {
            return this.formaSetGameObject.GetComponent<FormaSet>();
        }

        public void SetUpCharacterFormaSet(Team team)
        {
            FormaSet formaSet = this.GetCharacterFormaSet();
            foreach (Transform formaTransform in this.formaSetGameObject.transform)
            {
                Forma forma = formaTransform.gameObject.GetComponent<Forma>();
                forma.teamName = team.teamName;
                if (forma.name == "BasicSwamp")
                {
                    forma.active = true;
                }
                formaSet.formas.Add(forma);
            }
        }

        /*-------------------------------------------------
        *                     Initalization
        --------------------------------------------------*/
        public void SetUp(Team team, Tile tile)
        {
            this.team = team;
            this.originTile = tile;
            this.currentTile = tile;
            this.SetUpCharacterFormaSet(team);
        }

        /*-------------------------------------------------
        *                     Setters
        --------------------------------------------------*/

        public void MoveTowards(Vector3 destination)
        {
            this.characterTransform.position = Vector3.MoveTowards(
                this.characterTransform.position,
                destination,
                Time.deltaTime * 10f
            );
        }

        public void EnqueueNextPathTargetTile()
        {
            this.pathTargetTile = this.tilePath.RemoveFromTail();
        }

        public void FinishMovementToTile()
        {
            if (this.isUndoing)
            {
                this.currentTile = this.originTile;
                this.previousTile = null;
            } else
            {
                this.previousTile = this.currentTile;
                this.currentTile = this.targetTile;
            }

            this.tilePath = null;
            this.pathTargetTile = null;
            this.targetTile = null;
            this.isMoving = false;
            this.isUndoing = false;
        }

        public void SetPreviewCharacterMovementState(Path tilePath, Tile targetTile)
        {
            this.tilePath = tilePath;
            this.pathTargetTile = this.isPreviewing ? tilePath.RemoveFromHead() : tilePath.RemoveFromTail();
            this.isPreviewing = true;
            this.isMoving = true;
            this.targetTile = targetTile;
        }

        public void SetUndoCharacterMovementState()
        {
            this.isMoving = true;
            this.isUndoing = true;
            this.isPreviewing = false;
            this.pathTargetTile = this.originTile;
            this.targetTile = this.originTile;
        }

        public void SetSelectedCombatTile(Tile targetTile)
        {
            this.selectedCombatTile = targetTile;
        }

        public void ResetSelectedCombatTile()
        {
            this.selectedCombatTile = null;
        }

        public void SetActed()
        {
            this.acted = true;
        }

        public void ResetActed()
        {
            this.acted = false;
        }

        public void SetOriginTileToCurrentTile()
        {
            this.originTile = this.currentTile;
        }

        public void ResetPreviousTile()
        {
            this.previousTile = null;
        }

        public void ResetPreviewState()
        {
            this.isPreviewing = false;
        }

        public void SetSpriteMaterialGreyscale()
        {
            this.spriteRenderer.material = this.greyscaleMaterial;
        }

        public void SetSpriteMaterialDefault()
        {
            this.spriteRenderer.material = this.defaultMaterial;
        }

        public void SetSprite(Sprite sprite)
        {
            this.spriteRenderer.sprite = sprite;
            this.characterSprite = sprite;
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/

        public bool isOpposition(Character otherCharacter)
        {
            return this.team.teamName != otherCharacter.team.teamName;
        }

        public bool isAlly(Character otherCharacter)
        {
            return this.team.teamName == otherCharacter.team.teamName;
        }

        public bool HasReachedTargetPosition(Vector3 position)
        {
            return this.characterTransform.position == position;
        }

        public bool HasActed()
        {
            return this.acted;
        }

    }

}
