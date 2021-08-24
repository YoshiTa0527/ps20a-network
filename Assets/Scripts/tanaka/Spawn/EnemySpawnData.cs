using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "ScriptableObjects/CreateEnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    [SerializeField]
    List<SpawnData> data;

    public List<SpawnData> Data => data;
}

public enum ESpawnDataType
{
    Wait,
    Spawn
}

[Serializable]
public class SpawnData
{
    [SerializeField]
    ESpawnDataType type;
    [SerializeField]
    float wait;
    [SerializeField]
    float spawnInterval;
    [SerializeField, Range(0.0f, 0.1f)]
    float pos;
    [SerializeField]
    int spawnAmount;
    [SerializeField]
    string obj;

    public ESpawnDataType Type => type;
    public float Wait => wait;
    public float SpawnInterval => spawnInterval;
    public float Pos => pos;
    public int SpawnAmount => spawnAmount;
    public string Obj => obj;
}