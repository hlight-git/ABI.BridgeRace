using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public class Vector
    {
        public static bool Approximately(Vector3 vectorA, Vector3 vectorB, float acceptedLimit = 0.01f)
        {
            return Vector3.Distance(vectorA, vectorB) < acceptedLimit;
        }
    }
}
