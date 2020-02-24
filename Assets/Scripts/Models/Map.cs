using System.Collections.Generic;
using Tactics.Views;
using UnityEngine;


namespace Tactics.Models
{
    public class Map : MonoBehaviour
    {
        public static int xMinSize = 0;
        public static int xMaxSize = 9;
        public static int yMinSize = 0;
        public static int yMaxSize = 6;
        public Tile[,] tiles = new Tile[xMaxSize,yMaxSize];
        public Character currentSelectedCharacter;
        public Forma currentSelectedForma;
        public bool isPreviewingTerraform;
        public string currentFormaDirection;

        public Dictionary<string, int> terraCountMap;
        public List<Tile> terraformingTiles = new List<Tile>();
        public List<Tile> playerStartTiles = new List<Tile>();
        public List<Tile> enemyStartTiles = new List<Tile>();
        public SpriteRenderer spriteRenderer;

        /*-------------------------------------------------
        *                 Heirarchy
        --------------------------------------------------*/
        public GameObject mapTeamGameObject;
        public GameObject mapTilesGameObject;

        public void Awake()
        {
            this.terraCountMap = new Dictionary<string, int>{
                { TerraTypes.Neutral, 0 },
                { TerraTypes.Swamp, 0 },
                // { TerraTypes.Desert, 0 },
                // { TerraTypes.Forest, 0 },
                // { TerraTypes.Volcanic, 0 },
                // { TerraTypes.Oceanic, 0 },
                // { TerraTypes.Industrial, 0 },
                { "total", 0 }
            };
            this.SetUpMapTiles();
            this.SetUpMapTeamView();
            this.SetUpCamera();
        }

        public MapTeamView GetMapTeamView()
        {
            return this.mapTeamGameObject.GetComponent<MapTeamView>();
        }

        public Team GetActiveTeam()
        {
            return this.GetMapTeamView().GetActiveTeam();
        }

        private void SetUpMapTiles()
        {
            foreach (Transform columnTransform in this.mapTilesGameObject.transform)
            {
                foreach (Transform tileTransform in columnTransform)
                {
                    GameObject tileGameObject = tileTransform.gameObject;
                    Tile mapTile = tileGameObject.GetComponent<Tile>();
                    this.tiles[mapTile.XPosition, mapTile.YPosition] = mapTile;
                    this.AddTileTerraCount(mapTile.XPosition, mapTile.YPosition);
                }
            } 
        }

        private void SetUpMapTeamView()
        {
            MapTeamView mapTeamView = this.GetMapTeamView();
            mapTeamView.SetMap(this);
        }

        private void SetUpCamera()
        {
            // float orthoSize = this.map.spriteRenderer.bounds.size.x * Screen.height / Screen.width * 0.5f;
            float orthoSize = this.spriteRenderer.bounds.size.y * 0.75f;
            Camera.main.orthographicSize = orthoSize;
        }

        /*-------------------------------------------------
        *                     Setters
        --------------------------------------------------*/
        public void SetCurrentSelectedCharacter(Character character)
        {
            this.currentSelectedCharacter = character;
        }

        public void ClearCurrentSelectedCharacter()
        {
            this.currentSelectedCharacter = null;
        }

        public void AddTerraformingTile(Tile tile)
        {
            this.terraformingTiles.Add(tile);
        }

        public void ClearTerraformingTiles()
        {
            this.terraformingTiles.Clear();
        }

        public void SetCurrentSelectedForma(Forma forma)
        {
            this.currentSelectedForma = forma;
        }

        public void ClearCurrentSelectedForma()
        {
            this.currentSelectedForma = null;
        }

        public void SetIsPreviewingTerraform()
        {
            this.isPreviewingTerraform = true;
        }

        public void ResetIsPreviewingTerraform()
        {
            this.isPreviewingTerraform = false;
        }

        public void AddTileTerraCount(int xPosition, int yPosition)
        {
            Tile tile = this.tiles[xPosition, yPosition];
            string terraType = tile.GetTerra().type;
            this.terraCountMap[terraType] += 1;
            this.terraCountMap["total"] += 1;
        }

        public void UpdateTerraCountMap(List<Tile> terraformingTiles)
        {
            this.terraCountMap = this.GetPostTerraformTerraCountMap(
                terraformingTiles,
                this.terraCountMap
            );
        }
        public void UpdateTeamAuraScore(List<Tile> terraformingTiles)
        {
            Dictionary<Tile, Dictionary<string, int>> auraCountMap = this.GetCurrentAuraCountMap(terraformingTiles);
            Dictionary<Tile, Dictionary<string, int>> postTerraformTerraCountMap = this.GetPostTerraformAuraCountMap(terraformingTiles);
            Dictionary<string, int> teamAuraScoreMap = this.GetMapTeamView().GetTeamScoreMap();
            Dictionary<string, int> auraDeltaMap = this.GetAuraDeltaMap(teamAuraScoreMap, auraCountMap, postTerraformTerraCountMap);

            foreach (KeyValuePair<string, int> teamAuraDelta in auraDeltaMap)
            {
                string teamName = teamAuraDelta.Key;
                int score = teamAuraDelta.Value;
                this.GetMapTeamView().AddTeamScore(teamName, score);
            }
        }

