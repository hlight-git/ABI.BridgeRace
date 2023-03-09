using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstant
{
    public class Tag
    {
        public const string CHARACTER = "Player";
        public const string SURFACE = "Surface";
        public const string GROUND_BRICK = "GroundBrick";
        public const string COLLECTED_BRICK = "CollectedBrick";
        public const string UNBRICK = "UnBrick";
    }
    public class Layer
    {
        public const string SURFACE = "Surface";
        public const string UNBRICK = "UnBrick";
    }
    public class Character
    {
        public const int AMOUNT = 4;
        public const float HEIGHT = 2f;
        public const float THICHNESS = 0.35f;
        public const float BLOCKER_THICKNESS = 0.2f;
    }
    public class Brick
    {
        public const float THICHNESS = 0.25f;
        public const float UNBRICK_WIDTH = 1f;
    }
}
