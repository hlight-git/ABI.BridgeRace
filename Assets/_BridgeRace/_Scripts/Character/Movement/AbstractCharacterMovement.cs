using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCharacterMovement : MonoBehaviour
{
    protected Character character;
    private RayGuideCane detector;
    protected virtual void Awake() => OnInit();
    protected virtual void OnInit()
    {
        character = GetComponent<Character>();
        detector = GetComponent<RayGuideCane>();
    }
    public bool CanMoveToward(Vector3 direction, out Vector3 scaledDirection)
    {
        return detector.CanMoveToward(direction, out scaledDirection);
    }
    public abstract void StopRunning();
}
