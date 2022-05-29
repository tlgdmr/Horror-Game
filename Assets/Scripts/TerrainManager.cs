using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TerrainManager : MonoBehaviour
{

    Terrain _Terrain;
    [SerializeField] float Distance = 999999999f;

    void Start()
    {
        _Terrain = GetComponent<Terrain>();
        _Terrain.detailObjectDistance = Distance;
    }
}
