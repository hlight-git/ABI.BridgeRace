using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Utility
{
    private static System.Random rng = new System.Random();
    public static bool VectorApproximately(Vector3 vectorA, Vector3 vectorB, float acceptedLimit = 0.01f)
    {
        return Vector3.Distance(vectorA, vectorB) < acceptedLimit;
    }
    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}
