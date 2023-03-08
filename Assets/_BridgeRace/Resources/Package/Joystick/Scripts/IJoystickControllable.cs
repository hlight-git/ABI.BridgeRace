using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJoystickControllable
{
    public void MoveToward(Vector3 direction, float moveSpeedCoefficient);
    public void StopMoving();
}
