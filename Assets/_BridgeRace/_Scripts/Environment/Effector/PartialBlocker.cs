using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartialBlocker : MonoBehaviour
{
    [SerializeField] private float allowAngle = 90;
    private BoxCollider boxCollider;

    void Awake() => boxCollider = GetComponent<BoxCollider>();
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(GameConstant.Tag.CHARACTER) && Vector3.Angle(collision.transform.forward, transform.forward) <= allowAngle)
        {
            boxCollider.isTrigger = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        boxCollider.isTrigger = false;
    }
}
