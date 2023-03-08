using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[CreateAssetMenu(fileName = "Levels", menuName = "Scriptable Object/Levels")]
public class LevelDataset : ScriptableObject
{
    public List<LevelData> levels;
}

[Serializable]
public class LevelData
{
    public GameObject LevelPrefab;
    public NavMeshData NavMeshData;
}