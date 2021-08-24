using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "ScriptableObjects/CreateEnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    [SerializeField]
    List<EnemySpwanPrefab> data;

    public List<EnemySpwanPrefab> Data => data;
}