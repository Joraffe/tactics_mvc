using System;
using System.Collections.Generic;
using Tactics.Views;
using UnityEngine;


namespace Tactics.Models
{

    public class TileInteractType
    {   
        public const string Movement = "movement";
        public const string Combat = "combat";
        public const string Arrangement = "arrangement";
        public const string Terraform = "terraform";
    }

    public class TileOverlayTypes
    {
        public const string Path = "path";
        public const string Select = "select";
        public const string Move = "move";
        public const string Arrange = "arrange";
        public const string Terraform = "terraform";
        public const string Danger = "danger";
        public const string All = "all";
    }

    public class Tile : MonoBehaviour
    {
        public string terrainType;

        public int moveCost = 1;

        public int XPosition;  // X position on the map
        public int YPosition;  // Y position on the map

        public Character occupant;  // What is currently occupying this tile
        public bool active;  // If the tile has some kind of "active" state; i.e.
                             // If the player can select it to move, or selct to target
        public string activeState;  // What type of active state is it (move/combat)

        public bool isShowingDanger = false;

        public HashSet<Tile> associatedMovementTiles = new HashSet<Tile>();  // if active state is a combat tile

        public SpriteRenderer spriteRenderer;
        public Transform tileTransform;


        /*-------------------------------------------------
        *                 Heirarchy
        --------------------------------------------------*/
        public GameObject tileTerraformGameObject;
        public GameObject tileTerraGameObject;
        public GameObject tileAuraMapGameObject;
        public GameObject tileMoveOverlayGameObject;
        public GameObject tilePathOverlayGameObject;
        public GameObject tileTerraformOverlayGameObject;
        public GameObject tileSelectOverlayGameObject;

        public void Awake()
        {
            TileTerraformView tileTerraformView = this.GetTileTerraformView();
            tileTerraformView.tile = this;
            tileTerraformView.terra = this.GetTerra();
            tileTerraformView.auraMap = this.GetAuraMap();
        }

        // Note to self for the future -- look into cacheing these for optimization
        public Terra GetTerra()
        {
            return this.tileTerraGameObject.GetComponent<Terra>();
        }

        public AuraMap GetAuraMap()
        {
            return this.tileAuraMapGameObject.GetComponent<AuraMap>();
        }

        public MoveOverlay GetMoveOverlay()
        {
            return this.tileMoveOverlayGameObject.GetComponent<MoveOverlay>();
        }

        public PathOverlay GetPathOverlay()
        {
            return this.tilePathOverlayGameObject.GetComponent<PathOverlay>();
        }

        public TerraformOverlay GetTerraformOverlay()
        {
            return this.tileTerraformOverlayGameObject.GetComponent<TerraformOverlay>();
        }

        public SelectOverlay GetSelectOverlay()
        {
            return this.tileSelectOverlayGameObject.GetComponent<SelectOverlay>();
        }

        public TileTerraformView GetTileTerraformView()
        {
            return this.tileTerraformGameObject.GetComponent<TileTerraformView>();
        }
        public string GetPreviewTerraformTerraType()
        {
            return this.GetTileTerraformView().GetPreviewTerraformTerraType();
        }

        public string GetPreviewTerraformTeamName()
        {
            return this.GetTileTerraformView().GetPreviewTerraformTeamName();
        }

        public int GetPreviewTerrformAuraAmount()
        {
            return this.GetTileTerraformView().GetPreviewTerraformAuraAmount();
        }


        /*-------------------------------------------------
        *                     Setters
        --------------------------------------------------*/
        public void ClearTile()
        {
            ClearOverlay(TileOverlayTypes.All);
            ClearAssociatedMovementTiles();
            ClearActiveState();
        }

        public void SetOccupant(Character character)
        {
            this.occupant = character;
        }

        public void ClearOccupant()
        {
            this.occupant = null;
        }

        public void ClearAssociatedMovementTiles()
        {
            this.associatedMovementTiles.Clear();
        }

        public void SetIsShowingDanger()
        {
            this.isShowingDanger = true;
        }

        public void SetIsNotShowingDanger()
        {
            this.isShowingDanger = false;
        }
        public void SetActiveState(string activeState)
        {
            this.active = true;
            this.activeState = activeState;
        }

        public void ClearActiveState()
        {
            this.active = false;
            this.activeState = "";
        }

        public void ShowOverlay(string overlayImageKey, string overlayType)
        {
            switch (overlayType)
            {
                case TileOverlayTypes.Path:
                    this.GetPathOverlay().SetSprite(overlayImageKey);
                    break;
                case TileOverlayTypes.Select:
                    this.GetSelectOverlay().SetSprite(overlayImageKey);
                    break;
                case TileOverlayTypes.Move:
                    this.GetMoveOverlay().SetSprite(overlayImageKey);
                    break;
                case TileOverlayTypes.Terraform:
                    this.GetTerraformOverlay().SetSprite(overlayImageKey);
                    break;

                default:
                    return;
            }

        }

        public void ClearOverlay(string overlayType)
        {
            switch (overlayType)
            {
                case TileOverlayTypes.Path:
                    this.GetPathOverlay().ClearSprite();
                    break;
                case TileOverlayTypes.Select:
                    this.GetSelectOverlay().ClearSprite();
                    break;
                case TileOverlayTypes.Move:
                    this.GetMoveOverlay().ClearSprite();
                    break;
                case TileOverlayTypes.Terraform:
                    this.GetTerraformOverlay().ClearSprite();
                    break;
                case TileOverlayTypes.All:
                    this.GetPathOverlay().ClearSprite();
                    this.GetSelectOverlay().ClearSprite();
                    this.GetMoveOverlay().ClearSprite();
                    this.GetTerraformOverlay().ClearSprite();
                    break;
                default:
                    return;
            }
        }

        public void SetSprite(Sprite sprite)
        {
            this.spriteRenderer.sprite = sprite;
        }

        public void SetTerraType(string terraType)
        {
            this.GetTerra().SetTerraType(terraType);
        }


        /*-------------------------------------------------
        *                     Getters
        --------------------------------------------------*/
        public string GetCoordinates()
        {
            return $"({this.XPosition}, {this.YPosition})";
        }

        public Vector3 GetTransformPosition()
        {
            return this.tileTransform.position;
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        public bool isOccupiedByOpposition(Character character) {
            return this.occupant != null && this.occupant.isOpposition(character);
        }

        public bool isOccupiedByAlly(Character character)
        {
            return (
                this.occupant != null
                && this.occupant.isAlly(character)
                && this.occupant.name != character.name
            );
        }

        public bool isMoveableTerrain(Character character)
        {
            // Note: we pass in character to account for character move type later

            // if (this.terra.type == TerraTypes.Oceanic)
            // {
            //     return false;
            // }

            // This is a "normal" tile
            return true;
        }

        public bool isCombatableTerrain(Character character)
        {
            // if (this.terra.type == TerraTypes.Oceanic)
            // {
            //     return false;
            // }

            return true;
        }

        public bool isWithinMoveDistance(Character character)
        {
            int xDiff = character.originTile.XPosition - this.XPosition;
            int yDiff = character.originTile.YPosition - this.YPosition;

            return (Math.Abs(xDiff) + Math.Abs(yDiff)) <= character.movementRange;
        }

        public bool isMoveableForCharacter(Character character)
        {
            return (
                this.isMoveableTerrain(character)
                && this.isWithinMoveDistance(character)
                && !this.isOccupiedByOpposition(character)

            );
        }

        public bool isCombatableForCharacter(Character character)
        {
            return (
                this.isCombatableTerrain(character)
                || this.isOccupiedByOpposition(character)
            );
        }

    }
}
