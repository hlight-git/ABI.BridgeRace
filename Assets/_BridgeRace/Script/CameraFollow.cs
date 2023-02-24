using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    void LateUpdate()
    {
        if (GameConfiguration.Instance.MainCamera.isThirdPersonView)
        {
            ThirdPersonView();
        } else
        {
            DefaultView();
        }
    }

    void ThirdPersonView()
    {
        DefaultView();

        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }

    void DefaultView()
    {
        Vector3 newPos = target.position + GameConfiguration.Instance.MainCamera.offsetPosition;
        transform.position = newPos;
    }
}
