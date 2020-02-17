using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    public GameObject battlePrefab;
    public GameObject gameUIPrefab;
    void Start()
    {
        GameObject gameUI = Instantiate(gameUIPrefab, Vector3.zero, Quaternion.identity);
        GameObject battle = Instantiate(battlePrefab, Vector3.zero, Quaternion.identity);
    }

}
