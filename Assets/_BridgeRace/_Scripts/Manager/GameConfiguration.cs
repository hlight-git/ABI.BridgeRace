using System;
using UnityEngine;
using UnityEditor;

public class GameConfiguration : AbstractMonoSingleton<GameConfiguration>
{
    public SpeedConfiguration SpeedConfig;
    public CameraConfiguration CameraConfig;

    [Serializable]
    public class SpeedConfiguration
    {
        public float characterMovement;
        public float collectedBrickSurf;
    }
    [Serializable]
    public class CameraConfiguration
    {
        public Vector3 offsetPosition;
        public Vector3 offsetRotation;
    }
}

