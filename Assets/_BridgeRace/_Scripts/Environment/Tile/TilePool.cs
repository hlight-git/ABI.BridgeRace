using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class TilePool <T> : MiniPool <T> where T : Component
{
    [SerializeField] private List<T> tiles;
    public int PoolCount => tiles.Count;
    public int InactiveCount => pools.Count;

    public void OnInit()
    {
        Utility.Shuffle(tiles);
        for (int i = 0; i < PoolCount; i++)
        {
            Despawn(tiles[i]);
        }
    }
}
