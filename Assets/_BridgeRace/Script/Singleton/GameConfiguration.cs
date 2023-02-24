using System;
using UnityEngine;
using UnityEditor;

public class GameConfiguration : AbstractMonoSingleton<GameConfiguration>
{
    public InputConfig Input;
    public CharacterConfig Character;
    public CameraConfig MainCamera;


    [Serializable]
    public class InputConfig
    {
        public bool isUseKeyboard;
    }
    [Serializable]
    public class CharacterConfig
    {
        public float baseMoveSpeed;
    }
    [Serializable]
    public class CameraConfig
    {
        public bool isThirdPersonView;
        public Vector3 offsetPosition;
    }
}

