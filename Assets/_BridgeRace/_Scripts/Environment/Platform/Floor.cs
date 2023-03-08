using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Floor : MonoBehaviour
{
    private Dictionary<BaseColor, List<GroundBrick>> activeBricks;

    public TilePool<GroundBrick> GroundBrickPool;
    public List<Bridge> Bridges;

    void Awake() => OnInit();
    void OnInit()
    {
        activeBricks = new Dictionary<BaseColor, List<GroundBrick>>();
        GroundBrickPool.OnInit();
    }

    public void MassSpawn(BaseColor color)
    {
        activeBricks.Add(color, new List<GroundBrick>());
        int amount = Mathf.Min(
            GroundBrickPool.InactiveCount,
            Random.Range(GroundBrickPool.PoolCount / GameConstant.Character.AMOUNT - 2,
            GroundBrickPool.PoolCount / GameConstant.Character.AMOUNT + 2)
        );
        for (int i = 0; i < amount; i++)
        {
            SpawnBrick(color);
        }
    }

    public void SpawnBrick(BaseColor color)
    {
        GroundBrick brick = GroundBrickPool.Spawn();
        brick.ChangeColor(color);
        activeBricks[color].Add(brick);
    }
    public void DespawnBrick(GroundBrick brick)
    {
        activeBricks[brick.Color].Remove(brick);
        GroundBrickPool.Despawn(brick);
    }
    public void CollectBricks(BaseColor color)
    {
        while (activeBricks[color].Count > 0)
        {
            DespawnBrick(activeBricks[color][0]);
        }
    }
    public int Count(BaseColor color)
    {
        if (activeBricks.ContainsKey(color))
        {
            return activeBricks[color].Count;
        }
        return 0;
    }
    public GroundBrick GetAnActiveBrick(BaseColor color)
    {
        if (Count(color) == 0) return null;
        return activeBricks[color][0];
    }
}
