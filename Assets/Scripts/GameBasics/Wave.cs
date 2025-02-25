using System;
using UnityEngine;

[Serializable]
public struct Wave
{
    [SerializeField] public int EnemyAmount;
    [SerializeField] public float WaveTime;
    [SerializeField] public float SpawnTime;
}
