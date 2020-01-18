using System.Collections;
using System.Collections.Generic;
using Tactics.Models;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{   
    public GameObject mapPrefab;
    public GameObject columnPrefab;

    public GameObject tilePrefab;

    public GameObject characterPrefab;

    public GameObject teamPrefab;

    public GameObject battlePrefab;


    // UI Related
    public GameObject uiPrefab;
    public GameObject headerPrefab;
    public GameObject footerPrefab;

    // Character Sprites
    public Sprite joebineSprite;
    public Sprite caeSprite;
    public Sprite enemySprite;


    // Tile Sprites
    public Sprite normalTileSprite;
    public Sprite treeTileSprite;
    public Sprite rockTileSprite;

    private Map map;

    public void Start()
    {
        SetUpBattle();
        SetUpCamera();
    }

    private void SetUpBattle()
    {
        GameObject mapGameObject = SetUpMap();
        GameObject playerTeamGameObject = SetUpPlayerTeam();
        GameObject enemyTeamGameObject = SetUpEnemyTeam();
        // GameObject uiGameObject = SetUpUI();

        GameObject battleGameObject = Instantiate(battlePrefab, Vector3.zero, Quaternion.identity);
        battleGameObject.name = "Dev Battle";
        Battle battle = battleGameObject.GetComponent<Battle>();
        
        mapGameObject.transform.SetParent(battleGameObject.transform);
        battle.map = mapGameObject.GetComponent<Map>();

        playerTeamGameObject.transform.SetParent(battleGameObject.transform);
        battle.playerTeam = playerTeamGameObject.GetComponent<Team>();

        enemyTeamGameObject.transform.SetParent(battleGameObject.transform);
        battle.enemyTeam = enemyTeamGameObject.GetComponent<Team>();

        // uiGameObject.transform.SetParent(battleGameObject.transform);
    }

    private GameObject SetUpMap()
    {
        GameObject mapGameObject = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);
        mapGameObject.name = "Map";
        this.map = mapGameObject.GetComponent<Map>();
        
        for (int x = 0; x < Map.xMaxSize; x++)
        {
            GameObject columnGameObject = Instantiate(columnPrefab, Vector3.zero, Quaternion.identity);
            columnGameObject.transform.SetParent(mapGameObject.transform);
            columnGameObject.name = $"Column ({x})";

            for (int y = 0; y < Map.yMaxSize; y++)
            {
                GameObject tileGameObject;

                tileGameObject = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                Tile tile = tileGameObject.GetComponent<Tile>();
                tile.XPosition = x;
                tile.YPosition = y;
                tileGameObject.name = $"{tile.terrainType} ({x}, {y})";
                tileGameObject.transform.SetParent(columnGameObject.transform);
                this.map.tiles[x, y] = tile;

                GameObject pathOverlayGameObject = tileGameObject.transform.Find("PathOverlay").gameObject;
                tile.pathOverlay = pathOverlayGameObject.GetComponent<PathOverlay>();

                GameObject selectOverlayGameObject = tileGameObject.transform.Find("SelectOverlay").gameObject;
                tile.selectOverlay = selectOverlayGameObject.GetComponent<SelectOverlay>();

                GameObject dangerOverlayGameObject = tileGameObject.transform.Find("DangerOverlay").gameObject;
                tile.dangerOverlay = dangerOverlayGameObject.GetComponent<DangerOverlay>();

                if (x == 1 && y == 4) {
                    tile.terrainType = TileTerrainType.Rock;
                    tile.moveCost = 0;
                    tileGameObject.GetComponent<SpriteRenderer>().sprite = rockTileSprite;
                    tileGameObject.name = $"Rock {tile.GetCoordinates()}";
                }
                else if (x == 4 && y == 4)
                {
                    tile.terrainType = TileTerrainType.Tree;
                    tile.moveCost = 2;
                    tileGameObject.GetComponent<SpriteRenderer>().sprite = treeTileSprite;
                    tileGameObject.name = $"Tree {tile.GetCoordinates()}";
                }
                else
                {
                    tile.terrainType = TileTerrainType.Normal;
                    tile.moveCost = 1;
                    tileGameObject.GetComponent<SpriteRenderer>().sprite = normalTileSprite;
                    tileGameObject.name = $"Tile {tile.GetCoordinates()}";
                }
            }
        }

        return mapGameObject;
    }

    public void SetUpCamera()
    {
        // float orthoSize = this.map.spriteRenderer.bounds.size.x * Screen.height / Screen.width * 0.5f;
        float orthoSize = this.map.spriteRenderer.bounds.size.y * 0.5f;
        Camera.main.orthographicSize = orthoSize;
    }


    private GameObject SetUpUI()
    {
        GameObject uiGameObject = Instantiate(uiPrefab, Vector3.zero, Quaternion.identity);
        uiGameObject.name = "User Interface";
        GameObject headerGameObject = Instantiate(headerPrefab, new Vector3(2.5f, 8.5f, 0), Quaternion.identity);
        headerGameObject.name = "Header";
        GameObject footerGameObject = Instantiate(footerPrefab, new Vector3(2.5f, -1.0f, 0), Quaternion.identity);
        footerGameObject.name = "Footer";

        headerGameObject.transform.SetParent(uiGameObject.transform);
        footerGameObject.transform.SetParent(uiGameObject.transform);

        return uiGameObject;
    }
    private GameObject SetUpPlayerTeam()
    {
        GameObject playerTeamGameObject = Instantiate(teamPrefab, Vector3.zero, Quaternion.identity);
        playerTeamGameObject.name = "Players";
        Team playerTeam = playerTeamGameObject.GetComponent<Team>();
        playerTeam.faction = "Player";

        // Create Joebine
        int XPosition = 2;
        int YPosition = 1;
        GameObject joebineCharacterGameObject = Instantiate(characterPrefab, new Vector3(XPosition, YPosition, 0), Quaternion.identity);
        joebineCharacterGameObject.transform.SetParent(playerTeamGameObject.transform);

        Character joebineCharacter = joebineCharacterGameObject.GetComponent<Character>();
        Tile tile = this.map.tiles[XPosition, YPosition];
        this.map.playerStartTiles.Add(tile);
        tile.occupant = joebineCharacter;
        joebineCharacterGameObject.name = "Joebine";
        joebineCharacter.characterName = "Joebine";
        joebineCharacter.SetSprite(joebineSprite);
        joebineCharacter.currentTile = tile;
        joebineCharacter.originTile = tile;
        joebineCharacter.team = playerTeam;

        playerTeam.members.Add(joebineCharacter);

        // Create Cae
        XPosition = 3;
        YPosition = 1;
        GameObject caeCharacterGameObject = Instantiate(characterPrefab, new Vector3(XPosition, YPosition, 0), Quaternion.identity);
        caeCharacterGameObject.transform.SetParent(playerTeamGameObject.transform);

        Character caeCharacter = caeCharacterGameObject.GetComponent<Character>();
        tile = this.map.tiles[XPosition, YPosition];
        this.map.playerStartTiles.Add(tile);
        tile.occupant = caeCharacter;
        caeCharacterGameObject.name = "Cae";
        caeCharacter.characterName = "Cae";
        caeCharacter.SetSprite(caeSprite);
        caeCharacter.currentTile = tile;
        caeCharacter.originTile = tile;
        caeCharacter.team = playerTeam;

        playerTeam.members.Add(caeCharacter);

        return playerTeamGameObject;
    }

    private GameObject SetUpEnemyTeam()
    {
        GameObject enemyTeamGameObject = Instantiate(teamPrefab, Vector3.zero, Quaternion.identity);
        enemyTeamGameObject.name = "Enemies";
        Team enemyTeam = enemyTeamGameObject.GetComponent<Team>();
        enemyTeam.faction = "Enemy";

        // Enemy A
        int XPosition = 2;
        int YPosition = 4;
        GameObject enemyCharacterGameObjectA = Instantiate(characterPrefab, new Vector3(XPosition, YPosition, 0), Quaternion.identity);
        enemyCharacterGameObjectA.transform.SetParent(enemyTeamGameObject.transform);

        Character enemyCharacterA = enemyCharacterGameObjectA.GetComponent<Character>();
        Tile enemyCharacterATile = this.map.tiles[XPosition, YPosition];

        enemyCharacterATile.occupant = enemyCharacterA;
        enemyCharacterGameObjectA.name = "Enemy A";
        enemyCharacterA.name = "Enemy A";
        enemyCharacterA.SetSprite(enemySprite);
        enemyCharacterA.currentTile = enemyCharacterATile;
        enemyCharacterA.originTile = enemyCharacterATile;
        enemyCharacterA.movementRange = 2;
        enemyCharacterA.team = enemyTeam;

        enemyTeam.members.Add(enemyCharacterA);

        // Enemy B
        XPosition = 4;
        YPosition = 5;
        GameObject enemyCharacterGameObjectB = Instantiate(characterPrefab, new Vector3(XPosition, YPosition, 0), Quaternion.identity);
        enemyCharacterGameObjectB.transform.SetParent(enemyTeamGameObject.transform);

        Character enemyCharacterB = enemyCharacterGameObjectB.GetComponent<Character>();
        Tile enemyCharacterBTile = this.map.tiles[XPosition, YPosition];

        enemyCharacterBTile.occupant = enemyCharacterB;
        enemyCharacterGameObjectB.name = "Enemy B";
        enemyCharacterB.name = "Enemy B";
        enemyCharacterB.SetSprite(enemySprite);
        enemyCharacterB.currentTile = enemyCharacterBTile;
        enemyCharacterB.originTile = enemyCharacterBTile;
        enemyCharacterB.movementRange = 2;
        enemyCharacterB.team = enemyTeam;

        enemyTeam.members.Add(enemyCharacterB);

        return enemyTeamGameObject;
    }

}