        /*-------------------------------------------------
        *                     Getters
        --------------------------------------------------*/
        public Tile GetTile(int XPosition, int YPosition)
        {
            return this.tiles[XPosition, YPosition];
        }

        public Character GetCurrentSelectedCharacter()
        {
            return this.currentSelectedCharacter;
        }

        public List<Tile> GetAdjacentTilesForTile(Tile tile, int adjacencyRange)
        {
            List<Tile> adjacentTiles = new List<Tile>();

            // Left Tile: [(x - 1), y]
            if (isLeftAdjacentTileInMapForTile(tile, adjacencyRange)) {
                adjacentTiles.Add(GetLeftAdjacentTileForTile(tile, adjacencyRange));
            }

            // Right Tile [(x + 1), y]
            if (isRightAdjacentTileInMapForTile(tile, adjacencyRange)) {
                adjacentTiles.Add(GetRightAdjacentTileForTile(tile, adjacencyRange));
            }

            // Top Tile  [x, (y + 1)]
            if (isUpAdjacentTileInMapForTile(tile, adjacencyRange)) {
                adjacentTiles.Add(GetUpAdjacentTileForTile(tile, adjacencyRange));
            }

            // Bottom Tile [x, (y - 1)]
            if (isDownAdjacentTileInMapForTile(tile, adjacencyRange)) {
                adjacentTiles.Add(GetDownAdjacentTileForTile(tile, adjacencyRange));
            }

            return adjacentTiles;
        }

        public Tile GetLeftAdjacentTileForTile(Tile tile, int adjacencyRange)
        {
            return this.tiles[tile.XPosition - adjacencyRange, tile.YPosition];
        }

        public Tile GetRightAdjacentTileForTile(Tile tile, int adjacencyRange)
        {
            return this.tiles[tile.XPosition + adjacencyRange, tile.YPosition];
        }

        public Tile GetUpAdjacentTileForTile(Tile tile, int adjacencyRange)
        {
            return this.tiles[tile.XPosition, tile.YPosition + adjacencyRange];
        }

        public Tile GetDownAdjacentTileForTile(Tile tile, int adjacencyRange)
        {
            return this.tiles[tile.XPosition, tile.YPosition - adjacencyRange];
        }

        public List<Tile> GetPlayerStartTiles()
        {
            return this.playerStartTiles;
        }

        public string GetDangerOverlayImageForTile(Tile tile)
        {
            string right = "0";
            string left = "0";
            string up = "0";
            string down = "0";

            if (isRightAdjacentTileInMapForTile(tile, 1))
            {
                right = GetRightAdjacentTileForTile(tile, 1).isShowingDanger ? "1" : "0";
            }

            if (isLeftAdjacentTileInMapForTile(tile, 1))
            {
                left = GetLeftAdjacentTileForTile(tile, 1).isShowingDanger ? "1" : "0";
            }

            if (isUpAdjacentTileInMapForTile(tile, 1))
            {
                up = GetUpAdjacentTileForTile(tile, 1).isShowingDanger ? "1" : "0";
            }

            if (isDownAdjacentTileInMapForTile(tile, 1))
            {
                down = GetDownAdjacentTileForTile(tile, 1).isShowingDanger ? "1" : "0";
            }

            return $"{right}{left}{up}{down}";
        }

        /*-------------------------------------------------
        *                     Helpers
        --------------------------------------------------*/
        public List<Tile> FilterMovementAdjacentTilesForCharacter(List<Tile> adjacentTiles, Character character)
        {
            List<Tile> movementAdjacentTiles = new List<Tile>();

            foreach (Tile adjacentTile in adjacentTiles)
            {
                if (!adjacentTile.isOccupiedByOpposition(character) && adjacentTile.isMoveableTerrain(character))
                {
                    movementAdjacentTiles.Add(adjacentTile);
                }
            }

            return movementAdjacentTiles;
        }

        public List<Tile> FilterAlliesAdjacentTilesForCharacter(List<Tile> adjacentTiles, Character character)
        {
            List<Tile> alliesAdjacentTiles = new List<Tile>();

            foreach (Tile adjacentTile in adjacentTiles)
            {
                if (!adjacentTile.isOccupiedByAlly(character))
                {
                    alliesAdjacentTiles.Add(adjacentTile);
                }
            }

            return alliesAdjacentTiles;
        }

