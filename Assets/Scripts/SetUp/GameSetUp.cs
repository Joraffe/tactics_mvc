using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    public GameObject battlePrefab;
    void Start()
    {
        GameObject battle = Instantiate(battlePrefab, Vector3.zero, Quaternion.identity);
    }

}