        public bool isLeftAdjacentTileInMapForTile(Tile tile, int adjacencyRange)
        {
            return tile.XPosition - adjacencyRange >= xMinSize;
        }

        public bool isRightAdjacentTileInMapForTile(Tile tile, int adjacencyRange)
        {
            return tile.XPosition + adjacencyRange < xMaxSize;
        }

        public bool isUpAdjacentTileInMapForTile(Tile tile, int adjacencyRange)
        {
            return tile.YPosition + adjacencyRange < yMaxSize;
        }

        public bool isDownAdjacentTileInMapForTile(Tile tile, int adjacencyRange)
        {
            return tile.YPosition - adjacencyRange >= yMinSize;
        }

        public bool isCoordinatesWithinMap(int XPosition, int YPosition)
        {
            bool withinXBounds = (xMinSize <= XPosition) && (XPosition <= xMaxSize);
            bool withinYBounds = (yMinSize <= YPosition) && (YPosition <= yMaxSize);

            return withinXBounds && withinYBounds;
        }

        public Dictionary<string, int> GetPostTerraformTerraCountMap(List<Tile> terraformingTiles, Dictionary<string, int> terraCountMap)
        {

            Dictionary<string, int> postTerraformTerraCountMap = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> terraCount in terraCountMap)
            {
                if (terraCount.Key != "total")
                {
                    postTerraformTerraCountMap.Add(terraCount.Key, terraCount.Value);
                }
            }

            foreach (Tile terraformingTile in terraformingTiles)
            {
                string currentTerraType = terraformingTile.GetTerra().type;
                string nextTerraType = terraformingTile.GetPreviewTerraformTerraType();

                postTerraformTerraCountMap[currentTerraType] -= 1;
                postTerraformTerraCountMap[nextTerraType] += 1;
            }

            return postTerraformTerraCountMap;
        }

        public Dictionary<Tile, Dictionary<string, int>> GetCurrentAuraCountMap(List<Tile> terraformingTiles)
        {
            Dictionary<Tile, Dictionary<string, int>> auraCountMap = new Dictionary<Tile, Dictionary<string, int>>();
            foreach (Tile terraformingTile in terraformingTiles)
            {
                TileTerraformView tileTerraformView = terraformingTile.GetTileTerraformView();
                AuraMap tileAuraMap = tileTerraformView.auraMap;
                auraCountMap.Add(terraformingTile, tileAuraMap.GetCurrentAuraCount());
            }

            return auraCountMap;
        }

        public Dictionary<Tile, Dictionary<string, int>> GetPostTerraformAuraCountMap(List<Tile> terraformingTiles)
        {
            Dictionary<Tile, Dictionary<string, int>> auraCountMap = new Dictionary<Tile, Dictionary<string, int>>();
            foreach (Tile terraformingTile in terraformingTiles)
            {
                TileTerraformView tileTerraformView = terraformingTile.GetTileTerraformView();
                AuraMap tileAuraMap = tileTerraformView.auraMap;
                string previewTeam = tileTerraformView.GetPreviewTerraformTeamName();
                int previewAura = tileTerraformView.GetPreviewTerraformAuraAmount();

                auraCountMap.Add(terraformingTile, tileAuraMap.GetPreviewAuraCount(previewTeam, previewAura));
            }

            return auraCountMap;
        }

        public Dictionary<string, int> GetAuraDeltaMap(Dictionary<string, int> teamAuraScoreMap, Dictionary<Tile, Dictionary<string, int>> auraCountMap, Dictionary<Tile, Dictionary<string, int>> postTerraformAuraCountMap)
        {
            Dictionary<string, int> auraDeltaMap = new Dictionary<string, int>();

            foreach (KeyValuePair<string, int> teamScore in teamAuraScoreMap)
            {
                auraDeltaMap.Add(teamScore.Key, 0);
            }

            foreach (KeyValuePair<Tile, Dictionary<string, int>> auraCount in auraCountMap)
            {
                Tile tile = auraCount.Key;
                Dictionary<string, int> currentAuraValues = auraCount.Value;
                Dictionary<string, int> postAuraValues = postTerraformAuraCountMap[tile];

                foreach (KeyValuePair<string, int> teamAuraCount in currentAuraValues)
                {
                    string teamName = teamAuraCount.Key;
                    int currentTeamAuraValueForTile = teamAuraCount.Value;
                    int postTeamAuraValueForTile = postAuraValues[teamName];
                    int teamAuraDeltaForTile = postTeamAuraValueForTile - currentTeamAuraValueForTile;

                    auraDeltaMap[teamName] += teamAuraDeltaForTile;
                }
            }

            return auraDeltaMap;
        }
    }
}
